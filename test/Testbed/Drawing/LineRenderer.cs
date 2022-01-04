using Box2D.Drawing;
using Silk.NET.OpenGL;
using System.Numerics;

namespace Testbed.Drawing;

internal class LineRenderer
{
    private const int MaxVertices = 2 * 512;
    private const int VertexAttribute = 0;
    private const int ColorAttribute = 1;

    private readonly GL _gl;
    private readonly Camera _camera;

    private readonly float[] _vertices = new float[MaxVertices * 2];
    private readonly float[] _colors = new float[MaxVertices * 4];

    private uint _programId;
    private uint _vaoId;
    private readonly uint[] _vboIds = new uint[2];
    private readonly int _projectionUniform;

    private int _count;

    unsafe public LineRenderer(GL gl, Camera camera)
    {
        _gl = gl;
        _camera = camera;

        var vs = @"
#version 330
uniform mat4 projectionMatrix;
layout(location = 0) in vec2 v_position;
layout(location = 1) in vec4 v_color;
out vec4 f_color;
void main(void)
{
    f_color = v_color;
    gl_Position = projectionMatrix * vec4(v_position, 0.0f, 1.0f);
}
";

        var fs = @"
#version 330
in vec4 f_color;
out vec4 color;
void main(void)
{
    color = f_color;
}
";

        _programId = _gl.CreateShaderProgram(vs, fs);
        _projectionUniform = _gl.GetUniformLocation(_programId, "projectionMatrix");

        // Generate
        _gl.GenVertexArrays(1, out _vaoId);
        _gl.GenBuffers(_vboIds);

        _gl.BindVertexArray(_vaoId);
        _gl.EnableVertexAttribArray(VertexAttribute);
        _gl.EnableVertexAttribArray(ColorAttribute);

        // Vertex buffer
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vboIds[0]);
        _gl.VertexAttribPointer(VertexAttribute, 2, GLEnum.Float, false, 0, null);
        _gl.BufferData<float>(GLEnum.ArrayBuffer, _vertices, BufferUsageARB.DynamicDraw);

        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vboIds[1]);
        _gl.VertexAttribPointer(ColorAttribute, 4, GLEnum.Float, false, 0, null);
        _gl.BufferData<float>(GLEnum.ArrayBuffer, _colors, BufferUsageARB.DynamicDraw);

        //_gl.CheckForErrors();

        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
        _gl.BindVertexArray(0);

        _count = 0;
    }

    public void Vertex(Vector2 v, Color c)
    {
        if (_count == MaxVertices)
        {
            Flush();
        }

        _vertices[_count * 2 + 0] = v.X;
        _vertices[_count * 2 + 1] = v.Y;

        _colors[_count * 4 + 0] = c.R;
        _colors[_count * 4 + 1] = c.G;
        _colors[_count * 4 + 2] = c.B;
        _colors[_count * 4 + 3] = c.A;

        _count++;
    }

    public void Flush()
    {
        if (_count == 0)
        {
            return;
        }

        _gl.UseProgram(_programId);

        Span<float> proj = stackalloc float[16];
        _camera.BuildProjectionMatrix(proj, 0f);

        _gl.UniformMatrix4(_projectionUniform, 1, false, proj);

        _gl.BindVertexArray(_vaoId);

        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vboIds[0]);
        _gl.BufferSubData<float>(GLEnum.ArrayBuffer, 0, _vertices.AsSpan(0, _count * 2));

        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vboIds[1]);
        _gl.BufferSubData<float>(GLEnum.ArrayBuffer, 0, _colors.AsSpan(0, _count * 4));

        _gl.DrawArrays(PrimitiveType.Lines, 0, (uint)_count);

        //_gl.CheckForErrors();

        _gl.BindBuffer(GLEnum.ArrayBuffer, 0);
        _gl.BindVertexArray(0);
        _gl.UseProgram(0);

        _count = 0;
    }

    public void Dispose()
    {
        if (_vaoId != 0)
        {
            _gl.DeleteVertexArrays(1, _vaoId);
            _gl.DeleteBuffers(3, _vboIds);
            _vaoId = 0;
        }

        if (_programId != 0)
        {
            _gl.DeleteProgram(_programId);
            _programId = 0;
        }
    }
}
