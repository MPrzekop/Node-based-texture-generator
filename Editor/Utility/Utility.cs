using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Node_based_texture_generator.Editor.Utility
{
    public class Utility
    {
        public static List<Type> FindAttributeUsers(Type attr)
        {
            List<Type> result = new List<Type>();
            Type[] allTypes = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in allTypes)
            {
                if (Attribute.GetCustomAttributes(type, attr).Length > 0)
                {
                    result.Add(type);
                }
            }

            return result;
        }
        public static Texture2D ToTexture2D(RenderTexture rt)
        {
            var texture = new Texture2D(rt.width, rt.height, rt.graphicsFormat, 0, TextureCreationFlags.None);
            RenderTexture.active = rt;
            texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            texture.Apply();

            RenderTexture.active = null;

            return texture;
        }
        
        public static Texture2D GetColoredTexture(Color c)
        {
            var inputTexture = new Texture2D(1, 1, DefaultFormat.HDR, TextureCreationFlags.None);
            var textureColor = c;
            inputTexture.SetPixel(0, 0, textureColor);
            inputTexture.Apply();
            return inputTexture;
        }
        
        public static RenderTexture ResizeIfDifferentResolutionTexture(RenderTexture texture, Vector2Int resolution)
        {
            if (texture == null) return null;
            if (texture.width == resolution.x && texture.height == resolution.y)
            {
                return texture;
            }

            if (texture != null)
            {
                texture.Release();
            }

            resolution.x = Mathf.Max(1, resolution.x);
            resolution.y = Mathf.Max(1, resolution.y);
            texture.width = resolution.x;
            texture.height = resolution.y;
            // if (texture == null)
            // {
            //     texture = new RenderTexture(resolution.x, resolution.y, 32, DefaultFormat.HDR);
            // }

            texture.Create();
            return texture;
        }
    }
}