using System;
using System.Collections.Generic;
using Pirates.Shaders;

namespace Pirates.Loaders.Cloud
{
    public abstract class Manager
    {
        private Instancer instancer;

        public Instancer Instancer
        {
            get { return instancer; }
            set { instancer = value; }
        }

        private List<Instance> list = new List<Instance>();

        public List<Instance> InstancesList
        {
            get { return list; }
            set { list = value; }
        }

        private Random rnd = new Random(DateTime.Now.Millisecond);

        public Random Rnd
        {
            get { return rnd; }
            set { rnd = value; }
        }

        public void Update(float gameTime)
        {
            Instancer.Update(gameTime);
        }

        public void Draw(BaseShader shader)
        {
            Instancer.Draw(shader);
        }
    }
}