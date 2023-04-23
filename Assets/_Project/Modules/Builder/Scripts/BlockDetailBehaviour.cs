using Jenga.Data;
using TMPro;
using UnityEngine;

namespace Jenga.Builder
{
    public class BlockDetailBehaviour : MonoBehaviour, IInitializable<Block>
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TextMeshProUGUI _gradeDomain;
        [SerializeField] private TextMeshProUGUI _cluster;
        [SerializeField] private TextMeshProUGUI _standard;
        public void Initialize(Block obj)
        {
            _gradeDomain.text = obj.grade + " : " + obj.domain;
            _cluster.text = obj.cluster;
            _standard.text = obj.standardid + " : " + obj.standarddescription;

            _panel.SetActive(true);
        }
    }
}