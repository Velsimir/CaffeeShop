using Game.Scripts.GameLogic.CupLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.CustomerLogic
{
    [RequireComponent(typeof(Collider))]
    public class Customer : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent(out CupBuilder capBuilder))
            {
                if (capBuilder.IsCoffeeReady)
                {
                    Debug.Log("Coffee ready thanx!");
                }
                else
                {
                    Debug.Log("Coffee not ready make new coffe!");
                }
            }
        }
    }
}