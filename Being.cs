namespace PGA;

//* Класс особи
public class Being
{
    //* Фенотипы и генотипы особи 
    public Vector<double> Phenotype { get; set; }   /// Фенотип (значение в точках)
    public Vector<double> Genotype  { get; set; }   /// Генотип (значения коэффициентов)

    public double Functional { get; set; } = Double.MaxValue;   /// Значение функционала        

    // ***** Конструктор ***** //
    public Being(Vector<double> gen) {
        Genotype  = new Vector<double>(gen.Length);
        Vector<double>.Copy(gen, Genotype);
    }

    public Being(int CountGens, int CountPoints) {
        Genotype  = new Vector<double>(CountGens);
    }

    //* Расчет полиномa от точки с генами особи
    public double Polynom(double x) {
        double degreeX = 1;    /// перем. хранит степени X
        double sum = 0;        /// рещультат полинома
        for (int i = Genotype.Length - 1; i >= 0; i--) {
            sum += Genotype[i] * degreeX;
            degreeX *= x;
        }
        return sum;
    }

    //* Изменение фенотипа особи
    public void SetPhenotip(Vector<double> points) {
        Phenotype = new Vector<double>(points.Length);
        for (int i = 0; i < Phenotype.Length; i++)
            Phenotype[i] = Polynom(points[i]);
    }

    //* Строковое представление особи
    public override string ToString() {
        StringBuilder str = new StringBuilder();
        str.Append($"Gens: [");
        for (int i = 0; i < Genotype.Length - 1; i++)
            str.Append(Genotype[i].ToString("F3") + ", ");
        str.Append(Genotype[Genotype.Length - 1].ToString("F3"));
        str.Append($"]; Functional = {Functional}");
        return str.ToString();
    }
}