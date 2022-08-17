using UnityEngine;
using UnityEngine.Experimental.Rendering;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.MaterialNodes
{
    public abstract class BlitNodeBase : TextureGraphNode
    {
        [Input(connectionType = ConnectionType.Override), SerializeField]
        private Texture input;

        [SerializeField, Output(ShowBackingValue.Never)]
        private Texture output;

        protected RenderTexture _operatingTexture;

        protected Texture Input => input;

        protected Material BlitMaterial { get; set; }

        public override Texture GetTexture()
        {
            if (Input == null)
            {
                output = null;
                return null;
            }


            PrepareOperatingTexture();
            PrepareMaterial();
            if (BlitMaterial != null)
            {
                Graphics.Blit(Input, _operatingTexture, BlitMaterial);
            }
            else
            {
                Graphics.Blit(Input, _operatingTexture);
            }

            output = _operatingTexture;
            return output;
        }

        protected virtual void PrepareOperatingTexture()
        {
            if (_operatingTexture != null)
            {
                _operatingTexture.Release();
            }

            _operatingTexture = new RenderTexture(Input.width, Input.height, 32, DefaultFormat.HDR);
            _operatingTexture.Create();
        }

        protected override void OnInputChanged()
        {
            Texture newInput = GetPort("input").GetInputValue<Texture>();
            if (newInput != Input)
            {
                input = newInput;
            }

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

        protected abstract void PrepareMaterial();
    }
}