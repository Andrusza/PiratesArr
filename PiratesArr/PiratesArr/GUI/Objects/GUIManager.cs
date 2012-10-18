using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiratesArr.GUI.Objects
{
    internal class GUIManager
    {
        private List<GUIObject> objects;
        private SpriteBatch spriteBatch;

        private static Main mainInstance;

        public bool IsClicked(int id)
        {
            return objects[id].IsClicked;
        }

        public void MouseOver(int x, int y)
        {
            Parallel.ForEach(objects, currentObj =>
            {
                currentObj.IsMouseOver = currentObj.Click(x, y);
                currentObj.MouseOver();
            }
           );
        }

        public GUIManager()
        {
            objects = new List<GUIObject>();
            mainInstance = Main.GetInstance();
            spriteBatch = new SpriteBatch(mainInstance.GraphicsDevice);
        }

        public void Add(GUIObject obj)
        {
            obj.Texture = mainInstance.Content.Load<Texture2D>("GUI\\ButtonsTex\\" + obj.TextureName);
            obj.Frame = new Rectangle((int)obj.Pos.X, (int)obj.Pos.Y, obj.Width, obj.Height);

            objects.Add(obj);
        }

        public void CheckIfClick(int x, int y)
        {
            Parallel.ForEach(objects, currentObj =>
                {
                    currentObj.IsClicked = currentObj.Click(x, y);
                }
            );
        }

        public void Draw()
        {
            foreach (GUIObject obj in objects)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(obj.Texture, obj.Frame, Color.White);
                spriteBatch.End();
            }
        }
    }
}