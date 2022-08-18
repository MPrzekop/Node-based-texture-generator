using System.Collections;
using System.Collections.Generic;
using Node_based_texture_generator.Editor.Nodes.MaterialNodes;
using UnityEngine;

public class SaturateNode : BlitNodeBase
{
   

    protected override void PrepareMaterial()
    {
        if (BlitMaterial == null)
        {
            BlitMaterial = new Material(Shader.Find("Przekop/TextureGraph/Saturate"));
        }   
    }
}
