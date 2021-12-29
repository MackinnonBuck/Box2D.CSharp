using Box2D.Collections;
using Box2D.Drawing;
using Box2D.Math;
using ImGuiNET;
using Silk.NET.OpenGL;
using System.Numerics;

namespace Testbed.Drawing;

internal class DebugDraw : IDraw, IDisposable
{
    private const float CircleSegments = 16f;
    private const float CircleIncrement = 2f * MathF.PI / CircleSegments;
    private const float AxisScale = 0.4f;

    private static readonly Color _red = new(1f, 0f, 0f);
    private static readonly Color _green = new(0f, 1f, 0f);
    private static readonly Vector4 _textColor = new Vector4(230f, 153f, 153f, 255f) / 255f;

    private readonly PointRenderer _points;
    private readonly LineRenderer _lines;
    private readonly TriangleRenderer _triangles;

    public bool showUI = true;

    public DebugDraw(GL gl, Camera camera)
    {
        _points = new(gl, camera);
        _lines = new(gl, camera);
        _triangles = new(gl, camera);
    }

    public void Flush()
    {
        _points.Flush();
        _lines.Flush();
        _triangles.Flush();
    }

    public void DrawPolygon(in ArrayRef<Vector2> vertices, Color color)
    {
        var p1 = vertices[^1];

        for (var i = 0; i < vertices.Length; i++)
        {
            var p2 = vertices[i];
            _lines.Vertex(p1, color);
            _lines.Vertex(p2, color);
            p1 = p2;
        }
    }

    public void DrawSolidPolygon(in ArrayRef<Vector2> vertices, Color color)
    {
        var fillColor = new Color(0.5f * color.R, 0.5f * color.G, 0.5f * color.B, 0.5f);

        for (var i = 1; i < vertices.Length - 1; i++)
        {
            _triangles.Vertex(vertices[0], fillColor);
            _triangles.Vertex(vertices[i], fillColor);
            _triangles.Vertex(vertices[i + 1], fillColor);
        }

        var p1 = vertices[^1];

        for (var i = 0; i < vertices.Length; i++)
        {
            var p2 = vertices[i];
            _lines.Vertex(p1, color);
            _lines.Vertex(p2, color);
            p1 = p2;
        }
    }

    public void DrawCircle(Vector2 center, float radius, Color color)
    {
        var sinInc = MathF.Sin(CircleIncrement);
        var cosInc = MathF.Cos(CircleIncrement);

        var r1 = new Vector2(1f, 0f);
        var v1 = center + radius * r1;

        for (var i = 0; i < CircleSegments; i++)
        {
            var r2 = new Vector2(
                cosInc * r1.X - sinInc * r1.Y,
                sinInc * r1.X + cosInc * r1.Y);
            var v2 = center + radius * r2;
            _lines.Vertex(v1, color);
            _lines.Vertex(v2, color);
            r1 = r2;
            v1 = v2;
        }
    }

    public void DrawSolidCircle(Vector2 center, float radius, Vector2 axis, Color color)
    {
        var sinInc = MathF.Sin(CircleIncrement);
        var cosInc = MathF.Cos(CircleIncrement);

        var v0 = center;
        var r1 = new Vector2(cosInc, sinInc);
        var v1 = center + radius * r1;
        var fillColor = new Color(0.5f * color.R, 0.5f * color.G, 0.5f * color.B, 0.5f);

        for (var i = 0; i < CircleSegments; i++)
        {
            var r2 = new Vector2(
                cosInc * r1.X - sinInc * r1.Y,
                sinInc * r1.X + cosInc * r1.Y);
            var v2 = center + radius * r2;
            _triangles.Vertex(v0, fillColor);
            _triangles.Vertex(v1, fillColor);
            _triangles.Vertex(v2, fillColor);
            r1 = r2;
            v1 = v2;
        }

        r1 = new(1f, 0f);
        v1 = center + radius * r1;
        for (var i = 0; i < CircleSegments; i++)
        {
            var r2 = new Vector2(
                cosInc * r1.X - sinInc * r1.Y,
                sinInc * r1.X + cosInc * r1.Y);
            var v2 = center + radius * r2;
            _lines.Vertex(v1, color);
            _lines.Vertex(v2, color);
            r1 = r2;
            v1 = v2;
        }

        var p = center + radius * axis;
        _lines.Vertex(center, color);
        _lines.Vertex(p, color);
    }

    public void DrawSegment(Vector2 p1, Vector2 p2, Color color)
    {
        _lines.Vertex(p1, color);
        _lines.Vertex(p2, color);
    }

    public void DrawTransform(Transform xf)
    {
        var p1 = xf.Position;

        _lines.Vertex(p1, _red);
        var p2 = p1 + AxisScale * xf.Rotation.XAxis;
        _lines.Vertex(p2, _red);

        _lines.Vertex(p1, _green);
        p2 = p1 + AxisScale * xf.Rotation.YAxis;
        _lines.Vertex(p2, _green);
    }

    public void DrawPoint(Vector2 p, float size, Color color)
    {
        _points.Vertex(p, color, size);
    }

    public void DrawString(int x, int y, string s)
    {
        if (!showUI)
        {
            return;
        }

        ImGui.Begin("Overlay", ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoInputs | ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoScrollbar);
        ImGui.SetCursorPos(new(x, y));
        ImGui.TextColored(_textColor, s);
        ImGui.End();
    }

    public void Dispose()
    {
        _points.Dispose();
        _lines.Dispose();
        _triangles.Dispose();
    }
}
