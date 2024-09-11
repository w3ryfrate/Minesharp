using Microsoft.Xna.Framework.Input;
using System;

namespace Minesharp;

public class Camera
{
    public Vector3 Position;
    public float Speed;
    public float FOV;
    public float Sensitivity;

    private Vector3 _up = Vector3.UnitY;
    private Vector3 _front = Vector3.UnitZ;
    private Vector3 _right = -Vector3.UnitX;

    private float _pitch;
    private float _yaw = -90f;

    private bool _firstFrame = true;

    private GraphicsDevice graphicsDevice;

    public Camera(Vector3 position, float speed, float fov, float sensitivity, GraphicsDevice graphicsDevice)
    {
        Position = position;
        Speed = speed;
        FOV = fov;
        Sensitivity = sensitivity;
        this.graphicsDevice = graphicsDevice;
    }

    public void Update()
    {
        InputHandler();
        UpdateAxes();
    }

    public Matrix GetViewMatrix()
    {
        return Matrix.CreateLookAt(Position, Position + _front, _up);
    }

    public Matrix GetProjectionMatrix()
    {
        return Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(FOV), graphicsDevice.Viewport.AspectRatio, 0.1f, 1000f);
    }

    private void InputHandler()
    {
        KeyboardState kState = Keyboard.GetState();
        MouseState mouse = Mouse.GetState();

        if (kState.IsKeyDown(Keys.W))
        {
            Position += _front * Speed * Global.DeltaTime;
        }
        if (kState.IsKeyDown(Keys.A))
        {
            Position -= _right * Speed * Global.DeltaTime;
        }
        if (kState.IsKeyDown(Keys.S))
        {
            Position -= _front * Speed * Global.DeltaTime;
        }
        if (kState.IsKeyDown(Keys.D))
        {
            Position += _right * Speed * Global.DeltaTime;
        }
        if (kState.IsKeyDown(Keys.Space))
        {
            Position.Y += Global.DeltaTime * Speed;
        }
        if (kState.IsKeyDown(Keys.LeftAlt))
        {
            Position.Y -= Global.DeltaTime * Speed;
        }

        int screenCenterX = graphicsDevice.Viewport.Width / 2;
        int screenCenterY = graphicsDevice.Viewport.Height / 2;

        if (_firstFrame)
        {
            Mouse.SetPosition(screenCenterX, screenCenterY);
            _firstFrame = false;
        }
        else
        {
            float deltaX = mouse.X - screenCenterX;
            float deltaY = mouse.Y - screenCenterY;

            _yaw += deltaX * Sensitivity * Global.DeltaTime;
            _pitch -= deltaY * Sensitivity * Global.DeltaTime;

            // Clamp pitch value
            _pitch = Math.Clamp(_pitch, -89f, 89f);

            // Reset mouse position to the center of the screen
            Mouse.SetPosition(screenCenterX, screenCenterY);
        }
    }

    private void UpdateAxes()
    {
        _front.X = MathF.Cos(MathHelper.ToRadians(_pitch)) * MathF.Cos(MathHelper.ToRadians(_yaw));
        _front.Y = MathF.Sin(MathHelper.ToRadians(_pitch));
        _front.Z = MathF.Cos(MathHelper.ToRadians(_pitch)) * MathF.Sin(MathHelper.ToRadians(_yaw));
        _front = Vector3.Normalize(_front);

        _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
        _up = Vector3.Normalize(Vector3.Cross(_right, _front));
    }
}
