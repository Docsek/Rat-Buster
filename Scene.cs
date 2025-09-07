using Raylib_cs;

public abstract class Scene
{
    public abstract void Load();
    public abstract void Update(float dt);
    public abstract void Draw();
    public abstract void Unload();

    protected static void DrawCenteredText(string text, int fontSize, int height, Color color)
    {
        int textLength = Raylib.MeasureText(text, fontSize);
        int xpos = Raylib.GetScreenWidth() / 2 - textLength / 2;
        int ypos = height;
        Raylib.DrawText(text, xpos, ypos, fontSize, color);
    }
    protected static void DrawRightAlignedText(string text, int fontSize, int height, int rightMargin, Color color)
    {
        int textLength = Raylib.MeasureText(text, fontSize);
        int xpos = Raylib.GetScreenWidth() - textLength - rightMargin;
        int ypos = height;
        Raylib.DrawText(text, xpos, ypos, fontSize, color);
    }
}