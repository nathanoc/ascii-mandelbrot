using System;
using ASCII_Mandelbrot.ExtensionMethods;

namespace ASCII_Mandelbrot.Tests
{
    static class TestMethods
    {
        public static void RunTestsOnStart()
        {
            // Put tests here and they will run at the start of execution
            // e.g. TestPaletteGradient("'`-^_,:;><rvczxnutfXZ#MW&8%B@", GradientDrawType.Horizontal);
        }

        public enum GradientDrawType
        {
            Vertical,
            Horizontal,
            Diagonal
        }
        public static void TestPaletteGradient(string gradient, GradientDrawType drawType, int width = 160, int height = 80)
        {
            switch (drawType)
            {
                case GradientDrawType.Vertical:
                    foreach (char c in gradient)
                    {
                        for (int i = 0; i < height / gradient.Length; i++)
                        {
                            Console.WriteLine(c.Times(width));
                        }
                    }
                    break;
                case GradientDrawType.Horizontal:
                    for (int i = 0; i < height; i++)
                    {
                        foreach (char c in gradient)
                        {
                            Console.Write(c.Times(width / gradient.Length));
                        }
                        Console.Write("\n");
                    }
                    break;
                case GradientDrawType.Diagonal:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }

            Console.ReadKey();
        }
    }
}
