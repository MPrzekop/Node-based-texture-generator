using System;
using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.ComputeNodes
{
    public abstract class BaseComputeNode : TextureGraphNode
    {
        [Input(connectionType = ConnectionType.Override), SerializeField]
        private Texture input;


        [SerializeField, Output(ShowBackingValue.Never)]
        private Texture output;

        [SerializeField] protected ComputeShader shader;
        protected Texture Input => input;

        public Texture Output1
        {
            get => output;
            set => output = value;
        }


        private void OnValidate()
        {
            OnInputChanged();
        }

        public override Texture GetTexture()
        {
            return Output1;
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
                if (Output1 != null)
                {
                    ((RenderTexture) Output1).Release();
                    Output1 = null;
                }

                return;
            }

            if (Output1 == null || Input.width != Output1.width || Input.height != Output1.height)
            {
                Output1 = new RenderTexture(Input.width, Input.height, 32, RenderTextureFormat.DefaultHDR)
                    {enableRandomWrite = true};
                ((RenderTexture) Output1).Create();
            }

            SetupShader();
            DispatchShader();

            UpdateTexture();
            UpdateNode(GetPort("output"));
        }


        public override object GetValue(NodePort port)
        {
            // Check which output is being requested. 
            // In this node, there aren't any other outputs than "result".
            if (port.fieldName == "output")
            {
                return this.Output1;
            }
            // Hopefully this won't ever happen, but we need to return something
            // in the odd case that the port isn't "result"
            else return null;
        }

        protected abstract int GetKernel();
        protected abstract void SetupShader();

        protected virtual void DispatchShader()
        {
            if (Input.width > 0 && Input.height > 0)
                shader.Dispatch(GetKernel(), Input.width / 8, Input.height / 8, 1);
        }
    }
}