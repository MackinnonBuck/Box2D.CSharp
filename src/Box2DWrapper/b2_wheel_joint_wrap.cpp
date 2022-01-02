#include "pch.h"
#include "verify.h"
#include "b2_wheel_joint_wrap.h"

#include <memory>

/*
 * b2WheelJointDef
 */

b2WheelJointDef* b2WheelJointDef_new()
{
    return new b2WheelJointDef;
}

void b2WheelJointDef_Initialize(b2WheelJointDef* obj, b2Body* bodyA, b2Body* bodyB, b2Vec2* anchor, b2Vec2* axis)
{
    VERIFY_INSTANCE;
    obj->Initialize(bodyA, bodyB, *anchor, *axis);
}

void b2WheelJointDef_get_localAnchorA(b2WheelJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->localAnchorA;
}

void b2WheelJointDef_set_localAnchorA(b2WheelJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->localAnchorA = *value;
}

void b2WheelJointDef_get_localAnchorB(b2WheelJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->localAnchorB;
}

void b2WheelJointDef_set_localAnchorB(b2WheelJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->localAnchorB = *value;
}

void b2WheelJointDef_get_localAxisA(b2WheelJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->localAxisA;
}

void b2WheelJointDef_set_localAxisA(b2WheelJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->localAxisA = *value;
}

bool b2WheelJointDef_get_enableLimit(b2WheelJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->enableLimit;
}

void b2WheelJointDef_set_enableLimit(b2WheelJointDef* obj, bool value)
{
    VERIFY_INSTANCE;
    obj->enableLimit = value;
}

float b2WheelJointDef_get_lowerTranslation(b2WheelJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->lowerTranslation;
}

void b2WheelJointDef_set_lowerTranslation(b2WheelJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->lowerTranslation = value;
}

float b2WheelJointDef_get_upperTranslation(b2WheelJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->upperTranslation;
}

void b2WheelJointDef_set_upperTranslation(b2WheelJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->upperTranslation = value;
}

bool b2WheelJointDef_get_enableMotor(b2WheelJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->enableMotor;
}

void b2WheelJointDef_set_enableMotor(b2WheelJointDef* obj, bool value)
{
    VERIFY_INSTANCE;
    obj->enableMotor = value;
}

float b2WheelJointDef_get_maxMotorTorque(b2WheelJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->maxMotorTorque;
}

void b2WheelJointDef_set_maxMotorTorque(b2WheelJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->maxMotorTorque = value;
}

float b2WheelJointDef_get_motorSpeed(b2WheelJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->motorSpeed;
}

void b2WheelJointDef_set_motorSpeed(b2WheelJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->motorSpeed = value;
}

float b2WheelJointDef_get_stiffness(b2WheelJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->stiffness;
}

void b2WheelJointDef_set_stiffness(b2WheelJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->stiffness = value;
}

float b2WheelJointDef_get_damping(b2WheelJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->damping;
}

void b2WheelJointDef_set_damping(b2WheelJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->damping = value;
}

void b2WheelJointDef_reset(b2WheelJointDef* obj)
{
    VERIFY_INSTANCE;
    obj->~b2WheelJointDef();
    new (obj) b2WheelJointDef();
}

void b2WheelJointDef_delete(b2WheelJointDef* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}

/*
 * b2WheelJoint
 */

void b2WheelJoint_GetLocalAnchorA(b2WheelJoint* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLocalAnchorA();
}

void b2WheelJoint_GetLocalAnchorB(b2WheelJoint* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLocalAnchorB();
}

void b2WheelJoint_GetLocalAxisA(b2WheelJoint* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLocalAxisA();
}

float b2WheelJoint_GetJointTranslation(b2WheelJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetJointTranslation();
}

float b2WheelJoint_GetJointLinearSpeed(b2WheelJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetJointLinearSpeed();
}

float b2WheelJoint_GetJointAngle(b2WheelJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetJointAngle();
}

float b2WheelJoint_GetJointAngularSpeed(b2WheelJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetJointAngularSpeed();
}

bool b2WheelJoint_IsLimitEnabled(b2WheelJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->IsLimitEnabled();
}

void b2WheelJoint_EnableLimit(b2WheelJoint* obj, bool flag)
{
    VERIFY_INSTANCE;
    obj->EnableLimit(flag);
}

float b2WheelJoint_GetLowerLimit(b2WheelJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetLowerLimit();
}

float b2WheelJoint_GetUpperLimit(b2WheelJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetUpperLimit();
}

void b2WheelJoint_SetLimits(b2WheelJoint* obj, float lower, float upper)
{
    VERIFY_INSTANCE;
    obj->SetLimits(lower, upper);
}

bool b2WheelJoint_IsMotorEnabled(b2WheelJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->IsMotorEnabled();
}

void b2WheelJoint_EnableMotor(b2WheelJoint* obj, bool flag)
{
    VERIFY_INSTANCE;
    obj->EnableMotor(flag);
}

float b2WheelJoint_GetMotorSpeed(b2WheelJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetMotorSpeed();
}

void b2WheelJoint_SetMotorSpeed(b2WheelJoint* obj, float speed)
{
    VERIFY_INSTANCE;
    obj->SetMotorSpeed(speed);
}

float b2WheelJoint_GetMaxMotorTorque(b2WheelJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetMaxMotorTorque();
}

void b2WheelJoint_SetMaxMotorTorque(b2WheelJoint* obj, float torque)
{
    VERIFY_INSTANCE;
    obj->SetMaxMotorTorque(torque);
}

float b2WheelJoint_GetMotorTorque(b2WheelJoint* obj, float inv_dt)
{
    VERIFY_INSTANCE;
    return obj->GetMotorTorque(inv_dt);
}

float b2WheelJoint_GetStiffness(b2WheelJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetStiffness();
}

void b2WheelJoint_SetStiffness(b2WheelJoint* obj, float stiffness)
{
    VERIFY_INSTANCE;
    obj->SetStiffness(stiffness);
}

float b2WheelJoint_GetDamping(b2WheelJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetDamping();
}

void b2WheelJoint_SetDamping(b2WheelJoint* obj, float damping)
{
    VERIFY_INSTANCE;
    obj->SetDamping(damping);
}
