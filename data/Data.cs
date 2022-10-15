namespace PGA;
public class Data
{
    //* Данные для задачи
    public double[] Coefficients      { get; set; }   /// Коэффициенты полинома (генотип)
    public double[] Points            { get; set; }   /// Заданный набор точек (фенотип)
    public double MinFunctional       { get; set; }   /// Значение фукнционала для выхода
    public double MutationProbability { get; set; }   /// Вероятность мутации
    public uint CountGen              { get; set; }   /// Число генов
    public uint CountPopulation       { get; set; }   /// Число особей в популяции
    public uint CountGeneration       { get; set; }   /// Число генераций
    public uint MaxParent             { get; set; }   /// Максимальное число родителей

    //* Деконструктор
    public void Deconstruct(out Vector<double> coefs,
                            out Vector<double> points,
                            out double minFunctional, 
                            out double mutationProbability, 
                            out uint countGen, 
                            out uint countPopulation, 
                            out uint countGeneration, 
                            out uint maxParent) 
    {
        coefs               = new Vector<double>(Coefficients);
        points              = new Vector<double>(Points);
        minFunctional       = this.MinFunctional;
        mutationProbability = this.MutationProbability;
        countGen            = this.CountGen;
        countPopulation     = this.CountPopulation;
        countGeneration     = this.CountGeneration;
        maxParent           = this.MaxParent;
    }

    //* Проверка входных данных
    public bool Incorrect(out string mes) {
        StringBuilder errorStr = new StringBuilder("");
        
        if (CountGen != Coefficients.Count())
            errorStr.Append($"Incorrect data (CountGen = Coefficients.Count()): {CountGen} = {Coefficients.Count()}\n");

        if (MutationProbability < 0.0 && MutationProbability > 1.0)
            errorStr.Append($"Incorrect data (MutationProbability out of range): 0 < {MutationProbability} < 1\n");

        if (!errorStr.ToString().Equals("")) {
            mes = errorStr.ToString();
            return false;
        }

        mes = errorStr.ToString();
        return true;
    }

}