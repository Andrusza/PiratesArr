using PiratesArr.Game.GameMode.BaseMode;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PiratesArr.Game.Camera.FirstPersonCamera;

namespace PiratesArr.Game.GameMode.Scene
{
    public partial class Scene : Mode
    {
        private Effect basic;

     
        Matrix projectionMatrix;
        Matrix worldMatrix;
        Matrix viewMatrix;

        FirstPersonCamera camera =new FirstPersonCamera();

        VertexPositionColor[] vertices;

        public Scene() : base() 
        {
            SetUpCamera();
            SetUpVertices();
            
        }

        private void SetUpCamera()
        {
            mainInstance.ViewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 50), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, mainInstance.GraphicsDevice.Viewport.AspectRatio, 1.0f, 300.0f);    
        }


        private void SetUpVertices()
        {
            vertices = new VertexPositionColor[3];

            vertices[0].Position = new Vector3(0f, 0f, 0f);
            vertices[0].Color = Color.Red;
            vertices[1].Position = new Vector3(10f, 10f, 0f);
            vertices[1].Color = Color.Yellow;
            vertices[2].Position = new Vector3(10f, 0f, -5f);
            vertices[2].Color = Color.Green;
        }
    }
}