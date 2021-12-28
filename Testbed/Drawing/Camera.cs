using System.Diagnostics;
using System.Numerics;

namespace Testbed.Drawing;

internal class Camera
{
    public Vector2 Center { get; set; } = new(0f, 20f);

    public float Zoom { get; set; } = 1f;

    public int Width { get; set; } = 1280;

    public int Height { get; set; } = 800;

    public Vector2 ConvertScreenToWorld(Vector2 screenPoint)
    {
        var w = (float)Width;
        var h = (float)Height;
        var u = screenPoint.X / w;
        var v = (h - screenPoint.Y) / h;

        var ratio = w / h;
        var extents = new Vector2(ratio * 25f, 25f);
        extents *= Zoom;

        var lower = Center - extents;
        var upper = Center + extents;

        return new(
            (1f - u) * lower.X + u * upper.X,
            (1f - v) * lower.Y + v * upper.Y);
    }

    public Vector2 ConvertWorldToScreen(Vector2 worldPoint)
    {
        var w = (float)Width;
        var h = (float)Height;

        var ratio = w / h;
        var extents = new Vector2(ratio * 25f, 25f);
        extents *= Zoom;

        var lower = Center - extents;
        var upper = Center + extents;

        var u = (worldPoint.X - lower.X) / (upper.X - lower.X);
        var v = (worldPoint.Y - lower.Y) / (upper.Y - lower.Y);

        return new(u * w, (1f - v) * h);
    }

    public void BuildProjectionMatrix(Span<float> m, float zBias)
    {
        Debug.Assert(m.Length >= 16);

        var w = (float)Width;
        var h = (float)Height;

        var ratio = w / h;
        var extents = new Vector2(ratio * 25f, 25f);
        extents *= Zoom;

        var lower = Center - extents;
        var upper = Center + extents;

        m[0] = 2.0f / (upper.X - lower.X);
        m[1] = 0.0f;
        m[2] = 0.0f;
        m[3] = 0.0f;

        m[4] = 0.0f;
        m[5] = 2.0f / (upper.Y - lower.Y);
        m[6] = 0.0f;
        m[7] = 0.0f;

        m[8] = 0.0f;
        m[9] = 0.0f;
        m[10] = 1.0f;
        m[11] = 0.0f;

        m[12] = -(upper.X + lower.X) / (upper.X - lower.X);
        m[13] = -(upper.Y + lower.Y) / (upper.Y - lower.Y);
        m[14] = zBias;
        m[15] = 1.0f;
    }
}
