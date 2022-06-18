using System;

namespace ASCII_Mandelbrot
{
    class ZoomSettings
    {
        public double initialWidth;
        public double finalWidth;
        public double centrePosX;
        public double centrePosY;
        public int maxIterations;
        public float duration;

        public ZoomSettings(double initialWidth = 0, double finalWidth = 0, double centrePosX = 0, double centrePosY = 0, int maxIterations = 0, float duration = 0) // I really hope there's a better way to do this
        {
            this.initialWidth = initialWidth;
            this.finalWidth = finalWidth;
            this.centrePosX = centrePosX;
            this.centrePosY = centrePosY;
            this.maxIterations = maxIterations;
            this.duration = duration;
        }

        public static ZoomSettings inputZoomSettings()
        {
            ZoomSettings returnValue = new ZoomSettings();

            Console.WriteLine("Enter initial frame width");
            returnValue.initialWidth = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter final frame width");
            returnValue.finalWidth = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter X coordinate to zoom to");
            returnValue.centrePosX = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter Y coordinate to zoom to");
            returnValue.centrePosY = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter maximum iteration count");
            returnValue.maxIterations = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter animation duration");
            returnValue.duration = float.Parse(Console.ReadLine());

            return returnValue;
        }
    }
}
