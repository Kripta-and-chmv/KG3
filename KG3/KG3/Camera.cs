using OpenTK.Graphics.OpenGL;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KG3
{
    internal class Camera
    {
        private Vector3 _mPos;   // Вектор позиции камеры
        private Vector3 _mView;   // Направление, куда смотрит камера
        private Vector3 _mUp;     // Вектор верхнего направления
        private Vector3 _mStrafe; // Вектор для стрейфа (движения влево и вправо) камеры

        public void Look()
        {
            var lookat = Matrix4.LookAt(_mPos, _mView, _mUp);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
        }
        public void UpDown(float speed)
        {
            _mPos.Y += speed;
        }
        public void MoveXZCamera(float speed)
        {
            var vVector = Vector3.Normalize(_mView - _mPos); // Получаем вектор взгляда

            _mPos.X += vVector.X * speed;
            _mPos.Z += vVector.Z * speed;
            _mView.X += vVector.X * speed;
            _mView.Z += vVector.Z * speed;
        }
        public void Zoom(float delta)
        {
            var vVector = _mView - _mPos; // Получаем вектор взгляда
            if (vVector.Length < 30 || delta > 0)//приближение
            {
                vVector = Vector3.Normalize(vVector);
                var vTemp = _mPos + (vVector*delta);
                if (vTemp.X/_mPos.X > 0 || vTemp.X/_mPos.X > 0 || vTemp.X/_mPos.X > 0)
                    _mPos = vTemp;
            }
            else
            {
                if (delta < 0)//отдаление
                {
                    vVector = Vector3.Normalize(vVector);
                    var vTemp = _mPos + (vVector*delta);
                    if (vTemp.X/_mPos.X > 0 || vTemp.X/_mPos.X > 0 || vTemp.X/_mPos.X > 0)
                        _mPos = vTemp;
                }
            }

        }
        public void MoveYCamera(float speed)
        {
            _mView.Y += speed;
        }
        public void Strafe(float speed)
        {
            // добавим вектор стрейфа к позиции
            _mPos.X += _mStrafe.X * speed;
            _mPos.Z += _mStrafe.Z * speed;

            // Добавим теперь к взгляду
            _mView.X += _mStrafe.X * speed;
            _mView.Z += _mStrafe.Z * speed;
        }
        public void Rotate_Position(float angle, float x, float y, float z)
        {
            _mPos = _mPos - _mView;

            var vVector = _mPos;
            Vector3 aVector;

            var sinA = (float)Math.Sin(Math.PI * angle / 180.0);
            var cosA = (float)Math.Cos(Math.PI * angle / 180.0);

            // Найдем новую позицию X для вращаемой точки 
            aVector.X = (cosA + (1 - cosA) * x * x) * vVector.X;
            aVector.X += ((1 - cosA) * x * y - z * sinA) * vVector.Y;
            aVector.X += ((1 - cosA) * x * z + y * sinA) * vVector.Z;

            // Найдем позицию Y 
            aVector.Y = ((1 - cosA) * x * y + z * sinA) * vVector.X;
            aVector.Y += (cosA + (1 - cosA) * y * y) * vVector.Y;
            aVector.Y += ((1 - cosA) * y * z - x * sinA) * vVector.Z;

            // И позицию Z 
            aVector.Z = ((1 - cosA) * x * z - y * sinA) * vVector.X;
            aVector.Z += ((1 - cosA) * y * z + x * sinA) * vVector.Y;
            aVector.Z += (cosA + (1 - cosA) * z * z) * vVector.Z;

            _mPos = _mView + aVector;
        }
        public void Position_Camera(float posX, float posY, float posZ,
                float viewX, float viewY, float viewZ,
                float upX, float upY, float upZ)
        {
            _mPos.X = posX; // Позиция камеры
            _mPos.Y = posY; //
            _mPos.Z = posZ; //
            _mView.X = viewX; // Куда смотрит, т.е. взгляд
            _mView.Y = viewY; //
            _mView.Z = viewZ; //
            _mUp.X = upX; // Вертикальный вектор камеры
            _mUp.Y = upY; //
            _mUp.Z = upZ; //
        }

        public double GetPosX() // Возвращает позицию камеры по Х
        {
            return _mPos.X;
        }

        public double GetPosY() // Возвращает позицию камеры по Y
        {
            return _mPos.Y;
        }

        public double GetPosZ() // Возвращает позицию камеры по Z
        {
            return _mPos.Z;
        }

        public double GetViewX() // Возвращает позицию взгляда по Х
        {
            return _mView.X;
        }

        public double GetViewY() // Возвращает позицию взгляда по Y
        {
            return _mView.Y;
        }

        public double GetViewZ() // Возвращает позицию взгляда по Z
        {
            return _mView.Z;
        }

        public void Update()
        {
            Vector3 vCross = Cross(_mView, _mPos, _mUp);

            // Нормализуем вектор стрейфа
            _mStrafe = Vector3.Normalize(vCross);
        }

        private static Vector3 Cross(Vector3 vV1, Vector3 vV2, Vector3 vVector2)
        {
            Vector3 vNormal;
            Vector3 vVector1;
            vVector1 = vV1 - vV2;

            // Если у нас есть 2 вектора (вектор взгляда и вертикальный вектор), 
            // У нас есть плоскость, от которой мы можем вычислить угол в 90 градусов.
            // Рассчет cross'a прост, но его сложно запомнить с первого раза. 
            // Значение X для вектора = (V1.y * V2.z) - (V1.z * V2.y)
            vNormal.X = ((vVector1.Y * vVector2.Z) - (vVector1.Z * vVector2.Y));

            // Значение Y = (V1.z * V2.x) - (V1.x * V2.z)
            vNormal.Y = ((vVector1.Z * vVector2.X) - (vVector1.X * vVector2.Z));

            // Значение Z = (V1.x * V2.y) - (V1.y * V2.x)
            vNormal.Z = ((vVector1.X * vVector2.Y) - (vVector1.Y * vVector2.X));

            // *ВАЖНО* Вы не можете менять этот порядок, иначе ничего не будет работать.
            // Должно быть именно так, как здесь. Просто запомните, если вы ищите Х, вы не
            // используете значение X двух векторов, и то же самое для Y и Z. Заметьте,
            // вы рассчитываете значение из двух других осей и никогда из той же самой.

            // Итак, зачем всё это? Нам нужно найти ось, вокруг которой вращаться. Вращение камеры
            // влево и вправо простое - вертикальная ось всегда (0, 1, 0). 
            // Вращение камеры вверх и вниз отличается, так как оно происходит вне 
            // глобальных осей. Достаньте себе книгу по линейной алгебре, если у вас 
            // её ещё нет, она вам пригодится.

            // вернем результат.
            return vNormal;
        }

        public void RotateXY(float fi, float theta)
        {
            Rotate_Position(fi, 0, 1, 0);
            Rotate_Position(theta, _mStrafe.X, _mStrafe.Y, _mStrafe.Z);
            if (_mPos.X * _mPos.X + _mPos.Z * _mPos.Z - _mView.X * _mView.X - _mView.Z * _mView.Z < 5 || _mPos.Y < 0.1)
                Rotate_Position(-theta, _mStrafe.X, _mStrafe.Y, _mStrafe.Z);
        }

        public float GetMagnitude()
        {
            return (_mView - _mPos).Length;
        }

    }
}
