using Box2D.Collision;
using Box2D.Dynamics;
using Box2D.Math;
using Box2D.Profiling;
using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security;

namespace Box2D.Interop;

[SuppressUnmanagedCodeSecurity]
internal static class NativeMethods
{
    private const string Dll = "box2dwrapper";

    public const CallingConvention Conv = CallingConvention.Cdecl;

    /*
     * b2World
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2World_new([In] ref Vector2 gravity);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_SetDestructionListener(IntPtr obj, IntPtr listener);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_SetContactListener(IntPtr obj, IntPtr listener);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_SetDebugDraw(IntPtr obj, IntPtr debugDraw);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2World_CreateBody(IntPtr obj, IntPtr def, IntPtr userData);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2World_CreateBody2(IntPtr obj, BodyType type, [In] ref Vector2 position, float angle, IntPtr userData);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_DestroyBody(IntPtr obj, IntPtr body);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2World_CreateJoint(IntPtr obj, IntPtr def, IntPtr userData);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_DestroyJoint(IntPtr obj, IntPtr joint);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_Step(IntPtr obj, float timeStep, int velocityIterations, int positionIterations);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_ClearForces(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_DebugDraw(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_QueryAABB(IntPtr obj, IntPtr callback, [In] ref AABB aabb);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_RayCast(IntPtr obj, IntPtr callback, [In] ref Vector2 point1, [In] ref Vector2 point2);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2World_GetBodyList(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2World_GetJointList(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2World_GetContactList(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2World_GetAllowSleeping(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_SetAllowSleeping(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool flag);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2World_GetWarmStarting(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_SetWarmStarting(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool flag);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2World_GetContinuousPhysics(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_SetContinuousPhysics(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool flag);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2World_GetSubStepping(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_SetSubStepping(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool flag);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2World_GetProxyCount(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2World_GetBodyCount(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2World_GetJointCount(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2World_GetContactCount(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2World_GetTreeHeight(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2World_GetTreeBalance(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2World_GetTreeQuality(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_GetGravity(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_SetGravity(IntPtr obj, [In] ref Vector2 gravity);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_ShiftOrigin(IntPtr obj, [In] ref Vector2 newOrigin);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_GetProfile(IntPtr obj, out Profile value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_delete(IntPtr obj);

    /*
     * b2Contact
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Contact_GetManifold(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Contact_GetWorldManifold(IntPtr obj, IntPtr worldManifold);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2Contact_IsTouching(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2Contact_IsEnabled(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Contact_SetEnabled(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool flag);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Contact_GetNext(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Contact_GetFixtureA(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2Contact_GetChildIndexA(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Contact_GetFixtureB(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2Contact_GetChildIndexB(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2Contact_GetFriction(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Contact_SetFriction(IntPtr obj, float friction);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Contact_ResetFriction(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2Contact_GetRestitution(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Contact_SetRestitution(IntPtr obj, float restitution);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Contact_ResetRestitution(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2Contact_GetRestitutionThreshold(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Contact_SetRestitutionThreshold(IntPtr obj, float threshold);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Contact_ResetRestitutionThreshold(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2Contact_GetTangentSpeed(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Contact_SetTangentSpeed(IntPtr obj, float speed);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Contact_Evaluate(IntPtr obj, IntPtr manifold, [In] ref Transform xfA, [In] ref Transform xfB);

    /*
     * b2_collision_wrap top-level functions
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2GetPointStates_wrap(out PointState state1, out PointState state2, IntPtr manifold1, IntPtr manifold2);

    /*
     * b2Manifold
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Manifold_get_points(IntPtr obj, out int pointCount);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Manifold_get_localNormal(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Manifold_set_localNormal(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Manifold_get_localPoint(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Manifold_set_localPoint(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern ManifoldType b2Manifold_get_type(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Manifold_set_type(IntPtr obj, ManifoldType value);

    /*
     * b2WorldManifold
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2WorldManifold_new(out IntPtr points, out IntPtr separations);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WorldManifold_Initialize(IntPtr obj, IntPtr manifold, [In] ref Transform xfA, float radiusA, [In] ref Transform xfB, float radiusB);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WorldManifold_get_normal(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WorldManifold_set_normal(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WorldManifold_delete(IntPtr obj);

    /*
     * b2DestructionListenerWrapper
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2DestructionListenerWrapper_new(IntPtr sayGoodbyeJoint, IntPtr sayGoodbyeFixture);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DestructionListenerWrapper_delete(IntPtr obj);

    /*
     * b2ContactImpulse
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2ContactImpulse_get_impulses(IntPtr obj, out IntPtr normalImpulses, out IntPtr tangentImpulses, out int count);

    /*
     * b2ContactListenerWrapper
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2ContactListenerWrapper_new(IntPtr beginContact, IntPtr endContact, IntPtr preSolve, IntPtr postSolve);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2ContactListenerWrapper_delete(IntPtr obj);

    /*
     * b2QueryCallbackWrapper
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2QueryCallbackWrapper_new(IntPtr reportFixture);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2QueryCallbackWrapper_delete(IntPtr obj);

    /*
     * b2RayCastCallbackWrapper
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2RayCastCallbackWrapper_new(IntPtr reportFixture);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RayCastCallbackWrapper_delete(IntPtr obj);

    /*
     * b2DrawWrapper
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2DrawWrapper_new(IntPtr drawPolygon, IntPtr drawSolidPolygon, IntPtr drawCircle, IntPtr drawSolidCircle, IntPtr drawSegment, IntPtr drawTransform, IntPtr drawPoint);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern uint b2DrawWrapper_GetFlags(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DrawWrapper_SetFlags(IntPtr obj, uint flags);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DrawWrapper_delete(IntPtr obj);

    /*
     * b2BodyDef
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2BodyDef_new();
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern BodyType b2BodyDef_get_type(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_set_type(IntPtr obj, BodyType value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_get_position(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_set_position(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2BodyDef_get_angle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_set_angle(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_get_linearVelocity(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_set_linearVelocity(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2BodyDef_get_angularVelocity(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_set_angularVelocity(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2BodyDef_get_linearDamping(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_set_linearDamping(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2BodyDef_get_angularDamping(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_set_angularDamping(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2BodyDef_get_allowSleep(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_set_allowSleep(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool value);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2BodyDef_get_awake(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_set_awake(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool value);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2BodyDef_get_fixedRotation(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_set_fixedRotation(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool value);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2BodyDef_get_bullet(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_set_bullet(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool value);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2BodyDef_get_enabled(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_set_enabled(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2BodyDef_get_userData(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_set_userData(IntPtr obj, IntPtr value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2BodyDef_get_gravityScale(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_set_gravityScale(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_reset(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2BodyDef_delete(IntPtr obj);

    /*
     * b2Body
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Body_CreateFixture(IntPtr obj, IntPtr def, IntPtr userData);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Body_CreateFixture2(IntPtr obj, IntPtr shape, float density, IntPtr userData);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_DestroyFixture(IntPtr obj, IntPtr fixture);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_GetTransform(IntPtr obj, out Transform transform);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_SetTransform(IntPtr obj, [In] ref Vector2 position, float angle);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_ApplyForce(IntPtr obj, [In] ref Vector2 force, [In] ref Vector2 point, bool wake);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_ApplyForceToCenter(IntPtr obj, [In] ref Vector2 force, bool wake);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_ApplyTorque(IntPtr obj, float torque, bool wake);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_ApplyLinearImpulse(IntPtr obj, [In] ref Vector2 impulse, [In] ref Vector2 point, bool wake);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_ApplyLinearImpulseToCenter(IntPtr obj, [In] ref Vector2 impulse, bool wake);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_ApplyAngularImpulse(IntPtr obj, float impulse, bool wake);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_GetWorldPoint(IntPtr obj, [In] ref Vector2 localPoint, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_GetWorldVector(IntPtr obj, [In] ref Vector2 localVector, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_GetLocalPoint(IntPtr obj, [In] ref Vector2 worldPoint, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_GetLocalVector(IntPtr obj, [In] ref Vector2 worldVector, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_GetLinearVelocityFromWorldPoint(IntPtr obj, [In] ref Vector2 worldPoint, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_GetLinearVelocityFromLocalPoint(IntPtr obj, [In] ref Vector2 localPoint, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_GetPosition(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_SetPosition(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2Body_GetAngle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_SetAngle(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_GetWorldCenter(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_GetLocalCenter(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2Body_GetGravityScale(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_SetGravityScale(IntPtr obj, float scale);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_GetLinearVelocity(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_SetLinearVelocity(IntPtr obj, [In] ref Vector2 v);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2Body_GetAngularVelocity(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_SetAngularVelocity(IntPtr obj, float omega);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2Body_GetMass(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2Body_GetInertia(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern BodyType b2Body_GetType(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_SetType(IntPtr obj, BodyType type);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2Body_IsAwake(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_SetAwake(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool flag);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Body_GetFixtureList(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Body_GetJointList(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Body_GetNext(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Body_GetUserData(IntPtr obj);

    /*
     * b2FixtureDef
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2FixtureDef_new();
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2FixtureDef_get_shape(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FixtureDef_set_shape(IntPtr obj, IntPtr value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2FixtureDef_get_userData(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FixtureDef_set_userData(IntPtr obj, IntPtr value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2FixtureDef_get_friction(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FixtureDef_set_friction(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2FixtureDef_get_restitution(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FixtureDef_set_restitution(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2FixtureDef_get_restitutionThreshold(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FixtureDef_set_restitutionThreshold(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2FixtureDef_get_density(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FixtureDef_set_density(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2FixtureDef_get_isSensor(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FixtureDef_set_isSensor(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FixtureDef_get_filter(IntPtr obj, out Filter filter);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FixtureDef_set_filter(IntPtr obj, [In] ref Filter filter);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FixtureDef_reset(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FixtureDef_delete(IntPtr obj);

    /*
     * b2Fixture
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern ShapeType b2Fixture_GetType(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Fixture_GetShape(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2Fixture_IsSensor(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Fixture_SetSensor(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool sensor);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Fixture_GetBody(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Fixture_GetNext(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Fixture_GetUserData(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2Fixture_TestPoint(IntPtr obj, [In] ref Vector2 p);

    /*
     * b2Shape
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2Shape_GetChildCount(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2Shape_TestPoint(IntPtr obj, [In] ref Transform transform, [In] ref Vector2 p);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2Shape_RayCast(IntPtr obj, out RayCastOutput output, in RayCastInput input, [In] ref Transform transform, int childIndex);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Shape_ComputeAABB(IntPtr obj, out AABB aabb, [In] ref Transform transform, int childIndex);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Shape_ComputeMass(IntPtr obj, out MassData massData, float density);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2Shape_get_m_radius(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Shape_set_m_radius(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Shape_delete(IntPtr obj);

    /*
     * b2CircleShape
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2CircleShape_new();
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2CircleShape_get_m_p(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2CircleShape_set_m_p(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2CircleShape_reset(IntPtr obj);

    /*
     * b2EdgeShape
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2EdgeShape_new();
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2EdgeShape_SetOneSided(IntPtr obj, [In] ref Vector2 v0, [In] ref Vector2 v1, [In] ref Vector2 v2, [In] ref Vector2 v3);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2EdgeShape_SetTwoSided(IntPtr obj, [In] ref Vector2 v1, [In] ref Vector2 v2);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2EdgeShape_get_m_vertex0(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2EdgeShape_get_m_vertex1(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2EdgeShape_get_m_vertex2(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2EdgeShape_get_m_vertex3(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2EdgeShape_get_m_oneSided(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2EdgeShape_reset(IntPtr obj);

    /*
     * b2PolygonShape
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2PolygonShape_new();
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2PolygonShape_Set(IntPtr obj, [In] ref Vector2 points, int count);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2PolygonShape_SetAsBox(IntPtr obj, float hx, float hy);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2PolygonShape_SetAsBox2(IntPtr obj, float hx, float hy, [In] ref Vector2 center, float angle);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PolygonShape_get_m_centroid(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PolygonShape_reset(IntPtr obj);

    /*
     * b2_joint_wrap top-level functions
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2LinearStiffness_wrap(out float stiffness, out float damping, float frequencyHertz, float dampingRatio, IntPtr bodyA, IntPtr bodyB);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2AngularStiffness_wrap(out float stiffness, out float damping, float frequencyHertz, float dampingRatio, IntPtr bodyA, IntPtr bodyB);

    /*
     * b2JointDef
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern JointType b2JointDef_get_type(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2JointDef_set_type(IntPtr obj, JointType value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2JointDef_get_userData(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2JointDef_set_userData(IntPtr obj, IntPtr value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2JointDef_get_bodyA(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2JointDef_set_bodyA(IntPtr obj, IntPtr value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2JointDef_get_bodyB(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2JointDef_set_bodyB(IntPtr obj, IntPtr value);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2JointDef_get_collideConnected(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2JointDef_set_collideConnected(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool value);

    /*
     * b2Joint
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Joint_GetBodyA(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Joint_GetBodyB(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Joint_GetAnchorA(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Joint_GetAnchorB(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Joint_GetReactionForce(IntPtr obj, float inv_dt, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2Joint_GetReactionTorque(IntPtr obj, float inv_dt);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Joint_GetNext(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Joint_GetUserData(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2Joint_IsEnabled(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2Joint_GetCollideConnected(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Joint_ShiftOrigin(IntPtr obj, [In] ref Vector2 newOrigin);

    /*
     * b2JointEdge
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2JointEdge_get_other(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2JointEdge_set_other(IntPtr obj, IntPtr value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2JointEdge_get_joint(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2JointEdge_set_joint(IntPtr obj, IntPtr value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2JointEdge_get_prev(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2JointEdge_set_prev(IntPtr obj, IntPtr value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2JointEdge_get_next(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2JointEdge_set_next(IntPtr obj, IntPtr value);

    /*
     * b2DistanceJointDef
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2DistanceJointDef_new();
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_Initialize(IntPtr obj, IntPtr bodyA, IntPtr bodyB, Vector2 anchorA, Vector2 anchorB);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_get_localAnchorA(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_set_localAnchorA(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_get_localAnchorB(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_set_localAnchorB(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2DistanceJointDef_get_length(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_set_length(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2DistanceJointDef_get_minLength(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_set_minLength(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2DistanceJointDef_get_maxLength(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_set_maxLength(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2DistanceJointDef_get_stiffness(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_set_stiffness(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2DistanceJointDef_get_damping(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_set_damping(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_reset(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_delete(IntPtr obj);

    /*
     * b2DistanceJoint
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJoint_GetLocalAnchorA(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJoint_GetLocalAnchorB(IntPtr obj, out Vector2 value);
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

    /*
     * b2PrismaticJointDef
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2PrismaticJointDef_new();
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_Initialize(IntPtr obj, IntPtr bodyA, IntPtr bodyB, [In] ref Vector2 anchor, [In] ref Vector2 axis);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_get_localAnchorA(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_localAnchorA(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_get_localAnchorB(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_localAnchorB(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_get_localAxisA(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_localAxisA(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJointDef_get_referenceAngle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_referenceAngle(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2PrismaticJointDef_get_enableLimit(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_enableLimit(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJointDef_get_lowerTranslation(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_lowerTranslation(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJointDef_get_upperTranslation(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_upperTranslation(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2PrismaticJointDef_get_enableMotor(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_enableMotor(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJointDef_get_maxMotorForce(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_maxMotorForce(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJointDef_get_motorSpeed(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_motorSpeed(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_reset(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_delete(IntPtr obj);

    /*
     * b2PrismaticJoint
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJoint_GetLocalAnchorA(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJoint_GetLocalAnchorB(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJoint_GetLocalAxisA(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJoint_GetReferenceAngle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJoint_GetJointTranslation(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJoint_GetJointSpeed(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2PrismaticJoint_IsLimitEnabled(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJoint_EnableLimit(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool flag);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJoint_GetLowerLimit(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJoint_GetUpperLimit(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJoint_SetLimits(IntPtr obj, float lower, float upper);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2PrismaticJoint_IsMotorEnabled(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJoint_EnableMotor(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool flag);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJoint_GetMotorSpeed(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJoint_SetMotorSpeed(IntPtr obj, float speed);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJoint_GetMaxMotorForce(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJoint_SetMaxMotorForce(IntPtr obj, float force);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJoint_GetMotorForce(IntPtr obj, float inv_dt);

    /*
     * b2RevoluteJointDef
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2RevoluteJointDef_new();
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_Initialize(IntPtr obj, IntPtr bodyA, IntPtr bodyB, [In] ref Vector2 anchor);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_get_localAnchorA(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_localAnchorA(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_get_localAnchorB(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_localAnchorB(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJointDef_get_referenceAngle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_referenceAngle(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2RevoluteJointDef_get_enableLimit(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_enableLimit(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJointDef_get_lowerAngle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_lowerAngle(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJointDef_get_upperAngle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_upperAngle(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2RevoluteJointDef_get_enableMotor(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_enableMotor(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJointDef_get_motorSpeed(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_motorSpeed(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJointDef_get_maxMotorTorque(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_maxMotorTorque(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_reset(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_delete(IntPtr obj);

    /*
     * b2RevoluteJoint
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJoint_GetLocalAnchorA(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJoint_GetLocalAnchorB(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJoint_GetReferenceAngle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJoint_GetJointAngle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJoint_GetJointSpeed(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2RevoluteJoint_IsLimitEnabled(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJoint_EnableLimit(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool flag);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJoint_GetLowerLimit(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJoint_GetUpperLimit(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJoint_SetLimits(IntPtr obj, float lower, float upper);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2RevoluteJoint_IsMotorEnabled(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJoint_EnableMotor(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool flag);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJoint_GetMotorSpeed(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJoint_SetMotorSpeed(IntPtr obj, float speed);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJoint_GetMaxMotorTorque(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJoint_SetMaxMotorTorque(IntPtr obj, float torque);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJoint_GetMotorTorque(IntPtr obj, float inv_dt);

    /*
     * b2MouseJointDef
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2MouseJointDef_new();
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2MouseJointDef_get_target(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2MouseJointDef_set_target(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2MouseJointDef_get_maxForce(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2MouseJointDef_set_maxForce(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2MouseJointDef_get_stiffness(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2MouseJointDef_set_stiffness(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2MouseJointDef_get_damping(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2MouseJointDef_set_damping(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2MouseJointDef_reset(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2MouseJointDef_delete(IntPtr obj);

    /*
     * b2MouseJoint
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2MouseJoint_GetTarget(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2MouseJoint_SetTarget(IntPtr obj, [In] ref Vector2 target);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2MouseJoint_GetMaxForce(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2MouseJoint_SetMaxForce(IntPtr obj, float force);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2MouseJoint_GetStiffness(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2MouseJoint_SetStiffness(IntPtr obj, float stiffness);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2MouseJoint_GetDamping(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2MouseJoint_SetDamping(IntPtr obj, float damping);

    /*
     * b2WheelJointDef
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2WheelJointDef_new();
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJointDef_Initialize(IntPtr obj, IntPtr bodyA, IntPtr bodyB, [In] ref Vector2 anchor, [In] ref Vector2 axis);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJointDef_get_localAnchorA(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJointDef_set_localAnchorA(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJointDef_get_localAnchorB(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJointDef_set_localAnchorB(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJointDef_get_localAxisA(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJointDef_set_localAxisA(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2WheelJointDef_get_enableLimit(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJointDef_set_enableLimit(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2WheelJointDef_get_lowerTranslation(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJointDef_set_lowerTranslation(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2WheelJointDef_get_upperTranslation(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJointDef_set_upperTranslation(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2WheelJointDef_get_enableMotor(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJointDef_set_enableMotor(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2WheelJointDef_get_maxMotorTorque(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJointDef_set_maxMotorTorque(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2WheelJointDef_get_motorSpeed(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJointDef_set_motorSpeed(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2WheelJointDef_get_stiffness(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJointDef_set_stiffness(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2WheelJointDef_get_damping(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJointDef_set_damping(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJointDef_reset(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJointDef_delete(IntPtr obj);

    /*
     * b2WheelJoint
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJoint_GetLocalAnchorA(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJoint_GetLocalAnchorB(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJoint_GetLocalAxisA(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2WheelJoint_GetJointTranslation(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2WheelJoint_GetJointLinearSpeed(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2WheelJoint_GetJointAngle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2WheelJoint_GetJointAngularSpeed(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2WheelJoint_IsLimitEnabled(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJoint_EnableLimit(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool flag);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2WheelJoint_GetLowerLimit(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2WheelJoint_GetUpperLimit(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJoint_SetLimits(IntPtr obj, float lower, float upper);
    [DllImport(Dll, CallingConvention = Conv)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool b2WheelJoint_IsMotorEnabled(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJoint_EnableMotor(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool flag);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2WheelJoint_GetMotorSpeed(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJoint_SetMotorSpeed(IntPtr obj, float speed);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2WheelJoint_GetMaxMotorTorque(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJoint_SetMaxMotorTorque(IntPtr obj, float torque);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2WheelJoint_GetMotorTorque(IntPtr obj, float inv_dt);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2WheelJoint_GetStiffness(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJoint_SetStiffness(IntPtr obj, float stiffness);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2WheelJoint_GetDamping(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WheelJoint_SetDamping(IntPtr obj, float damping);

    /*
     * b2FrictionJointDef
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2FrictionJointDef_new();
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FrictionJointDef_Initialize(IntPtr obj, IntPtr bodyA, IntPtr bodyB, [In] ref Vector2 anchor);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FrictionJointDef_get_localAnchorA(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FrictionJointDef_set_localAnchorA(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FrictionJointDef_get_localAnchorB(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FrictionJointDef_set_localAnchorB(IntPtr obj, [In] ref Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2FrictionJointDef_get_maxForce(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FrictionJointDef_set_maxForce(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2FrictionJointDef_get_maxTorque(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FrictionJointDef_set_maxTorque(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FrictionJointDef_reset(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FrictionJointDef_delete(IntPtr obj);

    /*
     * b2FrictionJoint
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FrictionJoint_GetLocalAnchorA(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FrictionJoint_GetLocalAnchorB(IntPtr obj, out Vector2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2FrictionJoint_GetMaxForce(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FrictionJoint_SetMaxForce(IntPtr obj, float force);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2FrictionJoint_GetMaxTorque(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2FrictionJoint_SetMaxTorque(IntPtr obj, float torque);
}
