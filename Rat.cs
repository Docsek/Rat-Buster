using System.Numerics;
using Raylib_cs;

public class Rat
{
    public Coordinates position { get; private set; }
    private Grid<bool> snakeField;
    private Snake snake;
    public bool isVisible { get; private set; } = false;

    public Rat(Grid<bool> snakeField, Snake snake)
    {
        this.snakeField = snakeField;
        this.snake = snake;
        Respawn();
    }

    // Respawn
    public void Respawn()
    {
        Coordinates newPosition = new();
        do { newPosition = Coordinates.Randomize(snakeField.columns, snakeField.rows); }
        while (snake.IsCollidingWith(newPosition));
        position = newPosition;
    }

    // Check snake distance for rat sounds
    private bool SnakeIsFar()
    {
        List<Coordinates> farRange = snakeField.FarMooreNeighborhood(position);
        bool snakeSeen = false;
        foreach (Coordinates lookout in farRange)
        {
            if (snake.snakeHead == lookout) snakeSeen = true;
        }
        return snakeSeen;
    }
    private bool SnakeIsClose()
    {
        List<Coordinates> closeRange = snakeField.CloseMooreNeighborhood(position);
        bool snakeSeen = false;
        foreach (Coordinates lookout in closeRange)
        {
            if (snake.snakeHead == lookout) snakeSeen = true;
        }
        return snakeSeen;
    }
    private bool SnakeIsAdjacent()
    {
        List<Coordinates> adjRange = snakeField.AdjacentMooreNeighborhood(position);
        bool snakeSeen = false;
        foreach (Coordinates lookout in adjRange)
        {
            if (snake.snakeHead == lookout) snakeSeen = true;
        }
        return snakeSeen;
    }
    public bool IsEaten()
    {
        if (snake.snakeHead == position) return true;
        else return false;
    }

    // Update Rat Sounds and Snake Color
    public void RatSounds()
    {
        if (SnakeIsFar())
        {
            Console.WriteLine("snake is far !");
            snake.ChangeColor(Color.Gold);
            // playsound
        }
        else if (SnakeIsClose())
        {
            Console.WriteLine("snake is close !");
            snake.ChangeColor(Color.Orange);
            // playsound
        }
        else if (SnakeIsAdjacent())
        {
            Console.WriteLine("snake is adjacent !");
            snake.ChangeColor(Color.Maroon);
            // playsound
        }
        else if (IsEaten())
        {
            Console.WriteLine("rat is dead !");
            // playsound
        }
        else
        {
            Console.WriteLine("snake is NOWHERE !");
            snake.ChangeColor(Color.White);
            return;
        }
    }
    // Toggle the rat
    public void Toggle()
    {
        isVisible = !isVisible;
    }

    // Draw the rat
    public void Draw()
    {
        Vector2 worldPos = snakeField.GridToWorld(position);
        worldPos += new Vector2(snakeField.cellSize, snakeField.cellSize) * 0.5f; // since origin is topleft, we need half a cell bottomright to be in the center
        if (isVisible) Raylib.DrawCircle((int)worldPos.X, (int)worldPos.Y, snakeField.cellSize * 0.4f, Color.Red);
    }
}