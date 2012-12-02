using Microsoft.Xna.Framework;

namespace Pirates.Loaders.Cloud
{
    public class Instance : ObjectGeometry
    {
        private static int ID;
        private int id;

        public int myID
        {
            get { return id; }
        }

        public Instancer Instancer;

        public Instance(Instancer instancer, float scale, Vector3 translate, Vector3 information)
        {
            ID++;
            id = ID;
            this.Scale(scale);
            this.Translate(translate);
            this.Update();

            Instancer = instancer;

            modelMatrix.M13 = scale;
            modelMatrix.M24 = scale;

            modelMatrix.M12 = information.X;
            modelMatrix.M23 = information.Y;
            modelMatrix.M34 = information.Z;
            Instancer.instanceTransformMatrices.Add(this, ModelMatrix);
        }
    }
}