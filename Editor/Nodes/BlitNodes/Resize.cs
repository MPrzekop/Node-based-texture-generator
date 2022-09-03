using Node_based_texture_generator.Editor.Nodes.BlitNodes.Base;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.BlitNodes
{
    [CreateNodeMenu("Texture Generator/Image Operations/Resize")]
    public class Resize : BlitWithInputPort
    {
        [Input(backingValue = ShowBackingValue.Unconnected, connectionType = ConnectionType.Override), SerializeField]
        private Vector2Int resolution;


        protected override void PrepareOperatingTexture()
        {
            _operatingTexture = ResizeTexture(_operatingTexture, resolution);
        }

        protected override void PrepareMaterial()
        {
            BlitMaterial = null;
        }

        protected override void OnInputChanged()
        {
            if (GetPort("resolution").IsConnected)
                resolution = GetPort("resolution").GetInputValue<Vector2Int>();
            PrepareOperatingTexture();
            UpdatePreviewTexture();
            base.OnInputChanged();
        }

        public override object GetValue(NodePort port)
        {
            return base.GetValue(port);
        }

        public static RenderTexture ResizeTexture(RenderTexture texture, Vector2Int resolution)
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