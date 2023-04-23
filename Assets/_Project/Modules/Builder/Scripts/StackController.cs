using Jenga.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Jenga.Builder
{
    public class StackController : MonoBehaviour, IInitializable<List<Block>>
    {
        [SerializeField] private TextMeshPro _label;

        [SerializeField] private BlockBehaviour _blockStone;
        [SerializeField] private BlockBehaviour _blockGlass;
        [SerializeField] private BlockBehaviour _blockWood;

        [SerializeField] private Vector3[] _positions;
        private bool _flip = false;
        private int _currentPosition = 0;
        private float _initialHeight = 0.5f;
        private float _height;

        private bool _previousFlip = false;

        public void Initialize(List<Block> obj)
        {
            _label.text = obj[0].grade;

            _height = _initialHeight;

            StartCoroutine(SlowSpawn(obj));
        }

        private IEnumerator SlowSpawn(List<Block> obj)
        {
            foreach (Block b in obj)
            {
                switch (b.mastery)
                {
                    default:
                    case 0:
                        SpawnBlock(_blockGlass, b);
                        break;
                    case 1:
                        SpawnBlock(_blockWood, b);
                        break;
                    case 2:
                        SpawnBlock(_blockStone, b);
                        break;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void SpawnBlock(BlockBehaviour prefab, Block b)
        {
            Vector3 pos = _positions[_currentPosition] + new Vector3(0, _height, 0);
            Vector3 rot = _flip ? new Vector3(0, 90, 0) : Vector3.zero;

            var block = Instantiate(prefab, transform);
            block.transform.localPosition = pos;
            block.transform.localEulerAngles = rot;

            block.Initialize(b);

            _currentPosition = _currentPosition < _positions.Length-1 ? _currentPosition + 1 : 0;

            _flip = _currentPosition > 2;

            if (_previousFlip != _flip)
            {
                _height += _initialHeight;
                _previousFlip = _flip;
            }

        }
    }
}