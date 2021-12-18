#pragma once

#include "api.h"

#include <box2d/b2_joint.h>

extern "C"
{
    /*
     * b2JointDef
     */
    BOX2D_API b2JointType b2JointDef_get_type(b2JointDef* obj);
    BOX2D_API void b2JointDef_set_type(b2JointDef* obj, b2JointType value);
    BOX2D_API uintptr_t b2JointDef_get_userData(b2JointDef* obj);
    BOX2D_API void b2JointDef_set_userData(b2JointDef* obj, uintptr_t value);
    BOX2D_API b2Body* b2JointDef_get_bodyA(b2JointDef* obj);
    BOX2D_API void b2JointDef_set_bodyA(b2JointDef* obj, b2Body* value);
    BOX2D_API b2Body* b2JointDef_get_bodyB(b2JointDef* obj);
    BOX2D_API void b2JointDef_set_bodyB(b2JointDef* obj, b2Body* value);
    BOX2D_API bool b2JointDef_get_collideConnected(b2JointDef* obj);
    BOX2D_API void b2JointDef_set_collideConnected(b2JointDef* obj, bool value);

    /*
     * b2Joint
     */
    BOX2D_API b2Body* b2Joint_GetBodyA(b2Joint* obj);
    BOX2D_API b2Body* b2Joint_GetBodyB(b2Joint* obj);
    BOX2D_API void b2Joint_GetAnchorA(b2Joint* obj, b2Vec2* value);
    BOX2D_API void b2Joint_GetAnchorB(b2Joint* obj, b2Vec2* value);
    BOX2D_API void b2Joint_GetReactionForce(b2Joint* obj, float inv_dt, b2Vec2* value);
    BOX2D_API float b2Joint_GetReactionTorque(b2Joint* obj, float inv_dt);
    BOX2D_API b2Joint* b2Joint_GetNext(b2Joint* obj);
    BOX2D_API uintptr_t b2Joint_GetUserData(b2Joint* obj);
    BOX2D_API bool b2Joint_IsEnabled(b2Joint* obj);
    BOX2D_API bool b2Joint_GetCollideConnected(b2Joint* obj);
    BOX2D_API void b2Joint_ShiftOrigin(b2Joint* obj, b2Vec2* newOrigin);
}
