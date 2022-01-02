#pragma once

#include "api.h"

#include <box2d/b2_mouse_joint.h>

extern "C"
{
    /*
     * b2MouseJointDef
     */
    BOX2D_API b2MouseJointDef* b2MouseJointDef_new();
    BOX2D_API void b2MouseJointDef_get_target(b2MouseJointDef* obj, b2Vec2* value);
    BOX2D_API void b2MouseJointDef_set_target(b2MouseJointDef* obj, b2Vec2* value);
    BOX2D_API float b2MouseJointDef_get_maxForce(b2MouseJointDef* obj);
    BOX2D_API void b2MouseJointDef_set_maxForce(b2MouseJointDef* obj, float value);
    BOX2D_API float b2MouseJointDef_get_stiffness(b2MouseJointDef* obj);
    BOX2D_API void b2MouseJointDef_set_stiffness(b2MouseJointDef* obj, float value);
    BOX2D_API float b2MouseJointDef_get_damping(b2MouseJointDef* obj);
    BOX2D_API void b2MouseJointDef_set_damping(b2MouseJointDef* obj, float value);
    BOX2D_API void b2MouseJointDef_reset(b2MouseJointDef* obj);
    BOX2D_API void b2MouseJointDef_delete(b2MouseJointDef* obj);

    /*
     * b2MouseJoint
     */
    BOX2D_API void b2MouseJoint_GetTarget(b2MouseJoint* obj, b2Vec2* value);
    BOX2D_API void b2MouseJoint_SetTarget(b2MouseJoint* obj, b2Vec2* target);
    BOX2D_API float b2MouseJoint_GetMaxForce(b2MouseJoint* obj);
    BOX2D_API void b2MouseJoint_SetMaxForce(b2MouseJoint* obj, float force);
    BOX2D_API float b2MouseJoint_GetStiffness(b2MouseJoint* obj);
    BOX2D_API void b2MouseJoint_SetStiffness(b2MouseJoint* obj, float stiffness);
    BOX2D_API float b2MouseJoint_GetDamping(b2MouseJoint* obj);
    BOX2D_API void b2MouseJoint_SetDamping(b2MouseJoint* obj, float damping);
}
