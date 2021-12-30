#include "pch.h"
#include "verify.h"
#include "b2_contact_wrap.h"

b2Manifold* b2Contact_GetManifold(b2Contact* obj)
{
    VERIFY_INSTANCE;
    return obj->GetManifold();
}

void b2Contact_GetWorldManifold(b2Contact* obj, b2WorldManifold* worldManifold)
{
    VERIFY_INSTANCE;
    obj->GetWorldManifold(worldManifold);
}

bool b2Contact_IsTouching(b2Contact* obj)
{
    VERIFY_INSTANCE;
    return obj->IsTouching();
}

bool b2Contact_IsEnabled(b2Contact* obj)
{
    VERIFY_INSTANCE;
    return obj->IsEnabled();
}

void b2Contact_SetEnabled(b2Contact* obj, bool flag)
{
    VERIFY_INSTANCE;
    obj->SetEnabled(flag);
}

b2Contact* b2Contact_GetNext(b2Contact* obj)
{
    VERIFY_INSTANCE;
    return obj->GetNext();
}

b2Fixture* b2Contact_GetFixtureA(b2Contact* obj)
{
    VERIFY_INSTANCE;
    return obj->GetFixtureA();
}

int b2Contact_GetChildIndexA(b2Contact* obj)
{
    VERIFY_INSTANCE;
    return obj->GetChildIndexA();
}

b2Fixture* b2Contact_GetFixtureB(b2Contact* obj)
{
    VERIFY_INSTANCE;
    return obj->GetFixtureB();
}

int b2Contact_GetChildIndexB(b2Contact* obj)
{
    VERIFY_INSTANCE;
    return obj->GetChildIndexB();
}

float b2Contact_GetFriction(b2Contact* obj)
{
    VERIFY_INSTANCE;
    return obj->GetFriction();
}

void b2Contact_SetFriction(b2Contact* obj, float friction)
{
    VERIFY_INSTANCE;
    obj->SetFriction(friction);
}

void b2Contact_ResetFriction(b2Contact* obj)
{
    VERIFY_INSTANCE;
    obj->ResetFriction();
}

float b2Contact_GetRestitution(b2Contact* obj)
{
    VERIFY_INSTANCE;
    return obj->GetRestitution();
}

void b2Contact_SetRestitution(b2Contact* obj, float restitution)
{
    VERIFY_INSTANCE;
    obj->SetRestitution(restitution);
}

void b2Contact_ResetRestitution(b2Contact* obj)
{
    VERIFY_INSTANCE;
    obj->ResetRestitution();
}

float b2Contact_GetRestitutionThreshold(b2Contact* obj)
{
    VERIFY_INSTANCE;
    return obj->GetRestitutionThreshold();
}

void b2Contact_SetRestitutionThreshold(b2Contact* obj, float threshold)
{
    VERIFY_INSTANCE;
    obj->SetRestitutionThreshold(threshold);
}

void b2Contact_ResetRestitutionThreshold(b2Contact* obj)
{
    VERIFY_INSTANCE;
    obj->ResetRestitutionThreshold();
}

float b2Contact_GetTangentSpeed(b2Contact* obj)
{
    VERIFY_INSTANCE;
    return obj->GetTangentSpeed();
}

void b2Contact_SetTangentSpeed(b2Contact* obj, float speed)
{
    VERIFY_INSTANCE;
    obj->SetTangentSpeed(speed);
}

void b2Contact_Evaluate(b2Contact* obj, b2Manifold* manifold, b2Transform* xfA, b2Transform* xfB)
{
    VERIFY_INSTANCE;
    obj->Evaluate(manifold, *xfA, *xfB);
}
