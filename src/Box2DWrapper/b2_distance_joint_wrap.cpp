#include "pch.h"
#include "verify.h"
#include "b2_distance_joint_wrap.h"

#include <memory>

/*
 * b2DistanceJointDef
 */

b2DistanceJointDef* b2DistanceJointDef_new()
{
    return new b2DistanceJointDef;
}

void b2DistanceJointDef_Initialize(b2DistanceJointDef* obj, b2Body* bodyA, b2Body* bodyB, b2Vec2 anchorA, b2Vec2 anchorB)
{
    VERIFY_INSTANCE;
    obj->Initialize(bodyA, bodyB, anchorA, anchorB);
}

void b2DistanceJointDef_get_localAnchorA(b2DistanceJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->localAnchorA;
}

void b2DistanceJointDef_set_localAnchorA(b2DistanceJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->localAnchorA = *value;
}

void b2DistanceJointDef_get_localAnchorB(b2DistanceJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->localAnchorB;
}

void b2DistanceJointDef_set_localAnchorB(b2DistanceJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->localAnchorB = *value;
}

float b2DistanceJointDef_get_length(b2DistanceJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->length;
}

void b2DistanceJointDef_set_length(b2DistanceJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->length = value;
}

float b2DistanceJointDef_get_minLength(b2DistanceJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->minLength;
}

void b2DistanceJointDef_set_minLength(b2DistanceJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->minLength = value;
}

float b2DistanceJointDef_get_maxLength(b2DistanceJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->maxLength;
}

void b2DistanceJointDef_set_maxLength(b2DistanceJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->maxLength = value;
}

float b2DistanceJointDef_get_stiffness(b2DistanceJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->stiffness;
}

void b2DistanceJointDef_set_stiffness(b2DistanceJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->stiffness = value;
}

float b2DistanceJointDef_get_damping(b2DistanceJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->damping;
}

void b2DistanceJointDef_set_damping(b2DistanceJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->damping = value;
}

void b2DistanceJointDef_reset(b2DistanceJointDef* obj)
{
    VERIFY_INSTANCE;
    obj->~b2DistanceJointDef();
    new (obj) b2DistanceJointDef();
}

void b2DistanceJointDef_delete(b2DistanceJointDef* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}

/*
 * b2DistanceJoint
 */

void b2DistanceJoint_GetLocalAnchorA(b2DistanceJoint* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLocalAnchorA();
}

void b2DistanceJoint_GetLocalAnchorB(b2DistanceJoint* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLocalAnchorB();
}

float b2DistanceJoint_GetLength(b2DistanceJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetLength();
}

void b2DistanceJoint_SetLength(b2DistanceJoint* obj, float length)
{
    VERIFY_INSTANCE;
    obj->SetLength(length);
}

float b2DistanceJoint_GetMinLength(b2DistanceJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetMinLength();
}

void b2DistanceJoint_SetMinLength(b2DistanceJoint* obj, float minLength)
{
    VERIFY_INSTANCE;
    obj->SetMinLength(minLength);
}

float b2DistanceJoint_GetMaxLength(b2DistanceJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetMaxLength();
}

void b2DistanceJoint_SetMaxLength(b2DistanceJoint* obj, float maxLength)
{
    VERIFY_INSTANCE;
    obj->SetMaxLength(maxLength);
}

float b2DistanceJoint_GetCurrentLength(b2DistanceJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetCurrentLength();
}

float b2DistanceJoint_GetStiffness(b2DistanceJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetStiffness();
}

void b2DistanceJoint_SetStiffness(b2DistanceJoint* obj, float stiffness)
{
    VERIFY_INSTANCE;
    obj->SetStiffness(stiffness);
}

float b2DistanceJoint_GetDamping(b2DistanceJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetDamping();
}

void b2DistanceJoint_SetDamping(b2DistanceJoint* obj, float damping)
{
    VERIFY_INSTANCE;
    obj->SetDamping(damping);
}
