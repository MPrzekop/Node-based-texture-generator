using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Node_based_texture_generator.Editor.Nodes.MathNode.Multiply.MultiplyBehaviour
{
    [MultiplyNode(typeof(Texture), typeof(float), false)]
    [MultiplyNode(typeof(Texture), typeof(int), false)]
    [MultiplyNode(typeof(Texture), typeof(double), false)]
    [MultiplyNode(typeof(RenderTexture), typeof(float), false)]
    [MultiplyNode(typeof(RenderTexture), typeof(int), false)]
    [MultiplyNode(typeof(RenderTexture), typeof(double), false)]
    [MultiplyNode(typeof(Texture2D), typeof(float), false)]
    [MultiplyNode(typeof(Texture2D), typeof(int), false)]
    [MultiplyNode(typeof(Texture2D), typeof(double), false)]
    public class TextureNumberMultiplyBehaviour : IMathOperationBehaviour, IDisposable
    {
        private RenderTexture _result;

        private CommandBuffer _commandBuffer;

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


        public object Perform(object a, object b)
        {
            Texture texture;
            float value;
            if (a is Texture aT)
            {
                texture = aT;
                value = (float) b;
            }

            else if (b is Texture bT)
            {
                texture = bT;
                value = (float) a;
            }
            else
            {
                return null;
            }

            if (_result == null || _result.width != texture.width || _result.height != texture.height)
            {
                if (_result != null)
                {
                    _result.Release();
                }

                _result = new RenderTexture(texture.width, texture.height, 32, DefaultFormat.HDR);
            }

            var mat = new Material(Shader.Find("Przekop/TextureGraph/MultiplyTextureVector"));
            mat.SetTexture("_a", texture);
            mat.SetVector("_b", new Vector4(value, value, value, value));
            Buffer.Blit(texture, _result, mat);
            Graphics.ExecuteCommandBuffer(Buffer);
            Buffer.Clear();
            return _result;
        }


        ~TextureNumberMultiplyBehaviour()
        {
            Dispose();
        }

        public void Dispose()
        {
            _result?.Release();
            _commandBuffer?.Dispose();
        }
    }
}