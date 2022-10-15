namespace PGA.other;
public static class Helper
{
    public static readonly double MinValue =  0;    /// Минимальное значение для генерации
    public static readonly double MaxValue =  10;   /// Максимальное значение для генерации
    public static readonly double Noise    = 0.0;   /// Значение шума


    //* Расчет функционала
    public static double Functional(Being CurBeing, Being TrueBeing) {
        double f = 0;
        for (int i = 0; i < CurBeing.Phenotype.Length; i++)
            f += Abs(TrueBeing.Phenotype[i] - CurBeing.Phenotype[i]);
        return f;
    }

    //* Генерация рандомного числа в диапозоне
    public static double GetRandomDouble(double Min, double Max) => (new Random()).NextDouble() * (Max - Min) + Min;

    //* Окно помощи при запуске (если нет аргументов или по команде)
    public static void ShowHelp() {
        WriteLine("----Команды----                        \n" + 
        "-help             - показать справку             \n" + 
        "-i                - входной файл                 \n");
    }
}