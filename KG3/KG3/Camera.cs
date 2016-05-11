using OpenTK.Graphics.OpenGL;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KG3
{
    class Camera
    {
        public OpenTK.Vector3 Position;
        public OpenTK.Vector3 Rotation;
        public OpenTK.Quaternion Orientation;

        public OpenTK.Matrix4 Matrix;
        public OpenTK.Matrix4 Model;
        public OpenTK.Matrix4 Projection;

        public Camera()
        {
            Matrix = OpenTK.Matrix4.Identity;
            Projection = OpenTK.Matrix4.Identity;
            Orientation = OpenTK.Quaternion.Identity;
        }

        public void Update()
        {
            Orientation =
                OpenTK.Quaternion.FromAxisAngle(OpenTK.Vector3.UnitY, Rotation.Y) *
                OpenTK.Quaternion.FromAxisAngle(OpenTK.Vector3.UnitX, Rotation.X);

            var forward = OpenTK.Vector3.Transform(OpenTK.Vector3.UnitZ, Orientation);
            Model = OpenTK.Matrix4.LookAt(Position, Position + forward, OpenTK.Vector3.UnitY);
            Matrix = Model * Projection;
        }

        public void Resize(int width, int height)
        {
            Projection = OpenTK.Matrix4.CreatePerspectiveFieldOfView(
                OpenTK.MathHelper.PiOver4, (float)width / height, 0.1f, 1000f
            );
        }

        public void TurnX(float a)
        {
            Rotation.X += a;
            Rotation.X = OpenTK.MathHelper.Clamp(Rotation.X, -1.57f, 1.57f);
        }

        public void TurnY(float a)
        {
            Rotation.Y += a;
            Rotation.Y = ClampCircular(Rotation.Y, 0, OpenTK.MathHelper.TwoPi);
        }

        public void MoveX(float a)
        {
            Position += OpenTK.Vector3.Transform(OpenTK.Vector3.UnitX * a, OpenTK.Quaternion.FromAxisAngle(OpenTK.Vector3.UnitY, Rotation.Y));
        }

        public void MoveY(float a)
        {
            Position += OpenTK.Vector3.Transform(OpenTK.Vector3.UnitY * a, OpenTK.Quaternion.FromAxisAngle(OpenTK.Vector3.UnitY, Rotation.Y));
        }

        public void MoveZ(float a)
        {
            Position += OpenTK.Vector3.Transform(OpenTK.Vector3.UnitZ * a, OpenTK.Quaternion.FromAxisAngle(OpenTK.Vector3.UnitY, Rotation.Y));
        }

        public void MoveYLocal(float a)
        {
            Position += OpenTK.Vector3.Transform(OpenTK.Vector3.UnitY * a, Orientation);
        }

        public void MoveZLocal(float a)
        {
            Position += OpenTK.Vector3.Transform(OpenTK.Vector3.UnitZ * a, Orientation);
        }

        public static float ClampCircular(float n, float min, float max)
        {
            if (n >= max) n -= max;
            if (n < min) n += max;
            return n;
        }
    }
}
