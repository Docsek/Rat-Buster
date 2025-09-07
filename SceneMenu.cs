using Raylib_cs;
public class SceneMenu : Scene
{
    public override void Load()
    {
        //Console.WriteLine("SceneMenu.Load : ICI");
    }
    public override void Update(float dt)
    {
        //Console.WriteLine("SceneMenu.Update : ICI");
        if (Raylib.IsKeyPressed(KeyboardKey.Space)) SceneManager.Load<SceneGame>();
    }
    public override void Draw()
    {
        //Console.WriteLine("SceneMenu.Draw : ICI");
        DrawCenteredText("WELCOME TO RAT BUSTER", 30, Raylib.GetScreenHeight() / 2, Color.White);
        DrawCenteredText("Press Space to Start", 10, Raylib.GetScreenHeight() - 100, Color.White);
    }
    public override void Unload()
    {
        //Console.WriteLine("SceneMenu.Unload : ICI");
    }
}