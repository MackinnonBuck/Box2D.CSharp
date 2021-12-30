using Box2D.Core;
using Box2D.Math;
using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security;

namespace Box2D.Drawing;

using static Interop.NativeMethods;

internal sealed class InternalDraw : Box2DDisposableObject
{
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void DrawPolygonUnmanagedDelegate(IntPtr vertices, int vertexCount, [In] ref Color color);
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void DrawSolidPolygonUnmanagedDelegate(IntPtr vertices, int vertexCount, [In] ref Color color);
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void DrawCircleUnmanagedDelegate([In] ref Vector2 center, float radius, [In] ref Color color);
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void DrawSolidCircleUnmanagedDelegate([In] ref Vector2 center, float radius, [In] ref Vector2 axis, [In] ref Color color);
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void DrawSegmentUnmanagedDelegate([In] ref Vector2 p1, [In] ref Vector2 p2, [In] ref Color color);
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void DrawTransformUnmanagedDelegate([In] ref Transform xf);
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void DrawPointUnmanagedDelegate([In] ref Vector2 p, float size, [In] ref Color color);

    private readonly DrawPolygonUnmanagedDelegate _drawPolygon;
    private readonly DrawSolidPolygonUnmanagedDelegate _drawSolidPolygon;
    private readonly DrawCircleUnmanagedDelegate _drawCircle;
    private readonly DrawSolidCircleUnmanagedDelegate _drawSolidCircle;
    private readonly DrawSegmentUnmanagedDelegate _drawSegment;
    private readonly DrawTransformUnmanagedDelegate _drawTransform;
    private readonly DrawPointUnmanagedDelegate _drawPoint;

    private IDraw _userDraw;

    public InternalDraw(IDraw userDraw, DrawFlags flags) : base(isUserOwned: true)
    {
        _userDraw = userDraw;
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

        SetFlags(flags);
    }

    public void SetFlags(DrawFlags flags)
        => b2DrawWrapper_SetFlags(Native, (uint)flags);

    public void SetUserDraw(IDraw draw)
        => _userDraw = draw;

    private void DrawPolygonUnmanaged(IntPtr vertices, int vertexCount, ref Color color)
        => _userDraw.DrawPolygon(new(vertices, vertexCount), color);

    private void DrawSolidPolygonUnmanaged(IntPtr vertices, int vertexCount, ref Color color)
        => _userDraw.DrawSolidPolygon(new(vertices, vertexCount), color);

    private void DrawCircleUnmanaged(ref Vector2 center, float radius, ref Color color)
        => _userDraw.DrawCircle(center, radius, color);

    private void DrawSolidCircleUnmanaged(ref Vector2 center, float radius, ref Vector2 axis, ref Color color)
        => _userDraw.DrawSolidCircle(center, radius, axis, color);

    private void DrawSegmentUnmanaged(ref Vector2 p1, ref Vector2 p2, ref Color color)
        => _userDraw.DrawSegment(p1, p2, color);

    private void DrawTransformUnmanaged(ref Transform xf)
        => _userDraw.DrawTransform(xf);

    private void DrawPointUnmanaged(ref Vector2 p, float size, ref Color color)
        => _userDraw.DrawPoint(p, size, color);

    protected override void Dispose(bool disposing)
        => b2DrawWrapper_delete(Native);
}
