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
            _operatingTexture = Utility.Utility.ResizeTexture(_operatingTexture, resolution);
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

      
    }
}