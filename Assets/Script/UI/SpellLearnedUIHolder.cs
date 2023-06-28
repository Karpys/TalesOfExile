namespace KarpysDev.Script.UI
{
    public class SpellLearnedUIHolder : SpellUIHolder
    {
        private CanvasSkillLearned m_Controller = null;

        public void SetController(CanvasSkillLearned controller)
        {
            m_Controller = controller;
        }

        protected override void OnEnter()
        {
            base.OnEnter();
            m_Controller.SetCurrentHolder(this);
        }

        protected override void OnExit()
        {
            base.OnExit();
            m_Controller.HideDisplaySpell();
        }
    }
}