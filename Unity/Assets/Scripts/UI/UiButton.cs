using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetDefenders.UI
{
    public class UiButton : MonoBehaviour
    {
        public MenuState MenuState { get { return _menuState; } }

        [SerializeField] private MenuState _menuState;

        private Button _button;

        public void Init(Action<MenuState> callback)
        {
            _button = GetComponentInChildren<Button>();
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => callback(_menuState));
        }
    }
}