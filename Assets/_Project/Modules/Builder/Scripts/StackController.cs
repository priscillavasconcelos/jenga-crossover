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

        public void Initialize(List<Block> obj)
        {
            _label.text = obj[0].grade;

            foreach (Block b in obj) 
            { 
                switch (b.mastery)
                {
                    default: 
                    case 0:
                        var glass = Instantiate(_blockGlass, transform);
                        glass.Initialize(b);
                        break;
                    case 1:
                        var wood = Instantiate(_blockWood, transform);
                        wood.Initialize(b);
                        break; 
                    case 2:
                        var stone = Instantiate(_blockStone, transform);
                        stone.Initialize(b);
                        break;
                }

            }
        }
    }
}