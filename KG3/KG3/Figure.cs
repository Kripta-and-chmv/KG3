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

    
    class Figure
    {
        int raz = 20;
        double angle = 30*Math.PI/180;
        int texId;

        //List<Vector3> carcassFront = new List<Vector3>();
        List<List<Vector3d>> carcass = new List<List<Vector3d>>();

        public Figure(int tex)
        {
            texId = tex;
        }

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
            //отрисовка поверхностей
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
                    GL.Vertex3(carcass[i - 1][k - 1]);
                    GL.Vertex3(carcass[i][k - 1]);
                    k = j == 5 ? 0 : j;
                    GL.Vertex3(carcass[i][k]);
                    GL.Vertex3(carcass[i - 1][k]);
                    GL.End();
                }
            }
            ////Отрисовка нормалей
            GL.Begin(BeginMode.Lines);
            for (int i = 0; i < carcass.Count; i++)
            {
                if (i != carcass.Count - 1)
                    for (int j = 0; j < 5; j++)
                    {
                        GL.Vertex3(carcass[i][j]);
                        GL.Vertex3(0.1*CalculateNormal(carcass[i][j], carcass[i + 1][j], carcass[i][(j + 1)%5]));

                        GL.Vertex3(carcass[i][j]);
                        GL.Vertex3(0.1 * CalculateNormal(carcass[i][j], carcass[i + 1][j], carcass[i][Math.Abs((j - 1)%5)]));

                        GL.Vertex3(carcass[i][j]);
                        GL.Vertex3(0.1 * CalculateNormal(carcass[i][j], carcass[i][(j + 1)%5], carcass[i][Math.Abs((j - 1)%5)]));
                    }
                else
                {
                    for (int j = 0; j < 5; j++)
                    {
                        GL.Vertex3(carcass[i][j]);
                        GL.Vertex3(0.1 * CalculateNormal(carcass[i][j], carcass[i - 1][j], carcass[i][(j + 1) % 5]));

                        GL.Vertex3(carcass[i][j]);
                        GL.Vertex3(0.1 * CalculateNormal(carcass[i][j], carcass[i - 1][j], carcass[i][Math.Abs((j - 1) % 5)]));

                        GL.Vertex3(carcass[i][j]);
                        GL.Vertex3(0.1 * CalculateNormal(carcass[i][j], carcass[i][(j + 1) % 5], carcass[i][Math.Abs((j - 1) % 5)]));
                    }
                }
            }
            GL.End();
        }



        private Vector3d CalculateNormal(Vector3d v0, Vector3d v1, Vector3d v2)
        {
            return Vector3d.Cross(v2 - v0, v1 - v0);
        }

        public Figure()
        {

            string[] s = File.ReadAllLines("input.txt");
            /////////////////////////////////
            int x = 0, y = 0;
            carcass.Add(new List<Vector3d>());
            for (int i = 0; i < 5; i++)
            {
                string[] k = s[i].Split(' ');
                carcass[0].Add(new Vector3d(int.Parse(k[0]), int.Parse(k[1]), int.Parse(k[2])));
            }

            for (int i = 5; i < s.Length; i++)
            {
                carcass.Add( new List<Vector3d>());
                for (int j = 0; j < 5; j++)
                {
                    int t = i - 4;
                    string[] k = s[i].Split(' ');
                    carcass[t].Add(new Vector3d(carcass[t-1][j].X + int.Parse(k[0]), carcass[t-1][j].Y + int.Parse(k[1]), carcass[t-1][j].Z + int.Parse(k[2])));
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
