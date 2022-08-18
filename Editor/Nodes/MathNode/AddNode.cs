using System;
using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.MathNode
{
    public class AddNode : TextureGraphNode
    {
        [SerializeField, Input(connectionType = ConnectionType.Override)]
        private Texture a, b;

        [SerializeField] private object _result;
        private Type _typeA, _typeB;
        private Texture _operatingTexture;

        public override Texture GetTexture()
        {
            if (_typeA == typeof(RenderTexture) && _typeB != null)
            {
                return _operatingTexture;
            }

            return null;
//            throw new System.NotImplementedException();
        }

        void GetInputs()
        {
            object _inputValueA, _inputValueB;
            if (GetPort("a").IsConnected)
            {
                _inputValueA = GetPort("a").GetInputValue();
                _typeA = _inputValueA.GetType();
            }
            else
            {
                _inputValueA = null;
                _typeA = null;
                _result = null;

                if (_operatingTexture != null) RenderTexture.ReleaseTemporary((RenderTexture) _operatingTexture);

                try
                {
                    RemoveDynamicPort("result");
                }
                catch
                {
                }
            }

            if (GetPort("b").IsConnected)
            {
                _inputValueB = GetPort("b").GetInputValue();
                _typeB = _inputValueB.GetType();
            }
            else
            {
                _inputValueB = null;
                _typeB = null;
                _result = null;
                if (_operatingTexture != null) RenderTexture.ReleaseTemporary((RenderTexture) _operatingTexture);
                try
                {
                    RemoveDynamicPort("result");
                }
                catch
                {
                }
            }

            if (_inputValueA != null && _inputValueB != null)
            {
                if (_typeA == _typeB)
                {
                    add(_inputValueA, _inputValueB);

                    if (GetPort("result") == null)
                        AddDynamicOutput(_typeA, fieldName: "result");
                }
            }
        }

        protected override void OnInputChanged()
        {
            GetInputs();
            UpdateTexture();
            try
            {
                UpdateNode(GetPort("result"));
            }
            catch
            {
                //ignore
            }
        }

        public override object GetValue(NodePort port)
        {
            if (_result == null)
                GetInputs();
            if (port.fieldName == "result")
            {
                if (_typeA == typeof(RenderTexture))
                    return (Texture) _result;
                else
                {
                    return _result;
                }
            }

            return null;
        }

        void HandleInput<T>(T param)
        {
        }

        void add(object a, object b)
        {
            if (_typeA == typeof(int))
            {
                if (a is int aInt && b is int bInt)
                {
                    _result = aInt + bInt;
                }
            }
            else if (_typeA == typeof(uint))
            {
                if (a is uint aInt && b is uint bInt)
                {
                    _result = aInt + bInt;
                }
            }
            else if (_typeA == typeof(float))
            {
                if (a is float aInt && b is float bInt)
                {
                    _result = aInt + bInt;
                }
            }
            else if (_typeA == typeof(double))
            {
                if (a is double aInt && b is double bInt)
                {
                    _result = aInt + bInt;
                }
            }
            else if (_typeA == typeof(Vector2))
            {
                if (a is Vector2 aInt && b is Vector2 bInt)
                {
                    _result = aInt + bInt;
                }
            }
            else if (_typeA == typeof(Vector3))
            {
                if (a is Vector3 aInt && b is Vector3 bInt)
                {
                    _result = aInt + bInt;
                }
            }
            else if (_typeA == typeof(Vector4))
            {
                if (a is Vector4 aInt && b is Vector4 bInt)
                {
                    _result = aInt + bInt;
                }
            }
            else if (_typeA == typeof(Vector2Int))
            {
                if (a is Vector2Int aInt && b is Vector2Int bInt)
                {
                    _result = aInt + bInt;
                }
            }
            else if (_typeA == typeof(Vector3Int))
            {
                if (a is Vector3Int aInt && b is Vector3Int bInt)
                {
                    _result = aInt + bInt;
                }
            }
            else if (_typeA == typeof(RenderTexture))
            {
                if (!(a == null || b == null))
                {
                    if (a is RenderTexture aT && b is RenderTexture bT)
                    {
                        if (_operatingTexture == null)
                        {
                            _operatingTexture = RenderTexture.GetTemporary(aT.descriptor);
                            //_operatingTexture.Create();
                        }

                        var material = new Material(Shader.Find("Przekop/TextureGraph/AddTextures"));
                        material.SetTexture("_a", aT);
                        material.SetTexture("_b", bT);
                        Graphics.Blit(aT, (RenderTexture) _operatingTexture, material);
                        //DestroyImmediate(material);
                        _result = (Texture) _operatingTexture;
                        UpdateTexture();
                    }
                }
            }
        }
    }
}