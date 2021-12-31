#pragma once

#include "api.h"

#include <box2d/b2_world.h>
#include <box2d/b2_body.h>

extern "C"
{
    BOX2D_API b2World* b2World_new(b2Vec2* gravity);
    BOX2D_API void b2World_SetDestructionListener(b2World* obj, b2DestructionListener* listener);
    BOX2D_API void b2World_SetContactListener(b2World* obj, b2ContactListener* listener);
    BOX2D_API void b2World_SetDebugDraw(b2World* obj, b2Draw* debugDraw);
    BOX2D_API b2Body* b2World_CreateBody(b2World* obj, b2BodyDef* def, uintptr_t userData);
    BOX2D_API b2Body* b2World_CreateBody2(b2World* obj, b2BodyType type, b2Vec2* position, float angle, uintptr_t userData);
    BOX2D_API void b2World_DestroyBody(b2World* obj, b2Body* body);
    BOX2D_API b2Joint* b2World_CreateJoint(b2World* obj, b2JointDef* def, uintptr_t userData);
    BOX2D_API void b2World_DestroyJoint(b2World* obj, b2Joint* joint);
    BOX2D_API void b2World_Step(b2World* obj, float timeStep, int32 velocityIterations, int32 positionIterations);
    BOX2D_API void b2World_ClearForces(b2World* obj);
    BOX2D_API void b2World_DebugDraw(b2World* obj);
    BOX2D_API void b2World_QueryAABB(b2World* obj, b2QueryCallback* callback, b2AABB* aabb);
    BOX2D_API void b2World_RayCast(b2World* obj, b2RayCastCallback* callback, b2Vec2* point1, b2Vec2* point2);
    BOX2D_API b2Body* b2World_GetBodyList(b2World* obj);
    BOX2D_API b2Joint* b2World_GetJointList(b2World* obj);
    BOX2D_API b2Contact* b2World_GetContactList(b2World* obj);
    BOX2D_API bool b2World_GetAllowSleeping(b2World* obj);
    BOX2D_API void b2World_SetAllowSleeping(b2World* obj, bool flag);
    BOX2D_API bool b2World_GetWarmStarting(b2World* obj);
    BOX2D_API void b2World_SetWarmStarting(b2World* obj, bool flag);
    BOX2D_API bool b2World_GetContinuousPhysics(b2World* obj);
    BOX2D_API void b2World_SetContinuousPhysics(b2World* obj, bool flag);
    BOX2D_API bool b2World_GetSubStepping(b2World* obj);
    BOX2D_API void b2World_SetSubStepping(b2World* obj, bool flag);
    BOX2D_API int b2World_GetProxyCount(b2World* obj);
    BOX2D_API int b2World_GetBodyCount(b2World* obj);
    BOX2D_API int b2World_GetJointCount(b2World* obj);
    BOX2D_API int b2World_GetContactCount(b2World* obj);
    BOX2D_API int b2World_GetTreeHeight(b2World* obj);
    BOX2D_API int b2World_GetTreeBalance(b2World* obj);
    BOX2D_API float b2World_GetTreeQuality(b2World* obj);
    BOX2D_API void b2World_GetGravity(b2World* obj, b2Vec2* value);
    BOX2D_API void b2World_SetGravity(b2World* obj, b2Vec2* gravity);
    BOX2D_API void b2World_ShiftOrigin(b2World* obj, b2Vec2* newOrigin);
    BOX2D_API void b2World_GetProfile(b2World* obj, b2Profile* value);
    BOX2D_API void b2World_delete(b2World* obj);
}
