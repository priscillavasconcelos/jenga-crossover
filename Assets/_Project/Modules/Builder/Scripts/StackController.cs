using Jenga.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using static UnityEditor.PlayerSettings;

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

        private List<GameObject> _stoneBlocks = new List<GameObject>();
        private List<GameObject> _glassBlocks = new List<GameObject>();
        private List<GameObject> _woodBlocks = new List<GameObject>();

        private List<BlockTransform> _allBlocks = new();

        public Action<StackController> FinishedStack;
        public Action<Block> ShowDetail;

        public List<GameObject> GetStoneBlocks() { return _stoneBlocks; }
        public List<GameObject> GetGlassBlocks() { return _glassBlocks; }
        public List<GameObject> GetWoodBlocks() { return _woodBlocks; }

        public void ResetStack()
        {
            foreach (var item in _allBlocks)
            {
                item.BlockObj.SetActive(false);
                item.BlockObj.transform.localPosition = item.Position;
                item.BlockObj.transform.localEulerAngles = item.Rotation;
                item.BlockObj.SetActive(true);
            }
        }

        public void Initialize(List<Block> obj)
        {
            _label.text = obj[0].grade;

            _height = _initialHeight;

            StartCoroutine(SlowSpawn(OrderList(obj)));
        }

        private List<Block> OrderList(List<Block> obj)
        {
            return obj.OrderBy(x => x.domain).ThenBy(y => y.cluster).ThenBy(z => z.standardid).ToList();
        }

        private IEnumerator SlowSpawn(List<Block> obj)
        {
            foreach (Block b in obj)
            {
                switch (b.mastery)
                {
                    default:
                    case 0:
                        _glassBlocks.Add(SpawnBlock(_blockGlass, b));
                        break;
                    case 1:
                        _woodBlocks.Add(SpawnBlock(_blockWood, b));
                        break;
                    case 2:
                        _stoneBlocks.Add(SpawnBlock(_blockStone, b));
                        break;
                }
                yield return new WaitForSeconds(0.1f);
            }

            FinishedStack?.Invoke(this);
        }

        private GameObject SpawnBlock(BlockBehaviour prefab, Block b)
        {
            Vector3 pos = _positions[_currentPosition] + new Vector3(0, _height, 0);
            Vector3 rot = _flip ? new Vector3(0, 90, 0) : Vector3.zero;

            var block = Instantiate(prefab, transform);
            block.transform.localPosition = pos;
            block.transform.localEulerAngles = rot;

            var blockTransform = new BlockTransform(pos, rot, block.gameObject);
            _allBlocks.Add(blockTransform);

            block.Initialize(b);
            block.ShowDetail += BlockShowDetail;

            _currentPosition = _currentPosition < _positions.Length-1 ? _currentPosition + 1 : 0;

            _flip = _currentPosition > 2;

            if (_previousFlip != _flip)
            {
                _height += _initialHeight;
                _previousFlip = _flip;
            }

            return block.gameObject;
        }

        private void BlockShowDetail(Block obj)
        {
            ShowDetail?.Invoke(obj);
        }
    }
}