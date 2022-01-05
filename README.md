![Logo](/docs/images/logo.png)

# Box2D.CSharp

[![Build and Test](https://github.com/MackinnonBuck/Box2D.CSharp/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/MackinnonBuck/Box2D.CSharp/actions/workflows/build-and-test.yml)

A C# wrapper for Box2D, a 2D physics engine for games.

Targets .NET Standard 2.0 and uses [P/Invoke](https://docs.microsoft.com/en-us/dotnet/standard/native-interop/pinvoke) for native interoperability.

## About

This project was created to provide a fast, reliable, and memory-efficient 2D physics engine for games written in C#.

Speed is achieved by compiling Erin Catto's [Box2D](https://github.com/erincatto/box2d) as a dynamic library with wrapper functions that can be called from managed code using [P/Invoke](https://docs.microsoft.com/en-us/dotnet/standard/native-interop/pinvoke).

Reliability is partly inherited from the C++ Box2D implementation, but is also helped by toggleable access-checking mechanisms designed to catch invalid uses of the API and throw meaningful exceptions.

Memory-efficiency comes from the use of `struct`s for both small copies of unmanaged data and handles to native resources whose lifetime is known but controlled by unmanaged code. Temporary handles to unmanaged resources with unknown lifetimes are represented using `ref struct`s. This differs from C# reimplementations of Box2D that, for example, represent bodies, fixtures, and contact information using `class`es, which can create significant GC pressure in complex simulations (e.g. those with a high contact frequency).

## Status

Currently, a significant but incomplete subset of the full Box2D API is supported. This project is still in its early stages, so breaking design changes are not out of the question. If you notice a missing feature, feel free to create an issue or submit a pull request.

## Project Structure

Following is a high-level overview of the project structure:

**/src/Box2D:** The C# class library exposing the Box2D API. Most of the project lives in here.

**/src/Box2DWrapper:** A C/C++ wrapper that compiles Box2D as a native DLL. Windows is currently the only supported platform, but migrating to a CMake project for Linux/MacOS support is something being considered for the future.

**/test/Testbed:** A C# translation of the official [Box2D testbed](https://github.com/erincatto/box2d/tree/master/testbed).

**/test/UnitTests:** C# translations of the official [Box2D unit tests](https://github.com/erincatto/box2d/tree/master/unit-test).

## Contributing

If you find a missing feature (there are lots at the moment!), feel free to create an issue or submit a pull request.

The testbed only has ports of a handful of the official testbed entries, so contributions adding more entries are always welcome (and this could be a good way to find gaps in our API).

## Documentation

Our C# API aligns very closely with the original C++ API, so we recommend the [Official Box2D Documentation](https://box2d.org/documentation/index.html).

## Acknowledgements

[Box2D](https://github.com/erincatto/box2d) by Erin Catto, which serves as the core for this project.

[BulletSharpPInvoke](https://github.com/AndresTraks/BulletSharpPInvoke) by Andres Traks, which partially inspired this project.
