using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;

namespace KG3
{

    public static Vector3 operator *(Vector3 a, double t)
    {

        return new Vector3(a.X * (float)t, a.Y * (float)t, a.Z * (float)t);
    }
    class Figure
    {
        int raz = 20;
        double angle = 30*Math.PI/180;

        //List<Vector3> carcassFront = new List<Vector3>();
        List<List<Vector3>> carcass = new List<List<Vector3>>();

        public void DrawCarcass()
        {
            GL.Color3(Color.Blue);
           foreach(var car in carcass)
            {
                GL.Begin(BeginMode.LineLoop);
                foreach (var dot in car)
                {
                    GL.Vertex3(dot);
                }
                GL.End();
            }
            
            GL.Begin(BeginMode.Lines);
            for (int i = 1; i < carcass.Count; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    GL.Vertex3(carcass[i-1][j]);
                    GL.Vertex3(carcass[i][j]);
                }
            }
            GL.End();
        }

        public void DrawSurface()
        {
           GL.Color3(Color.Red);
            GL.Begin(BeginMode.Polygon);
            foreach (var nod in carcass[0])
                GL.Vertex3(nod);
            GL.End();
            GL.Begin(BeginMode.Polygon);
            foreach (var nod in carcass.Last())
                GL.Vertex3(nod);
            GL.End();
            int n = carcass.Select(x => x.Count).Min();
            for (int i = 1; i < carcass.Count; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    int k = j;
                    GL.Begin(BeginMode.Quads);
                    GL.Vertex3(carcass[i - 1][k-1]);
                    GL.Vertex3(carcass[i][k-1]);
                    k = j == 5 ? 0 : j;
                    GL.Vertex3(carcass[i][k]);
                    GL.Vertex3(carcass[i-1][k]);
                    GL.End();

                    // GL.Normal3(CalculateNormal(carcassBack[k], carcassBack[k-1], carcassBack[k+1]));
                    ;
                }

                GL.Begin(BeginMode.Lines);
                {
                    foreach (var b in Enumerable.Range(0, carcass.Count))
                    {
                        if (b != carcass.Count - 1)
                        {
                            GL.Vertex3(carcass[b][i] + 0.1 * CalculateNormal(carcass[b][i], carcass[b][(i + 1) % n], carcass[b + 1][(i + 1) % n]));
                            GL.Vertex3(carcass[b][i]);
                            GL.Vertex3(carcass[b][(i + 1) % n] + 0.1 * CalculateNormal(carcass[b][i], carcass[b][(i + 1) % n], carcass[b + 1][(i + 1) % n]));
                            GL.Vertex3(carcass[b][(i + 1) % n]);
                        }
                    }
                }
                GL.End();
            }
            ////Отрисовка нормалей
            //// GL.Normal3(CalculateNormal());

        }

       

        private Vector3 CalculateNormal(Vector3 v0, Vector3 v1, Vector3 v2)
        {
            return Vector3.Cross(v2 - v0, v1 - v0);
        }

        public Figure()
        {

            string[] s = File.ReadAllLines("input.txt");
            /////////////////////////////////
            int x = 0, y = 0;
            carcass.Add(new List<Vector3>());
            for (int i = 0; i < 5; i++)
            {
                string[] k = s[i].Split(' ');
                carcass[0].Add(new Vector3(int.Parse(k[0]), int.Parse(k[1]), int.Parse(k[2])));
            }

            for (int i = 5; i < s.Length; i++)
            {
                carcass.Add( new List<Vector3>());
                for (int j = 0; j < 5; j++)
                {
                    int t = i - 4;
                    string[] k = s[i].Split(' ');
                    carcass[t].Add(new Vector3(carcass[t-1][j].X + int.Parse(k[0]), carcass[t-1][j].Y + int.Parse(k[1]), carcass[t-1][j].Z + int.Parse(k[2])));
                }

            }

            //carcassFront.Add(new Vector3());
            //carcassFront.Add(new Vector3(-20, 20, 0));
            //carcassFront.Add(new Vector3(10, 40, 0));
            //carcassFront.Add(new Vector3(40, 20, 0));
            //carcassFront.Add(new Vector3(20, 0, 0));
            //x = 0 - raz; y = 0 - raz;
            //carcassBack.Add(new Vector3((float)(x *Math.Cos(angle) - y*Math.Sin(angle)),(float) (x*Math.Sin(angle) + y*Math.Cos(angle)), -40));
            //x = -20 - raz; y = 20 - raz;
            //carcassBack.Add(new Vector3((float)(x * Math.Cos(angle) - y * Math.Sin(angle)), (float)(x * Math.Sin(angle) + y * Math.Cos(angle)), -40));
            //x = 10 - raz; y = 40 - raz;
            //carcassBack.Add(new Vector3((float)(x * Math.Cos(angle) - y * Math.Sin(angle)), (float)(x * Math.Sin(angle) + y * Math.Cos(angle)), -40));
            //x = 40 - raz; y = 20 - raz;
            //carcassBack.Add(new Vector3((float)(x * Math.Cos(angle) - y * Math.Sin(angle)), (float)(x * Math.Sin(angle) + y * Math.Cos(angle)), -40));
            //x = 20 - raz; y = 0 - raz;
            //carcassBack.Add(new Vector3((float)(x * Math.Cos(angle) - y * Math.Sin(angle)), (float)(x * Math.Sin(angle) + y * Math.Cos(angle)), -40));

        }
    }
}
