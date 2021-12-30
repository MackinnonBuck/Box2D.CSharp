using ImGuiNET;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Windowing;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using Testbed.Drawing;
using B2 = Box2D;

namespace Testbed;

internal class Program
{
    private static readonly TimeSpan _targetFrameTime = TimeSpan.FromSeconds(1.0 / 60.0);

    private static readonly Camera _camera = new();
    private static readonly List<TestEntry> _testEntries = new();
    private static readonly Stopwatch _stopwatch = new();

    private static IWindow _window = default!;
    private static ImGuiController _controller = default!;
    private static GL _gl = default!;
    private static IInputContext _inputContext = default!;
    private static DebugDraw _debugDraw = default!;
    private static Settings _settings = default!;
    private static Test _test = default!;

    private static IMouse _mouse = default!;
    private static IKeyboard _keyboard = default!;
    private static bool _rightMouseDown;
    private static Vector2 _clickPointWorldSpace;

    private static TimeSpan _t1 = TimeSpan.Zero;
    private static TimeSpan _frameTime = TimeSpan.Zero;
    private static TimeSpan _sleepAdjust = TimeSpan.Zero;

    private static void Main()
    {
        _settings = Settings.Load();

        PopulateTestEntries();

        _camera.Width = _settings.windowWidth;
        _camera.Height = _settings.windowHeight;

        var options = WindowOptions.Default with
        {
            Size = new Vector2D<int>(_camera.Width, _camera.Height),
            Title = "Box2D TestBed (C#)",
        };

        _window = Window.Create(options);
        _window.Load += OnWindowLoad;
        _window.FramebufferResize += OnWindowFramebufferResize;
        _window.Render += OnWindowRender;
        _window.Closing += OnWindowClosing;
        _window.Run();

        _settings.Save();
    }

    private static void PopulateTestEntries()
    {
        _testEntries.Clear();
        _testEntries.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsDefined(typeof(TestEntryAttribute)))
            .Select(t => new TestEntry(t))
            .OrderBy(t => t));
    }

    private static void RestartTest()
    {
        _test.Dispose();
        _test = _testEntries[_settings.testIndex].CreateTest(_debugDraw, _settings, _camera);
    }

    private static void OnWindowLoad()
    {
        _gl = _window.CreateOpenGL();
        _inputContext = _window.CreateInput();
        _controller = new ImGuiController(_gl, _window, _inputContext);
        _debugDraw = new(_gl, _camera);

        InitializeInputCallbacks();

        _settings.testIndex = Math.Clamp(_settings.testIndex, 0, _testEntries.Count - 1);
        _test = _testEntries[_settings.testIndex].CreateTest(_debugDraw, _settings, _camera);

        _gl.ClearColor(0.2f, 0.2f, 0.2f, 1f);

        _stopwatch.Start();
    }

    private static void InitializeInputCallbacks()
    {
        if (_inputContext.Mice.Count == 0)
        {
            throw new InvalidOperationException("Could not find a connected mouse.");
        }

        if (_inputContext.Keyboards.Count == 0)
        {
            throw new InvalidOperationException("Could not find a connected keyboard.");
        }

        _mouse = _inputContext.Mice[0];
        _mouse.MouseMove += OnMouseMove;
        _mouse.MouseDown += OnMouseDown;
        _mouse.MouseUp += OnMouseUp;
        _mouse.Scroll += OnMouseScroll;

        _keyboard = _inputContext.Keyboards[0];
        _keyboard.KeyDown += OnKeyDown;
        _keyboard.KeyUp += OnKeyUp;
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
        
        if (_debugDraw.showUI)
        {
            ImGui.SetNextWindowPos(new(0f, 0f));
            ImGui.SetNextWindowSize(new(_camera.Width, _camera.Height));
            ImGui.SetNextWindowBgAlpha(0f);
            ImGui.Begin("Overlay", ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoInputs | ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoScrollbar);
            ImGui.End();

            var entry = _testEntries[_settings.testIndex];
            _test.DrawTitle($"{entry.Category} : {entry.Name}");
        }

        _test.Step();

        UpdateUI();

        if (_debugDraw.showUI)
        {
            _debugDraw.DrawString(5, _camera.Height - 20, $"{_frameTime.TotalMilliseconds:.0} ms");
        }

        _controller.Render();

        var t2 = _stopwatch.Elapsed;
        var timeUsed = t2 - _t1;
        var sleepTime = _targetFrameTime - timeUsed + _sleepAdjust;

        if (sleepTime.TotalMilliseconds > 0)
        {
            Thread.Sleep(sleepTime);
        }

        var t3 = _stopwatch.Elapsed;
        _frameTime = t3 - _t1;
        _sleepAdjust = 0.9 * _sleepAdjust + 0.1 * (_targetFrameTime - _frameTime);
        _t1 = t3;
    }

    private static void UpdateUI()
    {
        if (!_debugDraw.showUI)
        {
            return;
        }

        var menuWidth = 180f;

        ImGui.SetNextWindowPos(new(_camera.Width - menuWidth - 10, 10));
        ImGui.SetNextWindowSize(new(menuWidth, _camera.Height - 20));

        ImGui.Begin("Tools", ref _debugDraw.showUI, ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse);

        if (ImGui.BeginTabBar("ControlTabs", ImGuiTabBarFlags.None))
        {
            if (ImGui.BeginTabItem("Controls"))
            {
                ImGui.SliderInt("Vel Iters", ref _settings.velocityIterations, 0, 50);
                ImGui.SliderInt("Pos Iters", ref _settings.positionIterations, 0, 50);
                ImGui.SliderFloat("Hertz", ref _settings.hertz, 5.0f, 120.0f, "%.0f hz");

                ImGui.Separator();

                ImGui.Checkbox("Sleep", ref _settings.enableSleep);
                ImGui.Checkbox("Warm Starting", ref _settings.enableWarmStarting);
                ImGui.Checkbox("Time of Impact", ref _settings.enableContinuous);
                ImGui.Checkbox("Sub-Stepping", ref _settings.enableSubStepping);

                ImGui.Separator();

                ImGui.Checkbox("Shapes", ref _settings.drawShapes);
                ImGui.Checkbox("Joints", ref _settings.drawJoints);
                ImGui.Checkbox("AABBs", ref _settings.drawAABBs);
                ImGui.Checkbox("Contact Points", ref _settings.drawContactPoints);
                ImGui.Checkbox("Contact Normals", ref _settings.drawContactNormals);
                ImGui.Checkbox("Contact Impulses", ref _settings.drawContactImpulse);
                ImGui.Checkbox("Friction Impulses", ref _settings.drawFrictionImpulse);
                ImGui.Checkbox("Center of Masses", ref _settings.drawCOMs);
                ImGui.Checkbox("Statistics", ref _settings.drawStats);
                ImGui.Checkbox("Profile", ref _settings.drawProfile);

                var buttonSz = new Vector2(-1, 0);

                if (ImGui.Button("Pause (P)", buttonSz))
                {
                    _settings.pause = !_settings.pause;
                }

                if (ImGui.Button("Single Step (O)", buttonSz))
                {
                    _settings.singleStep = !_settings.singleStep;
                }

                if (ImGui.Button("Restart (R)", buttonSz))
                {
                    RestartTest();
                }

                if (ImGui.Button("Quit", buttonSz))
                {
                    _window.Close();
                }

                ImGui.EndTabItem();
            }

            var leafNodeFlags =
                ImGuiTreeNodeFlags.OpenOnArrow |
                ImGuiTreeNodeFlags.OpenOnDoubleClick |
                ImGuiTreeNodeFlags.Leaf |
                ImGuiTreeNodeFlags.NoTreePushOnOpen;

            var nodeFlags =
                ImGuiTreeNodeFlags.OpenOnArrow |
                ImGuiTreeNodeFlags.OpenOnDoubleClick;

            if (ImGui.BeginTabItem("Tests"))
            {
                var category = _testEntries[0].Category;
                var i = 0;

                while (i < _testEntries.Count)
                {
                    var categorySelected = category == _testEntries[_settings.testIndex].Category;
                    var nodeSelectionFlags = categorySelected ? ImGuiTreeNodeFlags.Selected : ImGuiTreeNodeFlags.None;
                    var nodeOpen = ImGui.TreeNodeEx(category, nodeFlags | nodeSelectionFlags);

                    if (nodeOpen)
                    {
                        while (i < _testEntries.Count && category == _testEntries[i].Category)
                        {
                            var selectionFlags = ImGuiTreeNodeFlags.None;
                            if (_settings.testIndex == i)
                            {
                                selectionFlags = ImGuiTreeNodeFlags.Selected;
                            }
                            ImGui.TreeNodeEx((IntPtr)i, leafNodeFlags | selectionFlags, _testEntries[i].Name);
                            if (ImGui.IsItemClicked())
                            {
                                _test.Dispose();
                                _settings.testIndex = i;
                                _test = _testEntries[i].CreateTest(_debugDraw, _settings, _camera);
                                _settings.testIndex = i;
                            }
                            i++;
                        }
                        ImGui.TreePop();
                    }
                    else
                    {
                        while (i < _testEntries.Count && category == _testEntries[i].Category)
                        {
                            i++;
                        }
                    }

                    if (i < _testEntries.Count)
                    {
                        category = _testEntries[i].Category;
                    }
                }

                ImGui.EndTabItem();
            }

            ImGui.EndTabBar();
        }

        ImGui.End();

        _test.UpdateUI();
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
        var ps = new Vector2(position.X, position.Y);
        var pw = _camera.ConvertScreenToWorld(ps);
        _test.MouseMove(pw);

        if (_rightMouseDown)
        {
            var diff = pw - _clickPointWorldSpace;
            _camera.Center -= diff;
            _clickPointWorldSpace = _camera.ConvertScreenToWorld(ps);
        }
    }

    private static void OnMouseDown(IMouse mouse, MouseButton button)
    {
        if (button == MouseButton.Left)
        {
            var pw = _camera.ConvertScreenToWorld(new(mouse.Position.X, mouse.Position.Y));

            if (_keyboard.IsKeyPressed(Key.ShiftLeft) || _keyboard.IsKeyPressed(Key.ShiftRight))
            {
                _test.ShiftMouseDown(pw);
            }
            else
            {
                _test.MouseDown(pw);
            }
        }
        else if (button == MouseButton.Right)
        {
            _clickPointWorldSpace = _camera.ConvertScreenToWorld(new(mouse.Position.X, mouse.Position.Y));
            _rightMouseDown = true;
        }
    }

    private static void OnMouseUp(IMouse mouse, MouseButton button)
    {
        if (button == MouseButton.Left)
        {
            var pw = _camera.ConvertScreenToWorld(new(mouse.Position.X, mouse.Position.Y));

            _test.MouseUp(pw);
        }
        else if (button == MouseButton.Right)
        {
            _rightMouseDown = false;
        }
    }

    private static void OnMouseScroll(IMouse mouse, ScrollWheel scrollWheel)
    {
        if (ImGui.GetIO().WantCaptureMouse)
        {
            return;
        }

        if (scrollWheel.Y > 0)
        {
            _camera.Zoom /= 1.1f;
        }
        else
        {
            _camera.Zoom *= 1.1f;
        }
    }

    private static void OnKeyDown(IKeyboard keyboard, Key key, int code)
    {
        if (ImGui.GetIO().WantCaptureKeyboard)
        {
            return;
        }

        var controlPressed = keyboard.IsKeyPressed(Key.ControlLeft) || keyboard.IsKeyPressed(Key.ControlRight);

        switch (key)
        {
            case Key.Escape:
                _window.Close();
                break;

            case Key.Left:
                if (controlPressed)
                {
                    var newOrigin = new Vector2(2f, 0f);
                    _test.ShiftOrigin(newOrigin);
                }
                else
                {
                    _camera.Center += new Vector2(-0.5f, 0f);
                }
                break;

            case Key.Right:
                if (controlPressed)
                {
                    var newOrigin = new Vector2(-2f, 0f);
                    _test.ShiftOrigin(newOrigin);
                }
                else
                {
                    _camera.Center += new Vector2(0.5f, 0f);
                }
                break;

            case Key.Down:
                if (controlPressed)
                {
                    var newOrigin = new Vector2(0f, 2f);
                    _test.ShiftOrigin(newOrigin);
                }
                else
                {
                    _camera.Center += new Vector2(0f, -0.5f);
                }
                break;

            case Key.Up:
                if (controlPressed)
                {
                    var newOrigin = new Vector2(0f, -2f);
                    _test.ShiftOrigin(newOrigin);
                }
                else
                {
                    _camera.Center += new Vector2(0f, 0.5f);
                }
                break;

            case Key.Home:
                _camera.Zoom = 1f;
                _camera.Center = new(0f, 20f);
                break;

            case Key.Z:
                _camera.Zoom = Math.Min(1.1f * _camera.Zoom, 20f);
                break;

            case Key.X:
                _camera.Zoom = Math.Max(0.9f * _camera.Zoom, 0.02f);
                break;

            case Key.R:
                RestartTest();
                break;

            case Key.Space:
                _test.LaunchBomb();
                break;

            case Key.O:
                _settings.singleStep = true;
                break;

            case Key.P:
                _settings.pause = !_settings.pause;
                break;

            case Key.LeftBracket:
                _settings.testIndex--;
                if (_settings.testIndex < 0)
                {
                    _settings.testIndex = _testEntries.Count - 1;
                }
                RestartTest();
                break;

            case Key.RightBracket:
                _settings.testIndex++;
                if (_settings.testIndex >= _testEntries.Count)
                {
                    _settings.testIndex = 0;
                }
                RestartTest();
                break;

            case Key.Tab:
                _debugDraw.showUI = !_debugDraw.showUI;
                break;

            default:
                _test.Keyboard(key);
                break;
        }
    }

    private static void OnKeyUp(IKeyboard keyboard, Key key, int code)
    {
        _test.KeyboardUp(key);
    }
}
