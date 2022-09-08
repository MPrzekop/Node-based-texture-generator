using System;
using System.Collections.Generic;
using System.Linq;
using Codice.CM.SEIDInfo;
using Node_based_texture_generator.Editor.Nodes.BlitNodes.Base;
using Node_based_texture_generator.Editor.Nodes.MaterialNodes;
using UnityEditor;
using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.BlitNodes
{
    [System.Serializable]
    enum propertyType
    {
        Color,
        Vector,
        Float,
        Texture,
        Int
    }

    [CreateNodeMenu("Texture Generator/Image Operations/General Material Operation")]
    public class GeneralMaterialBlit : BlitNodeBase
    {
        [SerializeField] private Material _material;
        [SerializeField, HideInInspector] private List<NodePort> _dynamicPorts = new List<NodePort>();
        [SerializeField, Input] private Vector2Int outputResolution;

        protected override void OnValidate()
        {
            PrepareMaterial();
            SetMaterialProperties();
            base.OnValidate();
        }

        protected override void PrepareMaterial()
        {
            if (BlitMaterial != _material)
            {
                RebuildProperties();
            }

            BlitMaterial = _material;
        }

        void RebuildProperties()
        {
#if UNITY_EDITOR


            // foreach (var port in DynamicInputs)
            // {
            //     Debug.Log("removing " + port.fieldName);
            //     RemoveDynamicPort(port);
            // }


            if (_material == null) return;
            var properties = UnityEditor.MaterialEditor.GetMaterialProperties((new[] {_material}));
            var disposablePorts = DynamicInputs.Where(x => !properties.Select(p => p.name).Contains(x.fieldName))
                .ToList();
            foreach (var port in disposablePorts)
            {
                RemoveDynamicPort(port);
            }

            foreach (var property in properties)
            {
                NodePort addedPort = null;
                Type t = typeof(Color);

                switch (property.type)
                {
                    case MaterialProperty.PropType.Color:
                        addedPort = AddPort(property, typeof(Color));

                        break;
                    case MaterialProperty.PropType.Vector:
                        addedPort = AddPort(property, typeof(Vector4));

                        break;
                    case MaterialProperty.PropType.Float:
                        addedPort = AddPort(property, typeof(float));
                        break;
                    case MaterialProperty.PropType.Range:
                        addedPort = AddPort(property, typeof(float));
                        break;
                    case MaterialProperty.PropType.Texture:
                        addedPort = AddPort(property, typeof(Texture));
                        break;
                    case MaterialProperty.PropType.Int:
                        addedPort = AddPort(property, typeof(int));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (addedPort != null)
                {
                    _dynamicPorts.Add(addedPort);
                }
            }
#endif
        }

        private NodePort AddPort(MaterialProperty property, Type t)
        {
            NodePort addedPort = null;
            if (ValidatePortType(property, t))
            {
                addedPort = AddDynamicInput(t, ConnectionType.Override,
                    TypeConstraint.Inherited,
                    fieldName: property.name);
            }

            return addedPort;
        }


        private bool ValidatePortType(MaterialProperty property, Type t)
        {
            if (HasPort(property.name))
            {
                if (GetPort(property.name).ValueType != t)
                {
                    RemoveDynamicPort(property.name);
                    return true;
                }

                return false;
            }

            return true;
        }

        void SetMaterialProperties()
        {
            if (_material == null) return;
            foreach (var port in DynamicInputs)
            {
                var value = port.GetInputValue();
                if (value == null) continue;
                if (value is Color c)
                {
                    _material.SetColor(port.fieldName, c);
                }
                else if (value is Vector4 v)
                {
                    _material.SetVector(port.fieldName, v);
                }
                else if (value is float f)
                {
                    _material.SetFloat(port.fieldName, f);
                }
                else if (value is int i)
                {
                    _material.SetInt(port.fieldName, i);
                }
                else if (value is Texture t)
                {
                    _material.SetTexture(port.fieldName, t);
                }
            }
        }


        protected override Texture GetBlitInputTexture()
        {
            return _dynamicPorts.FirstOrDefault(x => x.ValueType == typeof(Texture))?.GetInputValue<Texture>();
        }

        protected override Vector2Int GetOutputResolution()
        {
            return outputResolution;
        }

        protected override void OnInputChanged()
        {
            GetPortValue(ref outputResolution, "outputResolution");

            PrepareMaterial();
            SetMaterialProperties();
            base.OnInputChanged();
        }
    }
}