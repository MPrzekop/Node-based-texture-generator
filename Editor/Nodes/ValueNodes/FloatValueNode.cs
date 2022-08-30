using System;
using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.ValueNodes
{
    [CreateNodeMenu("Texture Generator/Values/Float")]

    public class FloatValueInput : TextureGraphNode
    {
        [Output(ShowBackingValue.Always), SerializeField]
        private float value;


        public override Texture GetTexture()
        {
            return null;
        }

        protected override void OnInputChanged()
        {
            OnValidate();
        }

        private void OnValidate()
        {
            UpdateTexture();
            UpdateNode(GetPort("value"));
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "value")
            {
                return value;
            }

            return null;
        }
    }
}