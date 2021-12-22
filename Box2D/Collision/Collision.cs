using System;

namespace Box2D;

using static NativeMethods;

public static class Collision
{
    public unsafe static void GetPointStates(Span<PointState> state1, Span<PointState> state2, in Manifold manifold1, in Manifold manifold2)
    {
        if (state1.Length != 2)
        {
            throw new ArgumentException($"Expected '{nameof(state1)}' to have a length of 2.", nameof(state1));
        }

        if (state2.Length != 2)
        {
            throw new ArgumentException($"Expected '{nameof(state2)}' to have a length of 2.", nameof(state2));
        }

        fixed (PointState* pinnedState1 = state1)
        {
            fixed (PointState* pinnedState2 = state2)
            {
                b2GetPointStates_wrap((IntPtr)pinnedState1, (IntPtr)pinnedState2, in manifold1, in manifold2);
            }
        }
    }
}
