using Jenga.Data;
using System;
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

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonUp(1))
            {
                ShowDetail?.Invoke(_block);
            }
        }

    }
}