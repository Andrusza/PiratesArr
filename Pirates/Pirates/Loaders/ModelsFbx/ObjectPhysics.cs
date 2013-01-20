using System;
using Microsoft.Xna.Framework;
using Pirates.Loaders.ModelsFbx;
using Pirates.Utility;
using Pirates.Weather;

namespace Pirates.Physics
{
    public enum MaterialType
    {
        Water, Island
    };

    public class ObjectPhysics
    {
        private float objSlopeX;
        private float objSlopeZ;

        private Vector3 oldPosition;
        private Vector2 acc;

        private float mass;

        public MaterialType material;
        private float frictionCoefficient;
        private Vector2 forces;

        private float pitchForce = 0;
        private float rollForce = 0;
        private Vector3 velocity = Vector3.Zero;
        private bool objStaticX = true;
        private bool objStaticZ = true;

        private Vector2 wind;

        private Vector2 sailDirection = new Vector2(0, 1);

        public void StartPosition(ObjectShip ship, float x)
        {
            Vector3 rotation = MathFunctions.ToEuler(ship.ModelMatrix);

            objSlopeX = 0;
            objSlopeZ = 0;

            acc = Acceleration(x);
            oldPosition = ship.ModelMatrix.Translation + new Vector3(-acc.X, 0, -acc.Y);
            objStaticX = false;
            objStaticZ = false;
        }

        public ObjectPhysics(float mass)
        {
            this.mass = mass;
        }

        public Vector3 Update(ObjectShip ship, float deltaTime)
        {
            if (deltaTime > 0.0f)
            {
                Vector3 rotation = MathFunctions.ToEuler(ship.ModelMatrix);

                //objSlopeX = Math.Abs(rotation.Y);
                //objSlopeZ = Math.Abs(rotation.Z);

                objSlopeX = 0;
                objSlopeZ = 0;


                if (objStaticX && objStaticZ)
                {
                    StartPosition(ship, -1f);
                    pitchForce = 0;
                    rollForce = 0;
                    acc = Acceleration(-1f);
                }

                if (!objStaticX || !objStaticZ)
                {
                    if (oldPosition.Y < ship.ModelMatrix.Translation.Y)
                    {
                        acc = Acceleration(1f);
                    }
                    else
                    {
                        acc = Acceleration(-1f);
                    }
                }

                if (!objStaticX || !objStaticZ)
                {
                    return Verlet(ship.ModelMatrix.Translation, deltaTime);
                }
                return ship.ModelMatrix.Translation;
            }
            else
            {
                return ship.ModelMatrix.Translation;
            }
        }

        public Vector2 Acceleration(float x)
        {
            if (material == MaterialType.Water)
            {
                frictionCoefficient = x * 0.05f;
                acc = (Forces(x) / mass) * 0.005f;
                Console.WriteLine(acc.ToString());
                return acc;
            }
            else
            {
                frictionCoefficient = x * 0.35f;
                acc = (SlopeForces(x) / mass) * 0.005f;
                //Console.WriteLine(acc.ToString());
                return acc;
            }
        }

        public Vector2 Forces(float x)
        {
            Vector2 waves = SlopeForces(x) * 3f;
            // Console.WriteLine(waves.X);
            wind += WindForce();
            // Console.WriteLine(waves.X);
            Vector2 airResistance = AirResistance(wind);
            return wind;
        }

        private Vector2 WindForce()
        {
            float dotProduct = Vector2.Dot(Wind.Direction, sailDirection);
            Wind.Force -= 1000;
            float wind = dotProduct * Wind.Force;
            return sailDirection* wind;
        }

        private Vector2 AirResistance(Vector2 wind)
        {
            wind.X -= 0.2f * velocity.Z * velocity.Z;
            wind.Y -= 0.2f * velocity.Y * velocity.Y;
            return wind;
        }

        public Vector2 SlopeForces(float x)
        {
            pitchForce += -x * (mass * 9.81f * ((float)Math.Sin(objSlopeX) + frictionCoefficient * (float)Math.Cos(objSlopeX)));
            rollForce += -x * (mass * 9.81f * ((float)Math.Sin(objSlopeZ) + frictionCoefficient * (float)Math.Cos(objSlopeZ)));

            if (objStaticX || objStaticZ)
            {
                if (objStaticX)
                {
                    if (rollForce < 0)
                    {
                        rollForce = 0;
                    }
                    else
                    {
                        objStaticX = false;
                    }
                }

                if (objStaticZ)
                {
                    if (pitchForce < 0)
                    {
                        pitchForce = 0;
                    }
                    else
                    {
                        objStaticZ = false;
                    }
                }
            }

            if (!objStaticX || !objStaticZ)
            {
                if (velocity.Z < 0)
                {
                    objStaticX = true;
                    rollForce = 0;
                    velocity.Z = 0;
                }

                if (velocity.X < 0)
                {
                    objStaticZ = true;
                    pitchForce = 0;
                    velocity.X = 0;
                }
            }
            return new Vector2(rollForce, pitchForce);
        }

        public Vector3 Verlet(Vector3 position, float deltaTime)
        {
            Vector3 nextPosition = position + (position - oldPosition) + new Vector3(acc.Y, 0, acc.X) * deltaTime * deltaTime;
            oldPosition = position;
            velocity += new Vector3(acc.Y, 0, acc.X) * deltaTime;
            //Console.WriteLine(velocity.ToString());

            return nextPosition;
        }
    }
}