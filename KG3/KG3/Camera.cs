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
        private Vector3 mPos= new Vector3(70, 70, 70);   // Вектор позиции камеры
        private Vector3 mView= new Vector3(0, 0, 0);   // Направление, куда смотрит камера
        private Vector3 mUp = new Vector3(0, 1, 0);     // Вектор верхнего направления
        private Vector3 mStrafe; // Вектор для стрейфа (движения влево и вправо) камеры

        public void Look()
        {
            Matrix4 lookat = Matrix4.LookAt(mPos, mView, mUp);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
        }

        public void MoveForward()
        {
            //GL.MatrixMode(MatrixMode.Projection);
            //GL.Translate(0, 0, 5);
            mPos.Z -= 5;
            mView.Z -= 5;

        }
        public void MoveBackward()
        {
            //GL.MatrixMode(MatrixMode.Projection);
            //GL.Translate(0, 0, -5);
            mPos.Z += 5;
            mView.Z += 5;
        }
        public void MoveRighward()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.Translate(-5, 0, 0);
        }
        public void MoveLeftward()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.Translate(5, 0, 0);
        }
        public void MoveUp()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.Translate(0, -5, 0);
        }
        public void MoveDown()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.Translate(0, 5, 0);
        }
        public void SeeLeft()
        {

            //GL.MatrixMode(MatrixMode.Projection);
            //GL.Rotate(-5, 0, 1, 0);
            mView.X -= 5;
        }
        public void SeeRight()
        {
            //GL.MatrixMode(MatrixMode.Projection);
            //GL.Rotate(5, 0, 1, 0);
            mView.X += 5;
        }
        public void SeeUp()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.Rotate(-5, 1, 0, 0);
        }
        public void SeeDown()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.Rotate(5, 1, 0, 0);
        }
    }
}
