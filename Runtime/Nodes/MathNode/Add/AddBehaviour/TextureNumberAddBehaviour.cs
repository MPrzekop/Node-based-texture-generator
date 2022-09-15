using Node_based_texture_generator.Editor.Nodes.MathNode.Add;
using Node_based_texture_generator.Runtime.Nodes.MathNode.Multiply.MultiplyBehaviour;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Node_based_texture_generator.Runtime.Nodes.MathNode.Add.AddBehaviour
{
    [AddNode(typeof(Texture), typeof(Vector2))]
    [AddNode(typeof(Texture), typeof(Vector3))]
    [AddNode(typeof(Texture), typeof(Vector4))]
    [AddNode(typeof(RenderTexture), typeof(Vector2))]
    [AddNode(typeof(RenderTexture), typeof(Vector3))]
    [AddNode(typeof(RenderTexture), typeof(Vector4))]
    [AddNode(typeof(Vector2), typeof(Texture))]
    [AddNode(typeof(Vector3), typeof(Texture))]
    [AddNode(typeof(Vector4), typeof(Texture))]
    [AddNode(typeof(Vector2), typeof(RenderTexture))]
    [AddNode(typeof(Vector3), typeof(RenderTexture))]
    [AddNode(typeof(Vector4), typeof(RenderTexture))]
    public class TextureVectorAddBehaviour : IMathOperationBehaviour
    {
        private RenderTexture _result;

        private CommandBuffer _commandBuffer;

        ~TextureVectorAddBehaviour()
        {
            if (_result != null)
            {
                _result.Release();
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

        public object Perform(object a, object b)
        {
            if (a is Texture aTex)
            {
                dynamic bVal = b;

                Vector4 bFloat = (Vector4) bVal;
                var mat = new Material(Shader.Find("Przekop/TextureGraph/AddTextureNumber"));

                if (_result == null || _result.width != aTex.width || _result.height != aTex.height)
                {
                    if (_result != null)
                    {
                        _result.Release();
                    }

                    _result = new RenderTexture(aTex.width, aTex.height, 32, DefaultFormat.HDR);
                }

                mat.SetTexture("_a", aTex);
                mat.SetVector("_b", bFloat);
                Buffer.Blit(aTex, _result, mat);
                Graphics.ExecuteCommandBuffer(Buffer);
                Buffer.Clear();
                return _result;
            }
            else if (b is Texture bTex)
            {
                dynamic aVal = a;
                Vector4 aFloat = (Vector4) aVal;
                var mat = new Material(Shader.Find("Przekop/TextureGraph/AddTextureNumber"));

                if (_result == null || _result.width != bTex.width || _result.height != bTex.height)
                {
                    if (_result != null)
                    {
                        _result.Release();
                    }

                    _result = new RenderTexture(bTex.width, bTex.height, 32, DefaultFormat.HDR);
                }

                mat.SetTexture("_a", bTex);
                mat.SetVector("_b", aFloat);
                Buffer.Blit(bTex, _result, mat);
                Graphics.ExecuteCommandBuffer(Buffer);
                Buffer.Clear();
                return _result;
            }

            return null;
        }
    }


    [AddNode(typeof(Texture), typeof(float))]
    [AddNode(typeof(Texture), typeof(int))]
    [AddNode(typeof(Texture), typeof(double))]
    [AddNode(typeof(RenderTexture), typeof(float))]
    [AddNode(typeof(RenderTexture), typeof(int))]
    [AddNode(typeof(RenderTexture), typeof(double))]
    [AddNode(typeof(float), typeof(Texture))]
    [AddNode(typeof(int), typeof(Texture))]
    [AddNode(typeof(double), typeof(Texture))]
    [AddNode(typeof(float), typeof(RenderTexture))]
    [AddNode(typeof(int), typeof(RenderTexture))]
    [AddNode(typeof(double), typeof(RenderTexture))]
    public class TextureNumberAddBehaviour : IMathOperationBehaviour
    {
        private RenderTexture _result;

        private CommandBuffer _commandBuffer;

        ~TextureNumberAddBehaviour()
        {
            if (_result != null)
            {
                _result.Release();
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

        public object Perform(object a, object b)
        {
            if (a is Texture aTex)
            {
                dynamic bVal = b;
                float bFloat = (float) bVal;
                var mat = new Material(Shader.Find("Przekop/TextureGraph/AddTextureNumber"));

                if (_result == null || _result.width != aTex.width || _result.height != aTex.height)
                {
                    if (_result != null)
                    {
                        _result.Release();
                    }

                    _result = new RenderTexture(aTex.width, aTex.height, 32, DefaultFormat.HDR);
                }

                mat.SetTexture("_a", aTex);
                mat.SetVector("_b", new Vector4(bFloat, bFloat, bFloat, bFloat));
                Buffer.Blit(aTex, _result, mat);
                Graphics.ExecuteCommandBuffer(Buffer);
                Buffer.Clear();
                return _result;
            }
            else if (b is Texture bTex)
            {
                dynamic aVal = a;
                float aFloat = (float) aVal;
                var mat = new Material(Shader.Find("Przekop/TextureGraph/AddTextureNumber"));

                if (_result == null || _result.width != bTex.width || _result.height != bTex.height)
                {
                    if (_result != null)
                    {
                        _result.Release();
                    }

                    _result = new RenderTexture(bTex.width, bTex.height, 32, DefaultFormat.HDR);
                }

                mat.SetTexture("_a", bTex);
                mat.SetVector("_b", new Vector4(aFloat, aFloat, aFloat, aFloat));
                Buffer.Blit(bTex, _result, mat);
                Graphics.ExecuteCommandBuffer(Buffer);
                Buffer.Clear();
                return _result;
            }

            return null;
        }
    }
}