﻿using System;
using Microsoft.Xna.Framework;
using Pirates.Shaders;

namespace Pirates.Loaders
{
    public partial class Terrain : ObjectGeometry
    {
        internal class PositionNormal
        {
            public Vector3 Position = new Vector3();
            public Vector3 Normal = new Vector3();
        }

        private Vector3 CalculatePosition(Vector3 position, WaterShader shader)
        {
            Vector3 P = position;
            for (int i = 0; i < 24; i += 6)
            {
                float A = shader.Waves[i] * shader.Waves[i + 3];
                float omega = 2.0f * 3.14f / shader.Waves[i];
                float phi = shader.Waves[i + 2] * omega;
                float Qi = shader.Waves[i + 1] / (omega * A * 4.0f);

                Vector2 a = new Vector2(shader.Waves[i + 4], shader.Waves[i + 5]);
                Vector2 b = new Vector2(position.X, position.Z);
                float term = omega * Vector2.Dot(a, b) + phi * shader.Time;

                float C = (float)Math.Cos(term);
                float S = (float)Math.Sin(term);
                P += new Vector3(Qi * A * shader.Waves[i + 4] * C, A * S * 10, i * A * shader.Waves[i + 5] * C);
            }
            return P;
        }

        private void CalculateNormal(PositionNormal v1, PositionNormal v2, PositionNormal v3)
        {
            Vector3 vu = v3.Position - v1.Position;
            Vector3 vt = v2.Position - v1.Position;
            Vector3 normal = Vector3.Cross(vu, vt);
            normal.Normalize();

            v1.Normal += normal;
            v2.Normal += normal;
            v3.Normal += normal;
        }

        public void GetObjectPositionOnWater(ObjectGeometry ship, WaterShader shader, out Vector3 height,out Vector3 normal)
        {
            
            PositionNormal p11=new PositionNormal();
            PositionNormal p12=new PositionNormal();
            PositionNormal p13=new PositionNormal();
            PositionNormal p21=new PositionNormal();
            PositionNormal p22=new PositionNormal();
            PositionNormal p23=new PositionNormal();
            PositionNormal p31=new PositionNormal();
            PositionNormal p32=new PositionNormal();
            PositionNormal p33=new PositionNormal();

            Vector3 position=new Vector3(50,20,100);
            

            p11.Position = CalculatePosition(Vector3.Transform(position + new Vector3(-10, 0, 10),ship.RotationMatrix), shader);
            p12.Position = CalculatePosition(Vector3.Transform(position + new Vector3(0, 0, 10),ship.RotationMatrix), shader);
            p13.Position = CalculatePosition(Vector3.Transform(position + new Vector3(10, 0, 10),ship.RotationMatrix), shader);

            p21.Position = CalculatePosition(Vector3.Transform(position + new Vector3(-10, 0, 0),ship.RotationMatrix), shader);
            p22.Position = CalculatePosition(Vector3.Transform(position + new Vector3(0, 0, 0),ship.RotationMatrix), shader);
            p23.Position = CalculatePosition(Vector3.Transform(position + new Vector3(10, 0, 0),ship.RotationMatrix), shader);

            p31.Position = CalculatePosition(Vector3.Transform(position + new Vector3(-10, 0, -10),ship.RotationMatrix), shader);
            p32.Position = CalculatePosition(Vector3.Transform(position + new Vector3(0, 0, -10),ship.RotationMatrix), shader);
            p33.Position = CalculatePosition(Vector3.Transform(position + new Vector3(10, 0, -10),ship.RotationMatrix), shader);

            CalculateNormal(p11, p22, p23);
            CalculateNormal(p11, p12, p23);

            CalculateNormal(p12, p22, p23);
            CalculateNormal(p12, p13, p23);

            CalculateNormal(p21, p31, p32);
            CalculateNormal(p21, p22, p32);

            CalculateNormal(p22, p32, p33);
            CalculateNormal(p22, p23, p33);

            //height = Vector3.Lerp(p11.Position, p33.Position, 0.5f);
            height = p22.Position;
            normal = Vector3.Lerp(p22.Normal, p32.Normal, 0.9f);
          normal.Normalize();
        }
    }
}