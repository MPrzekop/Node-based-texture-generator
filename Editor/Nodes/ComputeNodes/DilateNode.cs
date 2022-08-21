using UnityEngine;

namespace Node_based_texture_generator.Editor.Nodes.ComputeNodes
{
    class DilateNode : BaseComputeNode
    {
        [Input(connectionType = ConnectionType.Override), SerializeField]
        private int dilationRadius;

        protected override int GetKernel()
        {
            return shader.FindKernel("Dilate");
        }

        protected override void SetupShader()
        {
            shader.SetTexture(GetKernel(), "Input", Input);
            shader.SetTexture(GetKernel(), "Result", Output1);
            shader.SetInt("radius", dilationRadius);
        }
    }
}