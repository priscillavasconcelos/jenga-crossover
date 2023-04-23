using Jenga.Data;
using TMPro;
using UnityEngine;

namespace Jenga.Builder
{
    public class BlockBehaviour : MonoBehaviour, IInitializable<Block>
    {
        private Block _block;

        public void Initialize(Block obj)
        {
            _block = obj;
        }
    }
}