using System;

namespace Pirates.Loaders.Clouds
{
    internal class ObjectCloud : ObjectGeometry
    {
        private static int ID;
        private int id;

        public int myID
        {
            get { return id; }
        }

        public CloudInstancer Instancer;
        private Random rnd;

        public ObjectCloud(CloudInstancer instancer)
        {
            Instancer = instancer;
            Instancer.instanceTransformMatrices.Add(this, ModelMatrix);
            Instancer.Instances.Add(myID, this);
        }
    }
}