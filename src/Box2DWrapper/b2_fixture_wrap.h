#pragma once

#include "api.h"

#include <box2d/b2_fixture.h>

extern "C"
{
    /*
     * b2FixtureDef
     */
    BOX2D_API b2FixtureDef* b2FixtureDef_new();
    BOX2D_API const b2Shape* b2FixtureDef_get_shape(b2FixtureDef* obj);
    BOX2D_API void b2FixtureDef_set_shape(b2FixtureDef* obj, b2Shape* value);
    BOX2D_API uintptr_t b2FixtureDef_get_userData(b2FixtureDef* obj);
    BOX2D_API void b2FixtureDef_set_userData(b2FixtureDef* obj, uintptr_t value);
    BOX2D_API float b2FixtureDef_get_friction(b2FixtureDef* obj);
    BOX2D_API void b2FixtureDef_set_friction(b2FixtureDef* obj, float value);
    BOX2D_API float b2FixtureDef_get_restitution(b2FixtureDef* obj);
    BOX2D_API void b2FixtureDef_set_restitution(b2FixtureDef* obj, float value);
    BOX2D_API float b2FixtureDef_get_restitutionThreshold(b2FixtureDef* obj);
    BOX2D_API void b2FixtureDef_set_restitutionThreshold(b2FixtureDef* obj, float value);
    BOX2D_API float b2FixtureDef_get_density(b2FixtureDef* obj);
    BOX2D_API void b2FixtureDef_set_density(b2FixtureDef* obj, float value);
    BOX2D_API bool b2FixtureDef_get_isSensor(b2FixtureDef* obj);
    BOX2D_API void b2FixtureDef_set_isSensor(b2FixtureDef* obj, bool value);
    BOX2D_API void b2FixtureDef_get_filter(b2FixtureDef* obj, b2Filter* filter);
    BOX2D_API void b2FixtureDef_set_filter(b2FixtureDef* obj, b2Filter* filter);
    BOX2D_API void b2FixtureDef_reset(b2FixtureDef* obj);
    BOX2D_API void b2FixtureDef_delete(b2FixtureDef* obj);

    /*
     * b2Fixture
     */
    BOX2D_API b2Shape::Type b2Fixture_GetType(b2Fixture* obj);
    BOX2D_API b2Shape* b2Fixture_GetShape(b2Fixture* obj);
    BOX2D_API bool b2Fixture_IsSensor(b2Fixture* obj);
    BOX2D_API void b2Fixture_SetSensor(b2Fixture* obj, bool sensor);
    BOX2D_API b2Body* b2Fixture_GetBody(b2Fixture* obj);
    BOX2D_API b2Fixture* b2Fixture_GetNext(b2Fixture* obj);
    BOX2D_API uintptr_t b2Fixture_GetUserData(b2Fixture* obj);
    BOX2D_API bool b2Fixture_TestPoint(b2Fixture* obj, b2Vec2* p);
}
