using XNode;

namespace Node_based_texture_generator.Runtime.Nodes.Generators
{
    class WhiteNoiseNodePorts : INodePortsProvider
    {
        public void PopulatePorts(Node context)
        {
            ((INodePortsProvider) this).ClearPorts(context);
        }

        public object GetPortsValues(Node context)
        {
            return null;
        }
    }
}