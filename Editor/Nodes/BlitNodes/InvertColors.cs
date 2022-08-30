using Node_based_texture_generator.Editor.Nodes.BlitNodes.Base;
using UnityEngine;

namespace Node_based_texture_generator.Editor.Nodes.MaterialNodes
{    [CreateNodeMenu("Texture Generator/Image Operations/Invert Colors")]

    public class InvertColors : BlitWithInputPort
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