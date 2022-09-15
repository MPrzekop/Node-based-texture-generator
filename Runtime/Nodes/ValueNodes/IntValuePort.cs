using UnityEngine;

namespace Node_based_texture_generator.Editor.Nodes.ValueNodes
{
    [CreateNodeMenu("Texture Generator/Values/int")]
    public class IntValuePort : GenericValueNode<int>
    {
        protected override Texture GetPreviewTexture()
        {
            var tex = new Texture2D(1, 1);
            tex.SetPixel(1, 1, new Color(value, value, value, 0));
            tex.Apply();
            return tex;
        }
    }
}