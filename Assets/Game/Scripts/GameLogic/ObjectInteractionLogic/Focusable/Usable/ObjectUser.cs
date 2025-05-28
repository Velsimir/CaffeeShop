namespace Game.Scripts.GameLogic.PlayerLogic
{
    public class ObjectUser : IUser
    {
        public void Use(IUsable usableObject)
        {
            usableObject.TryUse();
        }
    }
}