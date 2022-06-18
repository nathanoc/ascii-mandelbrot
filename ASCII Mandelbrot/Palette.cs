using System;

namespace ASCII_Mandelbrot
{
    interface IPalette
    {
        public char GetShadeChar(int iteration, int maxIterations);
        public char InMandelbrotChar { get; }
    }

    class Palette : IPalette
    {
        private string Brightnesses;
        private float FractionExponent;

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
            Brightnesses = brightnesses;
            FractionExponent = fractionExponent;
            InMandelbrotChar = inMandelbrotChar;
        }
    }

    class FractionalLoopingPalette : IPalette
    {
        private string Brightnesses;
        private float LoopLength;

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
            Brightnesses = brightnesses;
            LoopLength = loopLength;
            InMandelbrotChar = inMandelbrotChar;
        }
    }

    class NumericLoopingPalette : IPalette
    {
        private string Brightnesses;
        private int LoopLength;

        public char InMandelbrotChar { get; }

        public char GetShadeChar(int iterations, int maxIterations)
        {
            int iterationModLength = iterations % LoopLength;
            
            if (iterationModLength == LoopLength)
            {
                return Brightnesses[Brightnesses.Length - 1];
            }

            return Brightnesses[(int)((float)iterationModLength / LoopLength * Brightnesses.Length)];
        }

        public NumericLoopingPalette(string brightnesses, int loopLength, char inMandelbrotChar)
        {
            Brightnesses = brightnesses;
            LoopLength = loopLength;
            InMandelbrotChar = inMandelbrotChar;
        }
    }
}
