namespace Minesharp.Graphics;

public static class GraphicsManager
{
    public const int SCREEN_WIDTH = 1280;
    public const int SCREEN_HEIGHT = 720;

    public static Matrix View { get; private set; }
    public static Matrix Projection { get; private set; }

    public static void SetScreenBounds(GraphicsDeviceManager graphics)
    {
        graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
        graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
        graphics.ApplyChanges();
    }

    public static void YieldViewAndProjectionMatrices(Camera camera)
    {
        View = camera.GetViewMatrix();
        Projection = camera.GetProjectionMatrix();
    }
}
