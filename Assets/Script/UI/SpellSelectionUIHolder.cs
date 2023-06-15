namespace KarpysDev.Script.UI
{
    public class SpellSelectionUIHolder : SpellUIHolder
    {
        private SpellSelectionUI m_SelectionController = null;

        public void SetSelectionController(SpellSelectionUI selectionController)
        {
            m_SelectionController = selectionController;
        }

        protected override void OnEnter()
        {
            m_SelectionController.SetCurrentPointer(this);
        }
    }
}