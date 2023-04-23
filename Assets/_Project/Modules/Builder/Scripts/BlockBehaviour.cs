using Jenga.Data;
using System;
using TMPro;
using UnityEngine;

namespace Jenga.Builder
{
    public class BlockBehaviour : MonoBehaviour, IInitializable<Block>
    {
        private Block _block;

        public Action<Block> ShowDetail;

        public void Initialize(Block obj)
        {
            _block = obj;
        }

        private void OnMouseUpAsButton()
        {
            ShowDetail?.Invoke(_block);
        }
    }
}