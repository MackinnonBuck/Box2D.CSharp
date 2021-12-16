// See https://aka.ms/new-console-template for more information
using Box2D;
using Box2D.Math;

var gravity = new Vec2(0f, -10f);

using var world = new World(gravity);

var groundBodyDef = new BodyDef
{
    Position = new(0f, -10f),
};

var groundBody = world.CreateBody(groundBodyDef);

var groundBox = new PolygonShape();

groundBox.SetAsBox(50f, 10f);

Console.WriteLine("Done!");


//Body? body = null;

//using (var world = new World(new(0f, 9.81f)))
//{
//    body = world.CreateBody(new()
//    {
//        Position = new(0f, 10f)
//    });

//    var body2 = world.CreateBody(new()
//    {
//        Position = new(-10f, 123f),
//        LinearVelocity = new(5f, -3f),
//    });

//    Console.WriteLine($"{body.Position.X}, {body.Position.Y}");
//}

//Console.WriteLine("After world dispose.");

//// This should crash the program with an informative error message.
//Console.WriteLine($"{body.Position.X}, {body.Position.Y}");

