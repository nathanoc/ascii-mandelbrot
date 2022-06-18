namespace ASCII_Mandelbrot
{
    class MandelbrotSettings
    {
        public int widthChars;
        public int heightChars;
        public double xWidth;
        public double yWidth;
        public double centrePosX;
        public double centrePosY;
        public int maxIterations;
        public IPalette palette;

        public MandelbrotSettings(int widthChars = 0, int heightChars = 0, double xWidth = 0, double yWidth = 0, double centrePosX = 0, double centrePosY = 0, int maxIterations = 0, IPalette palette = null) // There must be a better way to do this than defaulting everything to 0
        {
            this.widthChars = widthChars;
            this.heightChars = heightChars;
            this.xWidth = xWidth;
            this.yWidth = yWidth;
            this.centrePosX = centrePosX;
            this.centrePosY = centrePosY;
            this.maxIterations = maxIterations;
            this.palette = palette;
        }
    }
}
