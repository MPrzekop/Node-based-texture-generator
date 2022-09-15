using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.ValueNodes
{
    [CreateNodeMenu("Texture Generator/Values/Vector 4")]
    public class Vector4ValueInput : GenericValueNode<Vector4>
    {
        protected override Texture GetPreviewTexture()
        {
            var tex = new Texture2D(1, 1);
            tex.SetPixel(1, 1, value);
            tex.Apply();
            return tex;
        }
    }
}