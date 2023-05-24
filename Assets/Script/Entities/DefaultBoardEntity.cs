using TweenCustom;

namespace KarpysDev.Script.Entities
{
    public class DefaultBoardEntity : BoardEntity
    {
        protected override void Movement()
        {
            transform.DoKill();
            transform.DoMove( m_TargetMap.GetTilePosition(m_XPosition, m_YPosition),0.1f);
        }
    }
}