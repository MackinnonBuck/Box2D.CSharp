namespace Box2D;

using static NativeMethods;

public class World : Box2DDisposableObject
{
    public Body? BodyList => Body.FromIntPtr(b2World_GetBodyList(Native));

    public World(Vec2 gravity) : base(isUserOwned: true)
    {
        var native = b2World_new(ref gravity);

        Initialize(native);
    }

    public Body CreateBody(BodyDef def)
        => new(Native, ref def);

    public void DestroyBody(Body body)
    {
        b2World_DestroyBody(Native, body.Native);
        body.InvalidateInstance();
    }

    private protected override void Dispose(bool disposing)
    {
        var body = BodyList;

        while (body is not null)
        {
            var next = body.Next;
            body.InvalidateInstance();
            body = next;
        }

        // TODO: See if there's anything else to do here (do we care about the disposing parameter?).
        b2World_delete(Native);

        Invalidate();
    }
}
