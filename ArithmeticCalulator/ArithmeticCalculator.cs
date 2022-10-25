using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Globalization;
using Calculator;

namespace ArithmeticCalulator;

[Export(typeof(ICalculator))]
public class ArithmeticCalculator : ICalculator
{
    private string name = "Arithmetic Calculator";

    public string GetName()
    {
        return name;
        
    }
 
    public string Calculate(string operandA, string op, string operandB)
    {
        char oper = op[0];
        double a = 0, b = 0;
        Double.TryParse(operandA, out a);
        Double.TryParse(operandB, out b);
        double? solution = 0;
        
        switch (oper)
        {
            case '*':
                solution = a * b;
                break;
            case '+':
                solution = a + b;
                break;
            case '-':
                solution = a - b;
                break;
            case '/':
                solution = a / b;
                break;
            case '%':
                solution = a % b;
                break;
            default:
                solution = null;
                break;
        }

        return solution.ToString();
    }
}