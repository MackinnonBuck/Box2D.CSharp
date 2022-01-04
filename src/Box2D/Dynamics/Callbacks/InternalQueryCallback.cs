using Box2D.Collision;
using Box2D.Core;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Box2D.Dynamics.Callbacks;

using static Interop.NativeMethods;

internal sealed class InternalQueryCallback : Box2DDisposableObject
{
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    [return: MarshalAs(UnmanagedType.U1)]
    private delegate bool ReportFixtureUnmanagedDelegate(IntPtr fixture);

    private readonly ReportFixtureUnmanagedDelegate _reportFixture;

    private IQueryCallback? _userCallback;

    public InternalQueryCallback() : base(isUserOwned: true)
    {
        _reportFixture = ReportFixtureUnmanaged;

        var native = b2QueryCallbackWrapper_new(
            Marshal.GetFunctionPointerForDelegate(_reportFixture));
        Initialize(native);
    }

    public void QueryAABB(IntPtr world, IQueryCallback userCallback, ref AABB aabb)
    {
        _userCallback = userCallback;
        b2World_QueryAABB(world, Native, ref aabb);
        _userCallback = null;
    }

    private bool ReportFixtureUnmanaged(IntPtr fixture)
        => _userCallback!.ReportFixture(new(fixture));

    protected override void Dispose(bool disposing)
        => b2QueryCallbackWrapper_delete(Native);
}
