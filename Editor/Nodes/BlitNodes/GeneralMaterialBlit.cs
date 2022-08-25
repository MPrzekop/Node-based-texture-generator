using System;
using System.Collections.Generic;
using Node_based_texture_generator.Editor.Nodes.MaterialNodes;
using UnityEditor;
using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.BlitNodes
{
    public class GeneralMaterialBlit : BlitNodeBase
    {
        [SerializeField] private Material _material;
        [SerializeField, HideInInspector] private List<NodePort> _dynamicPorts = new List<NodePort>();

        private void OnValidate()
        {
            PrepareMaterial();
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

            foreach (var port in _dynamicPorts)
            {
                RemoveDynamicPort(port);
            }

            _dynamicPorts.Clear();
            if (_material == null) return;
            var properties = UnityEditor.MaterialEditor.GetMaterialProperties((new[] {_material}));
            foreach (var property in properties)
            {
                NodePort addedPort = null;
                switch (property.type)
                {
                    case MaterialProperty.PropType.Color:
                        ;
                        addedPort = AddDynamicInput(typeof(Color), ConnectionType.Override, TypeConstraint.Inherited,
                            fieldName: property.name);
                        break;
                    case MaterialProperty.PropType.Vector:
                        addedPort = AddDynamicInput(typeof(Vector4), ConnectionType.Override, TypeConstraint.Inherited,
                            fieldName: property.name);

                        break;
                    case MaterialProperty.PropType.Float:
                        addedPort = AddDynamicInput(typeof(float), ConnectionType.Override, TypeConstraint.Inherited,
                            fieldName: property.name);
                        break;
                    case MaterialProperty.PropType.Range:
                        addedPort = AddDynamicInput(typeof(float), ConnectionType.Override, TypeConstraint.Inherited,
                            fieldName: property.name);
                        break;
                    case MaterialProperty.PropType.Texture:
                        addedPort = AddDynamicInput(typeof(Texture), ConnectionType.Override, TypeConstraint.Inherited,
                            fieldName: property.name);
                        break;
                    case MaterialProperty.PropType.Int:
                        addedPort = AddDynamicInput(typeof(int), ConnectionType.Override, TypeConstraint.Inherited,
                            fieldName: property.name);
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
    }
}