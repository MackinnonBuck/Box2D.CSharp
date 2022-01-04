#pragma once

#include "api.h"

#include <box2d/b2_prismatic_joint.h>

extern "C"
{
    /*
     * b2PrismaticJointDef
     */
    BOX2D_API b2PrismaticJointDef* b2PrismaticJointDef_new();
    BOX2D_API void b2PrismaticJointDef_Initialize(b2PrismaticJointDef* obj, b2Body* bodyA, b2Body* bodyB, b2Vec2* anchor, b2Vec2* axis);
    BOX2D_API void b2PrismaticJointDef_get_localAnchorA(b2PrismaticJointDef* obj, b2Vec2* value);
    BOX2D_API void b2PrismaticJointDef_set_localAnchorA(b2PrismaticJointDef* obj, b2Vec2* value);
    BOX2D_API void b2PrismaticJointDef_get_localAnchorB(b2PrismaticJointDef* obj, b2Vec2* value);
    BOX2D_API void b2PrismaticJointDef_set_localAnchorB(b2PrismaticJointDef* obj, b2Vec2* value);
    BOX2D_API void b2PrismaticJointDef_get_localAxisA(b2PrismaticJointDef* obj, b2Vec2* value);
    BOX2D_API void b2PrismaticJointDef_set_localAxisA(b2PrismaticJointDef* obj, b2Vec2* value);
    BOX2D_API float b2PrismaticJointDef_get_referenceAngle(b2PrismaticJointDef* obj);
    BOX2D_API void b2PrismaticJointDef_set_referenceAngle(b2PrismaticJointDef* obj, float value);
    BOX2D_API bool b2PrismaticJointDef_get_enableLimit(b2PrismaticJointDef* obj);
    BOX2D_API void b2PrismaticJointDef_set_enableLimit(b2PrismaticJointDef* obj, bool value);
    BOX2D_API float b2PrismaticJointDef_get_lowerTranslation(b2PrismaticJointDef* obj);
    BOX2D_API void b2PrismaticJointDef_set_lowerTranslation(b2PrismaticJointDef* obj, float value);
    BOX2D_API float b2PrismaticJointDef_get_upperTranslation(b2PrismaticJointDef* obj);
    BOX2D_API void b2PrismaticJointDef_set_upperTranslation(b2PrismaticJointDef* obj, float value);
    BOX2D_API bool b2PrismaticJointDef_get_enableMotor(b2PrismaticJointDef* obj);
    BOX2D_API void b2PrismaticJointDef_set_enableMotor(b2PrismaticJointDef* obj, bool value);
    BOX2D_API float b2PrismaticJointDef_get_maxMotorForce(b2PrismaticJointDef* obj);
    BOX2D_API void b2PrismaticJointDef_set_maxMotorForce(b2PrismaticJointDef* obj, float value);
    BOX2D_API float b2PrismaticJointDef_get_motorSpeed(b2PrismaticJointDef* obj);
    BOX2D_API void b2PrismaticJointDef_set_motorSpeed(b2PrismaticJointDef* obj, float value);
    BOX2D_API void b2PrismaticJointDef_reset(b2PrismaticJointDef* obj);
    BOX2D_API void b2PrismaticJointDef_delete(b2PrismaticJointDef* obj);

    /*
     * b2PrismaticJoint
     */
    BOX2D_API void b2PrismaticJoint_GetLocalAnchorA(b2PrismaticJoint* obj, b2Vec2* value);
    BOX2D_API void b2PrismaticJoint_GetLocalAnchorB(b2PrismaticJoint* obj, b2Vec2* value);
    BOX2D_API void b2PrismaticJoint_GetLocalAxisA(b2PrismaticJoint* obj, b2Vec2* value);
    BOX2D_API float b2PrismaticJoint_GetReferenceAngle(b2PrismaticJoint* obj);
    BOX2D_API float b2PrismaticJoint_GetJointTranslation(b2PrismaticJoint* obj);
    BOX2D_API float b2PrismaticJoint_GetJointSpeed(b2PrismaticJoint* obj);
    BOX2D_API bool b2PrismaticJoint_IsLimitEnabled(b2PrismaticJoint* obj);
    BOX2D_API void b2PrismaticJoint_EnableLimit(b2PrismaticJoint* obj, bool flag);
    BOX2D_API float b2PrismaticJoint_GetLowerLimit(b2PrismaticJoint* obj);
    BOX2D_API float b2PrismaticJoint_GetUpperLimit(b2PrismaticJoint* obj);
    BOX2D_API void b2PrismaticJoint_SetLimits(b2PrismaticJoint* obj, float lower, float upper);
    BOX2D_API bool b2PrismaticJoint_IsMotorEnabled(b2PrismaticJoint* obj);
    BOX2D_API void b2PrismaticJoint_EnableMotor(b2PrismaticJoint* obj, bool flag);
    BOX2D_API float b2PrismaticJoint_GetMotorSpeed(b2PrismaticJoint* obj);
    BOX2D_API void b2PrismaticJoint_SetMotorSpeed(b2PrismaticJoint* obj, float speed);
    BOX2D_API float b2PrismaticJoint_GetMaxMotorForce(b2PrismaticJoint* obj);
    BOX2D_API void b2PrismaticJoint_SetMaxMotorForce(b2PrismaticJoint* obj, float force);
    BOX2D_API float b2PrismaticJoint_GetMotorForce(b2PrismaticJoint* obj, float inv_dt);
}
