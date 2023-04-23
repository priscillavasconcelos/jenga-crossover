using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jenga.Builder
{
    public class FocusButtonBehavour : MonoBehaviour, IInitializable<object[]>
    {
        [SerializeField] private TextMeshProUGUI _label;

        private Button _button;

        private GameObject _stack;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void Initialize(object[] obj)
        {
            _stack = obj[0] as GameObject;

            _label.text = obj[1] as string;

            
        }
    }
}