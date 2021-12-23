#include "pch.h"
#include "verify.h"
#include "b2_world_wrap.h"

b2World* b2World_new(b2Vec2* gravity)
{
    return new b2World(*gravity);
}

void b2World_SetContactListener(b2World* obj, b2ContactListener* listener)
{
    VERIFY_INSTANCE;
    obj->SetContactListener(listener);
}

void b2World_SetDebugDraw(b2World* obj, b2Draw* debugDraw)
{
    VERIFY_INSTANCE;
    obj->SetDebugDraw(debugDraw);
}

b2Body* b2World_CreateBody(b2World* obj, b2BodyDef* def)
{
    VERIFY_INSTANCE;
    return obj->CreateBody(def);
}

void b2World_DestroyBody(b2World* obj, b2Body* body)
{
    VERIFY_INSTANCE;
    obj->DestroyBody(body);
}

b2Joint* b2World_CreateJoint(b2World* obj, b2JointDef* def)
{
    VERIFY_INSTANCE;
    return obj->CreateJoint(def);
}

void b2World_DestroyJoint(b2World* obj, b2Joint* joint)
{
    VERIFY_INSTANCE;
    obj->DestroyJoint(joint);
}

void b2World_Step(b2World* obj, float timeStep, int32 velocityIterations, int32 positionIterations)
{
    VERIFY_INSTANCE;
    obj->Step(timeStep, velocityIterations, positionIterations);
}

void b2World_ClearForces(b2World* obj)
{
    VERIFY_INSTANCE;
    obj->ClearForces();
}

void b2World_DebugDraw(b2World* obj)
{
    VERIFY_INSTANCE;
    obj->DebugDraw();
}

b2Body* b2World_GetBodyList(b2World* obj)
{
    VERIFY_INSTANCE;
    return obj->GetBodyList();
}

b2Joint* b2World_GetJointList(b2World* obj)
{
    VERIFY_INSTANCE;
    return obj->GetJointList();
}

b2Contact* b2World_GetContactList(b2World* obj)
{
    VERIFY_INSTANCE;
    return obj->GetContactList();
}

bool b2World_GetAllowSleeping(b2World* obj)
{
    VERIFY_INSTANCE;
    return obj->GetAllowSleeping();
}

void b2World_SetAllowSleeping(b2World* obj, bool flag)
{
    VERIFY_INSTANCE;
    obj->SetAllowSleeping(flag);
}

bool b2World_GetWarmStarting(b2World* obj)
{
    VERIFY_INSTANCE;
    return obj->GetWarmStarting();
}

void b2World_SetWarmStarting(b2World* obj, bool flag)
{
    VERIFY_INSTANCE;
    obj->SetWarmStarting(flag);
}

bool b2World_GetContinuousPhysics(b2World* obj)
{
    VERIFY_INSTANCE;
    return obj->GetContinuousPhysics();
}

void b2World_SetContinuousPhysics(b2World* obj, bool flag)
{
    VERIFY_INSTANCE;
    obj->SetContinuousPhysics(flag);
}

bool b2World_GetSubStepping(b2World* obj)
{
    VERIFY_INSTANCE;
    return obj->GetSubStepping();
}

void b2World_SetSubStepping(b2World* obj, bool flag)
{
    VERIFY_INSTANCE;
    obj->SetSubStepping(flag);
}

void b2World_GetGravity(b2World* obj, b2Vec2* value)
{
    VERIFY_INSTANCE;
    *value = obj->GetGravity();
}

void b2World_SetGravity(b2World* obj, b2Vec2* gravity)
{
    VERIFY_INSTANCE;
    obj->SetGravity(*gravity);
}

void b2World_delete(b2World* obj)
{
    VERIFY_INSTANCE;
    delete obj;
}
