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
        List<List<Vector3d>> carcass = new List<List<Vector3d>>();

        public Figure(int tex)
        {
            texId = tex;
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
                carcass.Add(new List<Vector3d>());
                for (int j = 0; j < 5; j++)
                {
                    int t = i - 4;
                    string[] k = s[i].Split(' ');
                    double rotX, rotY, angle;
                    angle = double.Parse(k[3])*Math.PI/180;
                    rotX = (carcass[t - 1][j].X + int.Parse(k[0])) * Math.Cos(angle)- (carcass[t - 1][j].Y + int.Parse(k[1])) *Math.Sin(angle);
                    rotY= (carcass[t - 1][j].X + int.Parse(k[0])) * Math.Sin(angle) + (carcass[t - 1][j].Y + int.Parse(k[1])) * Math.Cos(angle);
                    carcass[t].Add(new Vector3d(rotX, rotY, carcass[t - 1][j].Z + int.Parse(k[2])));
                }
            }
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

        public void DrawSurface(bool tex)
        {
            //отрисовка поверхностей
            GL.BindTexture(TextureTarget.Texture2D, texId);

            GL.Color3(Color.Red);
            int i = 0, j = 0;
            foreach (var tr in carcass)
            {
                GL.Normal3(CalculateNormal(tr[0], tr[1], tr[2]));
                GL.Begin(BeginMode.Polygon);
                {
                    foreach (var v in tr)
                    {
                        if (tex)
                        {
                            GL.Color3(Color.White);
                            GL.TexCoord2(i, j);
                            i ^= 1;
                            GL.Vertex3(v);
                            GL.TexCoord2(i, j);
                            j ^= 1;
                        }
                        else
                            GL.Vertex3(v);
                    }
                }
                GL.End();
            }
            int n = carcass.Select(x => x.Count).Min();


            i = 0; j = 0;
            foreach (var a in Enumerable.Range(0, n))
            {
                GL.Begin(BeginMode.QuadStrip);
                if(tex)
                {
                    GL.Color3(Color.White);
                    foreach (var b in Enumerable.Range(0, carcass.Count))
                    {
                        if (b != carcass.Count - 1)
                        {
                            GL.Normal3(CalculateNormal(carcass[b][a], carcass[b][(a + 1) % n], carcass[b + 1][(a + 1) % n]));
                        }
                        GL.TexCoord2(i, j);

                        i ^= 1;
                        GL.Vertex3(carcass[b][a]);
                        GL.TexCoord2(i, j);

                        j ^= 1;
                        GL.Vertex3(carcass[b][(a + 1) % n]);
                    }
                }
                else
                {
                    foreach (var b in Enumerable.Range(0, carcass.Count))
                    {
                        if (b != carcass.Count - 1)
                        {
                            GL.Normal3(CalculateNormal(carcass[b][a], carcass[b][(a + 1) % n], carcass[b + 1][(a + 1) % n]));
                        }

                        GL.Vertex3(carcass[b][a]);
                        GL.Vertex3(carcass[b][(a + 1) % n]);
                    }
                }
                GL.End();
            }

        }
        public void DrawNormals(bool al)
        {
            GL.Color3(Color.Green);
            GL.Begin(BeginMode.Lines);

            for (int i = 1; i < carcass.Count; i++)
            {
                if (i != carcass.Count - 1)
                    for (int j = 0; j < 5; j++)
                    {
                        //GL.Vertex3(carcass[i][(j + 1) % 5]);
                        //GL.Vertex3(carcass[i][(j + 1) % 5] + CalculateNormal(carcass[i][(j + 1) % 5], carcass[i + 1][(j + 1) % 5], carcass[i][(j + 2) % 5]));

                        var a = CalculateNormal(carcass[i][(j + 1) % 5], carcass[i][j], carcass[i + 1][(j + 1) % 5]);
                        var b = CalculateNormal(carcass[i][(j + 1) % 5], carcass[i][(j + 2) % 5], carcass[i - 1][(j + 1) % 5]);
                        var c = CalculateNormal(carcass[i][(j + 1) % 5], carcass[i - 1][(j + 1) % 5], carcass[i][j]);
                        if (!al)
                        {
                            GL.Vertex3(carcass[i][(j + 1) % 5]);
                            GL.Vertex3(carcass[i][(j + 1) % 5] + a);

                            GL.Vertex3(carcass[i][(j + 1) % 5]);
                            GL.Vertex3(carcass[i][(j + 1) % 5] + b);

                            GL.Vertex3(carcass[i][(j + 1) % 5]);
                            GL.Vertex3(carcass[i][(j + 1) % 5] + c);
                        }
                        else
                        {
                            GL.Vertex3(carcass[i][(j + 1) % 5]);
                            GL.Vertex3(carcass[i][(j + 1) % 5] + a + b + c);
                        }

                    }
                else
                    for (int j = 0; j < 5; j++)

                    {
                        var a = CalculateNormal(carcass[0][(j + 1) % 5], carcass[0][(j + 2) % 5], carcass[0][j]);
                        var b = CalculateNormal(carcass[0][(j + 1) % 5], carcass[1][(j + 1) % 5], carcass[0][(j + 2) % 5]);
                        var c = CalculateNormal(carcass[0][(j + 1) % 5], carcass[0][j], carcass[1][(j + 1) % 5]);
                        var d = CalculateNormal(carcass[i][(j + 1) % 5], carcass[i][j], carcass[i][(j + 2) % 5]);
                        var e = CalculateNormal(carcass[i][(j + 1) % 5], carcass[i][(j + 2) % 5], carcass[i - 1][(j + 1) % 5]);
                        var f = CalculateNormal(carcass[i][(j + 1) % 5], carcass[i - 1][(j + 1) % 5], carcass[i][j]);
                        if (!al)
                        {

                            GL.Vertex3(carcass[0][(j + 1) % 5]);
                            GL.Vertex3(carcass[0][(j + 1) % 5] + a);

                            GL.Vertex3(carcass[0][(j + 1) % 5]);
                            GL.Vertex3(carcass[0][(j + 1) % 5] + b);

                            GL.Vertex3(carcass[0][(j + 1) % 5]);
                            GL.Vertex3(carcass[0][(j + 1) % 5] + c);

                            GL.Vertex3(carcass[i][(j + 1) % 5]);
                            GL.Vertex3(carcass[i][(j + 1) % 5] + d);

                            GL.Vertex3(carcass[i][(j + 1) % 5]);
                            GL.Vertex3(carcass[i][(j + 1) % 5] + e);

                            GL.Vertex3(carcass[i][(j + 1) % 5]);
                            GL.Vertex3(carcass[i][(j + 1) % 5] + f);
                        }
                        else
                        {
                            GL.Vertex3(carcass[0][(j + 1) % 5]);
                            GL.Vertex3(carcass[0][(j + 1) % 5] + a + b + c);

                            GL.Vertex3(carcass[i][(j + 1) % 5]);
                            GL.Vertex3(carcass[i][(j + 1) % 5] + d + e + f);

                        }
                    }
            }

            GL.End();
        }   

        private Vector3d CalculateNormal(Vector3d v0, Vector3d v1, Vector3d v2)
        {
            return 0.01*Vector3d.Cross(v2 - v0, v1 - v0);
        }

        
    }
}
