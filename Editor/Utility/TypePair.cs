using System;

namespace Node_based_texture_generator.Editor.Nodes.MathNode.Add
{
    public struct TypePair
    {
        public Type a, b;

        public bool OrderMatters;

        public TypePair(Type both, bool orderMatters = true)
        {
            a = both;
            OrderMatters = orderMatters;
            b = both;
        }

        public TypePair(Type a, Type b, bool orderMatters = true)
        {
            this.a = a;
            this.b = b;
            OrderMatters = orderMatters;
        }

        public static bool operator ==(TypePair a, TypePair b)
        {
            if (a.OrderMatters && b.OrderMatters)
            {
                return a.a == b.a && a.b == b.b;
            }
            else
            {
                return (a.a == b.a && a.b == b.b) || (a.a == b.b && a.b == b.a);
            }
        }

        public static bool operator !=(TypePair a, TypePair b)
        {
            return !(a == b);
        }
    }
}