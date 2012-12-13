namespace Pirates.Window
{
    internal class XnaWindow
    {
        private static int height = 600;
        private static int width = 840;

        private bool isMouseVisible = true;
        private bool isResizing = false;

        public static int Height
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

        public static int Width
        {
            get { return width; }
            set { width = value; }
        }
    }
}