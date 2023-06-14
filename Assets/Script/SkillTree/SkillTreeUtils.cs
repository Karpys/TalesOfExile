using UnityEngine;

namespace KarpysDev.Script.SkillTree
{
    public static class SkillTreeUtils
    {
        public static Vector2Int GetConnexion(BaseSkillTreeNode node1, BaseSkillTreeNode node2)
        {
            if (node1.NodeId > node2.NodeId)
                return new Vector2Int(node2.NodeId, node1.NodeId);

            return new Vector2Int(node1.NodeId, node2.NodeId);
        }
    }
}