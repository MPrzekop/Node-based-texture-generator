using Node_based_texture_generator.Editor.Nodes.BlitNodes.Base;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Node_based_texture_generator.Editor.Nodes.BlitNodes
{
    [CreateNodeMenu("Texture Generator/Image Operations/Resize")]

    public class Resize : BlitWithInputPort
    {
        [Input(backingValue = ShowBackingValue.Unconnected, connectionType = ConnectionType.Override), SerializeField]
        private Vector2Int resolution;


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

        protected override void OnValidate()
        {
            OnInputChanged();
            base.OnValidate();
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
}