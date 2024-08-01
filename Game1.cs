global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Minesharp.Graphics;
using Minesharp.World;

namespace Minesharp;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Color _backgroundColor = new(0, 102, 200); 

    public static Texture2D _DIRT_TEX;

    private Camera _camera;

    private Chunk _chunk0;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
        Window.AllowAltF4 = false;
    }

    protected override void Initialize()
    {
        GraphicsManager.SetScreenBounds(_graphics);

        _camera = new Camera(Vector3.Zero, 30f, 75f, 40f, GraphicsDevice);
        _chunk0 = new(GraphicsDevice);
        

        base.Initialize();

        _chunk0.GenerateChunk();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Global.DebugFont = Content.Load<SpriteFont>("DebuggingTools/minecraftRegular");

        _DIRT_TEX = Content.Load<Texture2D>("Textures/dirtTex");
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Global.YieldDeltaTime(gameTime);

        _chunk0.Update();
        _camera.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(_backgroundColor);


        GraphicsManager.YieldViewAndProjectionMatrices(_camera);

        // Save the current graphics device state
        var graphicsState = new GraphicsDeviceState();

        _chunk0.Draw();

        graphicsState.Save(GraphicsDevice);
        GraphicsDevice.DepthStencilState = DepthStencilState.None;

        _spriteBatch.Begin(SpriteSortMode.Immediate);

        string debugText = $"Camera Position: ({(int)_camera.Position.X}, {(int)_camera.Position.Y}, {(int)_camera.Position.Z})";
        _spriteBatch.DrawString(Global.DebugFont, debugText, Vector2.Zero, Color.Black);

        string fpsText = $"FPS: {1 / Global.DeltaTime}";
        _spriteBatch.DrawString(Global.DebugFont, fpsText, new Vector2(0, 20), Color.Black);

        _spriteBatch.End();

        // Restore the graphics device state to continue 3D drawing
        GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        graphicsState.Restore(GraphicsDevice);
        
        base.Draw(gameTime);
    }

}
