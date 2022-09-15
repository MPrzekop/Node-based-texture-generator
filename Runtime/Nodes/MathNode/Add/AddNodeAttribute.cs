using System;
using Node_based_texture_generator.Editor.Nodes.MathNode.Add;
using Node_based_texture_generator.Runtime.Nodes.MathNode.Multiply;

namespace Node_based_texture_generator.Runtime.Nodes.MathNode.Add
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AddNodeAttribute :Attribute, INodeMathOperationAttribute
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