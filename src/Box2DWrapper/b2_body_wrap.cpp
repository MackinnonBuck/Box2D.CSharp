#include "pch.h"
#include "verify.h"
#include "b2_body_wrap.h"

#include <box2d/b2_fixture.h>

/*
 * b2BodyDef
 */

b2BodyDef* b2BodyDef_new()
{
    return new b2BodyDef;
}

b2BodyType b2BodyDef_get_type(b2BodyDef* obj)
{
    VERIFY_INSTANCE;
    return obj->type;
}

void b2BodyDef_set_type(b2BodyDef* obj, b2BodyType value)
{
    VERIFY_INSTANCE;
    obj->type = value;
}

void b2BodyDef_get_position(b2BodyDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->position;
}

void b2BodyDef_set_position(b2BodyDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->position = *value;
}

float b2BodyDef_get_angle(b2BodyDef* obj)
{
    VERIFY_INSTANCE;
    return obj->angle;
}

void b2BodyDef_set_angle(b2BodyDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->angle = value;
}

void b2BodyDef_get_linearVelocity(b2BodyDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->linearVelocity;
}

void b2BodyDef_set_linearVelocity(b2BodyDef* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->linearVelocity = *value;
}

float b2BodyDef_get_angularVelocity(b2BodyDef* obj)
{
    VERIFY_INSTANCE;
    return obj->angularVelocity;
}

void b2BodyDef_set_angularVelocity(b2BodyDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->angularVelocity = value;
}

float b2BodyDef_get_linearDamping(b2BodyDef* obj)
{
    VERIFY_INSTANCE;
    return obj->linearDamping;
}

void b2BodyDef_set_linearDamping(b2BodyDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->linearDamping = value;
}

float b2BodyDef_get_angularDamping(b2BodyDef* obj)
{
    VERIFY_INSTANCE;
    return obj->angularDamping;
}

void b2BodyDef_set_angularDamping(b2BodyDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->angularDamping = value;
}

bool b2BodyDef_get_allowSleep(b2BodyDef* obj)
{
    VERIFY_INSTANCE;
    return obj->allowSleep;
}

void b2BodyDef_set_allowSleep(b2BodyDef* obj, bool value)
{
    VERIFY_INSTANCE;
    obj->allowSleep = value;
}

bool b2BodyDef_get_awake(b2BodyDef* obj)
{
    VERIFY_INSTANCE;
    return obj->awake;
}

void b2BodyDef_set_awake(b2BodyDef* obj, bool value)
{
    VERIFY_INSTANCE;
    obj->awake = value;
}

bool b2BodyDef_get_fixedRotation(b2BodyDef* obj)
{
    VERIFY_INSTANCE;
    return obj->fixedRotation;
}

void b2BodyDef_set_fixedRotation(b2BodyDef* obj, bool value)
{
    VERIFY_INSTANCE;
    obj->fixedRotation = value;
}

bool b2BodyDef_get_bullet(b2BodyDef* obj)
{
    VERIFY_INSTANCE;
    return obj->bullet;
}

void b2BodyDef_set_bullet(b2BodyDef* obj, bool value)
{
    VERIFY_INSTANCE;
    obj->bullet = value;
}

bool b2BodyDef_get_enabled(b2BodyDef* obj)
{
    VERIFY_INSTANCE;
    return obj->enabled;
}

void b2BodyDef_set_enabled(b2BodyDef* obj, bool value)
{
    VERIFY_INSTANCE;
    obj->enabled = value;
}

uintptr_t b2BodyDef_get_userData(b2BodyDef* obj)
{
    VERIFY_INSTANCE;
    return obj->userData.pointer;
}

void b2BodyDef_set_userData(b2BodyDef* obj, uintptr_t value)
{
    VERIFY_INSTANCE;
    obj->userData.pointer = value;
}

float b2BodyDef_get_gravityScale(b2BodyDef* obj)
{
    VERIFY_INSTANCE;
    return obj->gravityScale;
}

void b2BodyDef_set_gravityScale(b2BodyDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->gravityScale = value;
}

void b2BodyDef_reset(b2BodyDef* obj)
{
    VERIFY_INSTANCE;
    *obj = b2BodyDef();
}

void b2BodyDef_delete(b2BodyDef* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}

/*
 * b2Body
 */

b2Fixture* b2Body_CreateFixture(b2Body* obj, b2FixtureDef* def, uintptr_t userData)
{
    VERIFY_INSTANCE;
    def->userData.pointer = userData;
    return obj->CreateFixture(def);
}

b2Fixture* b2Body_CreateFixture2(b2Body* obj, b2Shape* shape, float density, uintptr_t userData)
{
    VERIFY_INSTANCE;
    b2FixtureDef def;
    def.shape = shape;
    def.density = density;
    def.userData.pointer = userData;

    return obj->CreateFixture(&def);
}

void b2Body_DestroyFixture(b2Body* obj, b2Fixture* fixture)
{
    VERIFY_INSTANCE;
    obj->DestroyFixture(fixture);
}

void b2Body_GetTransform(b2Body* obj, b2Transform* transform)
{
    VERIFY_INSTANCE;
    *transform = obj->GetTransform();
}

void b2Body_SetTransform(b2Body* obj, b2Vec2* position, float angle)
{
    VERIFY_INSTANCE;
    obj->SetTransform(*position, angle);
}

void b2Body_ApplyForce(b2Body* obj, b2Vec2* force, b2Vec2* point, bool wake)
{
    VERIFY_INSTANCE;
    obj->ApplyForce(*force, *point, wake);
}

void b2Body_ApplyForceToCenter(b2Body* obj, b2Vec2* force, bool wake)
{
    VERIFY_INSTANCE;
    obj->ApplyForceToCenter(*force, wake);
}

void b2Body_ApplyTorque(b2Body* obj, float torque, bool wake)
{
    VERIFY_INSTANCE;
    obj->ApplyTorque(torque, wake);
}

void b2Body_ApplyLinearImpulse(b2Body* obj, b2Vec2* impulse, b2Vec2* point, bool wake)
{
    VERIFY_INSTANCE;
    obj->ApplyLinearImpulse(*impulse, *point, wake);
}

void b2Body_ApplyLinearImpulseToCenter(b2Body* obj, b2Vec2* impulse, bool wake)
{
    VERIFY_INSTANCE;
    obj->ApplyLinearImpulseToCenter(*impulse, wake);
}

void b2Body_ApplyAngularImpulse(b2Body* obj, float impulse, bool wake)
{
    VERIFY_INSTANCE;
    obj->ApplyAngularImpulse(impulse, wake);
}

void b2Body_GetWorldPoint(b2Body* obj, b2Vec2* localPoint, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetWorldPoint(*localPoint);
}

void b2Body_GetWorldVector(b2Body* obj, b2Vec2* localVector, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetWorldVector(*localVector);
}

void b2Body_GetLocalPoint(b2Body* obj, b2Vec2* worldPoint, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLocalPoint(*worldPoint);
}

void b2Body_GetLocalVector(b2Body* obj, b2Vec2* worldVector, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLocalVector(*worldVector);
}

void b2Body_GetLinearVelocityFromWorldPoint(b2Body* obj, b2Vec2* worldPoint, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLinearVelocityFromWorldPoint(*worldPoint);
}

void b2Body_GetLinearVelocityFromLocalPoint(b2Body* obj, b2Vec2* localPoint, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLinearVelocityFromLocalPoint(*localPoint);
}

void b2Body_GetPosition(b2Body* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetPosition();
}

void b2Body_SetPosition(b2Body* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    obj->SetTransform(*value, obj->GetAngle());
}

float b2Body_GetAngle(b2Body* obj)
{
    VERIFY_INSTANCE;
    return obj->GetAngle();
}

void b2Body_SetAngle(b2Body* obj, float value)
{
    VERIFY_INSTANCE;
    obj->SetTransform(obj->GetPosition(), value);
}

void b2Body_GetWorldCenter(b2Body* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetWorldCenter();
}

void b2Body_GetLocalCenter(b2Body* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLocalCenter();
}

float b2Body_GetGravityScale(b2Body* obj)
{
    VERIFY_INSTANCE;
    return obj->GetGravityScale();
}

void b2Body_SetGravityScale(b2Body* obj, float scale)
{
    VERIFY_INSTANCE;
    obj->SetGravityScale(scale);
}

void b2Body_GetLinearVelocity(b2Body* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetLinearVelocity();
}

void b2Body_SetLinearVelocity(b2Body* obj, b2Vec2* v)
{
    VERIFY_INSTANCE;
    obj->SetLinearVelocity(*v);
}

float b2Body_GetAngularVelocity(b2Body* obj)
{
    VERIFY_INSTANCE;
    return obj->GetAngularVelocity();
}

void b2Body_SetAngularVelocity(b2Body* obj, float omega)
{
    VERIFY_INSTANCE;
    obj->SetAngularVelocity(omega);
}

float b2Body_GetMass(b2Body* obj)
{
    VERIFY_INSTANCE;
    return obj->GetMass();
}

float b2Body_GetInertia(b2Body* obj)
{
    VERIFY_INSTANCE;
    return obj->GetInertia();
}

b2BodyType b2Body_GetType(b2Body* obj)
{
    VERIFY_INSTANCE;
    return obj->GetType();
}

void b2Body_SetType(b2Body* obj, b2BodyType type)
{
    VERIFY_INSTANCE;
    obj->SetType(type);
}

bool b2Body_IsAwake(b2Body* obj)
{
    VERIFY_INSTANCE;
    return obj->IsAwake();
}

void b2Body_SetAwake(b2Body* obj, bool flag)
{
    VERIFY_INSTANCE;
    obj->SetAwake(flag);
}

b2Fixture* b2Body_GetFixtureList(b2Body* obj)
{
    VERIFY_INSTANCE;
    return obj->GetFixtureList();
}

b2JointEdge* b2Body_GetJointList(b2Body* obj)
{
    VERIFY_INSTANCE;
    return obj->GetJointList();
}

b2Body* b2Body_GetNext(b2Body* obj)
{
    VERIFY_INSTANCE;
    return obj->GetNext();
}

uintptr_t b2Body_GetUserData(b2Body* obj)
{
    VERIFY_INSTANCE;
    return obj->GetUserData().pointer;
}
