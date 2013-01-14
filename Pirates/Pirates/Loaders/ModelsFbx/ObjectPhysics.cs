using System;
using Microsoft.Xna.Framework;

namespace Pirates.Loaders.ModelsFbx
{
    internal class ObjectPhysics
    {
        private float mass;
        private Vector3 position;
        private Vector2 direction;

        private float velocity = 0;
        private float oldVelocity = 0;
        private float angularOldVelocity = 0;
        private float angularVelocity = 0;
        private float acceleration = 0;
        private float angularAcceleration = 0;
        private float inertia = 0;

        private float thurstForce = 0;
        private float angularForce = 0f;

        private float length=100f;
        private float width=20f;

        private float frictionFactor = 0.1f;
        private Vector2 sailDirection;
        private Vector2 windDirection = new Vector2(0, 1);

        private float orientationInRadians = 0;

        private float windForce = 100000f;

        public ObjectPhysics(float mass)
        {
            this.mass = mass;
        }

        public Vector2 Update(Vector2 position, Vector2 sailDirection, Vector2 direction, float deltaTime)
        {
            //angularForce -= 0.2f;
            this.sailDirection = sailDirection;
            this.direction = direction;
            return AngularVerlet(direction, deltaTime);
            //return Verlet(position, deltaTime);
        }

        public void Velocity(float deltaTime)
        {
            velocity = Forces() / mass * deltaTime + velocity;
        }

        public float Forces()
        {
            return WindForce() - frictionFactor - AirResistance();
        }

        private float FrictionForce()
        {
            return (mass * 9.81f) * frictionFactor;
        }

        private float AirResistance()
        {
            float result = -0.26f * velocity * velocity;
            Console.WriteLine("air: " + result);
            return result;
        }

        private float WindForce()
        {
            float dotProduct = Vector2.Dot(windDirection, sailDirection);
            return dotProduct * windForce;
        }

        private Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Sin(angle), (float)Math.Cos(angle));
        }

        public static float VectorToAngle(Vector2 orientation)
        {
            return (float)Math.Atan2(orientation.X, orientation.Y);
        }

        public float Inertia()
        {
            return (mass * length * length) / 24f + (mass * width * width) / 62f;
        }

        public float AngularForces()
        {
            angularForce = Inertia() * angularForce / mass;
            angularForce = -FrictionForce();
        }

        public Vector2 Verlet(Vector2 position, float dt)
        {
            oldVelocity = velocity;
            velocity += Forces() / mass * dt;
            position += direction * ((oldVelocity + velocity) * 0.5f * dt);
            return position;
        }

        public Vector2 AngularVerlet(Vector2 direction, float dt)
        {
            float angle = VectorToAngle(direction);
            angularOldVelocity = angularVelocity;
            angularVelocity += AngularForces() / mass * dt;
            angle += (angularOldVelocity + angularVelocity) * 0.5f * dt;
            return AngleToVector(angle);
        }
    }
}

//velX = x - lastX
//velY = y - lastY

//nextX = x + velX + accX * timestepSq
//nextY = y + velY + accY * timestepSq

//lastX = x
//lastY = y

//x = nextX
//y = nextY