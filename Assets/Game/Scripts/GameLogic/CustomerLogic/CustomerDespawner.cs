using UnityEngine;

namespace Game.Scripts.GameLogic.CustomerLogic
{
    [RequireComponent(typeof(BoxCollider))]
    
    public class CustomerDespawner: MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out ICustomer customer))
            {
                if (customer.IsServed)
                {
                    customer.Disappear();
                }
            }
        }
    }
}