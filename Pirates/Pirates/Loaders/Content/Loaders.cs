using Microsoft.Xna.Framework.Graphics;

namespace Pirates.Loaders
{
    public enum ContentType
    {
        SPLASH_SCREEN,
        BUTTON,
        TEXTURE,
        FONT,
        HEIGHTMAP,
        FBX,
        SHADER
    }

    public static class ContentLoader
    {
        public static T Load<T>(ContentType type, string path)
        {
            return BaseClass.GetInstance().Content.Load<T>(type.ToString() + "//" + path);
        }

        public static void SetBuffers(IndexBuffer ibo, VertexBuffer vbo)
        {
            BaseClass.GetInstance().GraphicsDevice.Indices = ibo;
            BaseClass.GetInstance().GraphicsDevice.SetVertexBuffer(vbo);
            BaseClass.GetInstance().GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vbo.VertexCount, 0, ibo.IndexCount / 3);
        }
    }
}