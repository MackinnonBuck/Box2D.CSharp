#pragma once

#include "api.h"

#include <box2d/b2_body.h>

extern "C"
{
    /*
     * b2BodyDef
     */
    BOX2D_API b2BodyDef* b2BodyDef_new();
    BOX2D_API b2BodyType b2BodyDef_get_type(b2BodyDef* obj);
    BOX2D_API void b2BodyDef_set_type(b2BodyDef* obj, b2BodyType value);
    BOX2D_API void b2BodyDef_get_position(b2BodyDef* obj, b2Vec2* value);
    BOX2D_API void b2BodyDef_set_position(b2BodyDef* obj, b2Vec2* value);
    BOX2D_API float b2BodyDef_get_angle(b2BodyDef* obj);
    BOX2D_API void b2BodyDef_set_angle(b2BodyDef* obj, float value);
    BOX2D_API void b2BodyDef_get_linearVelocity(b2BodyDef* obj, b2Vec2* value);
    BOX2D_API void b2BodyDef_set_linearVelocity(b2BodyDef* obj, b2Vec2* value);
    BOX2D_API float b2BodyDef_get_angularVelocity(b2BodyDef* obj);
    BOX2D_API void b2BodyDef_set_angularVelocity(b2BodyDef* obj, float value);
    BOX2D_API float b2BodyDef_get_linearDamping(b2BodyDef* obj);
    BOX2D_API void b2BodyDef_set_linearDamping(b2BodyDef* obj, float value);
    BOX2D_API float b2BodyDef_get_angularDamping(b2BodyDef* obj);
    BOX2D_API void b2BodyDef_set_angularDamping(b2BodyDef* obj, float value);
    BOX2D_API bool b2BodyDef_get_allowSleep(b2BodyDef* obj);
    BOX2D_API void b2BodyDef_set_allowSleep(b2BodyDef* obj, bool value);
    BOX2D_API bool b2BodyDef_get_awake(b2BodyDef* obj);
    BOX2D_API void b2BodyDef_set_awake(b2BodyDef* obj, bool value);
    BOX2D_API bool b2BodyDef_get_fixedRotation(b2BodyDef* obj);
    BOX2D_API void b2BodyDef_set_fixedRotation(b2BodyDef* obj, bool value);
    BOX2D_API bool b2BodyDef_get_bullet(b2BodyDef* obj);
    BOX2D_API void b2BodyDef_set_bullet(b2BodyDef* obj, bool value);
    BOX2D_API bool b2BodyDef_get_enabled(b2BodyDef* obj);
    BOX2D_API void b2BodyDef_set_enabled(b2BodyDef* obj, bool value);
    BOX2D_API uintptr_t b2BodyDef_get_userData(b2BodyDef* obj);
    BOX2D_API void b2BodyDef_set_userData(b2BodyDef* obj, uintptr_t value);
    BOX2D_API float b2BodyDef_get_gravityScale(b2BodyDef* obj);
    BOX2D_API void b2BodyDef_set_gravityScale(b2BodyDef* obj, float value);
    BOX2D_API void b2BodyDef_delete(b2BodyDef* obj);

    /*
     * b2Body
     */
    BOX2D_API b2Fixture* b2Body_CreateFixture(b2Body* obj, b2FixtureDef* def);
    BOX2D_API void b2Body_GetTransform(b2Body* obj, b2Transform* transform);
    BOX2D_API void b2Body_SetTransform(b2Body* obj, b2Vec2* position, float angle);
    BOX2D_API void b2Body_ApplyForce(b2Body* obj, b2Vec2* force, b2Vec2* point, bool wake);
    BOX2D_API void b2Body_ApplyForceToCenter(b2Body* obj, b2Vec2* force, bool wake);
    BOX2D_API void b2Body_ApplyTorque(b2Body* obj, float torque, bool wake);
    BOX2D_API void b2Body_ApplyLinearImpulse(b2Body* obj, b2Vec2* impulse, b2Vec2* point, bool wake);
    BOX2D_API void b2Body_ApplyLinearImpulseToCenter(b2Body* obj, b2Vec2* impulse, bool wake);
    BOX2D_API void b2Body_ApplyAngularImpulse(b2Body* obj, float impulse, bool wake);
    BOX2D_API void b2Body_GetPosition(b2Body* obj, b2Vec2* v);
    BOX2D_API float b2Body_GetAngle(b2Body* obj);
    BOX2D_API void b2Body_GetLinearVelocity(b2Body* obj, b2Vec2* value);
    BOX2D_API void b2Body_SetLinearVelocity(b2Body* obj, b2Vec2* v);
    BOX2D_API float b2Body_GetAngularVelocity(b2Body* obj);
    BOX2D_API void b2Body_SetAngularVelocity(b2Body* obj, float omega);
    BOX2D_API float b2Body_GetMass(b2Body* obj);
    BOX2D_API bool b2Body_IsAwake(b2Body* obj);
    BOX2D_API void b2Body_SetAwake(b2Body* obj, bool flag);
    BOX2D_API b2Fixture* b2Body_GetFixtureList(b2Body* obj);
    BOX2D_API b2JointEdge* b2Body_GetJointList(b2Body* obj);
    BOX2D_API b2Body* b2Body_GetNext(b2Body* obj);
    BOX2D_API uintptr_t b2Body_GetUserData(b2Body* obj);
}
