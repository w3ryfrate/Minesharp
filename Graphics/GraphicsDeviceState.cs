

namespace Minesharp.Graphics;

public struct GraphicsDeviceState
{
    private RasterizerState rasterizerState;
    private DepthStencilState depthStencilState;
    private BlendState blendState;
    private SamplerState samplerState;
    private Viewport viewport;
    private Rectangle scissorRectangle;

    public void Save(GraphicsDevice graphicsDevice)
    {
        rasterizerState = graphicsDevice.RasterizerState;
        depthStencilState = graphicsDevice.DepthStencilState;
        blendState = graphicsDevice.BlendState;
        samplerState = graphicsDevice.SamplerStates[0];
        viewport = graphicsDevice.Viewport;
        scissorRectangle = graphicsDevice.ScissorRectangle;
    }

    public readonly void Restore(GraphicsDevice graphicsDevice)
    {
        graphicsDevice.RasterizerState = rasterizerState;
        graphicsDevice.DepthStencilState = depthStencilState;
        graphicsDevice.BlendState = blendState;
        graphicsDevice.SamplerStates[0] = samplerState;
        graphicsDevice.Viewport = viewport;
        graphicsDevice.ScissorRectangle = scissorRectangle;
    }
}
