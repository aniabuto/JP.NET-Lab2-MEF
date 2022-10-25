namespace Calculator;

public interface ICalculator
{
    public string GetName();

    public string Calculate(string operandA, string op, string operandB);
}