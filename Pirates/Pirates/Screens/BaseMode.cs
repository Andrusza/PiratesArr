using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using System;

namespace Pirates.Screens
{
    [Serializable()]
    public abstract class BaseMode : ISerializable
    {

        public abstract void LoadContent();

        public abstract void UnloadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);

        public abstract void GetObjectData(SerializationInfo info, StreamingContext ctxt);

        public abstract void ToFile();
        
    }
}