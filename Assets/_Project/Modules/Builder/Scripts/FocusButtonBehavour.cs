using System;
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

        private Toggle _toggle;

        private GameObject _stack;

        public Action<GameObject> StackSelected;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(SelectStack);
        }

        public void Initialize(object[] obj)
        {
            _stack = obj[0] as GameObject;

            _label.text = obj[1] as string;

            _toggle.group = obj[2] as ToggleGroup;
        }

        private void SelectStack(bool isOn)
        {
            if (isOn) 
                StackSelected?.Invoke(_stack);
        }

        public GameObject GetStack()
        {
            return _stack;
        }
    }
}