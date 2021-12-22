using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Box2D;

using static NativeMethods;

[Flags]
public enum DrawFlags
{
    ShapeBit        = 0x0001,
    JointBit        = 0x0002,
    AabbBit         = 0x0004,
    PairBit         = 0x0008,
    CenterOfMassBit = 0x0010,
}

public class Draw : Box2DObject
{
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void DrawPolygonUnmanagedDelegate(IntPtr vertices, int vertexCount, [In] ref Color color);
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void DrawSolidPolygonUnmanagedDelegate(IntPtr vertices, int vertexCount, [In] ref Color color);
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void DrawCircleUnmanagedDelegate([In] ref Vec2 center, float radius, [In] ref Color color);
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void DrawSolidCircleUnmanagedDelegate([In] ref Vec2 center, float radius, [In] ref Vec2 axis, [In] ref Color color);
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void DrawSegmentUnmanagedDelegate([In] ref Vec2 p1, [In] ref Vec2 p2, [In] ref Color color);
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void DrawTransformUnmanagedDelegate([In] ref Transform xf);
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void DrawPointUnmanagedDelegate([In] ref Vec2 p, float size, [In] ref Color color);

    public DrawFlags Flags
    {
        get
        {
            ThrowIfDisposed();
            return (DrawFlags)b2DrawWrapper_GetFlags(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2DrawWrapper_SetFlags(Native, (uint)value);
        }
    }

    private readonly DrawPolygonUnmanagedDelegate _drawPolygon;
    private readonly DrawSolidPolygonUnmanagedDelegate _drawSolidPolygon;
    private readonly DrawCircleUnmanagedDelegate _drawCircle;
    private readonly DrawSolidCircleUnmanagedDelegate _drawSolidCircle;
    private readonly DrawSegmentUnmanagedDelegate _drawSegment;
    private readonly DrawTransformUnmanagedDelegate _drawTransform;
    private readonly DrawPointUnmanagedDelegate _drawPoint;

    public Draw() : base(isUserOwned: true)
    {
        _drawPolygon = DrawPolygonUnmanaged;
        _drawSolidPolygon = DrawSolidPolygonUnmanaged;
        _drawCircle = DrawCircleUnmanaged;
        _drawSolidCircle = DrawSolidCircleUnmanaged;
        _drawSegment = DrawSegmentUnmanaged;
        _drawTransform = DrawTransformUnmanaged;
        _drawPoint = DrawPointUnmanaged;

        var native = b2DrawWrapper_new(
            Marshal.GetFunctionPointerForDelegate(_drawPolygon),
            Marshal.GetFunctionPointerForDelegate(_drawSolidPolygon),
            Marshal.GetFunctionPointerForDelegate(_drawCircle),
            Marshal.GetFunctionPointerForDelegate(_drawSolidCircle),
            Marshal.GetFunctionPointerForDelegate(_drawSegment),
            Marshal.GetFunctionPointerForDelegate(_drawTransform),
            Marshal.GetFunctionPointerForDelegate(_drawPoint));
        Initialize(native);
    }

    private void DrawPolygonUnmanaged(IntPtr vertices, int vertexCount, ref Color color)
        => DrawPolygon(new(vertices, vertexCount), color);

    private void DrawSolidPolygonUnmanaged(IntPtr vertices, int vertexCount, ref Color color)
        => DrawSolidPolygon(new(vertices, vertexCount), color);

    private void DrawCircleUnmanaged(ref Vec2 center, float radius, ref Color color)
        => DrawCircle(center, radius, color);

    private void DrawSolidCircleUnmanaged(ref Vec2 center, float radius, ref Vec2 axis, ref Color color)
        => DrawSolidCircle(center, radius, axis, color);

    private void DrawSegmentUnmanaged(ref Vec2 p1, ref Vec2 p2, ref Color color)
        => DrawSegment(p1, p2, color);

    private void DrawTransformUnmanaged(ref Transform xf)
        => DrawTransform(xf);

    private void DrawPointUnmanaged(ref Vec2 p, float size, ref Color color)
        => DrawPoint(p, size, color);

    protected virtual void DrawPolygon(in Box2DArray<Vec2> vertices, Color color)
    {
    }

    protected virtual void DrawSolidPolygon(in Box2DArray<Vec2> vertices, Color color)
    {
    }

    protected virtual void DrawCircle(Vec2 center, float radius, Color color)
    {
    }

    protected virtual void DrawSolidCircle(Vec2 center, float radius, Vec2 axis, Color color)
    {
    }

    protected virtual void DrawSegment(Vec2 p1, Vec2 p2, Color color)
    {
    }

    protected virtual void DrawTransform(Transform xf)
    {
    }

    protected virtual void DrawPoint(Vec2 p, float size, Color color)
    {
    }

    sealed private protected override void Dispose(bool disposing)
    {
        b2DrawWrapper_delete(Native);
    }
}
