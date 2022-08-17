using System;
using System.Collections;
using System.Collections.Generic;
using Node_based_texture_generator.Editor.Nodes.MaterialNodes;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class ResizeNode : BlitNodeBase
{
    [Input(backingValue = ShowBackingValue.Unconnected), SerializeField]
    private Vector2Int resolution;

    protected override void PrepareOperatingTexture()
    {
        if (_operatingTexture != null)
        {
            _operatingTexture.Release();
        }

        _operatingTexture = new RenderTexture(resolution.x, resolution.y, 32, DefaultFormat.HDR);
        _operatingTexture.Create();
    }

    protected override void PrepareMaterial()
    {
        BlitMaterial = null;
    }

    private void OnValidate()
    {
        OnInputChanged();
    }

    protected override void OnInputChanged()
    {
        if (GetPort("resolution").IsConnected)
            resolution = GetPort("resolution").GetInputValue<Vector2Int>();
        PrepareOperatingTexture();
        UpdateTexture();
        base.OnInputChanged();
    }
}