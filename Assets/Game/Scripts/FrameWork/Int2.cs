public struct Int2 
{
    public static readonly Int2 UP = new Int2(0, 1);
    public static readonly Int2 DOWN = new Int2(0, -1);
    public static readonly Int2 RIGHT = new Int2(1, 0);
    public static readonly Int2 LEFT = new Int2(-1, 0);

    public int X;
    public int Y;

    public Int2(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public static Int2 operator +(Int2 x, Int2 y) 
    {
        return new Int2(x.X + y.X, x.Y + y.Y);
    }
}
