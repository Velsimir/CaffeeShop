using UnityEngine;

namespace Game.Scripts.GameLogic.PlayerLogic
{
    public class ObjectUser : IUser
    {
        public void Use(IUsable usableObject)
        {
            Debug.Log("Пробуем использовать");
        }
    }
}