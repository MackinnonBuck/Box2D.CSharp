namespace Testbed;

internal static class MathUtils
{
    public static float RandomFloat(float lo, float hi)
    {
        float r = Random.Shared.NextSingle();
        r = (hi - lo) * r + lo;
        return r;
    }
}
