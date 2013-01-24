using System;
using Microsoft.Xna.Framework;
using Pirates.Utility;
using Pirates.Weather;

namespace Pirates.Physics
{
    public class ObjectPhysics
    {
        private float objSlopeX;
        private float objSlopeZ;

        private Vector3 oldPosition;
        private Vector3 acc;

        private float mass;

        private float frictionCoefficient;

        public float FrictionCoefficient
        {
            get { return frictionCoefficient; }
            set { frictionCoefficient = value; }
        }

        private float pitchForce = 0;
        private float rollForce = 0;
        private Vector3 velocity = Vector3.Zero;

        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        private bool objStatic = true;

        public bool ObjStatic
        {
            get { return objStatic; }
            set { objStatic = value; }
        }
        private Vector3 forceOnObject = Vector3.Zero;

        public Vector3 ForceOnObject
        {
            get { return forceOnObject; }
            set { forceOnObject = value; }
        }

        private Vector3 vDir = Vector3.Zero;

        public Vector3 VDir
        {
            get { return vDir; }
            set { vDir = value; }
        }

        private Vector2 wind;

        private Vector2 sailDirection = new Vector2(0, 1);

        public ObjectPhysics(float mass, Matrix modelMatrix)
        {
            this.mass = mass;
            frictionCoefficient = 0.25f;
        }

        private Vector3 ForcesOnStatic(Vector2 forces, Matrix modelMatrix)
        {
            Vector3 rotation = MathFunctions.ToEuler(modelMatrix);
            objSlopeX = rotation.Y;
            objSlopeZ = rotation.Z;
            ForceOnObject = Vector3.Zero;

            float frictionX = mass * 9.81f * FrictionCoefficient * (float)Math.Cos(objSlopeX);
            float frictionZ = mass * 9.81f * FrictionCoefficient * (float)Math.Cos(objSlopeZ);

            float forceX = mass * 9.81f * (float)Math.Sin(objSlopeX) + forces.X;
            float forceZ = mass * 9.81f * (float)Math.Sin(objSlopeZ) + forces.Y;

            if (Math.Abs(forceX) > Math.Abs(frictionX))
            {
                ObjStatic = false;
                forceX += frictionX;
            }
            else forceX = 0;

            if (Math.Abs(forceZ) > Math.Abs(frictionZ))
            {
                ObjStatic = false;
                forceZ += frictionZ;
            }
            else forceZ = 0;

            if (forceX < 0) vDir.X = -1; else vDir.X = 1;
            if (forceZ < 0) vDir.Z = -1; else vDir.Z = 1;

            //Console.WriteLine(forceX + " " + forceZ);
            return new Vector3(forceX, 0, forceZ);
        }

        public Vector3 ForcesInMotion(Matrix modelmatrix, Vector2 wind)
        {
            Vector3 rotation = MathFunctions.ToEuler(modelmatrix);
            objSlopeX = rotation.Y;
            objSlopeZ = rotation.Z;

            if (oldPosition.Y > modelmatrix.Translation.Y)
            {
                float forceX = 0;
                float forceZ = 0;

                if (velocity.X > 0)
                {
                    forceX = (mass * 9.81f * ((float)Math.Sin(objSlopeX) - vDir.X * FrictionCoefficient * (float)Math.Cos(objSlopeX))) + wind.X;
                }
                else
                {
                    forceOnObject.X = 0;
                    float frictionX = mass * 9.81f * FrictionCoefficient * (float)Math.Cos(objSlopeX);
                    float forceX2 = mass * 9.81f * (float)Math.Sin(objSlopeX) + wind.X;

                    if (Math.Abs(forceX2) > Math.Abs(frictionX))
                    {
                        forceX = (forceX2 + frictionX);
                        if (forceX < 0) vDir.X = -1; else vDir.X = 1;
                    }
                }

                if (velocity.Z > 0)
                {
                    forceZ = (mass * 9.81f * ((float)Math.Sin(objSlopeZ) - vDir.Z * FrictionCoefficient * (float)Math.Cos(objSlopeZ))) + wind.Y;
                }
                else
                {
                    forceOnObject.Z = 0;
                    float frictionZ = mass * 9.81f * FrictionCoefficient * (float)Math.Cos(objSlopeZ);
                    float forceZ2 = mass * 9.81f * (float)Math.Sin(objSlopeZ) + wind.Y;

                    if (Math.Abs(forceZ2) > Math.Abs(frictionZ))
                    {
                        forceZ = (forceZ2 + frictionZ);
                        if (forceZ < 0) vDir.Z = -1; else vDir.Z = 1;
                    }
                }

                return new Vector3(forceX, 0, forceZ);
            }
            else
            {
                float forceX = 0;
                float forceZ = 0;

                if (velocity.X > 0)
                {
                    forceX = (mass * 9.81f * ((float)Math.Sin(objSlopeX) - vDir.X * FrictionCoefficient * (float)Math.Cos(objSlopeX)));
                }
                else
                {
                    forceOnObject.X = 0;
                    float frictionX = mass * 9.81f * FrictionCoefficient * (float)Math.Cos(objSlopeX);
                    float forceX2 = mass * 9.81f * (float)Math.Sin(objSlopeX) + wind.X;

                    if (Math.Abs(forceX2) > Math.Abs(frictionX))
                    {
                        forceX = (forceX2 + frictionX);
                        if (forceX < 0) vDir.X = -1; else vDir.X = 1;
                    }
                }

                if (velocity.Z > 0)
                {
                    forceZ = (mass * 9.81f * ((float)Math.Sin(objSlopeZ) - vDir.Z * FrictionCoefficient * (float)Math.Cos(objSlopeZ)));
                }
                else
                {
                    forceOnObject.Z = 0;
                    float frictionZ = mass * 9.81f * FrictionCoefficient * (float)Math.Cos(objSlopeZ);
                    float forceZ2 = mass * 9.81f * (float)Math.Sin(objSlopeZ) + wind.Y;

                    if (Math.Abs(forceZ2) > Math.Abs(frictionZ))
                    {
                        forceZ = (forceZ2 + frictionZ);
                        if (forceZ < 0) vDir.Z = -1; else vDir.Z = 1;
                    }
                }

                return new Vector3(forceX, 0, forceZ);
            }
        }

        public Vector3 Update(Matrix modelMatrix, float deltaTime)
        {
            if (deltaTime > 0.0f)
            {
                if (ObjStatic)
                {
                    ForceOnObject = Vector3.Zero;

                    wind = WindForce();
                    wind = AirResistance(wind);
                    //Console.WriteLine(wind.ToString());

                    ForceOnObject += ForcesOnStatic(wind, modelMatrix);
                    acc = ForceOnObject / mass;
                    oldPosition = modelMatrix.Translation - acc * deltaTime * deltaTime;
                    if (acc.Length() != 0)
                    {
                        return Verlet(modelMatrix.Translation, deltaTime);
                    }
                    return modelMatrix.Translation;
                }
                else
                {
                    if (velocity.X > 0 || velocity.Z > 0)
                    {
                        Vector2 wind = WindForce();
                        wind = AirResistance(wind);
                        //Console.WriteLine(wind.ToString());
                        Vector3 landForces = ForcesInMotion(modelMatrix, wind);
                        //Console.WriteLine(wind.X);
                        ForceOnObject += landForces; //+ new Vector3(wind.X, 0, wind.Y);
                        //Console.WriteLine(forceOnObject.ToString());
                        acc = ForceOnObject / mass * 0.005f;
                        return Verlet(modelMatrix.Translation, deltaTime);
                    }

                    velocity = Vector3.Zero;
                    ObjStatic = true;
                    return modelMatrix.Translation;
                }
            }
            else return modelMatrix.Translation;
        }

        private Vector2 WindForce()
        {
            float dotProduct = Vector2.Dot(Wind.Direction, sailDirection);
            float wind = dotProduct * Wind.Force;
            return sailDirection * wind;
        }

        private Vector2 AirResistance(Vector2 wind)
        {
            wind.X -= 0.2f * velocity.X * velocity.X;
            wind.Y -= 0.2f * velocity.Z * velocity.Z;
            return wind;
        }

        public Vector3 Verlet(Vector3 position, float deltaTime)
        {
            Vector3 nextPosition = position + (position - oldPosition) + acc * deltaTime * deltaTime;
            oldPosition = position;
            velocity += (vDir * acc) * deltaTime;
            //Console.WriteLine(velocity.X.ToString());
            Console.WriteLine(position.ToString());

            return nextPosition;
        }
    }
}