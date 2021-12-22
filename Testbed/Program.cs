using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Windowing;
using System.Drawing;
using System.Numerics;

namespace Testbed;

internal class Program
{
    private static IWindow _window = default!;
    private static ImGuiController _controller = default!;
    private static GL _gl = default!;
    private static IInputContext _inputContext = default!;

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

        InitializeInputCallbacks();
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
    }

    private static void OnWindowRender(double deltaTime)
    {
        _controller.Update((float)deltaTime);

        _gl.ClearColor(Color.FromArgb(255, (int)(.45f * 255), (int)(.55f * 255), (int)(.60f * 255)));
        _gl.Clear((uint)ClearBufferMask.ColorBufferBit);

        ImGuiNET.ImGui.ShowDemoWindow();

        _controller.Render();
    }

    private static void OnWindowClosing()
    {
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
