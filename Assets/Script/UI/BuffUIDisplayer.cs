using KarpysDev.Script.Entities.BuffRelated;
using TMPro;
using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class BuffUIDisplayer:MonoBehaviour
    {
        [SerializeField] private TMP_Text m_BuffName = null;
        [SerializeField] private TMP_Text m_BuffDescription = null;
        [SerializeField] private TMP_Text m_TurnLeft = null;

        private Buff m_AttachedBuff = null;

        private void Update()
        {
            if(m_AttachedBuff == null)
                return;

            m_TurnLeft.text = m_AttachedBuff.Cooldown + " turn left";
        }

        public void Initialize(Buff attachedBuff,BuffInfo buffInfo,Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
            m_BuffName.text = buffInfo.BuffName;
            m_BuffDescription.text = attachedBuff.GetDescription(buffInfo.BaseBuffDescription);
            m_AttachedBuff = attachedBuff;
        }

        public void Hide()
        {
            m_AttachedBuff = null;
            gameObject.SetActive(false);
        }
    }
}