using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Pirates.Loaders.Colision
{
    public static class Vector3Extensions
    {
        public static float Dot(this Vector3 op1, Vector3 op2)
        {
            return Vector3.Dot(op1, op2);
        }

        public static Vector3 Cross(this Vector3 op1, Vector3 op2)
        {
            return Vector3.Cross(op1, op2);
        }
    }

    internal static class GilbertJohnsonKeerthi
    {
        private const int MaxIterations = 20;

        public static bool BodiesIntersect(IList<Vector3> shape1, IList<Vector3> shape2)
        {
            Vector3 initialPoint = shape1[0] - shape2[0];
            Vector3 S = MaxPointInMinkDiffAlongDir(shape1, shape2, initialPoint);
            Vector3 D = -S;
            List<Vector3> simplex = new List<Vector3>();
            simplex.Add(S);

            for (int i = 0; i < MaxIterations; i++)
            {
                Vector3 A = MaxPointInMinkDiffAlongDir(shape1, shape2, D);
                if (Vector3.Dot(A, D) < 0)
                {
                    return false;
                }

                simplex.Add(A);

                if (UpdateSimplexAndDirection(simplex, ref D))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool UpdateSimplexAndDirection(List<Vector3> simplex, ref Vector3 direction)
        {
            if (simplex.Count == 2)
            {
                Vector3 A = simplex[1];
                Vector3 B = simplex[0];
                Vector3 AB = B - A;
                Vector3 AO = -A;

                if (AB.Dot(AO) > 0)
                {
                    direction = AB.Cross(AO).Cross(AB);
                }
                else
                {
                    direction = AO;
                }
            }

            else if (simplex.Count == 3)
            {
                Vector3 A = simplex[2];
                Vector3 B = simplex[1];
                Vector3 C = simplex[0];
                Vector3 AO = -A;

                Vector3 AB = B - A;
                Vector3 AC = C - A;
                Vector3 ABC = AB.Cross(AC);

                if (ABC.Cross(AC).Dot(AO) > 0)
                {
                    if (AC.Dot(AO) > 0)
                    {
                        simplex.Clear();
                        simplex.Add(C);
                        simplex.Add(A);
                        direction = AC.Cross(AO).Cross(AC);
                    }
                    else
                        if (AB.Dot(AO) > 0)
                        {
                            simplex.Clear();
                            simplex.Add(B);
                            simplex.Add(A);
                            direction = AB.Cross(AO).Cross(AB);
                        }
                        else
                        {
                            simplex.Clear();
                            simplex.Add(A);
                            direction = AO;
                        }
                }
                else
                {
                    if (AB.Cross(ABC).Dot(AO) > 0)
                    {
                        if (AB.Dot(AO) > 0)
                        {
                            simplex.Clear();
                            simplex.Add(B);
                            simplex.Add(A);
                            direction = AB.Cross(AO).Cross(AB);
                        }
                        else
                        {
                            simplex.Clear();
                            simplex.Add(A);
                            direction = AO;
                        }
                    }
                    else
                    {
                        if (ABC.Dot(AO) > 0)
                        {
                            //the simplex stays A, B, C
                            direction = ABC;
                        }
                        else
                        {
                            simplex.Clear();
                            simplex.Add(B);
                            simplex.Add(C);
                            simplex.Add(A);

                            direction = -ABC;
                        }
                    }
                }
            }

            else //if (simplex.Count == 4)
            {
                Vector3 A = simplex[3];
                Vector3 B = simplex[2];
                Vector3 C = simplex[1];
                Vector3 D = simplex[0];

                Vector3 AO = -A;
                Vector3 AB = B - A;
                Vector3 AC = C - A;
                Vector3 AD = D - A;
                Vector3 ABC = AB.Cross(AC);
                Vector3 ACD = AC.Cross(AD);
                Vector3 ADB = AD.Cross(AB);

                int BsideOnACD = Math.Sign(ACD.Dot(AB));
                int CsideOnADB = Math.Sign(ADB.Dot(AC));
                int DsideOnABC = Math.Sign(ABC.Dot(AD));

                bool ABsameAsOrigin = Math.Sign(ACD.Dot(AO)) == BsideOnACD;
                bool ACsameAsOrigin = Math.Sign(ADB.Dot(AO)) == CsideOnADB;
                bool ADsameAsOrigin = Math.Sign(ABC.Dot(AO)) == DsideOnABC;

                if (ABsameAsOrigin && ACsameAsOrigin && ADsameAsOrigin)
                {
                    return true;
                }

                else if (!ABsameAsOrigin)
                {
                    simplex.Remove(B);

                    direction = ACD * -BsideOnACD;
                }

                else if (!ACsameAsOrigin)
                {
                    simplex.Remove(C);

                    direction = ADB * -CsideOnADB;
                }

                else //if (!ADsameAsOrigin)
                {
                    simplex.Remove(D);

                    direction = ABC * -DsideOnABC;
                }

                return UpdateSimplexAndDirection(simplex, ref direction);
            }

            return false;
        }

        private static Vector3 MaxPointInMinkDiffAlongDir(IList<Vector3> shape1, IList<Vector3> shape2, Vector3 direction)
        {
            return MaxPointAlongDirection(shape1, direction) - MaxPointAlongDirection(shape2, Vector3.Negate(direction));
        }

        private static Vector3 MaxPointAlongDirection(IList<Vector3> shape, Vector3 direction)
        {
            Vector3 max = shape[0];
            foreach (Vector3 point in shape)
            {
                if (max.Dot(direction) < point.Dot(direction))
                {
                    max = point;
                }
            }

            return max;
        }
    }
}