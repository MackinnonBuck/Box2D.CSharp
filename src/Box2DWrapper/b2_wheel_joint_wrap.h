#pragma once

#include "api.h"

#include <box2d/b2_wheel_joint.h>

extern "C"
{
    /*
     * b2WheelJointDef
     */
    BOX2D_API b2WheelJointDef* b2WheelJointDef_new();
    BOX2D_API void b2WheelJointDef_Initialize(b2WheelJointDef* obj, b2Body* bodyA, b2Body* bodyB, b2Vec2* anchor, b2Vec2* axis);
    BOX2D_API void b2WheelJointDef_get_localAnchorA(b2WheelJointDef* obj, b2Vec2* value);
    BOX2D_API void b2WheelJointDef_set_localAnchorA(b2WheelJointDef* obj, b2Vec2* value);
    BOX2D_API void b2WheelJointDef_get_localAnchorB(b2WheelJointDef* obj, b2Vec2* value);
    BOX2D_API void b2WheelJointDef_set_localAnchorB(b2WheelJointDef* obj, b2Vec2* value);
    BOX2D_API void b2WheelJointDef_get_localAxisA(b2WheelJointDef* obj, b2Vec2* value);
    BOX2D_API void b2WheelJointDef_set_localAxisA(b2WheelJointDef* obj, b2Vec2* value);
    BOX2D_API bool b2WheelJointDef_get_enableLimit(b2WheelJointDef* obj);
    BOX2D_API void b2WheelJointDef_set_enableLimit(b2WheelJointDef* obj, bool value);
    BOX2D_API float b2WheelJointDef_get_lowerTranslation(b2WheelJointDef* obj);
    BOX2D_API void b2WheelJointDef_set_lowerTranslation(b2WheelJointDef* obj, float value);
    BOX2D_API float b2WheelJointDef_get_upperTranslation(b2WheelJointDef* obj);
    BOX2D_API void b2WheelJointDef_set_upperTranslation(b2WheelJointDef* obj, float value);
    BOX2D_API bool b2WheelJointDef_get_enableMotor(b2WheelJointDef* obj);
    BOX2D_API void b2WheelJointDef_set_enableMotor(b2WheelJointDef* obj, bool value);
    BOX2D_API float b2WheelJointDef_get_maxMotorTorque(b2WheelJointDef* obj);
    BOX2D_API void b2WheelJointDef_set_maxMotorTorque(b2WheelJointDef* obj, float value);
    BOX2D_API float b2WheelJointDef_get_motorSpeed(b2WheelJointDef* obj);
    BOX2D_API void b2WheelJointDef_set_motorSpeed(b2WheelJointDef* obj, float value);
    BOX2D_API float b2WheelJointDef_get_stiffness(b2WheelJointDef* obj);
    BOX2D_API void b2WheelJointDef_set_stiffness(b2WheelJointDef* obj, float value);
    BOX2D_API float b2WheelJointDef_get_damping(b2WheelJointDef* obj);
    BOX2D_API void b2WheelJointDef_set_damping(b2WheelJointDef* obj, float value);
    BOX2D_API void b2WheelJointDef_reset(b2WheelJointDef* obj);
    BOX2D_API void b2WheelJointDef_delete(b2WheelJointDef* obj);

    /*
     * b2WheelJoint
     */
    BOX2D_API void b2WheelJoint_GetLocalAnchorA(b2WheelJoint* obj, b2Vec2* value);
    BOX2D_API void b2WheelJoint_GetLocalAnchorB(b2WheelJoint* obj, b2Vec2* value);
    BOX2D_API void b2WheelJoint_GetLocalAxisA(b2WheelJoint* obj, b2Vec2* value);
    BOX2D_API float b2WheelJoint_GetJointTranslation(b2WheelJoint* obj);
    BOX2D_API float b2WheelJoint_GetJointLinearSpeed(b2WheelJoint* obj);
    BOX2D_API float b2WheelJoint_GetJointAngle(b2WheelJoint* obj);
    BOX2D_API float b2WheelJoint_GetJointAngularSpeed(b2WheelJoint* obj);
    BOX2D_API bool b2WheelJoint_IsLimitEnabled(b2WheelJoint* obj);
    BOX2D_API void b2WheelJoint_EnableLimit(b2WheelJoint* obj, bool flag);
    BOX2D_API float b2WheelJoint_GetLowerLimit(b2WheelJoint* obj);
    BOX2D_API float b2WheelJoint_GetUpperLimit(b2WheelJoint* obj);
    BOX2D_API void b2WheelJoint_SetLimits(b2WheelJoint* obj, float lower, float upper);
    BOX2D_API bool b2WheelJoint_IsMotorEnabled(b2WheelJoint* obj);
    BOX2D_API void b2WheelJoint_EnableMotor(b2WheelJoint* obj, bool flag);
    BOX2D_API float b2WheelJoint_GetMotorSpeed(b2WheelJoint* obj);
    BOX2D_API void b2WheelJoint_SetMotorSpeed(b2WheelJoint* obj, float speed);
    BOX2D_API float b2WheelJoint_GetMaxMotorTorque(b2WheelJoint* obj);
    BOX2D_API void b2WheelJoint_SetMaxMotorTorque(b2WheelJoint* obj, float torque);
    BOX2D_API float b2WheelJoint_GetMotorTorque(b2WheelJoint* obj, float inv_dt);
    BOX2D_API float b2WheelJoint_GetStiffness(b2WheelJoint* obj);
    BOX2D_API void b2WheelJoint_SetStiffness(b2WheelJoint* obj, float stiffness);
    BOX2D_API float b2WheelJoint_GetDamping(b2WheelJoint* obj);
    BOX2D_API void b2WheelJoint_SetDamping(b2WheelJoint* obj, float damping);
}
