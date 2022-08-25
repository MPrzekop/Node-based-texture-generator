using UnityEngine;
using UnityEngine.Rendering;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.BlitNodes.Base
{
    public abstract class BlitNodeBase : TextureGraphNode
    {
        [SerializeField, Output(ShowBackingValue.Never)]
        private Texture output;

        protected RenderTexture _operatingTexture;

        protected CommandBuffer blitBuffer;


        protected Material BlitMaterial { get; set; }

        protected CommandBuffer BlitBuffer
        {
            get
            {
                if (blitBuffer == null)
                {
                    blitBuffer = new CommandBuffer();
                }

                return blitBuffer;
            }
            set => blitBuffer = value;
        }

        public override Texture GetTexture()
        {
            if (GetInputTexture() == null)
            {
                output = null;
                return null;
            }


            PrepareOperatingTexture();
            PrepareMaterial();
            if (BlitMaterial != null)
            {
                BlitBuffer.Blit(GetInputTexture(), _operatingTexture, BlitMaterial);
            }
            else
            {
                BlitBuffer.Blit(GetInputTexture(), _operatingTexture);
            }

            ExecuteCommandBuffer();

            output = _operatingTexture;
            return output;
        }

        protected abstract Texture GetInputTexture();

        protected virtual void PrepareOperatingTexture()
        {
            if (_operatingTexture != null &&
                (GetInputTexture().width != _operatingTexture.width ||
                 GetInputTexture().height != _operatingTexture.height))
            {
                RenderTexture.ReleaseTemporary(_operatingTexture);
                _operatingTexture = null;
            }

            if (_operatingTexture == null)
            {
                _operatingTexture =
                    RenderTexture.GetTemporary(GetInputTexture().width, GetInputTexture().height, 32,
                        RenderTextureFormat.DefaultHDR);
                _operatingTexture.Create();
            }
        }

        protected virtual void ExecuteCommandBuffer()
        {
            Graphics.ExecuteCommandBuffer(BlitBuffer);
            BlitBuffer.Clear();
        }

        protected override void OnInputChanged()
        {
            UpdateTexture();
            UpdateNode(GetPort("output"));
        }

        public override object GetValue(NodePort port)
        {
            // Check which output is being requested. 
            // In this node, there aren't any other outputs than "result".
            if (port.fieldName == "output")
            {
                if (output == null)
                {
                    UpdateTexture();
                }

                return this.output;
            }
            // Hopefully this won't ever happen, but we need to return something
            // in the odd case that the port isn't "result"
            else return null;
        }

        protected abstract void PrepareMaterial();
    }
}