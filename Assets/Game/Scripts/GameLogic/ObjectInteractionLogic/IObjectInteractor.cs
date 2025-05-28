using Game.Scripts.GameLogic.CupLogic;

namespace Game.Scripts.GameLogic.PlayerLogic
{
    public interface IObjectInteractor
    {
        public IFocusable CurrentFocusable { get; }
        public void Get(IFocusable focusable);
        public void Remove(IFocusable focusable);
    }
}