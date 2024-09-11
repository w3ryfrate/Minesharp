using Microsoft.Xna.Framework.Content;

namespace Minesharp;

public static class Global
{
    public static float DeltaTime { get; private set; }
    public static SpriteFont DebugFont;
    public static ContentManager Content;

    public static void YieldDeltaTime(GameTime gt) => DeltaTime = (float)gt.ElapsedGameTime.TotalSeconds;
}
