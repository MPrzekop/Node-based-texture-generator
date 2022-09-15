using System;
using System.Collections.Generic;
using System.Linq;
using Node_based_texture_generator.Editor.Nodes;
using Node_based_texture_generator.Editor.Nodes.MathNode.Add;
using Node_based_texture_generator.Runtime.Nodes.Base;
using Node_based_texture_generator.Runtime.Nodes.MathNode.Multiply;
using Node_based_texture_generator.Runtime.Nodes.MathNode.Multiply.MultiplyBehaviour;
using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Runtime.Nodes.MathNode
{
    public abstract class MathOperationNode<TOperator, TAttribute> : TextureGraphNode
        where TAttribute : Attribute, INodeMathOperationAttribute where TOperator : IMathOperationBehaviour
    {
        [SerializeField, Input(connectionType = ConnectionType.Override)]
        private Texture a, b;

        [SerializeField] private object _result;

        private Texture _operatingTexture;
        private Dictionary<Type, TAttribute[]> _operatorTypes;
        private Dictionary<TypePair, Type> _pairsToOperator;
        private const string ResultPortName = "result";

        [SerializeField] private TOperator operationBehaviour;
        [SerializeField, HideInInspector] private NodePort resultTargetCache;

        public Dictionary<Type, TAttribute[]> OperatorTypes
        {
            get
            {
                if (_operatorTypes == null)
                {
                    SetupAdders();
                }

                return _operatorTypes;
            }
            set => _operatorTypes = value;
        }

        private Dictionary<TypePair, Type> PairsToOperator
        {
            get
            {
                if (_pairsToOperator == null)
                {
                    SetupAdders();
                }

                return _pairsToOperator;
            }
            set => _pairsToOperator = value;
        }

        private void SetupAdders()
        {
            if (_operatorTypes == null || _pairsToOperator == null)
            {
                _operatorTypes = new Dictionary<Type, TAttribute[]>();
                _pairsToOperator = new Dictionary<TypePair, Type>();
                var allAdders = Editor.Utility.Utility.FindAttributeUsers(typeof(TAttribute));
                foreach (var adder in allAdders)
                {
                    var arr = adder.GetCustomAttributes(typeof(TAttribute), true).ToArray();
                    var output = Array.ConvertAll(arr, x => (TAttribute) x);
                    foreach (var o in output)
                    {
                        _pairsToOperator.Add(o.SupportedPair, adder);
                    }

                    if (arr.Length > 0)
                    {
                        _operatorTypes.Add(adder, output);
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

        void ValidateOutput()
        {
            _result = null;
            if (_operatingTexture != null) RenderTexture.ReleaseTemporary((RenderTexture) _operatingTexture);

            try
            {
                RemoveDynamicPort(ResultPortName);
            }
            catch
            {
                //ignore
            }
        }

        void GetInputs()
        {
            object inputValueA, inputValueB;
            if (GetPort("a").IsConnected)
            {
                inputValueA = GetPort("a").GetInputValue();
            }
            else
            {
                inputValueA = null;
                ValidateOutput();
            }

            if (GetPort("b").IsConnected)
            {
                inputValueB = GetPort("b").GetInputValue();
            }
            else
            {
                inputValueB = null;
                ValidateOutput();
            }

            if (inputValueA != null && inputValueB != null)
            {
                Process(inputValueA, inputValueB);

                if (GetPort(ResultPortName) == null)
                {
                    var port = AddDynamicOutput(inputValueA.GetType(), fieldName: ResultPortName);
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
                UpdateNode(GetPort(ResultPortName));
            }
            catch
            {
                //ignore
            }
        }

        public override void OnRemoveConnection(NodePort port)
        {
            if (port.fieldName != ResultPortName)
            {
                if (GetPort(ResultPortName) != null)
                    resultTargetCache = GetPort(ResultPortName).Connection;
            }

            base.OnRemoveConnection(port);
        }

        public override object GetValue(NodePort port)
        {
            if (_result == null)
                GetInputs();
            if (port.fieldName == ResultPortName)
            {
                resultTargetCache = GetPort(ResultPortName).Connection;


                return _result;
            }

            return null;
        }

        void HandleInput<T>(T param)
        {
        }

        private void Process(object a, object b)
        {
            var pair = new TypePair(a.GetType(), b.GetType());
            var typePair = PairsToOperator.Where(x => x.Key == pair).ToList();

            if (typePair.Count > 0)
            {
                var value = typePair[0].Value;
                if (value.GetInterfaces().Contains(typeof(TOperator)))
                {
                    var adderInstance = Activator.CreateInstance(value);
                    if (adderInstance is TOperator addBehaviour)
                    {
                        this.operationBehaviour = addBehaviour;
                    }

                    _result = this.operationBehaviour.Perform(a, b);
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
                    RemoveDynamicPort(ResultPortName);
                }
                catch
                {
                    //ignore
                }
            }
        }
    }
}