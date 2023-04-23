using Jenga.APICommunication;
using Jenga.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Jenga.Builder
{
    public class BuildStacksManager : MonoBehaviour
    {
        [SerializeField] private StackController _stackController;

        private Dictionary<string, List<Block>> _stacks = new Dictionary<string, List<Block>>();

        private void Awake()
        {
            APIComManager com = new APIComManager();

            com.GetAPIInfo<Block>(BuildScene);
        }

        private void BuildScene(List<Block> blocks)
        {
            foreach (var block in blocks) 
            {
                if (!_stacks.ContainsKey(block.grade))
                    _stacks.Add(block.grade, new List<Block>());

                _stacks[block.grade].Add(block);
            }

            foreach (var item in _stacks)
            {
                var stack = Instantiate(_stackController);
                stack.Initialize(item.Value);
            }
        }
    }
}