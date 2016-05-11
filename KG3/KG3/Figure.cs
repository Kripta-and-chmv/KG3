using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace KG3
{
    class Figure
    {
        int raz = 20;
        double angle = 30*Math.PI/180;

        List<Vector3> carcassFront = new List<Vector3>();
        List<Vector3> carcassBack = new List<Vector3>();

        public void DrawCarcass()
        {
            //отрисовка передней части
            GL.Color3(Color.Blue);
            GL.Begin(BeginMode.LineLoop);
            foreach (var dot in carcassFront)
            {
                GL.Vertex3(dot);
            }
            GL.End();
            //отрисовка задней части
            GL.Begin(BeginMode.LineLoop);
            foreach (var dot in carcassBack)
            {
                GL.Vertex3(dot);
            }
            GL.End();
            //отрисовка соединительных линий
            GL.Begin(BeginMode.Lines);
            for (int i = 0; i < 5; i++)
            {
                GL.Vertex3(carcassBack[i]);
                GL.Vertex3(carcassFront[i]);
            }
            GL.End();
        }

        public void DrawSurface()
        {
            GL.Color3(Color.Red);
            GL.Begin(BeginMode.Polygon);
            foreach (var nod in carcassFront)
                GL.Vertex3(nod);
            GL.End();

            GL.Begin(BeginMode.Polygon);
            foreach (var nod in carcassBack)
                GL.Vertex3(nod);
            GL.End();

            for (int i=1; i<=5; i++)
            {
                int k = i;
                GL.Begin(BeginMode.Quads);
                GL.Vertex3(carcassBack[k - 1]);
                GL.Vertex3(carcassFront[k - 1]);
                k = i == 5 ? 0 : i;
                GL.Vertex3(carcassFront[k]);
                GL.Vertex3(carcassBack[k]);
                GL.End();
                
            }
        }

        public Figure()
        {
            int x = 0, y = 0;
            carcassFront.Add(new Vector3(0, 0, 0));
            carcassFront.Add(new Vector3(-20, 20, 0));
            carcassFront.Add(new Vector3(10, 40, 0));
            carcassFront.Add(new Vector3(40, 20, 0));
            carcassFront.Add(new Vector3(20, 0, 0));
            x = 0 - raz; y = 0 - raz;
            carcassBack.Add(new Vector3((float)(x *Math.Cos(angle) - y*Math.Sin(angle)),(float) (x*Math.Sin(angle) + y*Math.Cos(angle)), -40));
            x = -20 - raz; y = 20 - raz;
            carcassBack.Add(new Vector3((float)(x * Math.Cos(angle) - y * Math.Sin(angle)), (float)(x * Math.Sin(angle) + y * Math.Cos(angle)), -40));
            x = 10 - raz; y = 40 - raz;
            carcassBack.Add(new Vector3((float)(x * Math.Cos(angle) - y * Math.Sin(angle)), (float)(x * Math.Sin(angle) + y * Math.Cos(angle)), -40));
            x = 40 - raz; y = 20 - raz;
            carcassBack.Add(new Vector3((float)(x * Math.Cos(angle) - y * Math.Sin(angle)), (float)(x * Math.Sin(angle) + y * Math.Cos(angle)), -40));
            x = 20 - raz; y = 0 - raz;
            carcassBack.Add(new Vector3((float)(x * Math.Cos(angle) - y * Math.Sin(angle)), (float)(x * Math.Sin(angle) + y * Math.Cos(angle)), -40));

        }
    }
}
