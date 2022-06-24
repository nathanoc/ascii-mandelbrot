using System;

namespace ASCII_Mandelbrot
{
    public interface IPalette
    {
        public char GetShadeChar(int iteration, int maxIterations);
        public static IPalette InputPalette() => throw new NotImplementedException();
        public object[] PropertiesArray();

        public char InMandelbrotChar { get; }
    }

    public class Palette : IPalette
    {
        public string Brightnesses { get; } = "'`^,:;Il!i><~+_-?][}{1)(|/tfjrxnuvczXYUJCLQ0OZmwqpdbkhao*#MW&8%B@$";
        public float FractionExponent { get; }
        public char InMandelbrotChar { get; }

        public char GetShadeChar(int iterations, int maxIterations)
        {
            float iterationFraction = (float)iterations / maxIterations;

            // Manual handling of iterationFraction being 1 as otherwise the index of Brightnesses being accessed would be equal to its length (i.e. would cause an error)
            if (iterationFraction >= 1)
            {
                return Brightnesses[Brightnesses.Length - 1];
            }

            return Brightnesses[(int)(Math.Pow(iterationFraction, FractionExponent) * Brightnesses.Length)];
        }

        public Palette(string brightnesses, float fractionExponent, char inMandelbrotChar)
        {
            if (brightnesses != "")
            {
                Brightnesses = brightnesses;    // Else stay default
            }

            FractionExponent = fractionExponent;
            InMandelbrotChar = inMandelbrotChar;
        }

        public Palette() { }

        public static Palette InputPalette()
        {
            Console.WriteLine("Enter brightness gradient (press enter for default)");
            string brightnesses = Console.ReadLine();
            Console.WriteLine("Enter exponent for the iterations fraction");
            float fractionExponent = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter character for pixels inside the Mandelbrot set");
            char inMandelbrotChar = Console.ReadLine()[0];

            return new Palette(brightnesses, fractionExponent, inMandelbrotChar);
        }

        public object[] PropertiesArray()
        {
            return new object[] { Brightnesses, FractionExponent, InMandelbrotChar };
        }
    }

    public class FractionalLoopingPalette : IPalette
    {
        public string Brightnesses { get; } = "'`^,:;Il!i><~+_-?][}{1)(|/tfjrxnuvczXYUJCLQ0OZmwqpdbkhao*#MW&8%B@$";
        public float LoopLength { get; }
        public char InMandelbrotChar { get; }

        public char GetShadeChar(int iterations, int maxIterations)
        {
            float iterationFraction = (float)iterations / maxIterations;
            float fracModLength = iterationFraction % LoopLength;

            // Manual handling of iterationFraction being 1 as otherwise the index of Brightnesses being accessed would be equal to its length (i.e. would cause an error)
            if (fracModLength == LoopLength)
            {
                return Brightnesses[Brightnesses.Length - 1];
            }

            return Brightnesses[(int)(fracModLength / LoopLength * Brightnesses.Length)];
        }

        public FractionalLoopingPalette(string brightnesses, float loopLength, char inMandelbrotChar)
        {
            if (brightnesses != "")
            {
                Brightnesses = brightnesses;    // Else stay default
            }

            LoopLength = loopLength;
            InMandelbrotChar = inMandelbrotChar;
        }

        public FractionalLoopingPalette() { }

        public static FractionalLoopingPalette InputPalette()
        {
            Console.WriteLine("Enter brightness gradient (press enter for default)");
            string brightnesses = Console.ReadLine();
            Console.WriteLine("Enter fraction (as a decimal) of maximum iterations per gradient loop");
            float loopLength = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter character for pixels inside the Mandelbrot set");
            char inMandelbrotChar = Console.ReadLine()[0];

            return new FractionalLoopingPalette(brightnesses, loopLength, inMandelbrotChar);
        }

        public object[] PropertiesArray()
        {
            return new object[] { Brightnesses, LoopLength, InMandelbrotChar };
        }
    }

    public class NumericLoopingPalette : IPalette
    {
        public string Brightnesses { get; } = "'`^,:;Il!i><~+_-?][}{1)(|/tfjrxnuvczXYUJCLQ0OZmwqpdbkhao*#MW&8%B@$";
        public int LoopLength { get; } 
        public char InMandelbrotChar { get; }

        public char GetShadeChar(int iterations, int maxIterations)
        {
            int iterationModLength = iterations % LoopLength;

            return Brightnesses[(int)((float)iterationModLength / LoopLength * Brightnesses.Length)];
        }

        public NumericLoopingPalette(string brightnesses, int loopLength, char inMandelbrotChar)
        {
            if (brightnesses != "")
            {
                Brightnesses = brightnesses;    // Else stay default
            }

            LoopLength = loopLength;
            InMandelbrotChar = inMandelbrotChar;
        }

        public NumericLoopingPalette() { }

        public static NumericLoopingPalette InputPalette()
        {
            Console.WriteLine("Enter brightness gradient (press enter for default)");
            string brightnesses = Console.ReadLine();
            Console.WriteLine("Enter number of iterations per gradient loop");
            int loopLength = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter character for pixels inside the Mandelbrot set");
            char inMandelbrotChar = Console.ReadLine()[0];

            return new NumericLoopingPalette(brightnesses, loopLength, inMandelbrotChar);
        }

        public object[] PropertiesArray()
        {
            return new object[] { Brightnesses, LoopLength, InMandelbrotChar };
        }
    }
}
