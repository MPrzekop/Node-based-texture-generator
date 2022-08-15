using UnityEngine;

namespace Node_based_texture_generator.Editor.Nodes.MaterialNodes
{
    public class InvertColorsNode : BlitNodeBase
    {
        protected override void PrepareMaterial()
        {
            if (BlitMaterial == null)
            {
                BlitMaterial = new Material(Shader.Find("Przekop/TextureGraph/Invert"));
            }
        }
    }
}