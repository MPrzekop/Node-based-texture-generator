using System.Collections;
using System.Collections.Generic;
using Node_based_texture_generator.Editor.Nodes.BlitNodes.Base;
using UnityEngine;

public class Resample : BlitWithInputPort
{
    [SerializeField, Input(connectionType = ConnectionType.Override)] private Texture uv;
    private static readonly int UVMap = Shader.PropertyToID("_UVMap");


    protected override void PrepareMaterial()
    {
        var mat = new Material(Shader.Find("Przekop/TextureGraph/Resample"));
        mat.SetTexture(UVMap, uv);
        BlitMaterial = mat;
    }

    protected override void OnInputChanged()
    {
        GetPortValue(ref uv, "uv");
        PrepareMaterial();
        base.OnInputChanged();
    }
}