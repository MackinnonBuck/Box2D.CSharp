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
    BOX2D_API void b2DistanceJointDef_GetLocalAnchorA(b2DistanceJointDef* obj, b2Vec2* value);
    BOX2D_API void b2DistanceJointDef_GetLocalAnchorB(b2DistanceJointDef* obj, b2Vec2* value);
    BOX2D_API float b2DistanceJointDef_GetLength(b2DistanceJointDef* obj);
    BOX2D_API void b2DistanceJointDef_SetLength(b2DistanceJointDef* obj, float value);
    BOX2D_API float b2DistanceJointDef_GetMinLength(b2DistanceJointDef* obj);
    BOX2D_API void b2DistanceJointDef_SetMinLength(b2DistanceJointDef* obj, float value);
    BOX2D_API float b2DistanceJointDef_GetMaxLength(b2DistanceJointDef* obj);
    BOX2D_API void b2DistanceJointDef_SetMaxLength(b2DistanceJointDef* obj, float value);
    BOX2D_API float b2DistanceJointDef_GetStiffness(b2DistanceJointDef* obj);
    BOX2D_API void b2DistanceJointDef_SetStiffness(b2DistanceJointDef* obj, float value);
    BOX2D_API float b2DistanceJointDef_GetDamping(b2DistanceJointDef* obj);
    BOX2D_API void b2DistanceJointDef_SetDamping(b2DistanceJointDef* obj, float value);
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
