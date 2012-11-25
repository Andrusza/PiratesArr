using System;
using System.Collections.Generic;
using Pirates.Shaders;

namespace Pirates.Loaders.Cloud
{
    public interface IManager
    {
        Instancer Instancer { get; }

        List<Instance> InstancesList { get; set; }

        Random Rnd { get; set; }

        void Update(float gameTime);

        void Draw(BaseShader shader);
    }
}