namespace KarpysDev.Script.Entities
{
    using KarpysUtils.TweenCustom;

    public class DefaultBoardEntity : BoardEntity
    {
        protected override void Movement()
        {
            //transform.DoKill();
            transform.DoMove( m_TargetMap.GetTilePosition(m_XPosition, m_YPosition),0.1f);
        }
    }
}