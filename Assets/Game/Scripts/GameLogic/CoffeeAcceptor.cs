using System;
using Game.Scripts.GameLogic.CupLogic;
using UnityEngine;

public class CoffeeAcceptor : MonoBehaviour
{
    public event Action GotCoffee;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent(out CupBuilder cupBuilder))
        {
            if (cupBuilder.IsCoffeeReady)
            {
                cupBuilder.DeactivateCoffee();
                GotCoffee?.Invoke();
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
