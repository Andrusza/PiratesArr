using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Pirates.Colision
{
    public class OwnDictionary<T, P>
    {
        private List<Pair<T, P>> list;

        public void SortKeys()
        {
            list.Sort(delegate(Pair<T, P> a, Pair<T, P> b) { return Comparer<T>.Default.Compare(a.key, b.key); });
        }

        public void SortValues()
        {
            foreach (Pair<T, P> x in list)
            {
                x.values.Sort(delegate(P a, P b) { return Comparer<P>.Default.Compare(a, b); });
            }
        }

        public OwnDictionary()
        {
            list = new List<Pair<T, P>>();
        }

        public void Add(T key, P value)
        {
            int index = list.FindIndex(element => element.key.Equals(key));
            if(index==-1)
            {
            list.Add(new Pair<T, P>(key, value));
            }
            else
            {
                list[index].values.Add(value);
            }
        }

        internal class Pair<T, P>
        {
            public T key = default(T);
            public List<P> values;

            public Pair(T key, P value)
            {
                this.key = key;
                values = new List<P>();

                this.values.Add(value);
            }
        }
    }

    public class QuadTree
    {
        private OwnDictionary<float, float> list;

        public QuadTree(Vector3[] vertices)
        {
            list = new OwnDictionary<float, float>();
            foreach (Vector3 v in vertices)
            {
                if (v.Y >= 30)
                {
                    list.Add(v.X, v.Z);
                }
            }
            list.SortKeys();
            list.SortValues();
        }
    }
}