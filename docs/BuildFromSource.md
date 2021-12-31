# Building from Source

This document describes how to build and use Box2D.CSharp from source.

## Supported Platforms

Windows is currently the only supported platform, but Linux and MacOS support may be added in the future if there is enough interest around it.

## Requirements
* [Microsoft Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (17.0) or later with the following development workloads:
  * .NET desktop development
  * Desktop development with C++
* The [VCPKG](https://vcpkg.io/en/getting-started.html) dependency manager

## Steps to Build

### 1. Configure VCPKG dependencies

Run the following commands from an administrator terminal:
```batch
vcpkg install box2d:x64-windows
vcpkg integrate install
```

**NOTE:** Make sure you're in `VCPKG_ROOT` if `vcpkg.exe` is not added to `PATH`.

### 2. Build from Visual Studio

Open `Box2D.CSharp.sln` in Visual Studio 2022. Select `Build > Build Solution` or press `F6` to build the solution.

To verify that the build was successful, right-click on the `Testbed` project and select `Set as Startup Project`. Then select `Debug > Start Debugging` or press `F5` to run the testbed.

## Steps to Use

1. From an existing C# project, add a project reference to `Box2D.csproj` or shared project reference to `Box2D.dll`.
2. Right-click on your existing project, then select `Add > Existing Item`.
3. Navigate to your local copy of this repository, then select `box2dwrapper.dll` from the `lib` folder.
4. Right-click on `box2dwrapper.dll` in the Solution Explorer and select `Properties`.
5. Change the `Copy to Output Directory` action to `Copy if newer`.
