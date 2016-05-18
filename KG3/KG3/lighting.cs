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
        public static void On()
        {
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

            
           // GL.LightModel(LightModelParameter.LightModelAmbient, new float[] { 0.2f, 0.2f, 0.2f, 1.0f });
           // GL.LightModel(LightModelParameter.LightModelTwoSide, 1);
            GL.LightModel(LightModelParameter.LightModelLocalViewer, 1);

            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 0f, 0f, -100f });
            //GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.3f, 0.3f, 0.3f, 1.0f });
            //GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            //GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 0.5f, 0.5f, 0.5f, 0.5f });
            //GL.Light(LightName.Light0, LightParameter.SpotExponent, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, new[] { 0.5f, 0.5f, 0.5f });
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, new[] { 0.5f, 0.5f, 0.5f });
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, 128f);

            GL.Enable(EnableCap.ColorMaterial);
        }

        public static void Off()
        {
            GL.Disable(EnableCap.Lighting);
        }
    }
}
