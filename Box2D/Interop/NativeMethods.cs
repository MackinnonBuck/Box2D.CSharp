﻿using Box2D.Math;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Box2D.Interop;

[SuppressUnmanagedCodeSecurity]
internal static class NativeMethods
{
    private const string Dll = "libbox2d";

    public const CallingConvention Conv = CallingConvention.Cdecl;

    /*
     * b2World
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2World_new([In] ref Vec2 gravity);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2World_SetContactListener(IntPtr obj, IntPtr listener);
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
    public static extern IntPtr b2World_GetContactList(IntPtr obj);
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
    public static extern bool b2Contact_IsTouching(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern bool b2Contact_IsEnabled(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Contact_SetEnabled(IntPtr obj, bool flag);
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
     * b2Manifold
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Manifold_get_points(IntPtr obj, out int pointCount);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Manifold_get_localNormal(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Manifold_set_localNormal(IntPtr obj, [In] ref Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Manifold_get_localPoint(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Manifold_set_localPoint(IntPtr obj, [In] ref Vec2 value);
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
    public static extern void b2WorldManifold_get_normal(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WorldManifold_set_normal(IntPtr obj, [In] ref Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2WorldManifold_delete(IntPtr obj);

    /*
     * b2ContactListenerWrapper
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2ContactListenerWrapper_new(IntPtr beginContact, IntPtr endContact, IntPtr preSolve, IntPtr postSolve);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2ContactListenerWrapper_delete(IntPtr obj);

    /*
     * b2Body
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2Body_CreateFixture(IntPtr obj, [In] ref FixtureDefInternal def);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_GetTransform(IntPtr obj, out Transform transform);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2Body_SetTransform(IntPtr obj, [In] ref Vec2 position, float angle);
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
    public static extern bool b2Shape_TestPoint(IntPtr obj, [In] ref Transform transform, [In] ref Vec2 p);
    [DllImport(Dll, CallingConvention = Conv)]
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
    public static extern void b2CircleShape_get_m_p(IntPtr obj, out Vec2 value);

    /*
     * b2PolygonShape
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2PolygonShape_new();
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2PolygonShape_Set(IntPtr obj, [In] ref Vec2 points, int count);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2PolygonShape_SetAsBox(IntPtr obj, float hx, float hy);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern int b2PolygonShape_SetAsBox2(IntPtr obj, float hx, float hy, [In] ref Vec2 center, float angle);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PolygonShape_get_m_centroid(IntPtr obj, out Vec2 value);

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
    public static extern bool b2JointDef_get_collideConnected(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2JointDef_set_collideConnected(IntPtr obj, bool value);

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
    public static extern void b2Joint_ShiftOrigin(IntPtr obj, [In] ref Vec2 newOrigin);

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
    public static extern void b2DistanceJointDef_Initialize(IntPtr obj, IntPtr bodyA, IntPtr bodyB, Vec2 anchorA, Vec2 anchorB);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_get_localAnchorA(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_set_localAnchorA(IntPtr obj, [In] ref Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_get_localAnchorB(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2DistanceJointDef_set_localAnchorB(IntPtr obj, [In] ref Vec2 value);
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

    /*
     * b2PrismaticJointDef
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern IntPtr b2PrismaticJointDef_new();
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_Initialize(IntPtr obj, IntPtr bodyA, IntPtr bodyB, [In] ref Vec2 anchor, [In] ref Vec2 axis);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_get_localAnchorA(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_localAnchorA(IntPtr obj, [In] ref Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_get_localAnchorB(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_localAnchorB(IntPtr obj, [In] ref Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_get_localAxisA(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_localAxisA(IntPtr obj, [In] ref Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJointDef_get_referenceAngle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_referenceAngle(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern bool b2PrismaticJointDef_get_enableLimit(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_enableLimit(IntPtr obj, bool value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJointDef_get_lowerTranslation(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_lowerTranslation(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJointDef_get_upperTranslation(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_upperTranslation(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern bool b2PrismaticJointDef_get_enableMotor(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_enableMotor(IntPtr obj, bool value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJointDef_get_maxMotorForce(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_maxMotorForce(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJointDef_get_motorSpeed(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_set_motorSpeed(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJointDef_delete(IntPtr obj);

    /*
     * b2PrismaticJoint
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJoint_GetLocalAnchorA(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJoint_GetLocalAnchorB(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJoint_GetLocalAxisA(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJoint_GetReferenceAngle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJoint_GetJointTranslation(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJoint_GetJointSpeed(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern bool b2PrismaticJoint_IsLimitEnabled(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJoint_EnableLimit(IntPtr obj, bool flag);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJoint_GetLowerLimit(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2PrismaticJoint_GetUpperLimit(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJoint_SetLimits(IntPtr obj, float lower, float upper);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern bool b2PrismaticJoint_IsMotorEnabled(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2PrismaticJoint_EnableMotor(IntPtr obj, bool flag);
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
    public static extern void b2RevoluteJointDef_Initialize(IntPtr obj, IntPtr bodyA, IntPtr bodyB, [In] ref Vec2 anchor);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_get_localAnchorA(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_localAnchorA(IntPtr obj, [In] ref Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_get_localAnchorB(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_localAnchorB(IntPtr obj, [In] ref Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJointDef_get_referenceAngle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_referenceAngle(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern bool b2RevoluteJointDef_get_enableLimit(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_enableLimit(IntPtr obj, bool value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJointDef_get_lowerAngle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_lowerAngle(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJointDef_get_upperAngle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_upperAngle(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern bool b2RevoluteJointDef_get_enableMotor(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_enableMotor(IntPtr obj, bool value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJointDef_get_motorSpeed(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_motorSpeed(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJointDef_get_maxMotorTorque(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_set_maxMotorTorque(IntPtr obj, float value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJointDef_delete(IntPtr obj);

    /*
     * b2RevoluteJoint
     */
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJoint_GetLocalAnchorA(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJoint_GetLocalAnchorB(IntPtr obj, out Vec2 value);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJoint_GetReferenceAngle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJoint_GetJointAngle(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJoint_GetJointSpeed(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern bool b2RevoluteJoint_IsLimitEnabled(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJoint_EnableLimit(IntPtr obj, bool flag);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJoint_GetLowerLimit(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern float b2RevoluteJoint_GetUpperLimit(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJoint_SetLimits(IntPtr obj, float lower, float upper);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern bool b2RevoluteJoint_IsMotorEnabled(IntPtr obj);
    [DllImport(Dll, CallingConvention = Conv)]
    public static extern void b2RevoluteJoint_EnableMotor(IntPtr obj, bool flag);
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
}
