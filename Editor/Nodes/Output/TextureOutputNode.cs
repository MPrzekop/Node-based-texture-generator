using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes
{
    [DisallowMultipleNodesAttribute]
    public class TextureOutputNode : TextureGraphNode
    {
        [Input(connectionType = ConnectionType.Override), SerializeField]
        private Texture texture;

        [Input(), SerializeField] private TextureContainer _container;

        [Input(backingValue = ShowBackingValue.Never), SerializeField]
        private Vector2Int resolution;

        [Input(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict), SerializeField]
        private FilterMode filterMode;

        private RenderTexture result;

        protected override void OnInputChanged()
        {
            Texture newInput = GetPort("texture").GetInputValue<Texture>();

            if (GetPort("resolution").IsConnected)
            {
                var res = GetPort("resolution").GetInputValue<Vector2Int>();
                resolution = res;
            }
            else
            {
                if (newInput != null)
                {
                    resolution = new Vector2Int(newInput.width, newInput.height);
                }
                else
                {
                    resolution = Vector2Int.one;
                }
            }

            if (GetPort("filterMode").IsConnected)
            {
                var filter = GetPort("filterMode").GetInputValue<FilterMode>();
                filterMode = filter;
            }
            else
            {
                if (newInput != null)
                {
                    filterMode = newInput.filterMode;
                }
                else
                {
                    filterMode = FilterMode.Bilinear;
                }
            }


            if (result == null || result.filterMode != filterMode || result.width != resolution.x ||
                result.height != resolution.y)
            {
                if (result != null)
                {
                    result.Release();
                }

                result = new RenderTexture(resolution.x, resolution.y, 32, DefaultFormat.HDR);
                result.width = resolution.x;
                result.height = resolution.y;
                result.filterMode = filterMode;
                result.Create();
            }

            if (newInput != null)
                Graphics.Blit(newInput, result);

            else
            {
                result.Release();
                result = null;
            }

            texture = result;


            UpdateTexture();
        }

        public override Texture GetTexture()
        {
            return texture;
        }
    }
}