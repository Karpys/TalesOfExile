namespace KarpysDev.Script.UI.Pointer
{
    public class UIPointerController : UIPointer
    {
        private IUIPointerController m_Controller = null;
        
        public void AssignController(IUIPointerController controller)
        {
            m_Controller = controller;
        }
        protected override void OnEnter()
        {
            m_Controller.SetCurrentPointer(this);
        }

        protected override void OnExit()
        {
            return;
        }
    }

    public interface IUIPointerController
    {
        public void SetCurrentPointer(UIPointerController pointerController);
    }
}