using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes.Misc
{
    [CreateNodeMenu("Texture Generator/Image Operations/Split Colors")]
    public class Split : TextureGraphNode
    {
        [Input(connectionType = ConnectionType.Override), SerializeField]
        private Texture input;

        [Output(), SerializeField] private Texture _r, _g, _b, _a;

        private RenderTexture[] _mrtRgba;
        private static readonly int Input1 = Shader.PropertyToID("_input");
        private RenderBuffer[] _mrbRgba;
        protected Texture Input => input;

        protected override Texture GetPreviewTexture()
        {
            return input;
        }


        private void ReconstructBuffers()
        {
            if (input == null) return;
            if (_mrtRgba == null|| _mrtRgba.Length!=4)
            {
                _mrtRgba = new[]
                {
                    new RenderTexture(input.width, input.height, 24, RenderTextureFormat.DefaultHDR),
                    new RenderTexture(input.width, input.height, 24, RenderTextureFormat.DefaultHDR),
                    new RenderTexture(input.width, input.height, 24, RenderTextureFormat.DefaultHDR),
                    new RenderTexture(input.width, input.height, 24, RenderTextureFormat.DefaultHDR)
                };
            }

            if (_mrbRgba == null)
            {
                _mrbRgba = new[]
                {
                    _mrtRgba[0].colorBuffer, _mrtRgba[1].colorBuffer, _mrtRgba[2].colorBuffer, _mrtRgba[3].colorBuffer
                };
            }

            if (_mrtRgba[0].width != input.width || _mrtRgba[0].height != input.height)
            {
                foreach (var tex in _mrtRgba)
                {
                    Utility.Utility.ResizeTexture(tex, new Vector2Int(input.width, input.height));
                }
            }
        }

        private void RenderChannels()
        {
            if (input == null)
            {
                _a = null;
                _r = null;
                _g = null;
                _b = null;
                return;
            }


            foreach (var texture in _mrtRgba)
            {
                texture.Create();
            }


            var mat = new Material(Shader.Find("Przekop/TextureGraph/Split"));
            mat.SetTexture(Input1, input);
            Graphics.SetRenderTarget(_mrbRgba, _mrtRgba[0].depthBuffer);
            Graphics.Blit(null, mat);
            _r = _mrtRgba[0];
            _g = _mrtRgba[1];
            _b = _mrtRgba[2];
            _a = _mrtRgba[3];
        }

        protected override void OnInputChanged()
        {
            GetPortValue(ref input,"input");
           // input = GetPort("input").GetInputValue<Texture>();
            ReconstructBuffers();
            RenderChannels();
            UpdatePreviewTexture();
            foreach (var output in Outputs)
            {
                UpdateNode(output);
            }
        }

        public override object GetValue(NodePort port)
        {
            if (_mrtRgba == null|| _mrtRgba.Length<4) return null;
            _r = _mrtRgba[0];
            _g = _mrtRgba[1];
            _b = _mrtRgba[2];
            _a = _mrtRgba[3];
            Texture[] t = new[] {_r, _g, _b, _a};
            if (port.fieldName == "_r")
            {
                return _r;
            }

            if (port.fieldName == "_g")
            {
                return _g;
            }

            if (port.fieldName == "_b")
            {
                return _b;
            }

            if (port.fieldName == "_a")
            {
                return _a;
            }


            return null;
        }
    }
}