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
        private char[] channels = new[] {'r', 'g', 'b', 'a'};

        protected Texture Input => input;

        protected override Texture GetPreviewTexture()
        {
            return input;
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


            _mrtRgba = new[]
            {
                new RenderTexture(input.width, input.height, 24, RenderTextureFormat.DefaultHDR),
                new RenderTexture(input.width, input.height, 24, RenderTextureFormat.DefaultHDR),
                new RenderTexture(input.width, input.height, 24, RenderTextureFormat.DefaultHDR),
                new RenderTexture(input.width, input.height, 24, RenderTextureFormat.DefaultHDR)
            };


            foreach (var texture in _mrtRgba)
            {
                texture.Create();
            }

            RenderBuffer[] _mrbRgba = new[]
            {
                _mrtRgba[0].colorBuffer, _mrtRgba[1].colorBuffer, _mrtRgba[2].colorBuffer, _mrtRgba[3].colorBuffer
            };


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
            input = GetPort("input").GetInputValue<Texture>();
            RenderChannels();
            UpdatePreviewTexture();
            foreach (var output in Outputs)
            {
                UpdateNode(output);
            }
        }

        public override object GetValue(NodePort port)
        {
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

        static Mesh s_FullscreenMesh = null;
        private RenderTexture[] _mrtRgba;
        private static readonly int Input1 = Shader.PropertyToID("_input");

        public static Mesh fullscreenMesh
        {
            get
            {
                if (s_FullscreenMesh != null)
                    return s_FullscreenMesh;

                float topV = 1.0f;
                float bottomV = 0.0f;

                s_FullscreenMesh = new Mesh {name = "Fullscreen Quad"};
                s_FullscreenMesh.SetVertices(new List<Vector3>
                {
                    new Vector3(-1.0f, -1.0f, 0.0f),
                    new Vector3(-1.0f, 1.0f, 0.0f),
                    new Vector3(1.0f, -1.0f, 0.0f),
                    new Vector3(1.0f, 1.0f, 0.0f)
                });

                s_FullscreenMesh.SetUVs(0, new List<Vector2>
                {
                    new Vector2(0.0f, bottomV),
                    new Vector2(0.0f, topV),
                    new Vector2(1.0f, bottomV),
                    new Vector2(1.0f, topV)
                });

                s_FullscreenMesh.SetIndices(new[] {0, 1, 2, 2, 1, 3}, MeshTopology.Triangles, 0, false);
                s_FullscreenMesh.UploadMeshData(true);
                return s_FullscreenMesh;
            }
        }
    }
}