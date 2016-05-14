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
        Camera cam;
        private bool lookMode = false;
        private bool loaded = false;
        private bool IsOrtho = false;
        private bool IsNormalized = false;
        private int texId;

        private Point mouseCoord = new Point(0, 0), //Текущие координаты
            mouseCoordTemp = new Point(0, 0); //Предыдущие координаты

        public Form1()
        {
            InitializeComponent();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);
            GL.Viewport(0, 0, glControl1.ClientSize.Width, glControl1.ClientSize.Height);


            float aspect_ratio = Width / (float)Height;
            Matrix4 perspect = Matrix4.CreatePerspectiveFieldOfView((float)(50 * Math.PI / 180), aspect_ratio, 10, 200);
            Matrix4 ortho = Matrix4.CreateOrthographic(Width, Height, 10, 200);

            GL.MatrixMode(MatrixMode.Projection);

            if (chckbxProection.Checked) GL.LoadMatrix(ref ortho);
                else GL.LoadMatrix(ref perspect);
            //GL.MatrixMode(MatrixMode.Modelview);

            cam = new Camera {};

            cam.Resize(glControl1.Width, glControl1.Height);

            texId = Texture.LoadTexture("texture.bmp");
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
            cam.Update();
            GL.LoadMatrix(ref cam.Matrix);

            Figure f = new Figure();
            f.DrawCarcass();
            if(!chckbxCarcass.Checked)
                f.DrawSurface();
            //свет
            if (checkBox1.Checked)
                Lighting.On();

            glControl1.SwapBuffers();
        }

        private void SwitchProjection()
        {
            IsOrtho = !IsOrtho;

            float aspect_ratio = Width / (float)Height;
            GL.MatrixMode(MatrixMode.Projection);

            Matrix4 perspect = Matrix4.CreatePerspectiveFieldOfView((float)(90 * Math.PI / 180), aspect_ratio, 20, 500);
            Matrix4 ortho = Matrix4.CreateOrthographic(Width, Height, 500, 500);

            if (IsOrtho) GL.LoadMatrix(ref ortho);
            else GL.LoadMatrix(ref perspect);

            GL.MatrixMode(MatrixMode.Modelview);
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!loaded) return;
            if (e.KeyCode == Keys.Q)
                cam.MoveY(1);
            if (e.KeyCode == Keys.E)
                cam.MoveY(-1);
            if (e.KeyCode == Keys.A)
                cam.MoveX(1);
            if (e.KeyCode == Keys.D)
                cam.MoveX(-1);

            if (e.KeyCode == Keys.W)
                cam.MoveZ(1);
            if (e.KeyCode == Keys.S)
                cam.MoveZ(-1);
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            lookMode = true;
            glControl1.Cursor = Cursors.SizeAll; //меняем указатель
            mouseCoord.X = e.X;
            mouseCoord.Y = e.Y;
        }

        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            lookMode = false;
            glControl1.Cursor = Cursors.Arrow; //меняем указатель
        }

        private void chckbxProection_CheckedChanged(object sender, EventArgs e)
        {
            SwitchProjection();
        }

        private void chkbxNormal_CheckedChanged(object sender, EventArgs e)
        {
            SwitchNormalize();
        }
        private void SwitchNormalize()
        {
            IsNormalized = !IsNormalized;
            if (IsNormalized)
                GL.Enable(EnableCap.Normalize);
            else
                GL.Disable(EnableCap.Normalize);
        }
        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (lookMode)
            {
                mouseCoordTemp.X = e.X;
                mouseCoordTemp.Y = e.Y;

                var mouseX = mouseCoord.X - mouseCoordTemp.X;
                var mouseY = mouseCoord.Y - mouseCoordTemp.Y;

                if (Math.Abs(mouseX) > Math.Abs(mouseY))
                    if (mouseX > 0)
                        cam.TurnY(0.05f);
                    else
                        cam.TurnY(-0.05f);
                else if (mouseY > 0)
                    cam.TurnX(-0.05f);
                else
                    cam.TurnX(0.05f);

                mouseCoord = mouseCoordTemp;
            }
        }
    }
}
