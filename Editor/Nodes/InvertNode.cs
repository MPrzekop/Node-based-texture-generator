using System.Collections;
using System.Collections.Generic;
using Node_based_texture_generator.Editor.Nodes;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using XNode;

public class InvertNode : TextureGraphNode
{
    [Input(connectionType = ConnectionType.Override), SerializeField]
    private Texture textureIn;

    [SerializeField, Output(ShowBackingValue.Never)]
    private Texture textureOut;

    private RenderTexture _operatingTexture;
    private Material _invertMaterial;

    public override Texture GetTexture()
    {
        if (textureIn == null)
        {
            textureOut = null;
            return null;
        }

        if (_operatingTexture != null)
        {
            _operatingTexture.Release();
        }

        _operatingTexture = new RenderTexture(textureIn.width, textureIn.height, 32, DefaultFormat.HDR);
        _operatingTexture.Create();
        PrepareMaterial();
        Graphics.Blit(textureIn, _operatingTexture, _invertMaterial);
        textureOut = _operatingTexture;
        return textureOut;
    }

    protected override void OnInputChanged()
    {
        Texture newInput = GetPort("textureIn").GetInputValue<Texture>();
        if (newInput != textureIn)
        {
            textureIn = newInput;
        }

        UpdateTexture();
        UpdateNode(GetPort("textureOut"));
    }

    public override object GetValue(NodePort port)
    {
        // Check which output is being requested. 
        // In this node, there aren't any other outputs than "result".
        if (port.fieldName == "textureOut")
        {
            return this.textureOut;
        }
        // Hopefully this won't ever happen, but we need to return something
        // in the odd case that the port isn't "result"
        else return null;
    }

    void PrepareMaterial()
    {
        if (_invertMaterial == null)
        {
            _invertMaterial = new Material(Shader.Find("Przekop/TextureGraph/Invert"));
        }
    }
}