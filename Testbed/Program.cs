using ImGuiNET;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Windowing;
using System.Numerics;
using Testbed.Drawing;

namespace Testbed;

internal class Program
{
    private static readonly Camera _camera = new();

    private static IWindow _window = default!;
    private static ImGuiController _controller = default!;
    private static GL _gl = default!;
    private static IInputContext _inputContext = default!;
    private static DebugDraw _debugDraw = default!;

    private static void Main()
    {
        var options = WindowOptions.Default with
        {
            Size = new Vector2D<int>(800, 600),
            Title = "Box2D TestBed",
        };

        _window = Window.Create(options);
        _window.Load += OnWindowLoad;
        _window.FramebufferResize += OnWindowFramebufferResize;
        _window.Render += OnWindowRender;
        _window.Closing += OnWindowClosing;
        _window.Run();
    }

    private static void OnWindowLoad()
    {
        _gl = _window.CreateOpenGL();
        _inputContext = _window.CreateInput();
        _controller = new ImGuiController(_gl, _window, _inputContext);
        _debugDraw = new(_gl, _camera);

        InitializeInputCallbacks();

        _gl.ClearColor(0.2f, 0.2f, 0.2f, 1f);
    }

    private static void InitializeInputCallbacks()
    {
        foreach (var mouse in _inputContext.Mice)
        {
            mouse.MouseMove += OnMouseMove;
            mouse.MouseDown += OnMouseDown;
            mouse.MouseUp += OnMouseUp;
            mouse.Scroll += OnMouseScroll;
        }

        foreach (var keyboard in _inputContext.Keyboards)
        {
            keyboard.KeyChar += OnKeyChar;
            keyboard.KeyDown += OnKeyDown;
            keyboard.KeyUp += OnKeyUp;
        }
    }

    private static void OnWindowFramebufferResize(Vector2D<int> size)
    {
        _gl.Viewport(size);
        _camera.Width = size.X;
        _camera.Height = size.Y;
    }

    private static void OnWindowRender(double deltaTime)
    {
        _controller.Update((float)deltaTime);

        _gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        ImGui.NewFrame();
        
        if (_debugDraw.ShowUI)
        {
            ImGui.SetNextWindowPos(new(0f, 0f));
            ImGui.SetNextWindowSize(new(_camera.Width, _camera.Height));
            ImGui.SetNextWindowBgAlpha(0f);
            ImGui.Begin("Overlay", ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoInputs | ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoScrollbar);
            ImGui.End();

            // TODO: Draw test title (you've already implemented, just create test instance and call).
        }

        //ImGuiNET.ImGui.ShowDemoWindow();

        _controller.Render();

        //_debugDraw.DrawPoint(new Box2D.Vec2(0f, 0f), 5f, new Box2D.Color(1f, 0f, 0f));
        //_debugDraw.DrawPoint(new Box2D.Vec2(15f, 10f), 10f, new Box2D.Color(1f, 1f, 0f));
        //_debugDraw.DrawSolidCircle(new(0f, 10f), 10f, new(1, 0), new(1f, 1f, 0f));
        _debugDraw.Flush();
    }

    private static void OnWindowClosing()
    {
        _debugDraw?.Dispose();
        _controller?.Dispose();
        _inputContext?.Dispose();
        _gl?.Dispose();
    }

    private static void OnMouseMove(IMouse mouse, Vector2 position)
    {
    }

    private static void OnMouseDown(IMouse mouse, MouseButton button)
    {
    }

    private static void OnMouseUp(IMouse mouse, MouseButton button)
    {
    }

    private static void OnMouseScroll(IMouse mouse, ScrollWheel scrollWheel)
    {
    }

    private static void OnKeyChar(IKeyboard keyboard, char c)
    {
    }

    private static void OnKeyDown(IKeyboard keyboard, Key key, int _)
    {
    }

    private static void OnKeyUp(IKeyboard keyboard, Key key, int _)
    {
    }
}
