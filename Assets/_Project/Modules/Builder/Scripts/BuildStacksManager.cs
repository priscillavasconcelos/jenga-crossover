using Jenga.APICommunication;
using Jenga.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jenga.Builder
{
    public class BuildStacksManager : MonoBehaviour
    {
        [SerializeField] private StackController _stackController;

        [SerializeField] private FocusButtonBehavour _focusButton;

        [SerializeField] private Transform _focusButtonHolder;

        [SerializeField] private BlockDetailBehaviour _blockDetail;

        [SerializeField] private float _xToAdd;

        private Dictionary<string, List<Block>> _stacks = new Dictionary<string, List<Block>>();

        private List<StackController> _stackControllers = new List<StackController>();

        private Vector3 _initialPosition;

        private List<FocusButtonBehavour> _stacksButtons = new List<FocusButtonBehavour>();

        public Action<List<FocusButtonBehavour>> BuildingIsDone;

        private void Awake()
        {
            _initialPosition = transform.position;    

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

            ToggleGroup group = _focusButtonHolder.GetComponent<ToggleGroup>();

            foreach (var item in _stacks)
            {
                var stack = Instantiate(_stackController, _initialPosition, Quaternion.identity);
                stack.Initialize(item.Value);
                stack.ShowDetail += ShowBlockDetail;

                _stackControllers.Add(stack);

                var b = Instantiate(_focusButton, _focusButtonHolder);

                object[] objArray = { stack.gameObject, item.Value[0].grade, group };

                b.Initialize(objArray);

                _stacksButtons.Add(b);

                _initialPosition += new Vector3(_xToAdd, 0, 0);
            }

            BuildingIsDone?.Invoke(_stacksButtons);
        }

        private void ShowBlockDetail(Block obj)
        {
            _blockDetail.Initialize(obj);
        }

        public List<StackController> GetStacks()
        {
            return _stackControllers;
        }
    }
}