using UnityEngine;

namespace PlanetDefenders
{
    [RequireComponent(typeof(SphereCollider))]
    public class Skybox : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            var destroyAble = (IDestroyAble)other.GetComponent(typeof(IDestroyAble));
            if (destroyAble != null)
                destroyAble.SafeDestroy(0.1f);
        }
    }
}