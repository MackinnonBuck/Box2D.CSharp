#pragma once

#include "api.h"

#include <box2d/b2_distance_joint.h>

extern "C"
{
    /*
     * b2DistanceJointDef
     */
    BOX2D_API b2DistanceJointDef* b2DistanceJointDef_new();
    BOX2D_API void b2DistanceJointDef_Initialize(b2DistanceJointDef* obj, b2Body* bodyA, b2Body* bodyB, b2Vec2 anchorA, b2Vec2 anchorB);
    BOX2D_API void b2DistanceJointDef_get_localAnchorA(b2DistanceJointDef* obj, b2Vec2* value);
    BOX2D_API void b2DistanceJointDef_set_localAnchorA(b2DistanceJointDef* obj, b2Vec2* value);
    BOX2D_API void b2DistanceJointDef_get_localAnchorB(b2DistanceJointDef* obj, b2Vec2* value);
    BOX2D_API void b2DistanceJointDef_set_localAnchorB(b2DistanceJointDef* obj, b2Vec2* value);
    BOX2D_API float b2DistanceJointDef_get_length(b2DistanceJointDef* obj);
    BOX2D_API void b2DistanceJointDef_set_length(b2DistanceJointDef* obj, float value);
    BOX2D_API float b2DistanceJointDef_get_minLength(b2DistanceJointDef* obj);
    BOX2D_API void b2DistanceJointDef_set_minLength(b2DistanceJointDef* obj, float value);
    BOX2D_API float b2DistanceJointDef_get_maxLength(b2DistanceJointDef* obj);
    BOX2D_API void b2DistanceJointDef_set_maxLength(b2DistanceJointDef* obj, float value);
    BOX2D_API float b2DistanceJointDef_get_stiffness(b2DistanceJointDef* obj);
    BOX2D_API void b2DistanceJointDef_set_stiffness(b2DistanceJointDef* obj, float value);
    BOX2D_API float b2DistanceJointDef_get_damping(b2DistanceJointDef* obj);
    BOX2D_API void b2DistanceJointDef_set_damping(b2DistanceJointDef* obj, float value);
    BOX2D_API void b2DistanceJointDef_reset(b2DistanceJointDef* obj);
    BOX2D_API void b2DistanceJointDef_delete(b2DistanceJointDef* obj);

    /*
     * b2DistanceJoint
     */
    BOX2D_API void b2DistanceJoint_GetLocalAnchorA(b2DistanceJoint* obj, b2Vec2* value);
    BOX2D_API void b2DistanceJoint_GetLocalAnchorB(b2DistanceJoint* obj, b2Vec2* value);
    BOX2D_API float b2DistanceJoint_GetLength(b2DistanceJoint* obj);
    BOX2D_API void b2DistanceJoint_SetLength(b2DistanceJoint* obj, float length);
    BOX2D_API float b2DistanceJoint_GetMinLength(b2DistanceJoint* obj);
    BOX2D_API void b2DistanceJoint_SetMinLength(b2DistanceJoint* obj, float minLength);
    BOX2D_API float b2DistanceJoint_GetMaxLength(b2DistanceJoint* obj);
    BOX2D_API void b2DistanceJoint_SetMaxLength(b2DistanceJoint* obj, float maxLength);
    BOX2D_API float b2DistanceJoint_GetCurrentLength(b2DistanceJoint* obj);
    BOX2D_API float b2DistanceJoint_GetStiffness(b2DistanceJoint* obj);
    BOX2D_API void b2DistanceJoint_SetStiffness(b2DistanceJoint* obj, float stiffness);
    BOX2D_API float b2DistanceJoint_GetDamping(b2DistanceJoint* obj);
    BOX2D_API void b2DistanceJoint_SetDamping(b2DistanceJoint* obj, float damping);
}
