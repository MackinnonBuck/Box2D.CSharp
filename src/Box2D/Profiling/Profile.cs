using System.Runtime.InteropServices;

namespace Box2D.Profiling;

/// <summary>
/// Profiling data. Times are in milliseconds.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Profile
{
    public float Step { get; set; }

    public float Collide { get; set; }

    public float Solve { get; set; }

    public float SolveInit { get; set; }

    public float SolveVelocity { get; set; }

    public float SolvePosition { get; set; }

    public float Broadphase { get; set; }

    public float SolveTOI { get; set; }
}
