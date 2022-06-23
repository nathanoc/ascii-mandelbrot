using System;

namespace ASCII_Mandelbrot
{
    public class ZoomSettings
    {
        public double InitialWidth { get; set; }
        public double FinalWidth { get; set; }
        public double CentrePosX { get; set; }
        public double CentrePosY { get; set; }
        public int MaxIterations { get; set; }
        public float Duration { get; set; }
        public IPalette ZoomPalette { get; set; }

        public ZoomSettings(double initialWidth, double finalWidth, double centrePosX, double centrePosY, int maxIterations, float duration, IPalette zoomPalette)
        {
            InitialWidth = initialWidth;
            FinalWidth = finalWidth;
            CentrePosX = centrePosX;
            CentrePosY = centrePosY;
            MaxIterations = maxIterations;
            Duration = duration;
            ZoomPalette = zoomPalette;
        }

        public ZoomSettings() { }

        public static ZoomSettings InputZoomSettings()
        {
            Console.WriteLine("Enter initial frame width");
            double initialWidth = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter final frame width");
            double finalWidth = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter X coordinate to zoom to");
            double centrePosX = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter Y coordinate to zoom to");
            double centrePosY = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter maximum iteration count");
            int maxIterations = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter animation duration");
            float duration = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter palette type (1 = non-looping, 2 = fraction-based looping, 3 = number-based looping)");
            int paletteType = int.Parse(Console.ReadLine());

            IPalette zoomPalette;
            switch (paletteType)
            {
                case 1:
                    zoomPalette = Palette.InputPalette();
                    break;
                case 2:
                    zoomPalette = FractionalLoopingPalette.InputPalette();
                    break;
                case 3:
                    zoomPalette = NumericLoopingPalette.InputPalette();
                    break;
                default:
                    throw new NotImplementedException();
            }

            return new ZoomSettings(initialWidth, finalWidth, centrePosX, centrePosY, maxIterations, duration, zoomPalette);
        }
    }
}
