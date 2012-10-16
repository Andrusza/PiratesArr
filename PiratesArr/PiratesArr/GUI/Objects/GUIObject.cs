using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiratesArr.GUI.Objects
{
    abstract public class GUIObject
    {
        private string textureName;

        public string TextureName
        {
            get { return textureName; }
            set { textureName = value; }
        }

        private Texture2D texture;

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        private Rectangle frame;

        public int FrameWidth
        {
            set { frame.Width += value; }
        }

        public int FrameHeight
        {
            set { frame.Height += value; }
        }

        public Rectangle Frame
        {
            get { return frame; }
            set { frame = value; }
        }

        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        private int width;

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private Vector2 pos;

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        private bool isVisible;

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        private bool isClickAble;

        public bool IsClickAble
        {
            get { return isClickAble; }
            set { isClickAble = value; }
        }

        private bool isClicked;

        public bool IsClicked
        {
            get { return isClicked; }
            set { isClicked = value; }
        }

        private bool isEditable;

        public bool IsEditable
        {
            get { return isEditable; }
            set { isEditable = value; }
        }

        private bool isDragAble;

        public bool IsDragAble
        {
            get { return isDragAble; }
            set { isDragAble = value; }
        }

        private bool isMouseOver;

        public bool IsMouseOver
        {
            get { return isMouseOver; }
            set { isMouseOver = value; }
        }

        abstract public bool Click(int x, int y);

        abstract public void Drag();

        abstract public void MouseOver();
    }
}