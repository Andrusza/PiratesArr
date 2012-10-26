using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiratesArr.GUI.Objects
{
    abstract public class GUIObject
    {
        private Rectangle frame;
        private int height;
        private int width;

        private bool isClickable;
        private bool isClicked;
        private bool isDragAble;
        private bool isEditable;
        private bool isMouseOver;
        private bool isVisible;

        private Vector2 pos;

        private Texture2D texture;
        private string textureName;

        public Rectangle Frame
        {
            get { return frame; }
            set { frame = value; }
        }

        public int FrameHeight
        {
            set { frame.Height += value; }
        }

        public int FrameWidth
        {
            set { frame.Width += value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public bool IsClickable
        {
            get { return isClickable; }
            set { isClickable = value; }
        }

        public bool IsClicked
        {
            get { return isClicked; }
            set { isClicked = value; }
        }

        public bool IsDragAble
        {
            get { return isDragAble; }
            set { isDragAble = value; }
        }

        public bool IsEditable
        {
            get { return isEditable; }
            set { isEditable = value; }
        }

        public bool IsMouseOver
        {
            get { return isMouseOver; }
            set { isMouseOver = value; }
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public string TextureName
        {
            get { return textureName; }
            set { textureName = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        abstract public bool Click(int x, int y);

        abstract public void Drag();

        abstract public void MouseOver();
    }
}