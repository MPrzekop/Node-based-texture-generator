using System.Collections.Generic;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.Generators
{
    interface INodePortsProvider
    {
        void PopulatePorts(Node context);

        object GetPortsValues(Node context);

        void ClearPorts(Node context)
        {
            foreach (var port in new List<NodePort>(context.DynamicPorts))
            {
                context.RemoveDynamicPort(port);
            }
        }
    }
}