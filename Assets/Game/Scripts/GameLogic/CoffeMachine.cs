using Game.Scripts.GameLogic.PlayerLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic
{
    public class CoffeMachine : MonoBehaviour, IUsable
    {
        public bool CanBeUse { get; }
        
        public void TryUse()
        {
            Debug.Log("Используется кофе машина");
        }

        public void ActivateFocuse()
        {
            Debug.Log("Focuse кофе машина");
        }

        public void DeactivateFocuse()
        {
            Debug.Log("UnFocuse кофе машина");
        }

        public void AcceptVisitor(IFocusableVisitor focusableVisitor)
        {
            focusableVisitor.Visit(this);
        }
    }
}