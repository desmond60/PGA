namespace PGA;

//* Генетека
public class Genetica
{
    //* Данные для задачи
    protected Vector<double> Coefficients { get; set; }   /// Коэффициенты полинома
    protected Vector<double> Points       { get; set; }   /// Заданный набор точек (фенотип)
    protected double MinFunctional        { get; set; }   /// Значение фукнционала для выхода
    protected double MutationProbability  { get; set; }   /// Вероятность мутации
    protected uint CountGen               { get; set; }   /// Число генов
    protected uint CountPopulation        { get; set; }   /// Число особей в популяции
    protected uint CountGeneration        { get; set; }   /// Число генераций
    protected uint MaxParent              { get; set; }   /// Максимальное число родителей

    private Being TrueBeing;          /// Особь с правильным генотипом
    private List<Being> Population;   /// Поколение

    // ***** Конструктор ***** //
    public Genetica(Data data) {
        (Coefficients, Points, MinFunctional, MutationProbability, CountGen, CountPopulation, CountGeneration, MaxParent) = data;

        // Истинное особь и его функционал (c зашумлением)
        double noise = 1 + GetRandomDouble(-Noise, Noise);
        Vector<double> trueGens = Coefficients * noise;
        TrueBeing = new Being(trueGens);
        TrueBeing.SetPhenotip(Points);
        TrueBeing.Functional = Functional(TrueBeing, TrueBeing);
        
        // Генерация начального поколения
        Population = new List<Being>();
        for (int i = 0; i < CountPopulation; i++) {
            Vector<double> Gens = new Vector<double>((int)CountGen);
            for (int j = 0; j < CountGen; j++)
                Gens[j] = (GetRandomDouble(MinValue, MaxValue));

            var being = new Being(Gens);
            being.SetPhenotip(Points);
            being.Functional = Functional(being, TrueBeing);
            Population.Add(being);
        }
        Population = Population.OrderBy(item => item.Functional).ToList();
        double mid = 0;
        foreach (var item in Population)
            mid += item.Functional;
        WriteLine($"Средняя приспособленность (начального поколения): {mid / Population.Count}");
    }

    //* Реализация простого генетического алгоритма
    public void pga() {
        int Iter = 0;                                 // Текущее количество итераций
        double Functional = Population[0].Functional; // Текущий лучший функционал

        do {
            // Создаем новое поколение
            List<Being> newPopulation = NewPopulation();

            // Производим селекцию
            Selection(newPopulation, Population[0].Functional);
            
         } while (++Iter < CountGeneration &&
                  Population[0].Functional > MinFunctional);

        double mid = 0;
        foreach (var item in Population)
            mid += item.Functional;
        WriteLine($"Средняя приспособленность (конечного поколения): {mid / Population.Count}");
        WriteLine(Population[0].ToString());
    }

    //* Генерация нового поколения
    public List<Being> NewPopulation() {
        List<Being> NewPopul = new List<Being>(Population);
        // Для каждого мамы-родителя
        for (int i = 0; i < MaxParent; i++)
            // Генерируем по (CountPopulation / MaxParent) детей
            for (int j = 0; j < CountPopulation / MaxParent; j++) {
                Being father = GetBeing();                      // Выбираем рандомного отца
                Being child  = GetChild(Population[i], father); // Скрещиваем отца и мать (Кроссинговер)
                Mutation(child);                                // Вероятность проявление мутации у созданного ребенка
                child.SetPhenotip(Points);                      // Изменение фенотипа
                child.Functional = Functional(child, TrueBeing);
                NewPopul.Add(child);
            }
        return NewPopul;
    }

    //* Выбор рандомного существа из поколения
    public Being GetBeing() {
        int index = (new Random()).Next((int)MaxParent, (int)CountPopulation);
        return Population[index];
    }

    //* Кроссинговер (скрещивание отца и матери)
    public Being GetChild(Being mother, Being father) {
        Being child = new Being((int)CountGen, Points.Length);
        int index1 = (new Random()).Next(0,      (int)CountGen); // Первая точка кроссинговера
        int index2 = (new Random()).Next(index1, (int)CountGen); // Вторая точка Кроссинговера

        // Первая часть генотипа от отца
        for (int i = 0; i < index1; i++)
            child.Genotype[i] = father.Genotype[i];
        
        // Вторая часть генотипа от матери
        for (int i = index1; i < index2; i++)
            child.Genotype[i] = mother.Genotype[i];

        // Третья часть генотипа от отца
        for (int i = index2; i < CountGen; i++)
            child.Genotype[i] = father.Genotype[i];      

        return child;
    }

    //* Проявление мутации у существа
    public void Mutation(Being being) {
        double probability = GetRandomDouble(0, 1);  // Вероятность мутации
        
        // Если меньше заданной мутации (мутируем существо)
        if (probability < MutationProbability) {
            int index = (new Random()).Next(0, (int)CountGen); // Рандомное место для мутации
            being.Genotype[index] = GetRandomDouble(MinValue, MaxValue);
        }
    }

    //* Селекция (отбор лучших существ)
    public void Selection(List<Being> population, double bestFunctional) {
        // Сортируем новое поколение
        Population = population.OrderBy(item => item.Functional).ToList();

        // Оставляем лучших особей
        Population.RemoveRange((int)CountPopulation, (int)CountPopulation);
    }
    
}