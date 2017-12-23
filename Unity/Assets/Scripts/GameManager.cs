using System;
using PlanetDefenders.UI;
using UnityEngine;

namespace PlanetDefenders
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get { return _instance ?? new GameObject("Game Manager").AddComponent<GameManager>(); } }

        public event Action<MenuState> OnMenuStateUpdate;
        public event Action OnGameFinish;

        private static GameManager _instance;
        private MenuState _menuState;

        private void Awake()
        {
            if (_instance)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void UpdateMenuState(MenuState menuState)
        {
            _menuState = menuState;

            if (OnMenuStateUpdate != null)
                OnMenuStateUpdate(_menuState);
        }
        
        public void EndGame()
        {
            if (OnGameFinish != null)
                OnGameFinish();

            Time.timeScale = 0f;
            Debug.Log("Game has been brought to an end");
        }
    }
}