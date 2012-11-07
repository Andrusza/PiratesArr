using Pirates.Shaders;
using System;

namespace Pirates.Loaders.ModelsFbx
{
    public abstract class ObjectShader<T> : ObjectMesh
    {
        private BaseShader shader;

        public ObjectShader(string assetname, BaseShader effect): base(assetname, effect)
        {
            shader = effect;
        }
    }
}