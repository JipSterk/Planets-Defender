using System;
using System.Linq;
using UnityEngine;

namespace PlanetDefenders.UI
{
    public class InterfacePanel : MonoBehaviour
    {
        public MenuState MenuState { get { return _menuState; } }
        public InterfaceMenuLoadMode InterfaceMenuLoadMode { get { return _interfaceMenuLoadMode; } }

        [SerializeField] private MenuState _menuState;
        [SerializeField] private InterfaceMenuLoadMode _interfaceMenuLoadMode;

        public void Init(UiButton[] uiButtons, Action<MenuState> callback)
        {
            foreach (var uiButton in uiButtons.Where(x => x.MenuState == _menuState))
                uiButton.Init(callback);
        }
    }
}