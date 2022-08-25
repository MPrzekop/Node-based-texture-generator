using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Node_based_texture_generator.Editor.Nodes.BlitNodes.Base;
using Node_based_texture_generator.Editor.Nodes.MaterialNodes;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class GrayscaleNode : BlitWithInputPort
{
    
   
    public enum GrayscaleMode
    {
        [EnumMember(Value = "_PRZEKOPTEXTUREGRAPHGRAYSCALEMODE_PRZEKOPTEXTUREGRAPHGRAYSCALELUMINANCE")]
        Luminance = 0,

        [EnumMember(Value = "_PRZEKOPTEXTUREGRAPHGRAYSCALEMODE_PRZEKOPTEXTUREGRAPHCLASSIC")]
        Classic = 1,

        [EnumMember(Value = "_PRZEKOPTEXTUREGRAPHGRAYSCALEMODE_PRZEKOPTEXTUREGRAPHOLDSCHOOL")]
        OldSchool = 2
    }

    [SerializeField] private GrayscaleMode mode;

    private void OnValidate()
    {
        PrepareMaterial();
        SetMode();
        OnInputChanged();
    }

    void SetMode()
    {
        var keywords = BlitMaterial.shader.keywordSpace;
        foreach (GrayscaleMode m in Enum.GetValues(typeof(GrayscaleMode)))
        {
            var keyword = keywords.FindKeyword(m.ToEnumMemberAttrValue());
            BlitMaterial.SetKeyword(keyword, m == mode);
        }
    }

    protected override Texture GetInputTexture()
    {
        return Input;
    }

    protected override void OnInputChanged()
    {
       
        base.OnInputChanged();
    }

    protected override void PrepareOperatingTexture()
    {
        if (_operatingTexture != null)
        {
            _operatingTexture.Release();
        }

        _operatingTexture = new RenderTexture(Input.width, Input.height, 32, DefaultFormat.HDR);
        _operatingTexture.Create();
    }

    protected override void PrepareMaterial()
    {
        if (BlitMaterial == null)
        {
            BlitMaterial = new Material(Shader.Find("Przekop/TextureGraph/Grayscale"));
        }
    }
}

public static class EnumExtensions
{
    public static string ToEnumMemberAttrValue(this Enum @enum)
    {
        var attr =
            @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?.GetCustomAttributes(false)
                .OfType<EnumMemberAttribute>().FirstOrDefault();
        if (attr == null)
            return @enum.ToString();
        return attr.Value;
    }
}