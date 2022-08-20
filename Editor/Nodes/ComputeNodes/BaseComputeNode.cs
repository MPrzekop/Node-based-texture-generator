using System;
using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.ComputeNodes
{
    public abstract class BaseComputeNode : TextureGraphNode
    {
        [Input(connectionType = ConnectionType.Override), SerializeField]
        private Texture input;

        [Input(connectionType = ConnectionType.Override), SerializeField]
        private int dilationRadius;

        [SerializeField, Output(ShowBackingValue.Never)]
        private Texture output;

        [SerializeField] protected ComputeShader shader;
        protected Texture Input => input;


        private void OnValidate()
        {
            OnInputChanged();
        }

        public override Texture GetTexture()
        {
            return output;
        }

        protected override void OnInputChanged()
        {
            Texture newInput = GetPort("input").GetInputValue<Texture>();
            if (newInput != Input)
            {
                input = newInput;
            }

            if (input == null)
            {
                if (output != null)
                {
                    ((RenderTexture) output).Release();
                    output = null;
                }

                return;
            }

            if (output == null || Input.width != output.width || Input.height != output.height)
            {
                output = new RenderTexture(Input.width, Input.height, 32, RenderTextureFormat.DefaultHDR)
                    {enableRandomWrite = true};
                ((RenderTexture) output).Create();
            }

            shader.SetTexture(GetKernel(), "Input", Input);
            shader.SetTexture(GetKernel(), "Result", output);
            shader.SetInt("radius", dilationRadius);
            shader.Dispatch(GetKernel(), Input.width / 8, Input.height / 8, 1);

            UpdateTexture();
            UpdateNode(GetPort("output"));
        }


        public override object GetValue(NodePort port)
        {
            // Check which output is being requested. 
            // In this node, there aren't any other outputs than "result".
            if (port.fieldName == "output")
            {
                return this.output;
            }
            // Hopefully this won't ever happen, but we need to return something
            // in the odd case that the port isn't "result"
            else return null;
        }

        protected abstract int GetKernel();
    }
}