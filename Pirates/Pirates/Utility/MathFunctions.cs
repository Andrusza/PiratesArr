using System;
using Microsoft.Xna.Framework;

namespace Pirates.Utility
{
    public static class MathFunctions
    {
        public static Vector2 AngleToVector(float angle)
        {
            angle = MathHelper.ToRadians(angle);
            Vector2 temp = new Vector2((float)Math.Sin(angle), (float)Math.Cos(angle));
            return temp;
        }

        //    public final void rotate(matrix  m) {
        //// Assuming the angles are in radians.
        //if ( m.m12 > 0.998 || m.m12 < -0.998 ) { // singularity at south or north pole
        //    heading = Math.atan2( -m.m20, m.m00 );
        //    bank = 0;
        //} else {
        //    heading = Math.atan2( m.m02, m.m22 );
        //    bank = Math.atan2( m.m10, m.m11 );
        //}
        //attitude = Math.asin( m.m12 );
        public static Vector3 ToEuler(Matrix m)
        {
            double heading = 0;
            double bank = 0;
            double altitude = 0;
            if (m.M23 > 0.998 || m.M23 < -0.998)
            {
                heading = Math.Atan2(-m.M31, m.M11);
                bank = 0;
            }
            else
            {
                heading = Math.Atan2(m.M13, m.M33);
                bank = Math.Atan2(m.M21, m.M22);
            }
            altitude = Math.Asin(m.M23);
            return new Vector3((float)heading, (float)bank, (float)altitude);
        }

        public static Vector3 Abs(Vector3 vec)
        {
            return new Vector3(Math.Abs(vec.X), Math.Abs(vec.Y), Math.Abs(vec.Z));
        }
    }
}