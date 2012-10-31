namespace Pirates.Window
{
    internal class XnaWindow
    {
        private int height = 600;
        private int width = 800;

        private bool isMouseVisible = true;
        private bool isResizing = false;

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public bool IsMouseVisible
        {
            get { return isMouseVisible; }
            set { isMouseVisible = value; }
        }

        public bool IsResizing
        {
            get { return isResizing; }
            set { isResizing = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }
    }
}