using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Box2D;

using static NativeMethods;

public abstract class QueryCallback : Box2DObject
{
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    [return: MarshalAs(UnmanagedType.U1)]
    private delegate bool ReportFixtureUnmanagedDelegate(IntPtr fixture);

    private readonly ReportFixtureUnmanagedDelegate _reportFixture;

    protected QueryCallback() : base(isUserOwned: true)
    {
        _reportFixture = ReportFixtureUnmanaged;

        var native = b2QueryCallbackWrapper_new(
            Marshal.GetFunctionPointerForDelegate(_reportFixture));
        Initialize(native);
    }

    private bool ReportFixtureUnmanaged(IntPtr fixture)
        => ReportFixture(Fixture.FromIntPtr.Get(fixture)!);

    protected abstract bool ReportFixture(Fixture fixture);

    protected override void Dispose(bool disposing)
    {
        b2QueryCallbackWrapper_delete(Native);
    }
}
