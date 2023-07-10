using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class Fx_MeteorBlast : Fx_BurstAnimation
    {
        [SerializeField] private Vector3 m_Offset = Vector3.zero;
        [SerializeField] private float m_MovementDuration = 0.2f;
        [SerializeField] private Vector2 m_RangeDelay = Vector2.up;
        [SerializeField] private Transform m_VisualTransform = null;

        [Header("Call Back Anim")]
        [SerializeField] private SpellAnimation m_CallBackAnim = null;

        private Vector3 m_Destination = Vector3.zero;
        protected override void Start()
        {
            Transform targetTransform = transform;
            Vector3 position = targetTransform.position;
            m_Destination = position;
            position += m_Offset;
            targetTransform.position = position;
            
            base.Start();
        }

        protected override void Animate()
        {
            transform.DoMove(m_Destination, m_MovementDuration).SetDelay(Random.Range(m_RangeDelay.x,m_RangeDelay.y)).OnStart(
            () =>
            {
                m_VisualTransform.gameObject.SetActive(true);
            }).OnComplete(() =>
            {
                m_CallBackAnim.TriggerFx(m_Destination);
                Destroy(gameObject);
            });
        }
    }
}