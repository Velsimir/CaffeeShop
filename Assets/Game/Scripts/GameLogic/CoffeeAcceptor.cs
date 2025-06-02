using System;
using Game.Scripts.GameLogic.CupLogic;
using UnityEngine;

public class CoffeeAcceptor : MonoBehaviour
{
    public event Action GotCoffee;
    public static event Action<Vector3> CoffeeSold;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent(out CupBuilder cupBuilder))
        {
            if (cupBuilder.IsCoffeeReady)
            {
                cupBuilder.DeactivateCoffee();
                GotCoffee?.Invoke();
                CoffeeSold?.Invoke(other.contacts[0].point);
            }
            else
            {
                Debug.Log("Coffee not ready");
            }
        }
        else
        {
            Debug.Log("Not a coffee");
        }
    }
}
