﻿using Box2D;
using ImGuiNET;
using Silk.NET.OpenGL;

namespace Testbed.Drawing;

internal class DebugDraw : Draw
{
    private const float CircleSegments = 16f;
    private const float CircleIncrement = 2f * MathF.PI / CircleSegments;
    private const float AxisScale = 0.4f;

    private static readonly Color _red = new(1f, 0f, 0f);
    private static readonly Color _green = new(0f, 1f, 0f);

    private readonly PointRenderer _points;
    private readonly LineRenderer _lines;
    private readonly TriangleRenderer _triangles;

    public bool ShowUI { get; set; } = true;

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

    public override void DrawPolygon(in Box2DArray<Vec2> vertices, Color color)
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

    public override void DrawSolidPolygon(in Box2DArray<Vec2> vertices, Color color)
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

    public override void DrawCircle(Vec2 center, float radius, Color color)
    {
        var sinInc = MathF.Sin(CircleIncrement);
        var cosInc = MathF.Cos(CircleIncrement);

        var r1 = new Vec2(1f, 0f);
        var v1 = center + radius * r1;

        for (var i = 0; i < CircleSegments; i++)
        {
            var r2 = new Vec2(
                cosInc * r1.X - sinInc * r1.Y,
                sinInc * r1.X + cosInc * r1.Y);
            var v2 = center + radius * r2;
            _lines.Vertex(v1, color);
            _lines.Vertex(v2, color);
            r1 = r2;
            v1 = v2;
        }
    }

    public override void DrawSolidCircle(Vec2 center, float radius, Vec2 axis, Color color)
    {
        var sinInc = MathF.Sin(CircleIncrement);
        var cosInc = MathF.Cos(CircleIncrement);

        var v0 = center;
        var r1 = new Vec2(cosInc, sinInc);
        var v1 = center + radius * r1;
        var fillColor = new Color(0.5f * color.R, 0.5f * color.G, 0.5f * color.B, 0.5f);

        for (var i = 0; i < CircleSegments; i++)
        {
            var r2 = new Vec2(
                cosInc * r1.X - sinInc * r1.Y,
                sinInc * r1.X + cosInc * r1.Y);
            var v2 = center + radius * r2;
            _triangles.Vertex(v0, fillColor);
            _triangles.Vertex(v1, fillColor);
            _triangles.Vertex(v2, fillColor);
            r1 = r2;
            v1 = v2;
        }

        r1.Set(1f, 0f);
        v1 = center + radius * r1;
        for (var i = 0; i < CircleSegments; i++)
        {
            var r2 = new Vec2(
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

    public override void DrawSegment(Vec2 p1, Vec2 p2, Color color)
    {
        _lines.Vertex(p1, color);
        _lines.Vertex(p2, color);
    }

    public override void DrawTransform(Transform xf)
    {
        var p1 = xf.P;

        _lines.Vertex(p1, _red);
        var p2 = p1 + AxisScale * xf.Q.XAxis;
        _lines.Vertex(p2, _red);

        _lines.Vertex(p1, _green);
        p2 = p1 + AxisScale * xf.Q.YAxis;
        _lines.Vertex(p2, _green);
    }

    public override void DrawPoint(Vec2 p, float size, Color color)
    {
        _points.Vertex(p, color, size);
    }

    public void DrawString(int x, int y, string s)
    {
        if (!ShowUI)
        {
            return;
        }

        ImGui.Begin("Overlay", ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoInputs | ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoScrollbar);
        ImGui.SetCursorPos(new(x, y));
        ImGui.TextColored(new(230f, 153f, 153f, 255f), s);
        ImGui.End();
    }

    protected override void Dispose(bool disposing)
    {
        _points.Dispose();
        _lines.Dispose();
        _triangles.Dispose();

        base.Dispose(disposing);
    }
}
