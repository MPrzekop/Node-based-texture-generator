using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Node_based_texture_generator.Editor.Nodes.MathNode.Add;
using Node_based_texture_generator.Editor.Nodes.MathNode.Multiply;
using Node_based_texture_generator.Editor.Nodes.MathNode.Multiply.MultiplyBehaviour;
using Unity.EditorCoroutines.Editor;
using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.MathNode
{
    public abstract class MathOperationNode<TOperator, TAttribute> : TextureGraphNode
        where TAttribute : Attribute, INodeMathOperationAttribute where TOperator : IMathOperationBehaviour
    {
        [SerializeField, Input(connectionType = ConnectionType.Override)]
        private Texture a, b;

        [SerializeField] private object _result;

        private Texture _operatingTexture;
        private Dictionary<Type, TAttribute[]> adderTypes;
        private Dictionary<TypePair, Type> pairsToAdder;
        private const string resultPortName = "result";

        [SerializeField] private TOperator _addBehaviour;
        [SerializeField, HideInInspector] private NodePort resultTargetCache;

        public Dictionary<Type, TAttribute[]> AdderTypes
        {
            get
            {
                if (adderTypes == null)
                {
                    SetupAdders();
                }
                return adderTypes;
            }
            set => adderTypes = value;
        }

        public Dictionary<TypePair, Type> PairsToAdder
        {
            get
            {
                if (pairsToAdder == null)
                {
                    SetupAdders();
                }
                return pairsToAdder;
            }
            set => pairsToAdder = value;
        }

        void SetupAdders()
        {
            if (adderTypes == null || pairsToAdder == null)
            {
                adderTypes = new Dictionary<Type, TAttribute[]>();
                pairsToAdder = new Dictionary<TypePair, Type>();
                var allAdders = Utility.Utility.FindAttributeUsers(typeof(TAttribute));
                foreach (var adder in allAdders)
                {
                    var arr = adder.GetCustomAttributes(typeof(TAttribute), true).ToArray();
                    var output = Array.ConvertAll(arr, x => (TAttribute) x);
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
        
        protected override void OnValidate()
        {
            SetupAdders();
            base.OnValidate();
        }

        protected override Texture GetPreviewTexture()
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
            GetInputs();
            UpdatePreviewTexture();
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
            var typePair = PairsToAdder.Where(x => x.Key == pair).ToList();

            if (typePair.Count > 0)
            {
                value = typePair[0].Value;
                if (value.GetInterfaces().Contains(typeof(TOperator)))
                {
                    var adderInstance = Activator.CreateInstance(value);
                    if (adderInstance is TOperator addBehaviour)
                    {
                        _addBehaviour = addBehaviour;
                    }

                    _result = _addBehaviour.Perform(a, b);
                }
                else
                {
                    Debug.Log("does not implement addBehaviour");
                }
            }
            else
            {
                Debug.Log("Unsupported types");
                try
                {
                    RemoveDynamicPort(resultPortName);
                }
                catch
                {
                }
            }
        }
    }
}