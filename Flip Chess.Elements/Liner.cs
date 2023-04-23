namespace Flip_Chess.Elements
{
    public struct Liner
    {
        public int X1;
        public int Y1;
        public int X2;
        public int Y2;

        //@Construct
        public Liner(int x1, int y1, int x2, int y2) : this()
        {
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
        }
    }
}