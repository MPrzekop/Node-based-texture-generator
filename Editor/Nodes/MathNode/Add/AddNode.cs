using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Node_based_texture_generator.Editor.Nodes.MathNode.Add;
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
        private Dictionary<Type, AddNodeAttribute[]> adderTypes;
        private Dictionary<TypePair, Type> pairsToAdder;

        [SerializeField] private IAddBehaviour _addBehaviour;
        [SerializeField, HideInInspector] private NodePort resultTargetCache;

        private void OnValidate()
        {
            if (adderTypes == null || pairsToAdder == null)
            {
                adderTypes = new Dictionary<Type, AddNodeAttribute[]>();
                pairsToAdder = new Dictionary<TypePair, Type>();
                var allAdders = Utility.Utility.FindAttributeUsers(typeof(AddNodeAttribute));
                foreach (var adder in allAdders)
                {
                    var arr = adder.GetCustomAttributes(typeof(AddNodeAttribute), true).ToArray();
                    var output = Array.ConvertAll(arr, x => (AddNodeAttribute) x);
                    foreach (var o in output)
                    {
                        pairsToAdder.Add(o.SupportedPair, adder);
                    }

                    if (arr != null && arr.Length > 0)
                    {
                        adderTypes.Add(adder, output
                        );
                    }
                }
            }
        }

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
                    {
                        var port = AddDynamicOutput(_typeA, fieldName: "result");
                        if (resultTargetCache != null && !String.IsNullOrEmpty(resultTargetCache.fieldName))
                            port.Connect(resultTargetCache);
                    }
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

        public override void OnRemoveConnection(NodePort port)
        {
            if (port.fieldName != "result")
            {
                resultTargetCache = GetPort("result").Connection;
            }

            base.OnRemoveConnection(port);
        }

        public override object GetValue(NodePort port)
        {
            if (_result == null)
                GetInputs();
            if (port.fieldName == "result")
            {
                resultTargetCache = GetPort("result").Connection;

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
            var pair = new TypePair(a.GetType(), b.GetType());
            Type value;
            if (pairsToAdder.TryGetValue(pair, out value))
            {
                Debug.Log("have adder " + value.GetType());
                if (value.GetInterfaces().Contains(typeof(IAddBehaviour)))
                {
                    var adderInstance = Activator.CreateInstance(value);
                    if (adderInstance is IAddBehaviour addBehaviour)
                    {
                        _addBehaviour = addBehaviour;
                    }

                    _result = _addBehaviour.Add(a, b);
                }
                else
                {
                    Debug.Log("does not implement addBehaviour");
                }
            }
            else
            {
                Debug.Log("Unsupported types");
            }
        }
    }
}