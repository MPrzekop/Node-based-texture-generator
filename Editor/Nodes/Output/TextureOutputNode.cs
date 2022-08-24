using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes
{
    enum FileFormat
    {
        PNG,
        JPG,
        EXR,
        TGA
    }

    [DisallowMultipleNodesAttribute]
    public class TextureOutputNode : TextureGraphNode
    {
        [Input(connectionType = ConnectionType.Override), SerializeField]
        private Texture texture;

        [SerializeField] private FileFormat saveFormat;
        [SerializeField] private string filePath;


        private RenderTexture result;

        protected override void OnInputChanged()
        {
            Texture newInput = GetPort("texture").GetInputValue<Texture>();
            if (newInput != texture)
            {
                texture = newInput;
            }

            if (texture == null)
            {
                result?.Release();
                result = null;
                UpdateTexture();
                return;
            }

            if (result == null || result.filterMode != texture.filterMode || result.width != texture.width ||
                result.height != texture.height)
            {
                if (result != null)
                {
                    result.Release();
                }

                result = new RenderTexture(texture.width, texture.height, 32, DefaultFormat.HDR);
                result.width = texture.width;
                result.height = texture.height;
                result.filterMode = texture.filterMode;
                result.Create();
            }

            if (texture != null)
                Graphics.Blit(texture, result);


            texture = result;


            UpdateTexture();
        }

        public Texture2D toTexture2D(RenderTexture rt)
        {
            var texture = new Texture2D(rt.width, rt.height, rt.graphicsFormat, 0, TextureCreationFlags.None);
            RenderTexture.active = rt;
            texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            texture.Apply();

            RenderTexture.active = null;

            return texture;
        }

        public void SaveTexture(string path = null)
        {
            if (result == null)
            {
                OnInputChanged();
                if (result == null)
                    return;
            }

            if (path == null)
            {
#if UNITY_EDITOR
                string extension;
                switch (saveFormat)
                {
                    case FileFormat.PNG:
                        extension = "png";
                        break;


                    case FileFormat.JPG:
                        extension = "jpg";
                        break;

                    case FileFormat.EXR:
                        extension = "exr";
                        break;
                    case FileFormat.TGA:
                        extension = "tga";
                        break;
                    default:
                        extension = "png";
                        break;
                }

                path = UnityEditor.EditorUtility.SaveFilePanel("Save image", filePath ??= "", "texture", extension);
                if (path.Length != 0)
                {
                    var textureToSave = toTexture2D(result);
                    byte[] file = null;
                    switch (saveFormat)
                    {
                        case FileFormat.PNG:
                            file = textureToSave.EncodeToPNG();
                            break;


                        case FileFormat.JPG:
                            file = textureToSave.EncodeToJPG();

                            break;

                        case FileFormat.EXR:
                            file = textureToSave.EncodeToEXR();

                            break;
                        case FileFormat.TGA:
                            file = textureToSave.EncodeToTGA();

                            break;
                        default:
                            file = textureToSave.EncodeToPNG();
                            break;
                    }

                    File.WriteAllBytes(path, file);
                    filePath = path;
                }

#endif
            }
        }

        public override Texture GetTexture()
        {
            return texture;
        }
    }
}