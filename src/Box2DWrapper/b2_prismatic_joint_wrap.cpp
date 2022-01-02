#include "pch.h"
#include "verify.h"
#include "b2_prismatic_joint_wrap.h"

#include <memory>

/*
 * b2PrismaticJointDef
 */

b2PrismaticJointDef* b2PrismaticJointDef_new()
{
    return new b2PrismaticJointDef;
}

void b2PrismaticJointDef_Initialize(b2PrismaticJointDef* obj, b2Body* bodyA, b2Body* bodyB, b2Vec2* anchor, b2Vec2* axis)
{
    VERIFY_INSTANCE;
    obj->Initialize(bodyA, bodyB, *anchor, *axis);
}

void b2PrismaticJointDef_get_localAnchorA(b2PrismaticJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->localAnchorA;
}

void b2PrismaticJointDef_set_localAnchorA(b2PrismaticJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->localAnchorA = *value;
}

void b2PrismaticJointDef_get_localAnchorB(b2PrismaticJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->localAnchorB;
}

void b2PrismaticJointDef_set_localAnchorB(b2PrismaticJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->localAnchorB = *value;
}

void b2PrismaticJointDef_get_localAxisA(b2PrismaticJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->localAxisA;
}

void b2PrismaticJointDef_set_localAxisA(b2PrismaticJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->localAxisA = *value;
}

float b2PrismaticJointDef_get_referenceAngle(b2PrismaticJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->referenceAngle;
}

void b2PrismaticJointDef_set_referenceAngle(b2PrismaticJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->referenceAngle = value;
}

bool b2PrismaticJointDef_get_enableLimit(b2PrismaticJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->enableLimit;
}

void b2PrismaticJointDef_set_enableLimit(b2PrismaticJointDef* obj, bool value)
{
    VERIFY_INSTANCE;
    obj->enableLimit = value;
}

float b2PrismaticJointDef_get_lowerTranslation(b2PrismaticJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->lowerTranslation;
}

void b2PrismaticJointDef_set_lowerTranslation(b2PrismaticJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->lowerTranslation = value;
}

float b2PrismaticJointDef_get_upperTranslation(b2PrismaticJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->upperTranslation;
}

void b2PrismaticJointDef_set_upperTranslation(b2PrismaticJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->upperTranslation = value;
}

bool b2PrismaticJointDef_get_enableMotor(b2PrismaticJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->enableMotor;
}

void b2PrismaticJointDef_set_enableMotor(b2PrismaticJointDef* obj, bool value)
{
    VERIFY_INSTANCE;
    obj->enableMotor = value;
}

float b2PrismaticJointDef_get_maxMotorForce(b2PrismaticJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->maxMotorForce;
}

void b2PrismaticJointDef_set_maxMotorForce(b2PrismaticJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->maxMotorForce = value;
}

float b2PrismaticJointDef_get_motorSpeed(b2PrismaticJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->motorSpeed;
}

void b2PrismaticJointDef_set_motorSpeed(b2PrismaticJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->motorSpeed = value;
}

void b2PrismaticJointDef_reset(b2PrismaticJointDef* obj)
{
    VERIFY_INSTANCE;
    obj->~b2PrismaticJointDef();
    new (obj) b2PrismaticJointDef();
}

void b2PrismaticJointDef_delete(b2PrismaticJointDef* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}

/*
 * b2PrismaticJoint
 */

void b2PrismaticJoint_GetLocalAnchorA(b2PrismaticJoint* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLocalAnchorA();
}

void b2PrismaticJoint_GetLocalAnchorB(b2PrismaticJoint* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLocalAnchorB();
}

void b2PrismaticJoint_GetLocalAxisA(b2PrismaticJoint* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLocalAxisA();
}

float b2PrismaticJoint_GetReferenceAngle(b2PrismaticJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetReferenceAngle();
}

float b2PrismaticJoint_GetJointTranslation(b2PrismaticJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetJointTranslation();
}

float b2PrismaticJoint_GetJointSpeed(b2PrismaticJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetJointSpeed();
}

bool b2PrismaticJoint_IsLimitEnabled(b2PrismaticJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->IsLimitEnabled();
}

void b2PrismaticJoint_EnableLimit(b2PrismaticJoint* obj, bool flag)
{
    VERIFY_INSTANCE;
    obj->EnableLimit(flag);
}

float b2PrismaticJoint_GetLowerLimit(b2PrismaticJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetLowerLimit();
}

float b2PrismaticJoint_GetUpperLimit(b2PrismaticJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetUpperLimit();
}

void b2PrismaticJoint_SetLimits(b2PrismaticJoint* obj, float lower, float upper)
{
    VERIFY_INSTANCE;
    obj->SetLimits(lower, upper);
}

bool b2PrismaticJoint_IsMotorEnabled(b2PrismaticJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->IsMotorEnabled();
}

void b2PrismaticJoint_EnableMotor(b2PrismaticJoint* obj, bool flag)
{
    VERIFY_INSTANCE;
    obj->EnableMotor(flag);
}

float b2PrismaticJoint_GetMotorSpeed(b2PrismaticJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetMotorSpeed();
}

void b2PrismaticJoint_SetMotorSpeed(b2PrismaticJoint* obj, float speed)
{
    VERIFY_INSTANCE;
    obj->SetMotorSpeed(speed);
}

float b2PrismaticJoint_GetMaxMotorForce(b2PrismaticJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetMaxMotorForce();
}

void b2PrismaticJoint_SetMaxMotorForce(b2PrismaticJoint* obj, float force)
{
    VERIFY_INSTANCE;
    obj->SetMaxMotorForce(force);
}

float b2PrismaticJoint_GetMotorForce(b2PrismaticJoint* obj, float inv_dt)
{
    VERIFY_INSTANCE;
    return obj->GetMotorForce(inv_dt);
}
