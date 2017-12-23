using System.Linq;
using UnityEngine;

namespace PlanetDefenders
{
    [RequireComponent(typeof(SphereCollider))]
    public class GodHand : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        
        private SteamVR_TrackedObject _steamVrTrackedObject;
        private FixedJoint _fixedJoint;
        private Component _pickup;
        private float _radius;

        private void Start()
        {
            _steamVrTrackedObject = GetComponentInParent<SteamVR_TrackedObject>();
            _radius = GetComponent<SphereCollider>().radius;
        }
        
        private void FixedUpdate()
        {
            var device = SteamVR_Controller.Input((int) _steamVrTrackedObject.index);
            if (_fixedJoint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                var pickups =
                    Physics.OverlapSphere(transform.position, _radius)
                        .Select(x => x.GetComponent(typeof(IPickupAble)))
                        .Where(x => x)
                        .ToArray();

                if(pickups.Length <= 0)
                    return;

                _pickup = pickups.First();
                _pickup.transform.position = _rigidbody.transform.position;

                _fixedJoint = _pickup.gameObject.AddComponent<FixedJoint>();
                _fixedJoint.connectedBody = _rigidbody;
            }
            else if (_fixedJoint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                var rigidbody = _pickup.GetComponent<Rigidbody>();
                DestroyImmediate(_fixedJoint);
                _fixedJoint = null;
                
                var origin = _steamVrTrackedObject.origin ? _steamVrTrackedObject.origin : _steamVrTrackedObject.transform.parent;
                if (origin != null)
                {
                    rigidbody.velocity = origin.TransformVector(device.velocity);
                    rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
                }
                else
                {
                    rigidbody.velocity = device.velocity;
                    rigidbody.angularVelocity = device.angularVelocity;
                }

                rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
            }
        }
    }
}