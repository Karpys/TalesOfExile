using TweenCustom;

// public class FriendlyEntity : BoardEntity
// {
//     protected override void InitializeEntityBehaviour()
//     {
//         SetEntityBehaviour(new BaseEntityIA(this));
//     }
//
//     protected override void RegisterEntity()
//     {
//         GameManager.Instance.RegisterEntity(this);
//     }
//
//     public override void EntityAction()
//     {
//         if(m_CanBehave)
//             m_EntityBehaviour.Behave();
//     }
//
//     protected override void TriggerDeath()
//     {
//         if (m_IsDead)
//             return;
//                 
//         
//         base.TriggerDeath();
//     }
//     
//     protected override void Movement()
//     {
//         transform.DoKill();
//         transform.DoMove( m_TargetMap.GetTilePosition(m_XPosition, m_YPosition),0.1f);
//     }
// }