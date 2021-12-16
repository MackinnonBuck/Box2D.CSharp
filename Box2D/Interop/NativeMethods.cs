using Box2D.Math;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Box2D.Interop;

[SuppressUnmanagedCodeSecurity]
internal static class NativeMethods
{
    private const string Dll = "libbox2d";
    private const CallingConvention Conv = CallingConvention.Cdecl;

    /*
     * b2World
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2World_new([In] ref Vec2 gravity);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2World_CreateBody(IntPtr obj, ref BodyDefInternal def);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_DestroyBody(IntPtr obj, IntPtr body);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_Step(IntPtr obj, float timeStep, int velocityIterations, int positionIterations);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_ClearForces(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2World_GetBodyList(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_delete(IntPtr obj);

    /*
     * b2Body
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_GetPosition(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Body_GetNext(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Body_GetUserData(IntPtr obj);

    /*
     * b2PolygonShape
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2PolygonShape_new();
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2PolygonShape_GetChildCount(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2PolygonShape_SetAsBox(IntPtr obj, float hx, float hy);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2PolygonShape_SetAsBox2(IntPtr obj, float hx, float hy, Vec2 center, float angle);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PolygonShape_ComputeAABB(IntPtr obj, out AABB aabb, Transform transform, int childIndex);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PolygonShape_ComputeMass(IntPtr obj, out MassData massData, float density);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern bool b2PolygonShape_RayCast(IntPtr obj, out RayCastOutput output, in RayCastInput input, Transform transform, int childIndex);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern bool b2PolygonShape_TestPoint(IntPtr obj, Transform transform, Vec2 p);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PolygonShape_delete(IntPtr obj);
}
