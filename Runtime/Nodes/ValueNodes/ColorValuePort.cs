using UnityEngine;

namespace Node_based_texture_generator.Editor.Nodes.ValueNodes
{
    [CreateNodeMenu("Texture Generator/Values/Color")]
    public class ColorValuePort : GenericValueNode<Color>
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