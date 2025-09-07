using System.Numerics;

public struct Coordinates
{
    public readonly int columnpos;
    public readonly int rowpos;

    public Coordinates(int columnpos, int rowpos)
    {
        this.columnpos = columnpos;
        this.rowpos = rowpos;
    }

    // Define commonly used Coordinates
    public static Coordinates zero = new Coordinates(0, 0);
    public static Coordinates left = new Coordinates(-1, 0);
    public static Coordinates right = new Coordinates(1, 0);
    public static Coordinates up = new Coordinates(0, -1);
    public static Coordinates down = new Coordinates(0, 1);

    // Surcharge operators for Coordinates
    public static Coordinates operator +(Coordinates a, Coordinates b)
    {
        return new Coordinates(a.columnpos + b.columnpos, a.rowpos + b.rowpos);
    }

    public static Coordinates operator -(Coordinates a, Coordinates b)
    {
        return new Coordinates(a.columnpos - b.columnpos, a.rowpos - b.rowpos);
    }

    public static Coordinates operator -(Coordinates a)
    {
        return new Coordinates(-a.columnpos, -a.rowpos);
    }

    public static Coordinates operator *(Coordinates a, int b)
    {
        return new Coordinates(a.columnpos * b, a.rowpos * b);
    }

    public static Coordinates operator *(int a, Coordinates b)
    {
        return new Coordinates(a * b.columnpos, a * b.rowpos);
    }

    public static Coordinates operator /(Coordinates a, int b)
    {
        return new Coordinates(a.columnpos / b, a.rowpos / b);
    }

    public static bool operator ==(Coordinates a, Coordinates b) // if you do == you must do != too AND Equals method too
    {
        if (a.columnpos == b.columnpos && a.rowpos == b.rowpos) return true;
        else return false;
    }

    public static bool operator !=(Coordinates a, Coordinates b)
    {
        if (a.columnpos == b.columnpos && a.rowpos == b.rowpos) return false;
        else return true;
    }

    // Equals override NEEDED to be used by multiple C# methods (because we did surcharge operators)
    public override bool Equals(object? obj)
    {
        if ((obj is Coordinates myObj) && (this == myObj)) return true;
        else return false;
    }
    // GetHashCode Override also kinda needed because we messed with operators
    public override int GetHashCode()
    {
        return HashCode.Combine(columnpos, rowpos);
    }

    // ToString override for traces and all
    public override string ToString()
    {
        return $"({columnpos}, {rowpos})";
    }

    // Randomize Coordinates
    public static Coordinates Randomize(int maxColumns, int maxRows)
    {
        var randomN = new Random();
        return new Coordinates(randomN.Next(maxColumns), randomN.Next(maxRows));

    }

    // Turn Coordinates into vectors 2 (let's discuss this example during exam)
    //// MY VERSION 
    public static Vector2 ToVector(Coordinates coord)
    {
        return new Vector2(coord.columnpos, coord.rowpos);
    }

    //// NICO'S VERSION
    public Vector2 ToVector()
    {
        return new Vector2(columnpos, rowpos);
    }
}