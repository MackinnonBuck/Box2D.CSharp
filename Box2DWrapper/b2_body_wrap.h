#pragma once

#include "api.h"

#include <box2d/b2_body.h>

extern "C"
{
    BOX2D_API b2Fixture* b2Body_CreateFixture(b2Body* obj, b2FixtureDef* def);
    BOX2D_API void b2Body_GetPosition(b2Body* obj, b2Vec2* v);
    BOX2D_API float b2Body_GetAngle(b2Body* obj);
    BOX2D_API void b2Body_SetLinearVelocity(b2Body* obj, const b2Vec2* v);
    BOX2D_API b2Fixture* b2Body_GetFixtureList(b2Body* obj);
    BOX2D_API b2JointEdge* b2Body_GetJointList(b2Body* obj);
    BOX2D_API b2Body* b2Body_GetNext(b2Body* obj);
    BOX2D_API uintptr_t b2Body_GetUserData(b2Body* obj);
}
