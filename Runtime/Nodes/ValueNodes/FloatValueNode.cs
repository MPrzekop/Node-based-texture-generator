using System;
using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.ValueNodes
{
    [CreateNodeMenu("Texture Generator/Values/Float")]
    public class FloatValueInput : GenericValueNode<float>
    {
        protected override Texture GetPreviewTexture()
        {
            return null;
        }
    }
}