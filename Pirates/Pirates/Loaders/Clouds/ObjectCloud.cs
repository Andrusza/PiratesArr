using Microsoft.Xna.Framework;

namespace Pirates.Loaders.Cloud
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

        public ObjectCloud(CloudInstancer instancer, float scale, Vector3 translate)
        {
            ID++;
            id = ID;
            //this.Scale(scale);
            this.Translate(translate);
            this.Update();

            Instancer = instancer;

            modelMatrix.M13 = 100;
            modelMatrix.M24 = 100;

            modelMatrix.M12 = 0;
            modelMatrix.M23 = 1;
            modelMatrix.M34 = 0.2f;
            Instancer.instanceTransformMatrices.Add(this, ModelMatrix);

            Instancer.Instances.Add(myID, this);
        }
    }
}