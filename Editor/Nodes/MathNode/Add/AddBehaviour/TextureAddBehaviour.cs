using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Node_based_texture_generator.Editor.Nodes.MathNode.Add.AddBehaviour
{
    [AddNode(typeof(Texture))]
    [AddNode(typeof(RenderTexture))]
    public class TextureAddBehaviour : IAddBehaviour
    {
        private RenderTexture result;

        private CommandBuffer _commandBuffer;

        ~TextureAddBehaviour()
        {
            if (result != null)
            {
                result.Release();
            }
        }

        public CommandBuffer Buffer
        {
            get
            {
                if (_commandBuffer == null)
                {
                    _commandBuffer = new CommandBuffer();
                }

                return _commandBuffer;
            }
            set => _commandBuffer = value;
        }

        public object Add(object a, object b)
        {
            var mat = new Material(Shader.Find("Przekop/TextureGraph/AddTextures"));
            if (a is Texture aTex && b is Texture bTex)
            {
                if (result == null || result.width != aTex.width || result.height != aTex.height)
                {
                    if (result != null)
                    {
                        result.Release();
                    }

                    result = new RenderTexture(aTex.width, aTex.height, 32, DefaultFormat.HDR);
                }

                mat.SetTexture("_a", aTex);
                mat.SetTexture("_b", bTex);
                Buffer.Blit(aTex, result, mat);
                Graphics.ExecuteCommandBuffer(Buffer);
                Buffer.Clear();
                return result;
            }

            return null;
        }
    }
}