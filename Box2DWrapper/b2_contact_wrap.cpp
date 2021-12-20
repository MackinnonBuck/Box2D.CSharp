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
