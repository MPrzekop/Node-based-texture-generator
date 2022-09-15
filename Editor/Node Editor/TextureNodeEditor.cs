using System;
using System.Runtime.Serialization;
using Node_based_texture_generator.Runtime.Nodes.Base;
using Node_based_texture_generator.Runtime.Nodes.BlitNodes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using XNodeEditor;

namespace Node_based_texture_generator.Editor.Node_Editor
{
    [CustomNodeEditor(typeof(TextureGraphNode))]
    public class TextureNodeEditor : NodeEditor
    {
        public enum ChannelMode
        {
            [EnumMember(Value = "_KEYWORD0_ALL")] ALL = 0,

            [EnumMember(Value = "_KEYWORD0_RED")] RED = 1,

            [EnumMember(Value = "_KEYWORD0_GREEN")]
            GREEN = 2,

            [EnumMember(Value = "_KEYWORD0_BLUE")] BLUE = 3,

            [EnumMember(Value = "_KEYWORD0_ALPHA")]
            Alpha = 4
        }


        private RenderTexture _currentPreview;
        private Texture _cache;
        private int _selectedChannel = 0;
        private readonly string[] headers = new[] {"All", "R", "G", "B", "A"};
        private bool foldedOut = false;
        private CommandBuffer _commandBuffer;

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

        private Material channelSelectMaterial;

        public Material ChannelSelectMaterial
        {
            get
            {
                if (channelSelectMaterial == null)
                {
                    channelSelectMaterial = new Material(Shader.Find("Przekop/TextureGraph/ChannelSelect"));
                }

                return channelSelectMaterial;
            }
            set => channelSelectMaterial = value;
        }

        public override void OnBodyGUI()
        {
            try
            {
                ((TextureGraphNode) target).OnInputUpdate -= SelectChannel;
            }
            catch
            {
                //ignore
            }

            ((TextureGraphNode) target).OnInputUpdate += SelectChannel;
            base.OnBodyGUI();


            EditorGUILayout.BeginVertical();
            var t = ((TextureGraphNode) target).PreviewTexture;

            if (t != null)
            {
                foldedOut = EditorGUILayout.Foldout(foldedOut, "Preview");
                if (foldedOut)
                {
                    DrawPreview(t);
                }
            }

            EditorGUILayout.EndVertical();
        }

        void DrawPreview(Texture t)
        {
            var newTabValue = GUILayout.Toolbar(_selectedChannel, headers);

            SelectChannel(newTabValue);
            _cache = t;
            _selectedChannel = newTabValue;
            var r = EditorGUILayout.GetControlRect(GUILayout.Height(GetWidth() - 20));

            EditorGUI.DrawPreviewTexture(r, _currentPreview);
        }


        private void SelectChannel()
        {
            SelectChannel(_selectedChannel);
        }

        private void SelectChannel(int channel)
        {
            var t = ((TextureGraphNode) target).PreviewTexture;
            if (t == null) return;
            if (_currentPreview == null)
            {
                _currentPreview = new RenderTexture(t.width, t.height, 24, DefaultFormat.HDR);
            }

            _currentPreview =
                Utility.Utility.ResizeIfDifferentResolutionTexture(_currentPreview, new Vector2Int(t.width, t.height));
            SetMode(channel);
            Buffer.Blit(t, _currentPreview, ChannelSelectMaterial);
            Graphics.ExecuteCommandBuffer(Buffer);
            Buffer.Clear();
        }

        void SetMode(int mode)
        {
            var keywords = ChannelSelectMaterial.shader.keywordSpace;
            foreach (ChannelMode m in Enum.GetValues(typeof(ChannelMode)))
            {
                var keyword = keywords.FindKeyword(m.ToEnumMemberAttrValue());
                ChannelSelectMaterial.SetKeyword(keyword, (int) m == mode);
            }
        }

        private Texture2D TextureField(string name, Texture2D texture, int width = 70)
        {
            GUILayout.BeginVertical();

            var result = (Texture2D) EditorGUILayout.ObjectField(texture, typeof(Texture2D), false,
                GUILayout.Width(width), GUILayout.Height(width));
            GUILayout.EndVertical();
            return result;
        }
    }
}