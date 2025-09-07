using System.Numerics;
using Raylib_cs;

public static class SceneManager
{
    private static Scene? currentScene;

    public static void Load<TgenericScene>() where TgenericScene : Scene, new() // The method uses genericScene that can be null BUT must be a type/subtype of Scene. Also this methods creates new instances of genericScene.
    {
        currentScene?.Unload(); // can use if (currentScene != null) currentScene.Unload();
        currentScene = new TgenericScene();
        currentScene.Load();
    }

    public static void Update(float dt)
    {
        currentScene?.Update(dt);
    }

    public static void Draw()
    {
        currentScene?.Draw();
    }
}