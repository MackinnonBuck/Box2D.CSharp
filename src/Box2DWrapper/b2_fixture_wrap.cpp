#include "pch.h"
#include "verify.h"
#include "b2_fixture_wrap.h"

/*
 * b2FixtureDef
 */

b2FixtureDef* b2FixtureDef_new()
{
    return new b2FixtureDef;
}

const b2Shape* b2FixtureDef_get_shape(b2FixtureDef* obj)
{
    VERIFY_INSTANCE;
    return obj->shape;
}

void b2FixtureDef_set_shape(b2FixtureDef* obj, b2Shape* value)
{
    VERIFY_INSTANCE;
    obj->shape = value;
}

uintptr_t b2FixtureDef_get_userData(b2FixtureDef* obj)
{
    VERIFY_INSTANCE;
    return obj->userData.pointer;
}

void b2FixtureDef_set_userData(b2FixtureDef* obj, uintptr_t value)
{
    VERIFY_INSTANCE;
    obj->userData.pointer = value;
}

float b2FixtureDef_get_friction(b2FixtureDef* obj)
{
    VERIFY_INSTANCE;
    return obj->friction;
}

void b2FixtureDef_set_friction(b2FixtureDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->friction = value;
}

float b2FixtureDef_get_restitution(b2FixtureDef* obj)
{
    VERIFY_INSTANCE;
    return obj->restitution;
}

void b2FixtureDef_set_restitution(b2FixtureDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->restitution = value;
}

float b2FixtureDef_get_restitutionThreshold(b2FixtureDef* obj)
{
    VERIFY_INSTANCE;
    return obj->restitutionThreshold;
}

void b2FixtureDef_set_restitutionThreshold(b2FixtureDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->restitutionThreshold = value;
}

float b2FixtureDef_get_density(b2FixtureDef* obj)
{
    VERIFY_INSTANCE;
    return obj->density;
}

void b2FixtureDef_set_density(b2FixtureDef* obj, float value)
{
    VERIFY_INSTANCE;
    obj->density = value;
}

bool b2FixtureDef_get_isSensor(b2FixtureDef* obj)
{
    VERIFY_INSTANCE;
    return obj->isSensor;
}

void b2FixtureDef_set_isSensor(b2FixtureDef* obj, bool value)
{
    VERIFY_INSTANCE;
    obj->isSensor = value;
}

void b2FixtureDef_get_filter(b2FixtureDef* obj, b2Filter* filter)
{
    VERIFY_INSTANCE;
    *filter = obj->filter;
}

void b2FixtureDef_set_filter(b2FixtureDef* obj, b2Filter* filter)
{
    VERIFY_INSTANCE;
    obj->filter = *filter;
}

void b2FixtureDef_reset(b2FixtureDef* obj)
{
    VERIFY_INSTANCE;
    *obj = b2FixtureDef();
}

void b2FixtureDef_delete(b2FixtureDef* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}

/*
 * b2Fixture
 */

b2Shape::Type b2Fixture_GetType(b2Fixture* obj)
{
    VERIFY_INSTANCE;
    return obj->GetType();
}

b2Shape* b2Fixture_GetShape(b2Fixture* obj)
{
    VERIFY_INSTANCE;
    return obj->GetShape();
}

bool b2Fixture_IsSensor(b2Fixture* obj)
{
    VERIFY_INSTANCE;
    return obj->IsSensor();
}

void b2Fixture_SetSensor(b2Fixture* obj, bool sensor)
{
    VERIFY_INSTANCE;
    obj->SetSensor(sensor);
}

b2Body* b2Fixture_GetBody(b2Fixture* obj)
{
    VERIFY_INSTANCE;
    return obj->GetBody();
}

b2Fixture* b2Fixture_GetNext(b2Fixture* obj)
{
    VERIFY_INSTANCE;
    return obj->GetNext();
}

uintptr_t b2Fixture_GetUserData(b2Fixture* obj)
{
    VERIFY_INSTANCE;
    return obj->GetUserData().pointer;
}

bool b2Fixture_TestPoint(b2Fixture* obj, b2Vec2* p)
{
    VERIFY_INSTANCE;
    return obj->TestPoint(*p);
}
