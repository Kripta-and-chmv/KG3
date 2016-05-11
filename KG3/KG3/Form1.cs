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
using OpenTK;

namespace KG3
{
    public partial class Form1 : Form
    {
        Camera cam = new Camera();

        private bool loaded = false;
        private bool IsOrtho = false;
        Matrix4 perspect = Matrix4.CreatePerspectiveFieldOfView((float)(80 * Math.PI / 180), 1, 20, 500);
        Matrix4 ortho = Matrix4.CreateOrthographic(5, 5, 20, 100);

        public Form1()
        {
            InitializeComponent();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);

           

            GL.MatrixMode(MatrixMode.Projection);

            if (IsOrtho) GL.LoadMatrix(ref ortho);
                else GL.LoadMatrix(ref perspect);

            //Matrix4 modelview = Matrix4.LookAt(70, 70, 70, 0, 0, 0, 0, 1, 0);
            //GL.MatrixMode(MatrixMode.Modelview);
            //GL.LoadMatrix(ref modelview);
            cam.Look();
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
            if (!loaded)
                return;       

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);         

            Figure f = new Figure();
            f.DrawCarcass();
            f.DrawSurface();

            glControl1.SwapBuffers();
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!loaded) return;
            cam.Look();

            if (e.KeyCode == Keys.A)
            {
                cam.MoveLeftward();
            }
            if (e.KeyCode == Keys.D)
            {
                cam.MoveRighward();
            }
            if (e.KeyCode == Keys.O)
                IsOrtho = !IsOrtho;

            if(e.KeyCode==Keys.W)
            {
                cam.MoveForward();
            }
            if (e.KeyCode == Keys.S)
            {
                cam.MoveBackward();
            }

            if (e.KeyCode == Keys.R)
                cam.MoveUp();
            if (e.KeyCode == Keys.F)
                cam.MoveDown();

            if (e.KeyCode == Keys.R)
                cam.MoveUp();
            if (e.KeyCode == Keys.F)
                cam.MoveDown();

            if (e.KeyCode == Keys.J)
                cam.SeeLeft();
            if (e.KeyCode == Keys.L)
                cam.SeeRight();
            if (e.KeyCode == Keys.I)
                cam.SeeUp();
            if (e.KeyCode == Keys.K)
                cam.SeeDown();

            glControl1.Invalidate();
        }
    }
}
