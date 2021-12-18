#include "pch.h"
#include "verify.h"
#include "b2_joint_wrap.h"

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
