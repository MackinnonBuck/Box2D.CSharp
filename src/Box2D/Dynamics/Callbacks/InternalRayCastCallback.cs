using Box2D.Core;
using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security;

namespace Box2D.Dynamics.Callbacks;

using static Interop.NativeMethods;

internal sealed class InternalRayCastCallback : Box2DDisposableObject
{
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate float ReportFixtureUnmanagedDelegate(IntPtr fixture, [In] ref Vector2 point, [In] ref Vector2 normal, float fraction);

    private readonly ReportFixtureUnmanagedDelegate _reportFixture;

    private IRayCastCallback? _userCallback;

    public InternalRayCastCallback() : base(isUserOwned: true)
    {
        _reportFixture = ReportFixtureUnmanaged;

        var native = b2RayCastCallbackWrapper_new(
            Marshal.GetFunctionPointerForDelegate(_reportFixture));
        Initialize(native);
    }

    public void RayCast(IntPtr world, IRayCastCallback userCallback, ref Vector2 point1, ref Vector2 point2)
    {
        _userCallback = userCallback;
        b2World_RayCast(world, Native, ref point1, ref point2);
        _userCallback = null;
    }

    private float ReportFixtureUnmanaged(IntPtr fixture, ref Vector2 point, ref Vector2 normal, float fraction)
        => _userCallback!.ReportFixture(new(fixture), point, normal, fraction);

    protected override void Dispose(bool disposing)
        => b2RayCastCallbackWrapper_delete(Native);
}
