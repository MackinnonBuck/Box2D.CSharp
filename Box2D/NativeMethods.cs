using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Box2D;

[SuppressUnmanagedCodeSecurity]
internal static class NativeMethods
{
    /*
     * b2World
     */
    [DllImport(Interop.Dll, CallingConvention = Interop.CallConv)]
    public static extern IntPtr b2World_new([In] ref Vec2 gravity);
    [DllImport(Interop.Dll, CallingConvention = Interop.CallConv)]
    public static extern IntPtr b2World_CreateBody(IntPtr obj, ref BodyDefInternal def);
    [DllImport(Interop.Dll, CallingConvention = Interop.CallConv)]
    public static extern void b2World_DestroyBody(IntPtr obj, IntPtr body);
    [DllImport(Interop.Dll, CallingConvention = Interop.CallConv)]
    public static extern IntPtr b2World_GetBodyList(IntPtr obj);
    [DllImport(Interop.Dll, CallingConvention = Interop.CallConv)]
    public static extern void b2World_delete(IntPtr obj);

    /*
     * b2Body
     */
    [DllImport(Interop.Dll, CallingConvention = Interop.CallConv)]
    public static extern void b2Body_GetPosition(IntPtr obj, out Vec2 value);
    [DllImport(Interop.Dll, CallingConvention = Interop.CallConv)]
    public static extern IntPtr b2Body_GetNext(IntPtr obj);
    [DllImport(Interop.Dll, CallingConvention = Interop.CallConv)]
    public static extern IntPtr b2Body_GetUserData(IntPtr obj);
}
