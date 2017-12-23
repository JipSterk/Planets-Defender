using System;
using PlanetDefenders.Planets;
using UnityEngine;

namespace PlanetDefenders.Waves
{
    [RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
    public class Comet : MonoBehaviour, IPickupAble, IDestroyAble
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private ParticleSystem _impact;

        private event Action<Comet> Callback;
        private Rigidbody _rigidbody;
        
        public void Init(Planet planet, Action<Comet> callback)
        {
            Callback = callback;

            transform.LookAt(planet.transform);
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.AddForce(transform.forward * _speed);
        }

        private void OnTriggerEnter(Collider other)
        {
            var planet = other.GetComponent<Planet>();
            
            if (!planet)
                return;
            
            planet.TakeDamage(_damage);
            Impact(planet.transform.position);
        }

        private void Impact(Vector3 position)
        {
            var impact = Instantiate(_impact, position, Quaternion.identity);
            Destroy(impact.gameObject, impact.main.duration);

            if (Callback != null)
                Callback(this);
        }
        
        public void SafeDestroy(float time)
        {
            Impact(transform.position);
            Destroy(gameObject, time);
        }
    }
}