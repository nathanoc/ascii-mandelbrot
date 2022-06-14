namespace ASCII_Mandelbrot
{
    class MandelbrotSettings
    {
        public int widthChars;
        public int heightChars;
        public decimal xWidth;
        public decimal yWidth;
        public decimal centrePosX;
        public decimal centrePosY;
        public int maxIterations;

        public MandelbrotSettings(int widthChars = 0, int heightChars = 0, decimal xWidth = 0, decimal yWidth = 0, decimal centrePosX = 0, decimal centrePosY = 0, int maxIterations = 0) // There must be a better way to do this than defaulting everything to 0
        {
            this.widthChars = widthChars;
            this.heightChars = heightChars;
            this.xWidth = xWidth;
            this.yWidth = yWidth;
            this.centrePosX = centrePosX;
            this.centrePosY = centrePosY;
            this.maxIterations = maxIterations;
        }
    }
}
