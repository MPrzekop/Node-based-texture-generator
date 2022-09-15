using UnityEngine;

namespace Node_based_texture_generator.Editor.Nodes.ValueNodes
{
    [CreateNodeMenu("Texture Generator/Values/Vector3")]
    public class Vector3ValuePort : GenericValueNode<Vector3>
    {
        protected override Texture GetPreviewTexture()
        {
            var tex = new Texture2D(1, 1);
            tex.SetPixel(1, 1, new Color(value.x, value.y, value.z, 0));
            tex.Apply();
            return tex;
        }
    }
}