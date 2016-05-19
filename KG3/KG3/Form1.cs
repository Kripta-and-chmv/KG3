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
        private bool lookMode = false;
        private bool loaded = false;
        private bool IsNormalized = false;
        private int texId;
        private readonly Camera _camera = new Camera();


        private bool _mouseRotate;
        private bool _mouseXzMove;
        private bool _mouseYMove;
        private int _myMouseYcoord;
        private int _myMouseXcoord;
        private int _myMouseYcoordVar;
        private int _myMouseXcoordVar;
        private double rotateZ;

        public Form1()
        {
            InitializeComponent();
            glControl1.MouseWheel += new MouseEventHandler(glControl1_MouseWheel);
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);
            GL.Viewport(0, 0, glControl1.ClientSize.Width, glControl1.ClientSize.Height);

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            texId = Texture.LoadTexture("1.bmp");


            SwitchProjection();
            _camera.Position_Camera(0, 0, -200, 0, 0, 0, 0, 10, 0);


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
            GL.MatrixMode(MatrixMode.Modelview);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            rotateZ += 0.2d;
            glControl1.Invalidate();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!loaded)
                return;
            GL.LoadIdentity();
            mouse_Events();
            _camera.Update();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _camera.Look();

            //свет
            if (chckbxLight.Checked)
                Lighting.On();
            else
                Lighting.Off();

            GL.Rotate(rotateZ, 0, 0, 1.0d);
            Figure f = new Figure(texId);
            f.DrawCarcass();
            if (chckbxNormalView.Checked)
                f.DrawNormals(chckbxNormalAl.Checked);
            if (!chckbxCarcass.Checked)
                f.DrawSurface(chckbxTexture.Checked);

            GL.Rotate(-rotateZ, 0, 0, 1.0d);




            //проекция

            SwitchProjection();

            GL.Disable(EnableCap.Light0);
            glControl1.SwapBuffers();
        }

        private void SwitchProjection()
        {

            var aspectRatio = glControl1.Width / (float)glControl1.Height;
            GL.MatrixMode(MatrixMode.Projection);

            var perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1, 500);
            var ortho = Matrix4.CreateOrthographic(80 * aspectRatio, 80, 1, 500);

            if (chckbxProection.Checked)
                GL.LoadMatrix(ref ortho);
            else
                GL.LoadMatrix(ref perpective);

            GL.MatrixMode(MatrixMode.Modelview);
        }

        private void chckbxProection_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkbxNormal_CheckedChanged(object sender, EventArgs e)
        {
        }
      

        private void chckbxLight_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chckbxNormalView.Checked)
                chckbxNormalAl.Enabled = false;
            else
                chckbxNormalAl.Enabled = true;

        }


        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (!loaded) return;
            //if (e.KeyCode == Keys.Q)
            //    cam.MoveY(1);
            //if (e.KeyCode == Keys.E)
            //    cam.MoveY(-1);
            //if (e.KeyCode == Keys.A)
            //    cam.MoveX(1);
            //if (e.KeyCode == Keys.D)
            //    cam.MoveX(-1);

            //if (e.KeyCode == Keys.W)
            //    cam.MoveZ(1);
            //if (e.KeyCode == Keys.S)
            //    cam.MoveZ(-1);
        }
        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _mouseRotate = true; // Если нажата левая кнопка мыши

            if (e.Button == MouseButtons.Right)
                _mouseXzMove = true; // Если нажата правая кнопка мыши

            if (e.Button == MouseButtons.Middle)
                _mouseYMove = true;// Если нажата средняя кнопка мыши

            _myMouseYcoord = e.X; // Передаем в нашу глобальную переменную позицию мыши по Х
            _myMouseXcoord = e.Y;
        }
        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            glControl1.Cursor = Cursors.Arrow; //меняем указатель
            _mouseRotate = _mouseXzMove = _mouseYMove = false;
        }
        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            _myMouseXcoordVar = e.Y;
            _myMouseYcoordVar = e.X;
        }
        private void glControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            _camera.Zoom(e.Delta / 100.0f);
        }
        private void mouse_Events()
        {
            if (_mouseRotate)  // Если нажата левая кнопка мыши
            {
                glControl1.Cursor = Cursors.SizeAll; //меняем указатель

                _camera.RotateXY(_myMouseYcoordVar - _myMouseYcoord, _myMouseXcoordVar - _myMouseXcoord);

                _myMouseYcoord = _myMouseYcoordVar;
                _myMouseXcoord = _myMouseXcoordVar;
            }
            else if (_mouseXzMove)
            {
                glControl1.Cursor = Cursors.SizeAll;

                _camera.MoveXZCamera((float)(_myMouseXcoordVar - _myMouseXcoord) / 10);
                _camera.Strafe(-((float)(_myMouseYcoordVar - _myMouseYcoord) / 10));

                _myMouseYcoord = _myMouseYcoordVar;
                _myMouseXcoord = _myMouseXcoordVar;

            }
            else if (_mouseYMove)
            {
                glControl1.Cursor = Cursors.SizeAll;

                _camera.MoveYCamera((float)(_myMouseXcoordVar - _myMouseXcoord) / 10);

                _myMouseYcoord = _myMouseYcoordVar;
                _myMouseXcoord = _myMouseXcoordVar;

            }
            else
            {
                glControl1.Invoke(new Action(() => glControl1.Cursor = Cursors.Default)); // возвращаем курсор
            }
        }
    }
}
