using System;
using System.Linq;

class Calculator
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Enter an expression (e.g., 2+3*4/5) or 'q' to quit:");
            string? input = Console.ReadLine();

            if (input?.ToLower() == "q")
                break;

            try
            {
                if (input != null)
                {
                    double result = EvaluateExpression(input);
                    Console.WriteLine("Result: " + result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine();
        }
    }

    static double EvaluateExpression(string expression)
    {
        char[] operators = { '+', '-', '*', '/' };

        string[] tokens = expression.Split(operators, StringSplitOptions.RemoveEmptyEntries);
        string[] operatorsOnly = expression.Split(tokens, StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length != operatorsOnly.Length + 1)
        {
            throw new ArgumentException("Invalid expression: " + expression);
        }

        double result = double.Parse(tokens[0]);

        for (int i = 0; i < operatorsOnly.Length; i++)
        {
            string operation = operatorsOnly[i];
            double operand = double.Parse(tokens[i + 1]);

            if (!operators.Contains(operation[0]))
            {
                throw new ArgumentException("Invalid operation: " + operation);
            }

            switch (operation)
            {
                case "+":
                    result += operand;
                    break;
                case "-":
                    result -= operand;
                    break;
                case "*":
                    result *= operand;
                    break;
                case "/":
                    if (Math.Abs(operand) < double.Epsilon)
                    {
                        throw new DivideByZeroException("Cannot divide by zero.");
                    }
                    result /= operand;
                    break;
            }
        }

        return result;
    }
}
