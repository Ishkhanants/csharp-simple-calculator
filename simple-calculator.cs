using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_3
{
    class Program
    {
        static List<string> applyBasicMathOperation(List<string> expression, string operation)
        {
            int operationIndex = expression.LastIndexOf(operation);

            while (operationIndex != -1)
            {
                double operand1 = Convert.ToDouble(expression[operationIndex - 1]);
                double operand2 = Convert.ToDouble(expression[operationIndex + 1]);

                double operationApplied;

                if(operation == "+")
                {
                    operationApplied = operand1 + operand2;
                } else if(operation == "-")
                {
                    operationApplied = operand1 - operand2;
                } else if(operation == "*")
                {
                    operationApplied = operand1 * operand2;
                } else if(operation == "/")
                {
                    operationApplied = operand1 / operand2;
                }else
                {
                    throw new Exception("Unpredicted Scenario!");
                }

                string result = operationApplied.ToString();
                expression[operationIndex] = result;

                expression.RemoveAt(operationIndex + 1);
                expression.RemoveAt(operationIndex - 1);

                operationIndex = expression.LastIndexOf(operation);
            }

            return expression;
        }

        static void checkExpressionValidity(string expression)
        {
            char[] numericRegExp = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            string[] allCharsInExpressionExceptNumbers = expression.Split(numericRegExp, StringSplitOptions.RemoveEmptyEntries);

            string[] basicMathOperations = { "+", "-", "*", "/" };

            List<string> operationRegExp = new List<string>(basicMathOperations);

            foreach (string s in allCharsInExpressionExceptNumbers)
            {
                if (!basicMathOperations.Contains(s))
                {
                    throw new Exception("Illegal operation sign input!");
                }
            }
        }

        static void calculateExpression(string expression)
        {
            try
            {
                checkExpressionValidity(expression);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            string[] expressionArray = expression.Replace(" ", "").Replace("*", " * ").Replace("/", " / ").Replace("+", " + ").Replace("-", " - ").Split(' ');

            List<string> expressionList = new List<string>(expressionArray);

            try
            {
                expressionList = applyBasicMathOperation(expressionList, "/");
                expressionList = applyBasicMathOperation(expressionList, "*");
                expressionList = applyBasicMathOperation(expressionList, "-");
                expressionList = applyBasicMathOperation(expressionList, "+");  
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine(expressionList[0]);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Please input your expression without '=' in the end and push 'Enter' key to see the\n" +
                "result and to input another expression. You may push the 'Q' key to end the session.\n");
            
            for (; ; )
            {
                string expression = Console.ReadLine();

                calculateExpression(expression);

                if (Console.ReadKey().Key == ConsoleKey.Q)
                {
                    return;
                }
            }
        }
    }
}