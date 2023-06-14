using UnityEngine;

namespace KarpysDev.Script.SkillTree
{
    public class SkillTreeHelper : MonoBehaviour
    {
        [SerializeField] private Transform m_NodeHolder = null;
        [SerializeField] private Transform m_ConnexionHolder = null;
        [SerializeField] private SkillTreeNodeController m_NodeController = null;

        [Header("Connected Parameters")]
        //[SerializeField] private BaseSkillTreeNode m_TestNode = null;
        //[SerializeField] private BaseSkillTreeNode m_TestNode2 = null;
        [SerializeField] private float m_ConnectedNodesDistance = 15f;
        [SerializeField] private GameObject m_ConnexionPrefab = null;
        public Transform NodeHolder => m_NodeHolder;
        public Transform ConnexionHolder => m_ConnexionHolder;
        public SkillTreeNodeController NodeController => m_NodeController;
        public float ConnectedNodeDistance => m_ConnectedNodesDistance;

        public GameObject ConnexionPrefab => m_ConnexionPrefab;

        //public BaseSkillTreeNode TestNode => m_TestNode;

        //public BaseSkillTreeNode TestNode2 => m_TestNode2;
    }
}