#include "pch.h"
#include "verify.h"
#include "b2_revolute_joint_wrap.h"

#include <memory>

/*
 * b2RevoluteJointDef
 */

b2RevoluteJointDef* b2RevoluteJointDef_new()
{
    return new b2RevoluteJointDef;
}

void b2RevoluteJointDef_Initialize(b2RevoluteJointDef* obj, b2Body* bodyA, b2Body* bodyB, b2Vec2* anchor)
{
    VERIFY_INSTANCE;
    obj->Initialize(bodyA, bodyB, *anchor);
}

void b2RevoluteJointDef_get_localAnchorA(b2RevoluteJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->localAnchorA;
}

void b2RevoluteJointDef_set_localAnchorA(b2RevoluteJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->localAnchorA = *value;
}

void b2RevoluteJointDef_get_localAnchorB(b2RevoluteJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->localAnchorB;
}

void b2RevoluteJointDef_set_localAnchorB(b2RevoluteJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->localAnchorB = *value;
}

float b2RevoluteJointDef_get_referenceAngle(b2RevoluteJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->referenceAngle;
}

void b2RevoluteJointDef_set_referenceAngle(b2RevoluteJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->referenceAngle = value;
}

bool b2RevoluteJointDef_get_enableLimit(b2RevoluteJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->enableLimit;
}

void b2RevoluteJointDef_set_enableLimit(b2RevoluteJointDef* obj, bool value)
{
    VERIFY_INSTANCE;
    obj->enableLimit = value;
}

float b2RevoluteJointDef_get_lowerAngle(b2RevoluteJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->lowerAngle;
}

void b2RevoluteJointDef_set_lowerAngle(b2RevoluteJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->lowerAngle = value;
}

float b2RevoluteJointDef_get_upperAngle(b2RevoluteJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->upperAngle;
}

void b2RevoluteJointDef_set_upperAngle(b2RevoluteJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->upperAngle = value;
}

bool b2RevoluteJointDef_get_enableMotor(b2RevoluteJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->enableMotor;
}

void b2RevoluteJointDef_set_enableMotor(b2RevoluteJointDef* obj, bool value)
{
    VERIFY_INSTANCE;
    obj->enableMotor = value;
}

float b2RevoluteJointDef_get_motorSpeed(b2RevoluteJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->motorSpeed;
}

void b2RevoluteJointDef_set_motorSpeed(b2RevoluteJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->motorSpeed = value;
}

float b2RevoluteJointDef_get_maxMotorTorque(b2RevoluteJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->maxMotorTorque;
}

void b2RevoluteJointDef_set_maxMotorTorque(b2RevoluteJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->maxMotorTorque = value;
}

void b2RevoluteJointDef_reset(b2RevoluteJointDef* obj)
{
    VERIFY_INSTANCE;
    obj->~b2RevoluteJointDef();
    new (obj) b2RevoluteJointDef();
}

void b2RevoluteJointDef_delete(b2RevoluteJointDef* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}

/*
 * b2RevoluteJoint
 */

void b2RevoluteJoint_GetLocalAnchorA(b2RevoluteJoint* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLocalAnchorA();
}

void b2RevoluteJoint_GetLocalAnchorB(b2RevoluteJoint* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLocalAnchorB();
}

float b2RevoluteJoint_GetReferenceAngle(b2RevoluteJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetReferenceAngle();
}

float b2RevoluteJoint_GetJointAngle(b2RevoluteJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetJointAngle();
}

float b2RevoluteJoint_GetJointSpeed(b2RevoluteJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetJointSpeed();
}

bool b2RevoluteJoint_IsLimitEnabled(b2RevoluteJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->IsLimitEnabled();
}

void b2RevoluteJoint_EnableLimit(b2RevoluteJoint* obj, bool flag)
{
    VERIFY_INSTANCE;
    obj->EnableLimit(flag);
}

float b2RevoluteJoint_GetLowerLimit(b2RevoluteJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetLowerLimit();
}

float b2RevoluteJoint_GetUpperLimit(b2RevoluteJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetUpperLimit();
}

void b2RevoluteJoint_SetLimits(b2RevoluteJoint* obj, float lower, float upper)
{
    VERIFY_INSTANCE;
    obj->SetLimits(lower, upper);
}

bool b2RevoluteJoint_IsMotorEnabled(b2RevoluteJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->IsMotorEnabled();
}

void b2RevoluteJoint_EnableMotor(b2RevoluteJoint* obj, bool flag)
{
    VERIFY_INSTANCE;
    obj->EnableMotor(flag);
}

float b2RevoluteJoint_GetMotorSpeed(b2RevoluteJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetMotorSpeed();
}

void b2RevoluteJoint_SetMotorSpeed(b2RevoluteJoint* obj, float speed)
{
    VERIFY_INSTANCE;
    obj->SetMotorSpeed(speed);
}

float b2RevoluteJoint_GetMaxMotorTorque(b2RevoluteJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetMaxMotorTorque();
}

void b2RevoluteJoint_SetMaxMotorTorque(b2RevoluteJoint* obj, float torque)
{
    VERIFY_INSTANCE;
    obj->SetMaxMotorTorque(torque);
}

float b2RevoluteJoint_GetMotorTorque(b2RevoluteJoint* obj, float inv_dt)
{
    VERIFY_INSTANCE;
    return obj->GetMotorTorque(inv_dt);
}
