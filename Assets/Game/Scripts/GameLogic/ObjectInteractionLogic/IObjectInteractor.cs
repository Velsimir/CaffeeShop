using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable;

namespace Game.Scripts.GameLogic.ObjectInteractionLogic
{
    public interface IObjectInteractor
    {
        public IFocusable CurrentFocusable { get; }
        public void Get(IFocusable focusable);
        public void Remove(IFocusable focusable);
    }
}