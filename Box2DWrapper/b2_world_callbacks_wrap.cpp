#include "pch.h"
#include "verify.h"
#include "b2_world_callbacks_wrap.h"

b2ContactListenerWrapper::b2ContactListenerWrapper(
    b2ContactListener_callback_BeginContact beginContact,
    b2ContactListener_callback_EndContact endContact,
    b2ContactListener_callback_PreSolve preSolve,
    b2ContactListener_callback_PostSolve postSolve)
    : m_beginContact(beginContact)
    , m_endContact(endContact)
    , m_preSolve(preSolve)
    , m_postSolve(postSolve)
{
}

void b2ContactListenerWrapper::BeginContact(b2Contact* contact)
{
    m_beginContact(contact);
}

void b2ContactListenerWrapper::EndContact(b2Contact* contact)
{
    m_endContact(contact);
}

void b2ContactListenerWrapper::PreSolve(b2Contact* contact, const b2Manifold* oldManifold)
{
    m_preSolve(contact, oldManifold);
}

void b2ContactListenerWrapper::PostSolve(b2Contact* contact, const b2ContactImpulse* impulse)
{
    m_postSolve(contact, impulse);
}

/*
 * b2ContactImpulse
 */

void b2ContactImpulse_get_impulses(b2ContactImpulse* obj, float** normalImpulses, float** tangentImpulses, int32* count)
{
    VERIFY_INSTANCE;
    *normalImpulses = obj->normalImpulses;
    *tangentImpulses = obj->tangentImpulses;
    *count = obj->count;
}

/*
 * b2ContactListenerWrapper
 */

b2ContactListenerWrapper* b2ContactListenerWrapper_new(
    b2ContactListener_callback_BeginContact beginContact,
    b2ContactListener_callback_EndContact endContact,
    b2ContactListener_callback_PreSolve preSolve,
    b2ContactListener_callback_PostSolve postSolve)
{
    return new b2ContactListenerWrapper(beginContact, endContact, preSolve, postSolve);
}

void b2ContactListenerWrapper_delete(b2ContactListenerWrapper* obj)
{
    delete obj;
}
