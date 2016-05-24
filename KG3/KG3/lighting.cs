using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace KG3
{
    static class Lighting
    {
        public static void On(int numb)
        {
            GL.Enable(EnableCap.Lighting);

            if (numb == 1)
            {
                //направленный источник света
                //находится в бесконечности и свет от него распространяется в заданном направлении
                GL.Enable(EnableCap.Light0);                
                GL.LightModel(LightModelParameter.LightModelLocalViewer, 1);
                GL.Light(LightName.Light0, LightParameter.Position, new float[] { 1000f, 0f, -100f });
            }
            if(numb==2)
            {
                //точечный источник света
                //убывание интенсивности с расстоянием
                //отключено (по умолчанию)
                GL.Enable(EnableCap.Light1);
                GL.LightModel(LightModelParameter.LightModelLocalViewer, 1);
                GL.Light(LightName.Light1, LightParameter.Position, new float[] { 0, 50f, 50 });
                GL.Light(LightName.Light1, LightParameter.Ambient, new float[] { 0.1f, 0.1f, 0.1f, 1.0f });
                GL.Light(LightName.Light1, LightParameter.Diffuse, new float[] { 0.4f, 0.7f, 0.2f });
            }
            if(numb ==3)
            {
                //точечный источник света
                //убывание интенсивности с расстоянием
                GL.Enable(EnableCap.Light2);
                 GL.LightModel(LightModelParameter.LightModelLocalViewer, 1);
                float kQ;
                float kL;
                float kC;
                float radius;
                float att;

                att = 800;
                radius = (float)Math.Sqrt(2)*50;
                kQ = att / (3 * radius * radius);
                kL = att / (3 * radius);
                kC = att / 3;


                GL.Light(LightName.Light2, LightParameter.Position, new float[] { 0, 50f, 50 });
                GL.Light(LightName.Light2, LightParameter.Ambient, new float[] { 0.1f, 0.1f, 0.1f, 1.0f });
                GL.Light(LightName.Light2, LightParameter.Diffuse, new float[] { 0.4f, 0.7f, 0.2f });
                GL.Light(LightName.Light2, LightParameter.ConstantAttenuation, kC);
                GL.Light(LightName.Light2, LightParameter.LinearAttenuation, kL);
                GL.Light(LightName.Light2, LightParameter.QuadraticAttenuation, kQ);
            }
            if(numb==4)
            {
                //прожектор
                //убывание интенсивности с расстоянием
                //отключено (по умолчанию)
                GL.Enable(EnableCap.Light3);
                GL.LightModel(LightModelParameter.LightModelLocalViewer, 1);
                GL.Light(LightName.Light3, LightParameter.Position, new float[] { -50, 50f, 0 });
                GL.Light(LightName.Light3, LightParameter.Ambient, new float[] { 0.1f, 0.1f, 0.1f, 1.0f });
                GL.Light(LightName.Light3, LightParameter.Diffuse, new float[] { 0.4f, 0.7f, 0.2f });
                GL.Light(LightName.Light3, LightParameter.SpotDirection, new float[] { 0f, 0f, 0f });
                GL.Light(LightName.Light3, LightParameter.SpotCutoff, 70);

            }
            if(numb==5)
            {
                //прожектор
                //включен рассчет убывания интенсивности для прожектора
                GL.Enable(EnableCap.Light4);
                GL.LightModel(LightModelParameter.LightModelLocalViewer, 1);
                GL.Light(LightName.Light4, LightParameter.Position, new float[] { -50, 50f, 0 });
                GL.Light(LightName.Light4, LightParameter.Ambient, new float[] { 0.1f, 0.1f, 0.1f, 1.0f });
                GL.Light(LightName.Light4, LightParameter.Diffuse, new float[] { 0.4f, 0.7f, 0.2f });
                GL.Light(LightName.Light4, LightParameter.SpotDirection, new float[] { 0f, 0f, 0f });
                GL.Light(LightName.Light4, LightParameter.SpotCutoff, 50);
                GL.Light(LightName.Light4, LightParameter.SpotExponent, 15.0f);

            }
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, new[] { 0.5f, 0.5f, 0.5f });
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, new[] { 0.5f, 0.5f, 0.5f });
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, 128f);

            GL.Enable(EnableCap.ColorMaterial);
        }

        public static void Off()
        {
            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.Light0);
            GL.Disable(EnableCap.Light1);
            GL.Disable(EnableCap.Light2);
            GL.Disable(EnableCap.Light3);
            GL.Disable(EnableCap.Light4);
            GL.Disable(EnableCap.ColorMaterial);
        }
    }
}
