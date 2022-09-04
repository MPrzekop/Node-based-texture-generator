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


        protected override Texture GetPreviewTexture()
        {
            return null;
        }

        protected override void OnInputChanged()
        {
            OnValidate();
        }

        protected override void OnValidate()
        {
            UpdatePreviewTexture();
            base.OnValidate();
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