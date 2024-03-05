namespace KarpysDev.Script.Manager.Library
{
    using KarpysUtils;
    using UnityEngine;

    public class VisualEffectLibrary : SingletonMonoBehavior<VisualEffectLibrary>
    {
        [SerializeField] private GenericLibrary<VisualEffectType, Transform> m_VisualEffectLibrary = null;

        private void Awake()
        {
            m_VisualEffectLibrary.InitializeDictionary();
        }

        public Transform GetVisualEffect(VisualEffectType visualEffectType)
        {
            return Instantiate(m_VisualEffectLibrary.GetViaKey(visualEffectType));
        }
    }
    
    public enum VisualEffectType
    {
        None = -1,
        FireDot = 0,
        RegenerationLeaf = 1,
        StunStars = 2,
        SkeletonCurseHead = 3,
    }
}