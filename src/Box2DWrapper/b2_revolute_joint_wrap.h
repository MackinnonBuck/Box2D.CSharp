#pragma once

#include "api.h"

#include <box2d/b2_revolute_joint.h>

extern "C"
{
    /*
     * b2RevoluteJointDef
     */
    BOX2D_API b2RevoluteJointDef* b2RevoluteJointDef_new();
    BOX2D_API void b2RevoluteJointDef_Initialize(b2RevoluteJointDef* obj, b2Body* bodyA, b2Body* bodyB, b2Vec2* anchor);
    BOX2D_API void b2RevoluteJointDef_get_localAnchorA(b2RevoluteJointDef* obj, b2Vec2* value);
    BOX2D_API void b2RevoluteJointDef_set_localAnchorA(b2RevoluteJointDef* obj, b2Vec2* value);
    BOX2D_API void b2RevoluteJointDef_get_localAnchorB(b2RevoluteJointDef* obj, b2Vec2* value);
    BOX2D_API void b2RevoluteJointDef_set_localAnchorB(b2RevoluteJointDef* obj, b2Vec2* value);
    BOX2D_API float b2RevoluteJointDef_get_referenceAngle(b2RevoluteJointDef* obj);
    BOX2D_API void b2RevoluteJointDef_set_referenceAngle(b2RevoluteJointDef* obj, float value);
    BOX2D_API bool b2RevoluteJointDef_get_enableLimit(b2RevoluteJointDef* obj);
    BOX2D_API void b2RevoluteJointDef_set_enableLimit(b2RevoluteJointDef* obj, bool value);
    BOX2D_API float b2RevoluteJointDef_get_lowerAngle(b2RevoluteJointDef* obj);
    BOX2D_API void b2RevoluteJointDef_set_lowerAngle(b2RevoluteJointDef* obj, float value);
    BOX2D_API float b2RevoluteJointDef_get_upperAngle(b2RevoluteJointDef* obj);
    BOX2D_API void b2RevoluteJointDef_set_upperAngle(b2RevoluteJointDef* obj, float value);
    BOX2D_API bool b2RevoluteJointDef_get_enableMotor(b2RevoluteJointDef* obj);
    BOX2D_API void b2RevoluteJointDef_set_enableMotor(b2RevoluteJointDef* obj, bool value);
    BOX2D_API float b2RevoluteJointDef_get_motorSpeed(b2RevoluteJointDef* obj);
    BOX2D_API void b2RevoluteJointDef_set_motorSpeed(b2RevoluteJointDef* obj, float value);
    BOX2D_API float b2RevoluteJointDef_get_maxMotorTorque(b2RevoluteJointDef* obj);
    BOX2D_API void b2RevoluteJointDef_set_maxMotorTorque(b2RevoluteJointDef* obj, float value);
    BOX2D_API void b2RevoluteJointDef_reset(b2RevoluteJointDef* obj);
    BOX2D_API void b2RevoluteJointDef_delete(b2RevoluteJointDef* obj);

    /*
     * b2RevoluteJoint
     */
    BOX2D_API void b2RevoluteJoint_GetLocalAnchorA(b2RevoluteJoint* obj, b2Vec2* value);
    BOX2D_API void b2RevoluteJoint_GetLocalAnchorB(b2RevoluteJoint* obj, b2Vec2* value);
    BOX2D_API float b2RevoluteJoint_GetReferenceAngle(b2RevoluteJoint* obj);
    BOX2D_API float b2RevoluteJoint_GetJointAngle(b2RevoluteJoint* obj);
    BOX2D_API float b2RevoluteJoint_GetJointSpeed(b2RevoluteJoint* obj);
    BOX2D_API bool b2RevoluteJoint_IsLimitEnabled(b2RevoluteJoint* obj);
    BOX2D_API void b2RevoluteJoint_EnableLimit(b2RevoluteJoint* obj, bool flag);
    BOX2D_API float b2RevoluteJoint_GetLowerLimit(b2RevoluteJoint* obj);
    BOX2D_API float b2RevoluteJoint_GetUpperLimit(b2RevoluteJoint* obj);
    BOX2D_API void b2RevoluteJoint_SetLimits(b2RevoluteJoint* obj, float lower, float upper);
    BOX2D_API bool b2RevoluteJoint_IsMotorEnabled(b2RevoluteJoint* obj);
    BOX2D_API void b2RevoluteJoint_EnableMotor(b2RevoluteJoint* obj, bool flag);
    BOX2D_API float b2RevoluteJoint_GetMotorSpeed(b2RevoluteJoint* obj);
    BOX2D_API void b2RevoluteJoint_SetMotorSpeed(b2RevoluteJoint* obj, float speed);
    BOX2D_API float b2RevoluteJoint_GetMaxMotorTorque(b2RevoluteJoint* obj);
    BOX2D_API void b2RevoluteJoint_SetMaxMotorTorque(b2RevoluteJoint* obj, float torque);
    BOX2D_API float b2RevoluteJoint_GetMotorTorque(b2RevoluteJoint* obj, float inv_dt);
}
