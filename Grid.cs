using System.Numerics;
using Raylib_cs;

public class Grid<TCell>
{
    public readonly int columns; // readonly, defined HERE or in CONSTRUCTOR
    public readonly int rows;
    public readonly int cellSize;
    public readonly Vector2 origin;
    protected TCell[,] cells; // protected means only children classes can access this

    public Grid(int columns, int rows, int cellSize, Vector2 origin)
    {
        this.columns = columns;
        this.rows = rows;
        this.cellSize = cellSize;
        this.origin = origin;
        this.cells = new TCell[columns, rows];
    }

    // Check if items in Grid
    public bool IsInGrid(Coordinates coord)
    {
        if (coord.columnpos < 0 || coord.rowpos < 0 || coord.columnpos >= columns || coord.rowpos >= rows) return false;
        else return true;
    }
    private bool IsColumnInGrid(int column)
    {
        if (column < 0 || column >= columns) return false;
        else return true;
    }
    private bool IsRowInGrid(int row)
    {
        if (row < 0 || row >= rows) return false;
        else return true;
    }

    // Methods to manipulate indiviual cells
    public TCell GetCell(Coordinates coord)
    {
        if (IsInGrid(coord) == true) return cells[coord.columnpos, coord.rowpos];
        else throw new ArgumentOutOfRangeException("Grid index is out of range !");

    }
    public void SetCell(Coordinates coord, TCell value)
    {
        if (IsInGrid(coord) == true) cells[coord.columnpos, coord.rowpos] = value;
        else throw new ArgumentOutOfRangeException("Grid index is out of range !");
    }

    // Methods to switch from grid to world pos and back
    public Vector2 GridToWorld(Coordinates coord)
    {
        return Coordinates.ToVector(coord * cellSize) + origin;

        // NICO'S VERSION
        // coord *= cellSize;
        // return coord.ToVector() + origin;
    }
    public Coordinates WorldToGrid(Vector2 position)
    {
        int columnpos = (int)Math.Round((position.X - origin.X) / cellSize);
        int rowpos = (int)Math.Round((position.Y - origin.Y) / cellSize);
        return new Coordinates(columnpos, rowpos);
    }

    // Methods to find neighbors
    public List<Coordinates> MooreNeighborhood(Coordinates coord, int distance)
    {
        List<Coordinates> neighbors = new();
        if (IsInGrid(coord) == true)
        {
            for (int i = coord.columnpos - distance; i <= coord.columnpos + distance; i++)
            {
                if (IsColumnInGrid(i) == true) for (int j = coord.rowpos - distance; j <= coord.rowpos + distance; j++)
                    {
                        if (IsRowInGrid(j) == true && (!(i == coord.columnpos && j == coord.rowpos)))
                        {
                            neighbors.Add(new Coordinates(i, j));
                        }
                    }
            }
        }
        return neighbors;
    }
    public List<Coordinates> OuterMooreNeighborhood(Coordinates coord, int distance)
    {
        List<Coordinates> outerneighbors = new();
        if (IsInGrid(coord) == true)
        {
            outerneighbors = MooreNeighborhood(coord, distance).Except(MooreNeighborhood(coord, distance - 1)).ToList();
        }
        return outerneighbors;
    }
    public List<Coordinates> FarMooreNeighborhood(Coordinates coord)
    {
        List<Coordinates> farneighbors = new();
        int maxRange = 5;
        int exclusionRange = 3;
        if (IsInGrid(coord) == true)
        {
            farneighbors = MooreNeighborhood(coord, maxRange).Except(MooreNeighborhood(coord, exclusionRange)).ToList();
        }
        return farneighbors;
    }
    public List<Coordinates> CloseMooreNeighborhood(Coordinates coord)
    {
        List<Coordinates> closeneighbors = new();
        int maxRange = 3;
        int exclusionRange = 1;
        if (IsInGrid(coord) == true)
        {
            closeneighbors = MooreNeighborhood(coord, maxRange).Except(MooreNeighborhood(coord, exclusionRange)).ToList();
        }
        return closeneighbors;
    }
    public List<Coordinates> AdjacentMooreNeighborhood(Coordinates coord)
    {
        List<Coordinates> adjneighbors = new();
        if (IsInGrid(coord) == true)
        {
            adjneighbors = MooreNeighborhood(coord, 1);
        }
        return adjneighbors;
    }

    // Draw the grid
    public void Draw()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Vector2 cellPos = GridToWorld(new Coordinates(i, j));
                Raylib.DrawRectangleLines((int)cellPos.X, (int)cellPos.Y, cellSize, cellSize, Color.DarkGray);
            }
        }
    }
}