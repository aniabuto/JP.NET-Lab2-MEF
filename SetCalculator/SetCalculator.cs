using System.ComponentModel.Composition;
using Calculator;

namespace SetCalculator;

[Export(typeof(ICalculator))]
public class SetCalculator : ICalculator
{
    public string Calculate(string operandA, string op, string operandB)
    {
        char oper = op[0];
        double[] setA = FindSetElements(operandA);
        double[] setB = FindSetElements(operandB);

        double[] solution;
        
        switch (oper)
        {
            case '*':
                solution = JoinSets(setA, setB);
                break;
            case '+':
                solution = SumSets(setA, setB);
                break;
            default:
                solution = null;
                break;
        }

        return solution.ToString();
    }

    private double[] FindSetElements(string set)
    {
        string[] elements = set.Split(",");
        double[] numbers = new double[elements.Length];
        
        for (int i = 0; i < elements.Length; i++)
        {
            double.TryParse(elements[i], out numbers[i]);
        }

        return numbers;
    }

    private double[] JoinSets(double[] setA, double[] setB)
    {
        List<double> solution = new List<double>();
        foreach (var elementA in setA)
        {
            if (setB.Contains(elementA))
            {
                solution.Add(elementA);
            }
        }

        return solution.ToArray();
    }

    private double[] SumSets(double[] setA, double[] setB)
    {
        List<double> solution = setA.ToList();
        foreach (var elementB in setB)
        {
            if (!setA.Contains(elementB))
            {
                solution.Add(elementB);
            }
        }

        return solution.ToArray();
    }
}