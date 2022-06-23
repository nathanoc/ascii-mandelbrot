using System;

namespace ASCII_Mandelbrot
{
    class MandelbrotSettings
    {
        public int WidthChars;
        public int HeightChars;
        public double XWidth;
        public double YWidth;
        public double CentrePosX;
        public double CentrePosY;
        public int MaxIterations;
        public IPalette MandelbrotPalette;

        public MandelbrotSettings(int widthChars, int heightChars, double xWidth, double yWidth, double centrePosX, double centrePosY, int maxIterations, IPalette palette)
        {
            WidthChars = widthChars;
            HeightChars = heightChars;
            XWidth = xWidth;
            YWidth = yWidth;
            CentrePosX = centrePosX;
            CentrePosY = centrePosY;
            MaxIterations = maxIterations;
            MandelbrotPalette = palette;
        }

        public static MandelbrotSettings InputMandelbrotSettings()
        {
            Console.WriteLine("Enter width in characters");
            int widthChars = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter height in characters");
            int heightChars = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter width in coordinates");
            double xWidth = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter height in coordinates");
            double yWidth = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter centre X coordinate");
            double centrePosX = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter centre Y coordinate");
            double centrePosY = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter maximum iterations");
            int maxIterations = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter palette type (1 = non-looping, 2 = fraction-based looping, 3 = number-based looping)");
            int paletteType = int.Parse(Console.ReadLine());

            IPalette palette;
            switch (paletteType)
            {
                case 1:
                    palette = Palette.InputPalette();
                    break;
                case 2:
                    palette = FractionalLoopingPalette.InputPalette();
                    break;
                case 3:
                    palette = NumericLoopingPalette.InputPalette();
                    break;
                default:
                    throw new NotImplementedException();
            }

            return new MandelbrotSettings(widthChars, heightChars, xWidth, yWidth, centrePosX, centrePosY, maxIterations, palette);
        }
    }
}
