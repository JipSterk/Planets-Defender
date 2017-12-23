using UnityEngine;

namespace PlanetDefenders.UI
{
    public class InterfaceManager : MonoBehaviour
    {
        public static InterfaceManager Instance { get { return _instance ?? new GameObject("Interface Manager").AddComponent<InterfaceManager>(); } }

        private static InterfaceManager _instance;


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
    }
}