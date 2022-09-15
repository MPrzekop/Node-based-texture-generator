using UnityEngine;

namespace Node_based_texture_generator.Editor.Nodes.ValueNodes
{
    [CreateNodeMenu("Texture Generator/Values/Vector2")]
    public class Vector2ValuePort : GenericValueNode<Vector2>
    {
        protected override Texture GetPreviewTexture()
        {
            var tex = new Texture2D(1, 1);
            tex.SetPixel(1, 1, new Color(value.x, value.y, 0, 0));
            tex.Apply();
            return tex;
        }
    }
}