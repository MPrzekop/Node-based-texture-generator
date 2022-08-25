using System;
using Node_based_texture_generator.Editor.GraphBase;
using UnityEngine;
using XNode;

namespace Node_based_texture_generator.Editor.Nodes
{
    public abstract class TextureGraphNode : Node
    {
        private Texture _resultTexture;
        private Action _onInputUpdate;

        public Texture ResultTexture
        {
            get
            {
                if (_resultTexture == null)
                {
                    _resultTexture = GetTexture();
                }

                return _resultTexture;
            }
        }

        public Action ONInputUpdate
        {
            get => _onInputUpdate;
            set => _onInputUpdate = value;
        }


        public abstract Texture GetTexture();

        public void UpdateTexture()
        {
            _resultTexture = GetTexture();
        }


        protected virtual void OnValidate()
        {
            ONInputUpdate?.Invoke();
        }

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
            UpdateTexture();
            ONInputUpdate?.Invoke();
        }


        public override void OnRemoveConnection(NodePort port)
        {
            base.OnRemoveConnection(port);
            if (!ValidateConnection())
            {
            }

            OnInputChanged();
            UpdateTexture();
            ONInputUpdate?.Invoke();
        }

        protected abstract void OnInputChanged();

        public void UpdateNode(NodePort output)
        {
            UpdateTexture();
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
                    connectedNode.OnInputChanged();
                }
            }
        }

        bool ValidateConnection()
        {
            return ((TextureMainGraph) graph).ValidateGraph();
        }
    }
}