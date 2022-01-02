#include "pch.h"
#include "verify.h"
#include "b2_friction_joint_wrap.h"

#include <memory>

/*
 * b2FrictionJointDef
 */

b2FrictionJointDef* b2FrictionJointDef_new()
{
    return new b2FrictionJointDef;
}

void b2FrictionJointDef_Initialize(b2FrictionJointDef* obj, b2Body* bodyA, b2Body* bodyB, b2Vec2* anchor)
{
    VERIFY_INSTANCE;
    obj->Initialize(bodyA, bodyB, *anchor);
}

void b2FrictionJointDef_get_localAnchorA(b2FrictionJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->localAnchorA;
}

void b2FrictionJointDef_set_localAnchorA(b2FrictionJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->localAnchorA = *value;
}

void b2FrictionJointDef_get_localAnchorB(b2FrictionJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->localAnchorB;
}

void b2FrictionJointDef_set_localAnchorB(b2FrictionJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->localAnchorB = *value;
}

float b2FrictionJointDef_get_maxForce(b2FrictionJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->maxForce;
}

void b2FrictionJointDef_set_maxForce(b2FrictionJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->maxForce = value;
}

float b2FrictionJointDef_get_maxTorque(b2FrictionJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->maxTorque;
}

void b2FrictionJointDef_set_maxTorque(b2FrictionJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->maxTorque = value;
}

void b2FrictionJointDef_reset(b2FrictionJointDef* obj)
{
    VERIFY_INSTANCE;
    obj->~b2FrictionJointDef();
    new (obj) b2FrictionJointDef();
}

void b2FrictionJointDef_delete(b2FrictionJointDef* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}

/*
 * b2FrictionJoint
 */

void b2FrictionJoint_GetLocalAnchorA(b2FrictionJoint* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLocalAnchorA();
}

void b2FrictionJoint_GetLocalAnchorB(b2FrictionJoint* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLocalAnchorB();
}

float b2FrictionJoint_GetMaxForce(b2FrictionJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetMaxForce();
}

void b2FrictionJoint_SetMaxForce(b2FrictionJoint* obj, float force)
{
    VERIFY_INSTANCE;
    obj->SetMaxForce(force);
}

float b2FrictionJoint_GetMaxTorque(b2FrictionJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetMaxTorque();
}

void b2FrictionJoint_SetMaxTorque(b2FrictionJoint* obj, float torque)
{
    VERIFY_INSTANCE;
    obj->SetMaxTorque(torque);
}
