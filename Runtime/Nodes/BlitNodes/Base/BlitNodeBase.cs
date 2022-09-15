using Node_based_texture_generator.Editor.Nodes;
using Node_based_texture_generator.Runtime.Nodes.Base;
using UnityEngine;
using UnityEngine.Rendering;
using XNode;

namespace Node_based_texture_generator.Runtime.Nodes.BlitNodes.Base
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

        protected override Texture GetPreviewTexture()
        {
            if (GetOutputResolution().x < 0)
            {
                output = null;
                return null;
            }


            PrepareOperatingTexture();
            PrepareMaterial();
            RenderToResult(ref _operatingTexture);
            output = _operatingTexture;
            return output;
        }


        protected virtual void RenderToResult(ref RenderTexture result)
        {
            if (BlitMaterial != null)
            {
                BlitBuffer.Blit(GetBlitInputTexture(), result, BlitMaterial);
            }
            else
            {
                BlitBuffer.Blit(GetBlitInputTexture(), result);
            }

            ExecuteCommandBuffer();
        }

        /// <summary>
        /// Get output resolution where -1,-1 means error/no output
        /// </summary>
        /// <returns></returns>
        protected virtual Vector2Int GetOutputResolution()
        {
            return new Vector2Int(-1, -1);
        }

        /// <summary>
        /// Provide input texture for Blit('INPUT',result,material)
        /// </summary>
        /// <returns>input texture null if none</returns>
        protected virtual Texture GetBlitInputTexture()
        {
            return null;
        }

        protected virtual void PrepareOperatingTexture()
        {
            if (GetOutputResolution().x < 0) return;
            if (_operatingTexture != null &&
                (GetOutputResolution().x != _operatingTexture.width ||
                 GetOutputResolution().y != _operatingTexture.height))
            {
                RenderTexture.ReleaseTemporary(_operatingTexture);
                _operatingTexture = null;
            }

            if (_operatingTexture == null)
            {
                _operatingTexture =
                    RenderTexture.GetTemporary(GetOutputResolution().x, GetOutputResolution().y, 0,
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
            UpdatePreviewTexture();
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
                    UpdatePreviewTexture();
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