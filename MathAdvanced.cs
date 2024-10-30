using System;

namespace corv1njano.MathAdvanced
{
    public class MathAdvanced
    {
        public static double Square(int number)
        {
            double suqared = number * number;
            return suqared;
        }

        public static double Square(int number, int exponent)
        {
            double squared = 1;
            if (exponent == 0)
            {
                return 1;
            }
            else if (exponent == 1)
            {
                return number;
            }
            else if (exponent > 1)
            {
                for (int i = 0; i < exponent; i++)
                {
                    squared *= number;
                }
                return squared;
            }
            else
            {
                for (int i = 0; i < Math.Abs(exponent); i++)
                {
                    squared *= Math.Abs(number);
                }
                return (1 / squared);
            }
        }

        public static double Factorial(int number)
        {
            double factorial = 1;
            if (number == 0 || number == 1)
            {
                return 1;
            }
            else if (number > 1)
            {
                for (int i = 1; i <= number; i++)
                {
                    factorial *= i;
                }
                return factorial;
            }
            else
            {
                return 0;
            }
        }

        public static double Average(int[] numbers)
        {
            if (numbers.Length > 0)
            {
                int sum = 0;
                foreach (int number in numbers)
                {
                    sum += number;
                }
                double average = (double)sum / numbers.Length;
                return average;
            }
            else
            {
                return 0;
            }
        }
    }
}
