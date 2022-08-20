using UnityEngine;

namespace Node_based_texture_generator.Editor.Nodes.ComputeNodes
{
    class DilateNode : BaseComputeNode
    {
        protected override int GetKernel()
        {
            return shader.FindKernel("Dilate");
        }
    }
}