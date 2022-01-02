#include "pch.h"
#include "verify.h"
#include "b2_mouse_joint_wrap.h"

#include <memory>

/*
 * b2MouseJointDef
 */

b2MouseJointDef* b2MouseJointDef_new()
{
    return new b2MouseJointDef;
}

void b2MouseJointDef_get_target(b2MouseJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->target;
}

void b2MouseJointDef_set_target(b2MouseJointDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->target = *value;
}

float b2MouseJointDef_get_maxForce(b2MouseJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->maxForce;
}

void b2MouseJointDef_set_maxForce(b2MouseJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->maxForce = value;
}

float b2MouseJointDef_get_stiffness(b2MouseJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->stiffness;
}

void b2MouseJointDef_set_stiffness(b2MouseJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->stiffness = value;
}

float b2MouseJointDef_get_damping(b2MouseJointDef* obj)
{
    VERIFY_INSTANCE;
    return obj->damping;
}

void b2MouseJointDef_set_damping(b2MouseJointDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->damping = value;
}

void b2MouseJointDef_reset(b2MouseJointDef* obj)
{
    VERIFY_INSTANCE;
    obj->~b2MouseJointDef();
    new (obj) b2MouseJointDef();
}

void b2MouseJointDef_delete(b2MouseJointDef* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}

/*
 * b2MouseJoint
 */

void b2MouseJoint_GetTarget(b2MouseJoint* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetTarget();
}

void b2MouseJoint_SetTarget(b2MouseJoint* obj, b2Vec2* target)
{
    VERIFY_INSTANCE;
    obj->SetTarget(*target);
}

float b2MouseJoint_GetMaxForce(b2MouseJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetMaxForce();
}

void b2MouseJoint_SetMaxForce(b2MouseJoint* obj, float force)
{
    VERIFY_INSTANCE;
    obj->SetMaxForce(force);
}

float b2MouseJoint_GetStiffness(b2MouseJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetStiffness();
}

void b2MouseJoint_SetStiffness(b2MouseJoint* obj, float stiffness)
{
    VERIFY_INSTANCE;
    obj->SetStiffness(stiffness);
}

float b2MouseJoint_GetDamping(b2MouseJoint* obj)
{
    VERIFY_INSTANCE;
    return obj->GetDamping();
}

void b2MouseJoint_SetDamping(b2MouseJoint* obj, float damping)
{
    VERIFY_INSTANCE;
    obj->SetDamping(damping);
}
