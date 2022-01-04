#pragma once

#include "api.h"

#include <box2d/b2_friction_joint.h>

extern "C"
{
    /*
     * b2FrictionJointDef
     */
    BOX2D_API b2FrictionJointDef* b2FrictionJointDef_new();
    BOX2D_API void b2FrictionJointDef_Initialize(b2FrictionJointDef* obj, b2Body* bodyA, b2Body* bodyB, b2Vec2* anchor);
    BOX2D_API void b2FrictionJointDef_get_localAnchorA(b2FrictionJointDef* obj, b2Vec2* value);
    BOX2D_API void b2FrictionJointDef_set_localAnchorA(b2FrictionJointDef* obj, b2Vec2* value);
    BOX2D_API void b2FrictionJointDef_get_localAnchorB(b2FrictionJointDef* obj, b2Vec2* value);
    BOX2D_API void b2FrictionJointDef_set_localAnchorB(b2FrictionJointDef* obj, b2Vec2* value);
    BOX2D_API float b2FrictionJointDef_get_maxForce(b2FrictionJointDef* obj);
    BOX2D_API void b2FrictionJointDef_set_maxForce(b2FrictionJointDef* obj, float value);
    BOX2D_API float b2FrictionJointDef_get_maxTorque(b2FrictionJointDef* obj);
    BOX2D_API void b2FrictionJointDef_set_maxTorque(b2FrictionJointDef* obj, float value);
    BOX2D_API void b2FrictionJointDef_reset(b2FrictionJointDef* obj);
    BOX2D_API void b2FrictionJointDef_delete(b2FrictionJointDef* obj);

    /*
     * b2FrictionJoint
     */
    BOX2D_API void b2FrictionJoint_GetLocalAnchorA(b2FrictionJoint* obj, b2Vec2* value);
    BOX2D_API void b2FrictionJoint_GetLocalAnchorB(b2FrictionJoint* obj, b2Vec2* value);
    BOX2D_API float b2FrictionJoint_GetMaxForce(b2FrictionJoint* obj);
    BOX2D_API void b2FrictionJoint_SetMaxForce(b2FrictionJoint* obj, float force);
    BOX2D_API float b2FrictionJoint_GetMaxTorque(b2FrictionJoint* obj);
    BOX2D_API void b2FrictionJoint_SetMaxTorque(b2FrictionJoint* obj, float torque);
}
