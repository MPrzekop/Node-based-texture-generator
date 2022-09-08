using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.Generators
{
    public class PerlinNoiseNodePorts : INodePortsProvider
    {
        public struct PerlinPortsValues
        {
            public Vector2 tiling;
        }

        public void PopulatePorts(Node context)
        {
            ((INodePortsProvider) this).ClearPorts(context);
            var port = context.AddDynamicInput(typeof(Vector2), Node.ConnectionType.Override,
                Node.TypeConstraint.Inherited,
                "Tiling");
        }

        public object GetPortsValues(Node context)
        {
            return new PerlinPortsValues()
            {
                tiling = context.GetPort("Tiling").IsConnected
                    ? context.GetInputValue<Vector2>("Tiling")
                    : new Vector2Int(1, 1)
            };
        }
    }
}