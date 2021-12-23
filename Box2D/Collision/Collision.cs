using System;

namespace Box2D;

using static NativeMethods;

public static class Collision
{
    public static void GetPointStates(Span<PointState> state1, Span<PointState> state2, in Manifold manifold1, in Manifold manifold2)
    {
        if (state1.Length != 2)
        {
            throw new ArgumentException($"Expected '{nameof(state1)}' to have a length of 2.", nameof(state1));
        }

        if (state2.Length != 2)
        {
            throw new ArgumentException($"Expected '{nameof(state2)}' to have a length of 2.", nameof(state2));
        }

        b2GetPointStates_wrap(out state1.GetPinnableReference(), out state2.GetPinnableReference(), manifold1.Native, manifold2.Native);
    }
}
