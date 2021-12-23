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
            Title = "Box2D TestBed",
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
        _test = _testEntries[_settings.testIndex].CreateTest(_debugDraw, _settings);
    }

    private static void OnWindowLoad()
    {
        _gl = _window.CreateOpenGL();
        _inputContext = _window.CreateInput();
        _controller = new ImGuiController(_gl, _window, _inputContext);
        _debugDraw = new(_gl, _camera);

        InitializeInputCallbacks();

        _settings.testIndex = Math.Clamp(_settings.testIndex, 0, _testEntries.Count - 1);
        _test = _testEntries[_settings.testIndex].CreateTest(_debugDraw, _settings);

        _gl.ClearColor(0.2f, 0.2f, 0.2f, 1f);

        _stopwatch.Start();
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
        _debugDraw.Flush();

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
                                _test = _testEntries[i].CreateTest(_debugDraw, _settings);
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
