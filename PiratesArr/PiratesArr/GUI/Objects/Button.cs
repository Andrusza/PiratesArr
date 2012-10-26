namespace PiratesArr.GUI.Objects
{
    internal class Button : GUIObject
    {
        public Button()
        {
            this.IsClickable = true;
            this.IsDragAble = false;
            this.IsEditable = false;
            this.IsVisible = true;
        }

        public override bool Click(int x, int y)
        {
            if (this.IsClickable == true)
            {
                if (x >= this.Pos.X && x <= this.Pos.X + this.Width)
                {
                    if (y > this.Pos.Y && y < this.Pos.Y + this.Height)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override void MouseOver()
        {
            if (this.IsMouseOver == true)
            {
                if (this.Width + 10 >= this.Frame.Width)
                {
                    this.FrameWidth = 10;
                    this.FrameHeight = 10;
                }
            }
            else
            {
                if (this.Width <= this.Frame.Width - 10)
                {
                    this.FrameWidth = -10;
                    this.FrameHeight = -10;
                }
            }
        }

        public override void Drag()
        {
        }
    }
}