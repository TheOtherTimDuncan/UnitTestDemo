using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo
{
    public class Calculator
    {
        public int Add(params int[] values)
        {
            if (values.Length <= 1)
            {
                throw new ArgumentOutOfRangeException("values", "At least two values are required");
            }

            return values.Sum();
        }

        public int Subtract(params int[] values)
        {
            if (values.Length <= 1)
            {
                throw new ArgumentOutOfRangeException("values", "At least two values are required");
            }

            //return values[0] - values.Skip(1).Sum(x => x * -1);
            return values[0] + values.Skip(1).Sum(x => x * -1);
        }

        public int Multiply(params int[] values)
        {
            if (values.Length <= 1)
            {
                throw new ArgumentOutOfRangeException("values", "At least two values are required");
            }

            int result = 1;
            foreach (int v in values)
            {
                result *= v;
            }
            return result;
        }

        public decimal Divide(params int[] values)
        {
            if (values.Length <= 1)
            {
                throw new ArgumentOutOfRangeException("values", "At least two values are required");
            }

            decimal result = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                result = result / values[i];
            }
            return result;
        }
    }
}
