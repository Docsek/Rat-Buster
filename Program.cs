using Raylib_cs;
class Game
{
    static void Main()
    {
        Raylib.InitWindow(1024, 768, "Mygame");
        Raylib.SetTargetFPS(60);
        Raylib.InitAudioDevice();

        SceneManager.Load<SceneMenu>();

        while (!Raylib.WindowShouldClose())
        {
            // UPDATE
            Update();
            // DRAW
            Raylib.BeginDrawing();
            Draw();
            Raylib.EndDrawing();
        }
        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }

    static void Update()
    {
        SceneManager.Update(Raylib.GetFrameTime());
    }
    static void Draw()
    {
        Raylib.ClearBackground(Color.Black);
        SceneManager.Draw();
    }
}