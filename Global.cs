using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Minesharp;

public static class Global
{
    public static float DeltaTime { get; private set; }
    public static SpriteFont DebugFont;

    public static void YieldDeltaTime(GameTime gt) => DeltaTime = (float)gt.ElapsedGameTime.TotalSeconds;
}
