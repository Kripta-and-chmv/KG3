using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;

namespace KG3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(Color.White);
            SetupViewport();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            timer1.Start();
        }

        private void SetupViewport()
        {
            int w = glControl1.Width;
            int h = glControl1.Height;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, w, 0, h, -1, 1); // Верхний левый угол имеет кооординаты(0, 0)
            GL.Viewport(0, 0, w, h); // Использовать всю поверхность GLControl под рисование
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            glControl1.SwapBuffers();
            GL.ClearColor(Color.White);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.PointSize(6);

            SetupViewport();

            GL.Color3(Color.Black);
            GL.Begin(PrimitiveType.Lines);
            {
                GL.Vertex2(120, 120);
                GL.Vertex2(140, 100);

                GL.Vertex2(140, 100);
                GL.Vertex2(130, 80);

                GL.Vertex2(130, 80);
                GL.Vertex2(110, 80);

                GL.Vertex2(110, 80);
                GL.Vertex2(100, 100);

                GL.Vertex2(100, 100);
                GL.Vertex2(120, 120);
            }
            GL.End();
        }
    }
}
