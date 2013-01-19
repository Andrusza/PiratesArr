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
        private float mass = 0;
        private Vector3 oldPosition;

        private float elepasedTime;
        private float lastElapsed = 1;
        private float velocity;

        public float frictionCoefficient = 0.1f;
        public float objSlopeX = 0;
        public float objSlopeZ = 0;

        private float pitchForce = 0;
        private float rollForce = 0;

        private Vector2 forces = Vector2.Zero;

        public MaterialType material;
        public Vector2 acc;

        public Vector2 sailDirection = new Vector2(0, 1);

        public ObjectPhysics(float mass)
        {
            this.mass = mass;
        }

        public void StartPosition(Vector3 position)
        {
            oldPosition = position;
        }

        public Vector3 Update(ObjectShip ship, float deltaTime)
        {
            if (deltaTime > 0.0f)
            {
                Vector3 rotation = MathFunctions.ToEuler(ship.ModelMatrix);

                objSlopeX = rotation.Y;
                objSlopeZ = rotation.Z;

                if (oldPosition.Y < ship.ModelMatrix.Translation.Y) acc = Acceleration(1f);
                else acc = Acceleration(-1f);

                return Verlet(ship.ModelMatrix.Translation, deltaTime);
            }
            else
            {
                return ship.ModelMatrix.Translation;
            }
        }

        public Vector3 Verlet(Vector3 position, float deltaTime)
        {
            Vector3 tempPosition = position;

            oldPosition = position - deltaTime * (position - oldPosition) / lastElapsed;

            position += (position - oldPosition) + new Vector3(acc.X, 0, acc.Y) * deltaTime * deltaTime;
            velocity = Vector3.DistanceSquared(position, oldPosition) / deltaTime;
            Console.WriteLine(velocity);

            lastElapsed = deltaTime;

            oldPosition = tempPosition;
            return position;
        }

        private Vector2 WindForce()
        {
            float dotProduct = Vector2.Dot(Wind.Direction, sailDirection);

            Vector2 wind = dotProduct * Wind.Force * Wind.Direction;
            return wind;
        }

        private Vector2 Acceleration(float x)
        {
            if (material == MaterialType.Water)
            {
                frictionCoefficient = x * 0.05f;
                forces = Forces() / (mass);
                return forces;
            }
            else
            {
                frictionCoefficient = x * 0.2f;
                forces = SlopeForces() / (mass);
                return forces;
            }
        }

        private Vector2 Forces()
        {
            forces += WindForce();
            if (forces.Length() != 0)
            {
                float pitchForce = 0;
                float rollForce = 0;

                pitchForce = mass * 9.81f * ((float)Math.Sin(objSlopeX) + frictionCoefficient * (float)Math.Cos(objSlopeX));

                rollForce = mass * 9.81f * ((float)Math.Sin(objSlopeZ) + frictionCoefficient * (float)Math.Cos(objSlopeZ));

                forces += AirResistance(forces);
                return forces;
            }
            else return Vector2.Zero;
        }

        private Vector2 SlopeForces()
        {
            pitchForce += mass * 9.81f * ((float)Math.Sin(objSlopeX) + frictionCoefficient * (float)Math.Cos(objSlopeX));
            rollForce += mass * 9.81f * ((float)Math.Sin(objSlopeZ) + frictionCoefficient * (float)Math.Cos(objSlopeZ));

            return new Vector2(pitchForce, rollForce);
        }

        private Vector2 AirResistance(Vector2 wind)
        {
            float resistance = 0.00025f * velocity * velocity;
            wind.X -= resistance;
            wind.Y -= resistance;

            if (wind.X < 0) wind.X = 0f;
            if (wind.Y < 0) wind.Y = 0f;
            return wind;
        }
    }
}