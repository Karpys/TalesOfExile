using UnityEngine;

namespace KarpysDev.Script.Entities.EntitiesBehaviour
{
    public abstract class EntityBehaviour
    {
        protected BoardEntity m_AttachedEntity = null;
        public abstract void Behave();

        public void SetEntity(BoardEntity boardEntity)
        {
            m_AttachedEntity = boardEntity;
            InitializeEntityBehaviour();
        }

        protected abstract void InitializeEntityBehaviour();
    }
}