#include "pch.h"
#include "verify.h"
#include "b2_joint_wrap.h"

/*
 * Top-level functions
 */

void b2LinearStiffness_wrap(float* stiffness, float* damping, float frequencyHertz, float dampingRatio, b2Body* bodyA, b2Body* bodyB)
{
    b2LinearStiffness(*stiffness, *damping, frequencyHertz, dampingRatio, bodyA, bodyB);
}

void b2AngularStiffness_wrap(float* stiffness, float* damping, float frequencyHertz, float dampingRatio, b2Body* bodyA, b2Body* bodyB)
{
    b2AngularStiffness(*stiffness, *damping, frequencyHertz, dampingRatio, bodyA, bodyB);
}

/*
 * b2JointDef
 */

b2JointType b2JointDef_get_type(b2JointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->type;
}

void b2JointDef_set_type(b2JointDef* obj, b2JointType value)
{
    VERIFY_INSTANCE;
    obj->type = value;
}

uintptr_t b2JointDef_get_userData(b2JointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->userData.pointer;
}

void b2JointDef_set_userData(b2JointDef* obj, uintptr_t value)
{
    VERIFY_INSTANCE;
    obj->userData.pointer = value;
}

b2Body* b2JointDef_get_bodyA(b2JointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->bodyA;
}

void b2JointDef_set_bodyA(b2JointDef* obj, b2Body* value)
{
    VERIFY_INSTANCE;
    obj->bodyA = value;
}

b2Body* b2JointDef_get_bodyB(b2JointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->bodyB;
}

void b2JointDef_set_bodyB(b2JointDef* obj, b2Body* value)
{
    VERIFY_INSTANCE;
    obj->bodyB = value;
}

bool b2JointDef_get_collideConnected(b2JointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->collideConnected;
}

void b2JointDef_set_collideConnected(b2JointDef* obj, bool value)
{
    VERIFY_INSTANCE;
    obj->collideConnected = value;
}

/*
 * b2Joint
 */

b2Body* b2Joint_GetBodyA(b2Joint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetBodyA();
}

b2Body* b2Joint_GetBodyB(b2Joint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetBodyB();
}

void b2Joint_GetAnchorA(b2Joint* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetAnchorA();
}

void b2Joint_GetAnchorB(b2Joint* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetAnchorB();
}

void b2Joint_GetReactionForce(b2Joint* obj, float inv_dt, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetReactionForce(inv_dt);
}

float b2Joint_GetReactionTorque(b2Joint* obj, float inv_dt)
{
    VERIFY_INSTANCE;
    return obj->GetReactionTorque(inv_dt);
}

b2Joint* b2Joint_GetNext(b2Joint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetNext();
}

uintptr_t b2Joint_GetUserData(b2Joint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetUserData().pointer;
}

bool b2Joint_IsEnabled(b2Joint* obj)
{
    VERIFY_INSTANCE;
    return obj->IsEnabled();
}

bool b2Joint_GetCollideConnected(b2Joint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetCollideConnected();
}

void b2Joint_ShiftOrigin(b2Joint* obj, b2Vec2* newOrigin)
{
    VERIFY_INSTANCE;
    obj->ShiftOrigin(*newOrigin);
}

/*
 * b2JointEdge
 */

b2Body* b2JointEdge_get_other(b2JointEdge* obj)
{
    VERIFY_INSTANCE;
    return obj->other;
}

void b2JointEdge_set_other(b2JointEdge* obj, b2Body* value)
{
    VERIFY_INSTANCE;
    obj->other = value;
}

b2Joint* b2JointEdge_get_joint(b2JointEdge* obj)
{
    VERIFY_INSTANCE;
    return obj->joint;
}

void b2JointEdge_set_joint(b2JointEdge* obj, b2Joint* value)
{
    VERIFY_INSTANCE;
    obj->joint = value;
}

b2JointEdge* b2JointEdge_get_prev(b2JointEdge* obj)
{
    VERIFY_INSTANCE;
    return obj->prev;
}

void b2JointEdge_set_prev(b2JointEdge* obj, b2JointEdge* value)
{
    VERIFY_INSTANCE;
    obj->prev = value;
}

b2JointEdge* b2JointEdge_get_next(b2JointEdge* obj)
{
    VERIFY_INSTANCE;
    return obj->next;
}

void b2JointEdge_set_next(b2JointEdge* obj, b2JointEdge* value)
{
    VERIFY_INSTANCE;
    obj->next = value;
}
