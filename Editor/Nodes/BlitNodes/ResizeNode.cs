using System;
using System.Collections;
using System.Collections.Generic;
using Node_based_texture_generator.Editor.Nodes.MaterialNodes;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class ResizeNode : BlitNodeBase
{
    [Input(backingValue = ShowBackingValue.Unconnected, connectionType = ConnectionType.Override), SerializeField]
    private Vector2Int resolution;

    [Output(), SerializeField] private RenderTexture test;
    protected override void PrepareOperatingTexture()
    {
        if (_operatingTexture != null)
        {
            _operatingTexture.Release();
        }

        resolution.x = Mathf.Max(1, resolution.x);
        resolution.y = Mathf.Max(1, resolution.y);
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