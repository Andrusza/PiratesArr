namespace Pirates.Loaders.Clouds
{
    internal class CloudInstancer : Instancer
    {
        private float x = 0;

        public override void Update(float time)
        {
            x += 0.0001f;

            this.Rotate(x, 0, 0);
            this.Update();
        }
    }
}