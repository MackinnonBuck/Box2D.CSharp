using Box2D.Collision.Shapes;
using Box2D.Dynamics;
using System.Numerics;

// Define the gravity vector.
var gravity = new Vector2(0f, -10f);

// Construct a world object, which will hold and simulate the rigid bodies.
var world = new World(gravity);

// Define the ground body.
using var groundBodyDef = new BodyDef
{
    Position = new(0f, -10f),
};

// Call the body factory to create a body and add it to the world.
var groundBody = world.CreateBody(groundBodyDef);

// Define the ground box shape.
var groundBox = new PolygonShape();

// Set the extents (half-widths) of the box.
groundBox.SetAsBox(50f, 10f);

// Add the ground fixture to the ground body.
groundBody.CreateFixture(groundBox, 0.0f);

// Define the dynamic body. We set its position and call the body factory.
using var bodyDef = new BodyDef
{
    Type = BodyType.Dynamic,
    Position = new(0f, 4f),
};
var body = world.CreateBody(bodyDef);

// Define another box shape for our dynamic body.
using var dynamicBox = new PolygonShape();
dynamicBox.SetAsBox(1f, 1f);

// Define the dynamic body fixture.
using var fixtureDef = new FixtureDef
{
    Shape = dynamicBox,
    Density = 1f,       // Non-zero density so it will be dynamic.
    Friction = 0.3f,    // Override the default friction.
};

// Add the fixture to the body.
body.CreateFixture(fixtureDef);

// Prepare for simulation with a time step of 1/60 of a second.
var timeStep = 1f / 60f;
var velocityIterations = 6;
var positionIterations = 2;

for (var i = 0; i < 60; i++)
{
    // Instruct the world to perform a single step of simulation.
    world.Step(timeStep, velocityIterations, positionIterations);

    Console.WriteLine($"{body.Position.X} {body.Position.Y} {body.Angle}");
}

world.Dispose();

Console.WriteLine("Done!");
