using Jenga.Builder;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Jenga.Mods
{
    public abstract class Mod : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected BuildStacksManager _buildStacksManager;

        [SerializeField] private GameObject _toolTip;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ModClicked);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _toolTip.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _toolTip.SetActive(false);
        }

        public abstract void ModClicked();
    }
}