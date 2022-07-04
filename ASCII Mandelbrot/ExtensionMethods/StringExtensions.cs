using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Mandelbrot.ExtensionMethods
{
    static class StringExtensions
    {
        public static string Times(this string toMultiply, int multiplyBy)
        {
            if (multiplyBy < 0)
                throw new ArgumentOutOfRangeException(nameof(multiplyBy));

            string result = "";

            for (int j = 0; j < multiplyBy; j++)
            {
                result += toMultiply;
            }

            return result;
        }

        public static string Times(this char toMultiply, int multiplyBy) => toMultiply.ToString().Times(multiplyBy);
    }
}
