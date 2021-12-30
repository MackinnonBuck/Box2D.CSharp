using System.Diagnostics;

namespace Silk.NET.OpenGL;

internal static class GLExtensions
{
    public static void CheckForErrors(this GL gl)
    {
        var error = gl.GetError();

        if (error != GLEnum.NoError)
        {
            var message = $"OpenGL Error: {error}.";
            Console.Error.WriteLine(message);
            Debug.Fail(message);
        }
    }

    public static uint CreateShaderProgram(this GL gl, string vs, string fs)
    {
        var vsId = CreateShaderFromString(gl, vs, GLEnum.VertexShader);
        var fsId = CreateShaderFromString(gl, fs, GLEnum.FragmentShader);
        Debug.Assert(vsId != 0 && fsId != 0);

        var programId = gl.CreateProgram();
        gl.AttachShader(programId, vsId);
        gl.AttachShader(programId, fsId);
        gl.BindFragDataLocation(programId, 0, "color");
        gl.LinkProgram(programId);

        gl.DetachShader(programId, vsId);
        gl.DetachShader(programId, fsId);
        gl.DeleteShader(vsId);
        gl.DeleteShader(fsId);

        gl.GetProgram(programId, GLEnum.LinkStatus, out var success);
        Debug.Assert(success != 0);

        return programId;
    }

    private static uint CreateShaderFromString(GL gl, string source, GLEnum type)
    {
        var shaderId = gl.CreateShader(type);
        gl.ShaderSource(shaderId, source);
        gl.CompileShader(shaderId);
        gl.GetShader(shaderId, GLEnum.CompileStatus, out var success);

        if (success == 0)
        {
            Console.Error.WriteLine($"Error compiling shader of type {type}!");
            PrintLog(gl, shaderId);
            gl.DeleteShader(shaderId);
            return 0;
        }

        return shaderId;
    }

    private static void PrintLog(GL gl, uint obj)
    {
        string log;

        if (gl.IsShader(obj))
        {
            gl.GetShaderInfoLog(obj, out log);
        }
        else if (gl.IsProgram(obj))
        {
            gl.GetProgramInfoLog(obj, out log);
        }
        else
        {
            Console.Error.WriteLine($"Could not print the log of the object with ID '{obj}'.");
            return;
        }

        Console.Error.WriteLine(log);
    }
}
