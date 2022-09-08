using System;
using Node_based_texture_generator.Editor.Nodes.MathNode.Add;

namespace Node_based_texture_generator.Editor.Nodes.MathNode.Multiply
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MultiplyNodeAttribute : Attribute, INodeMathOperationAttribute
    {
        private TypePair _supportedPair;
        private bool _orderMatters;

        public MultiplyNodeAttribute(Type a, Type b, bool orderMatters = true)
        {
            SupportedPair = new TypePair(a, b, orderMatters);
        }

        public MultiplyNodeAttribute(Type both, bool orderMatters = true)
        {
            SupportedPair = new TypePair(both, both, orderMatters);
        }

        public TypePair SupportedPair
        {
            get => _supportedPair;
            set => _supportedPair = value;
        }
    }


    public interface INodeMathOperationAttribute
    {
         TypePair SupportedPair { get; set; }
    }
}