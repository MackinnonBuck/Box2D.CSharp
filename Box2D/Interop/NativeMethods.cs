using Box2D.Math;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Box2D.Interop;

[SuppressUnmanagedCodeSecurity]
internal static class NativeMethods
{
    // TODO: Use different naming conventions for functions that get/set class member fields.
    // TODO: Lots of these can probably be made into "ref"s.

    private const string Dll = "libbox2d";
    private const CallingConvention Conv = CallingConvention.Cdecl;

    /*
     * b2World
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2World_new([In] ref Vec2 gravity);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2World_CreateBody(IntPtr obj, [In] ref BodyDefInternal def);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_DestroyBody(IntPtr obj, IntPtr body);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2World_CreateJoint(IntPtr obj, IntPtr def);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_DestroyJoint(IntPtr obj, IntPtr def);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_Step(IntPtr obj, float timeStep, int velocityIterations, int positionIterations);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_ClearForces(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2World_GetBodyList(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2World_GetJointList(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_delete(IntPtr obj);

    /*
     * b2Body
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Body_CreateFixture(IntPtr obj, [In] ref FixtureDefInternal def);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_GetPosition(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2Body_GetAngle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Body_GetFixtureList(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Body_GetJointList(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Body_GetNext(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Body_GetUserData(IntPtr obj);

    /*
     * b2Fixture
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Fixture_GetShape(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Fixture_GetNext(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Fixture_GetUserData(IntPtr obj);

    /*
     * b2Shape
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2Shape_GetChildCount(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern bool b2Shape_TestPoint(IntPtr obj, Transform transform, Vec2 p);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern bool b2Shape_RayCast(IntPtr obj, out RayCastOutput output, in RayCastInput input, Transform transform, int childIndex);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Shape_ComputeAABB(IntPtr obj, out AABB aabb, Transform transform, int childIndex);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Shape_ComputeMass(IntPtr obj, out MassData massData, float density);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2Shape_GetRadius(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Shape_SetRadius(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Shape_delete(IntPtr obj);

    /*
     * b2CircleShape
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2CircleShape_new();
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2CircleShape_GetP(IntPtr obj, out Vec2 value);

    /*
     * b2PolygonShape
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2PolygonShape_new();
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2PolygonShape_GetChildCount(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PolygonShape_GetCentroid(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2PolygonShape_Set(IntPtr obj, [In] ref Vec2 points, int count);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2PolygonShape_SetAsBox(IntPtr obj, float hx, float hy);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2PolygonShape_SetAsBox2(IntPtr obj, float hx, float hy, Vec2 center, float angle);

    /*
     * b2JointDef
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern JointType b2JointDef_GetType(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2JointDef_SetType(IntPtr obj, JointType value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2JointDef_GetUserData(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2JointDef_SetUserData(IntPtr obj, IntPtr value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2JointDef_GetBodyA(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2JointDef_SetBodyA(IntPtr obj, IntPtr value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2JointDef_GetBodyB(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2JointDef_SetBodyB(IntPtr obj, IntPtr value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern bool b2JointDef_GetCollideConnected(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2JointDef_SetCollideConnected(IntPtr obj, bool value);

    /*
     * b2Joint
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Joint_GetBodyA(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Joint_GetBodyB(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Joint_GetAnchorA(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Joint_GetAnchorB(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Joint_GetReactionForce(IntPtr obj, float inv_dt, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2Joint_GetReactionTorque(IntPtr obj, float inv_dt);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Joint_GetNext(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Joint_GetUserData(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern bool b2Joint_IsEnabled(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern bool b2Joint_GetCollideConnected(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Joint_ShiftOrigin(IntPtr obj, Vec2 newOrigin);

    /*
     * b2DistanceJointDef
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2DistanceJointDef_new();
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_Initialize(IntPtr obj, IntPtr bodyA, IntPtr bodyB, Vec2 anchorA, Vec2 anchorB);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_GetLocalAnchorA(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_GetLocalAnchorB(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2DistanceJointDef_GetLength(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_SetLength(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2DistanceJointDef_GetMinLength(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_SetMinLength(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2DistanceJointDef_GetMaxLength(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_SetMaxLength(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2DistanceJointDef_GetStiffness(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_SetStiffness(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2DistanceJointDef_GetDamping(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_SetDamping(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_delete(IntPtr obj);

    /*
     * b2DistanceJoint
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJoint_GetLocalAnchorA(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJoint_GetLocalAnchorB(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2DistanceJoint_GetLength(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJoint_SetLength(IntPtr obj, float length);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2DistanceJoint_GetMinLength(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJoint_SetMinLength(IntPtr obj, float minLength);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2DistanceJoint_GetMaxLength(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJoint_SetMaxLength(IntPtr obj, float maxLength);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2DistanceJoint_GetCurrentLength(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2DistanceJoint_GetStiffness(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJoint_SetStiffness(IntPtr obj, float stiffness);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2DistanceJoint_GetDamping(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJoint_SetDamping(IntPtr obj, float damping);
}
