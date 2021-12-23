using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Box2D;

using static NativeMethods;

public abstract class RayCastCallback : Box2DObject
{
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate float ReportFixtureUnmanagedDelegate(IntPtr fixture, [In] ref Vec2 point, [In] ref Vec2 normal, float fraction);

    private readonly ReportFixtureUnmanagedDelegate _reportFixture;

    protected RayCastCallback() : base(isUserOwned: true)
    {
        _reportFixture = ReportFixtureUnmanaged;

        var native = b2RayCastCallbackWrapper_new(
            Marshal.GetFunctionPointerForDelegate(_reportFixture));
        Initialize(native);
    }

    private float ReportFixtureUnmanaged(IntPtr fixture, ref Vec2 point, ref Vec2 normal, float fraction)
        => ReportFixture(Fixture.FromIntPtr.Get(fixture)!, point, normal, fraction);

    protected abstract float ReportFixture(Fixture fixture, Vec2 point, Vec2 normal, float fraction);

    protected override void Dispose(bool disposing)
    {
        b2RayCastCallbackWrapper_delete(Native);
    }
}
