// See https://aka.ms/new-console-template for more information
using Box2D;

Body? body = null;

using (var world = new World(new(0f, 9.81f)))
{
    body = world.CreateBody(new()
    {
        Position = new(0f, 10f)
    });

    var body2 = world.CreateBody(new()
    {
        Position = new(-10f, 123f),
        LinearVelocity = new(5f, -3f),
    });

    Console.WriteLine($"{body.Position.X}, {body.Position.Y}");
}

Console.WriteLine("After world dispose.");

// This should crash the program with an informative error message.
Console.WriteLine($"{body.Position.X}, {body.Position.Y}");

