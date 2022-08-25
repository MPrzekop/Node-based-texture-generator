using Node_based_texture_generator.Editor.Nodes.BlitNodes.Base;
using UnityEngine;

namespace Node_based_texture_generator.Editor.Nodes.BlitNodes
{
    public class SaturateNode : BlitWithInputPort
    {
        protected override void PrepareMaterial()
        {
            if (BlitMaterial == null)
            {
                BlitMaterial = new Material(Shader.Find("Przekop/TextureGraph/Saturate"));
            }
        }
    }
}