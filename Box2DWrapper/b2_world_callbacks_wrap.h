#pragma once

#include "api.h"

#include <box2d/b2_world.h>

typedef void (*b2ContactListener_callback_BeginContact)(b2Contact* contact);
typedef void (*b2ContactListener_callback_EndContact)(b2Contact* contact);
typedef void (*b2ContactListener_callback_PreSolve)(b2Contact* contact, const b2Manifold* oldManifold);
typedef void (*b2ContactListener_callback_PostSolve)(b2Contact* contact, const b2ContactImpulse* impulse);

class b2ContactListenerWrapper : public b2ContactListener
{
public:
    b2ContactListenerWrapper(
        b2ContactListener_callback_BeginContact beginContact,
        b2ContactListener_callback_EndContact endContact,
        b2ContactListener_callback_PreSolve preSolve,
        b2ContactListener_callback_PostSolve postSolve);

    void BeginContact(b2Contact* contact) override;
    void EndContact(b2Contact* contact) override;
    void PreSolve(b2Contact* contact, const b2Manifold* oldManifold) override;
    void PostSolve(b2Contact* contact, const b2ContactImpulse* impulse) override;

private:
    b2ContactListener_callback_BeginContact m_beginContact;
    b2ContactListener_callback_EndContact m_endContact;
    b2ContactListener_callback_PreSolve m_preSolve;
    b2ContactListener_callback_PostSolve m_postSolve;
};

extern "C"
{
    /*
     * b2ContactImpulse
     */
    BOX2D_API void b2ContactImpulse_get_impulses(b2ContactImpulse* obj, float** normalImpulses, float** tangentImpulses, int32* count);

    /*
     * b2ContactListenerWrapper
     */
    BOX2D_API b2ContactListenerWrapper* b2ContactListenerWrapper_new(
        b2ContactListener_callback_BeginContact beginContact,
        b2ContactListener_callback_EndContact endContact,
        b2ContactListener_callback_PreSolve preSolve,
        b2ContactListener_callback_PostSolve postSolve);
    BOX2D_API void b2ContactListenerWrapper_delete(b2ContactListenerWrapper* obj);
}
