using System.Collections;
using System.Collections.Generic;
using Node_based_texture_generator.Editor.Nodes.BlitNodes.Base;
using UnityEngine;

public class UV : BlitNodeBase
{
    [SerializeField, Input] private Vector2Int outputResolution;


    protected override void PrepareMaterial()
    {
        BlitMaterial = new Material(Shader.Find("Przekop/TextureGraph/UVGenerator"));
    }

    protected override Vector2Int GetOutputResolution()
    {
        if (outputResolution.x <= 0) outputResolution.x = 1;
        if (outputResolution.y <= 0) outputResolution.y = 1;
        return outputResolution;
    }
    
    protected override void OnInputChanged()
    {
        GetPortValue(ref outputResolution, "outputResolution");


        base.OnInputChanged();
    }
}