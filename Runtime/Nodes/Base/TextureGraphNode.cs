using System;
using Node_based_texture_generator.Runtime.GraphBase;
using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Runtime.Nodes.Base
{
    /// <summary>
    /// Base texture generator node that provides preview functionality and recurrent updates upon input change.
    /// </summary>
    public abstract class TextureGraphNode : Node
    {
        private Texture _previewTexture;
        private Action _onInputUpdate;

        public Texture PreviewTexture
        {
            get
            {
                if (_previewTexture == null)
                {
                    _previewTexture = GetPreviewTexture();
                }

                return _previewTexture;
            }
        }

        /// <summary>
        /// internal action called when input changed or OnValidate method was invoked
        /// </summary>
        public Action OnInputUpdate
        {
            get => _onInputUpdate;
            set => _onInputUpdate = value;
        }

        /// <summary>
        /// provide node preview texture, if null then no preview will be drawn
        /// </summary>
        /// <returns></returns>
        protected abstract Texture GetPreviewTexture();

        /// <summary>
        /// Update preview texture
        /// </summary>
        protected void UpdatePreviewTexture()
        {
            _previewTexture = GetPreviewTexture();
        }


        protected virtual void OnValidate()
        {
            OnInputChanged();
            OnInputUpdate?.Invoke();
        }

        /// <summary>
        /// Called when new connection is created.
        /// Updates other connected nodes. Regenerates preview texture.
        /// </summary>
        /// <param name="from">source port</param>
        /// <param name="to">target port</param>
        public override void OnCreateConnection(NodePort @from, NodePort to)
        {
            base.OnCreateConnection(@from, to);
            if (!ValidateConnection())
            {
                from.Disconnect(to);
                return;
            }

            ValidateConnection();
            OnInputChanged();
            UpdatePreviewTexture();
            OnInputUpdate?.Invoke();
        }

        /// <summary>
        /// Called when connection was removed.
        /// Updates other connected nodes. Regenerates preview texture.
        /// </summary>
        /// <param name="port">port that lost a connection</param>
        public override void OnRemoveConnection(NodePort port)
        {
            base.OnRemoveConnection(port);
            if (!ValidateConnection())
            {
            }

            OnInputChanged();
            UpdatePreviewTexture();
            OnInputUpdate?.Invoke();
        }

        /// <summary>
        /// Called when connection is created or removed, after node validation.
        /// </summary>
        protected abstract void OnInputChanged();

        /// <summary>
        /// Update nodes connected to an output
        /// </summary>
        /// <param name="output">port from which the update will be performed</param>
        public void UpdateNode(NodePort output)
        {
            UpdatePreviewTexture();
            int connectionCount = output.ConnectionCount;
            for (int i = 0; i < connectionCount; i++)
            {
                NodePort connectedPort = output.GetConnection(i);

                if (connectedPort == null) continue;
                // Get connected ports logic node
                TextureGraphNode connectedNode = connectedPort.node as TextureGraphNode;

                // Trigger it
                if (connectedNode != null)
                {
                    connectedNode?.OnInputChanged();
                }
            }
        }

        /// <summary>
        /// Check if node connections are valid
        /// </summary>
        /// <returns>true if node connections are valid</returns>
        bool ValidateConnection()
        {
            return ((TextureMainGraph) graph).ValidateGraph();
        }


        /// <summary>
        /// Get value if port is connected
        /// </summary>
        /// <param name="t">where to put the value</param>
        /// <param name="name">name of the port</param>
        protected void GetPortValue<T>(ref T t, string name)
        {
            if (GetInputPort(name).IsConnected)
            {
                t = GetInputPort(name).GetInputValue<T>();
            }
        }
    }
}