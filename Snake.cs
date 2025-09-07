using System.Numerics;
using Raylib_cs;
public class Snake
{
    Grid<bool> snakeField;
    Queue<Coordinates> snakeBody = new();
    Coordinates direction;
    Coordinates nextDirection;
    public float moveSpeed { get; private set; }
    private float maxSpeed = 0.05f;
    private float minSpeed = 1.0f;
    public readonly int startSize;
    private bool IsGrowing = false;
    private Color color = Color.White;
    public Coordinates snakeHead => snakeBody.Last(); // this => operator has a getter inside, snakeHead is therefore evaluated each time, this is cool
    public Snake(Coordinates startPos, Coordinates direction, float moveSpeed, Grid<bool> snakeField, int startSize)
    {
        this.direction = direction;
        this.nextDirection = direction;
        this.moveSpeed = moveSpeed;
        this.snakeField = snakeField;
        this.startSize = startSize;
        for (int i = startSize - 1; i >= 0; i--) snakeBody.Enqueue(startPos - direction * i);
    }

    // Manage Snake Movements and turns
    public void Move() // Move 1 square and dequeue if not growing
    {
        direction = nextDirection;
        // manage Enqueue for out of grid situations and standard movement
        if (IsLeavingRight()) snakeBody.Enqueue(new Coordinates(0, snakeBody.Last().rowpos));
        else if (IsLeavingLeft()) snakeBody.Enqueue(new Coordinates(snakeField.columns - 1, snakeBody.Last().rowpos));
        else if (IsLeavingBottom()) snakeBody.Enqueue(new Coordinates(snakeBody.Last().columnpos, 0));
        else if (IsLeavingTop()) snakeBody.Enqueue(new Coordinates(snakeBody.Last().columnpos, snakeField.rows - 1));
        else snakeBody.Enqueue(snakeBody.Last() + direction);

        // manage Dequeue
        if (!IsGrowing) snakeBody.Dequeue();
        else IsGrowing = false;
    }
    public void Grow()
    {
        IsGrowing = true;
    }
    public void Pivot(Coordinates newDirection)
    {
        if (newDirection == direction || newDirection == -direction) return; // Prevent double down or reverse direction
        if (newDirection == Coordinates.zero) return; // Prevent snake stopping when nothing is pressed
        else nextDirection = newDirection;
    }
    public void SpeedUp() // Means interval goes down
    {
        if (moveSpeed > maxSpeed) moveSpeed /= 1.2f;
    }
    public void SpeedDown() // Means interval goes up
    {
        if (moveSpeed < minSpeed) moveSpeed *= 1.2f;
    }

    // Manage collisions with map, rat nest and self
    public bool IsOnRatNest(Rat rat)
    {
        if (snakeHead == rat.position) return true;
        else return false;
    }
    public bool IsCollidingWith(Coordinates coordinates)
    {
        bool isColliding = false;
        foreach (Coordinates part in snakeBody)
        {
            if (part == coordinates) isColliding = true;
        }
        return isColliding;
    }
    public bool IsCollidingWithSelf()
    {
        if (snakeBody.Count() != snakeBody.Distinct().Count()) return true; // compare number of elements in snake body to number of DISTINCT elements
        else return false;
    }
    public bool IsOutOfField()
    {
        return !snakeField.IsInGrid(snakeHead);
    }
    public bool IsLeavingRight()
    {
        if (snakeHead.columnpos == snakeField.columns - 1 && direction == Coordinates.right) return true;
        else return false;
    }
    public bool IsLeavingLeft()
    {
        if (snakeHead.columnpos == 0 && direction == Coordinates.left) return true;
        else return false;
    }
    public bool IsLeavingBottom()
    {
        if (snakeHead.rowpos == snakeField.rows - 1 && direction == Coordinates.down) return true;
        else return false;
    }
    public bool IsLeavingTop()
    {
        if (snakeHead.rowpos == 0 && direction == Coordinates.up) return true;
        else return false;
    }

    // Change Snake color when getting closer
    public void ChangeColor(Color newColor)
    {
        color = newColor;
    }

    // Draw
    public void Draw()
    {
        foreach (Coordinates part in snakeBody)
        {
            Vector2 worldPos = snakeField.GridToWorld(part);
            Raylib.DrawRectangle((int)worldPos.X, (int)worldPos.Y, snakeField.cellSize, snakeField.cellSize, color);
        }
    }
}