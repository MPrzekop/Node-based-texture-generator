using System;
using System.Linq;

namespace Node_based_texture_generator.Editor.Nodes.MathNode.Add
{
    public struct TypePair
    {
        public Type a, b;

        public TypePair(Type both)
        {
            a = both;
            b = both;
        }

        public TypePair(Type a, Type b)
        {
            this.a = a;
            this.b = b;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AddNodeAttribute : Attribute
    {
        private TypePair _supportedPair;


        public AddNodeAttribute(Type a, Type b)
        {
            SupportedPair = new TypePair(a, b);
        }
        public AddNodeAttribute(Type both)
        {
            SupportedPair = new TypePair(both, both);
        }

        public bool CanAdd(Type a, Type b)
        {
            return SupportedPair.a == a && SupportedPair.b == b;
        }


        public TypePair SupportedPair
        {
            get => _supportedPair;
            set => _supportedPair = value;
        }
    }
}