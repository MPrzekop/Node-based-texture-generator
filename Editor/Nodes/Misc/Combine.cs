using UnityEngine;
using UnityEngine.Rendering;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.Misc
{
    public class Combine : TextureGraphNode
    {
        [Output(connectionType = ConnectionType.Override), SerializeField]
        private Texture result;

        [Input(connectionType = ConnectionType.Override), SerializeField]
        private Texture _r, _g, _b, _a;

        [SerializeField, HideInInspector] private RenderTexture _operatingTexture;

        private Vector2Int _resolution;
        private CommandBuffer _commandBuffer;
        private static readonly int A = Shader.PropertyToID("_a");
        private static readonly int B = Shader.PropertyToID("_b");
        private static readonly int G = Shader.PropertyToID("_g");
        private static readonly int R = Shader.PropertyToID("_r");

        public CommandBuffer Buffer
        {
            get
            {
                if (_commandBuffer == null)
                {
                    _commandBuffer = new CommandBuffer();
                }

                return _commandBuffer;
            }
            set => _commandBuffer = value;
        }


        protected override Texture GetPreviewTexture()
        {
            return result;
        }

        

        void CollectInputs()
        {
            GetPortValue(ref _r, "_r");
            GetPortValue(ref _g, "_g");
            GetPortValue(ref _b, "_b");
            GetPortValue(ref _a, "_a");

            var tempArray = new[] {_r, _g, _b, _a};
            _resolution = new Vector2Int(-1, -1);
            foreach (var element in tempArray)
            {
                if (element == null)
                {
                    continue;
                }

                if (_resolution.x < element.width)
                {
                    _resolution.x = element.width;
                }

                if (_resolution.y < element.height)
                {
                    _resolution.y = element.height;
                }
            }
        }

        void PrepareResult()
        {
            if (_resolution.x < 0)
            {
                if (_operatingTexture != null)
                {
                    _operatingTexture.Release();
                }

                _operatingTexture = null;
                result = _operatingTexture;
                return;
            }

            if (_operatingTexture == null)
            {
                _operatingTexture = new RenderTexture(_resolution.x, _resolution.y, 0, RenderTextureFormat.DefaultHDR);
                _operatingTexture.Create();
            }

            if (_operatingTexture.height != _resolution.x || _operatingTexture.width != _resolution.y)
            {
                _operatingTexture = Utility.Utility.ResizeIfDifferentResolutionTexture(_operatingTexture, _resolution);
            }

            result = _operatingTexture;
        }


        void RenderResult()
        {
            if (_operatingTexture == null) return;
            var mat = new Material(Shader.Find("Przekop/TextureGraph/Combine"));
            mat.SetTexture(R, _r);
            mat.SetTexture(G, _g);

            mat.SetTexture(B, _b);

            mat.SetTexture(A, _a);
            Buffer.Blit(null, _operatingTexture, mat);

            Graphics.ExecuteCommandBuffer(Buffer);
            Buffer.Clear();
        }

        protected override void OnInputChanged()
        {
            CollectInputs();
            PrepareResult();
            RenderResult();
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "result")
            {
                return result;
            }

            return base.GetValue(port);
        }
    }
}