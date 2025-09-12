using Raylib_cs;
using System.Numerics;
public class SceneGame : Scene
{
    // CORE GAME PARAMETERS
    private static int fieldColumns = 60;
    private static int fieldRows = 45;
    private static int cellSize = 16;
    private static Vector2 fieldOrigin = new Vector2((Raylib.GetScreenWidth() - fieldColumns * cellSize) / 2, (Raylib.GetScreenHeight() - fieldRows * cellSize) / 2);
    private static Coordinates startPos = new Coordinates(fieldColumns / 2, fieldRows / 2);
    private static Coordinates startDir = Coordinates.right;
    private static float startSpeed = 0.2f;
    private static int startSize = 3;
    private static int starveTime = 40;
    private static int maxSuperSenseCharges = 3;

    // Game objects in the scene
    private Timer moveTimer;
    private Timer starveTimer;
    private Grid<bool> snakeField;
    private Snake snake;
    private Rat rat;
    private int score;
    private static int superSenseCharges;
    public static Sound squeak;

    public SceneGame()
    {
        this.snakeField = new Grid<bool>(fieldColumns, fieldRows, cellSize, fieldOrigin);
        this.snake = new Snake(startPos, startDir, startSpeed, snakeField, startSize);
        this.rat = new Rat(snakeField, snake);
        this.moveTimer = new Timer(snake.moveSpeed, true, null, OnMoveTimer);
        this.starveTimer = new Timer(starveTime, false, 1, Loose);
        this.score = 0;
        superSenseCharges = maxSuperSenseCharges;
    }
    public override void Load()
    {
        squeak = Raylib.LoadSound("sounds/Squeak.wav");
    }
    public override void Update(float dt)
    {
        Controls();
        snake.Pivot(GetInputDirection());
        moveTimer.Update(dt);
        starveTimer.Update(dt);
        rat.RatSounds();
    }
    public override void Draw()
    {
        snakeField.Draw();
        snake.Draw();
        rat.Draw();
        DrawInterface();
    }
    public override void Unload()
    {
        Raylib.UnloadSound(squeak);
    }

    // What happens every time the snale moves
    public void OnMoveTimer()
    {
        snake.Move();
        if (rat.IsEaten() == true)
        {
            snake.Grow();
            // this would be for traditional snake :
            //snake.SpeedUp(); 
            //moveTimer.SetDuration(snake.moveSpeed); -> This was needed because Nico's code didn't work
            rat.Respawn();
            starveTimer.Reset();
            score++;
            if (rat.isVisible) rat.Toggle(); // hide the rat again if super sense was active
            Raylib.PlaySound(squeak);
        }
        if (snake.IsCollidingWithSelf() == true) Loose();
    }
    private void Loose()
    {
        Console.WriteLine("GAME OVER ");
        SceneManager.Load<SceneGameOver>();
    }
    private void DrawInterface()
    {
        int topMargin = 2;
        // Starvation Counter
        DrawCenteredText("Starvation in " + starveTimer.ToString() + " s", 20, topMargin, Color.White);
        // Score
        DrawRightAlignedText($"Score: {score}", 20, topMargin, cellSize, Color.White);
        // Super sense charge
        Raylib.DrawText($"Super Sense: {superSenseCharges}", cellSize, topMargin, 20, Color.White);
    }
    private void Controls()
    {
        if (Raylib.IsKeyDown(KeyboardKey.Enter)) rat.Toggle(); // Cheat to see rat all the time
        if (Raylib.IsKeyDown(KeyboardKey.R)) SceneManager.Load<SceneGame>(); // Reset
        if (Raylib.IsMouseButtonPressed(MouseButton.Left)) // Go Faster
        {
            snake.SpeedUp();
            moveTimer.SetDuration(snake.moveSpeed);
        }
        if (Raylib.IsMouseButtonPressed(MouseButton.Right)) // Go Slower
        {
            snake.SpeedDown();
            moveTimer.SetDuration(snake.moveSpeed);
        }
        if (Raylib.IsMouseButtonPressed(MouseButton.Middle) && superSenseCharges > 0) // Use super sense
        {
            superSenseCharges--;
            rat.Toggle();
        }
    }
    private Coordinates GetInputDirection()
    {
        Coordinates direction = Coordinates.zero;
        if (Raylib.IsKeyDown(KeyboardKey.W)) direction = Coordinates.up;
        if (Raylib.IsKeyDown(KeyboardKey.A)) direction = Coordinates.left;
        if (Raylib.IsKeyDown(KeyboardKey.S)) direction = Coordinates.down;
        if (Raylib.IsKeyDown(KeyboardKey.D)) direction = Coordinates.right;
        return direction;
    }
}
