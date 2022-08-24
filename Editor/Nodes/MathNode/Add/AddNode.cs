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

        private Texture _operatingTexture;
        private Dictionary<Type, AddNodeAttribute[]> adderTypes;
        private Dictionary<TypePair, Type> pairsToAdder;
        private const string resultPortName = "result";

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
            if (_result is Texture t)
                return t;


            return null;
//            throw new System.NotImplementedException();
        }

        void GetInputs()
        {
            object _inputValueA, _inputValueB;
            if (GetPort("a").IsConnected)
            {
                _inputValueA = GetPort("a").GetInputValue();
            }
            else
            {
                _inputValueA = null;

                _result = null;

                if (_operatingTexture != null) RenderTexture.ReleaseTemporary((RenderTexture) _operatingTexture);

                try
                {
                    RemoveDynamicPort(resultPortName);
                }
                catch
                {
                }
            }

            if (GetPort("b").IsConnected)
            {
                _inputValueB = GetPort("b").GetInputValue();
            }
            else
            {
                _inputValueB = null;

                _result = null;
                if (_operatingTexture != null) RenderTexture.ReleaseTemporary((RenderTexture) _operatingTexture);
                try
                {
                    RemoveDynamicPort(resultPortName);
                }
                catch
                {
                }
            }

            if (_inputValueA != null && _inputValueB != null)
            {
                add(_inputValueA, _inputValueB);

                if (GetPort(resultPortName) == null)
                {
                    var port = AddDynamicOutput(_inputValueA.GetType(), fieldName: resultPortName);
                    if (resultTargetCache != null && !String.IsNullOrEmpty(resultTargetCache.fieldName))
                        port.Connect(resultTargetCache);
                }
            }
        }

        protected override void OnInputChanged()
        {
            OnValidate();
            GetInputs();
            UpdateTexture();
            try
            {
                UpdateNode(GetPort(resultPortName));
            }
            catch
            {
                //ignore
            }
        }

        public override void OnRemoveConnection(NodePort port)
        {
            if (port.fieldName != resultPortName)
            {
                if (GetPort(resultPortName) != null)
                    resultTargetCache = GetPort(resultPortName).Connection;
            }

            base.OnRemoveConnection(port);
        }

        public override object GetValue(NodePort port)
        {
            if (_result == null)
                GetInputs();
            if (port.fieldName == resultPortName)
            {
                resultTargetCache = GetPort(resultPortName).Connection;


                return _result;
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