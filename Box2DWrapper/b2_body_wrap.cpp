#include "pch.h"
#include "verify.h"
#include "b2_body_wrap.h"

b2Fixture* b2Body_CreateFixture(b2Body* obj, b2FixtureDef* def)
{
    VERIFY_INSTANCE;
    return obj->CreateFixture(def);
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

void b2Body_GetPosition(b2Body* obj, b2Vec2* v)
{
    VERIFY_INSTANCE;
    *v = obj->GetPosition();
}

float b2Body_GetAngle(b2Body* obj)
{
    VERIFY_INSTANCE;
    return obj->GetAngle();
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
