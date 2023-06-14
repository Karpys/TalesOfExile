
using System.Collections.Generic;
using KarpysDev.Script.Utils;
using UnityEditor;
using UnityEngine;

namespace KarpysDev.Script.SkillTree
{
    [CustomEditor(typeof(SkillTreeHelper))]
    public class SkillTreeHelperEditor : UnityEditor.Editor
    {
        private SkillTreeHelper m_Target = null;
        private void OnEnable()
        {
            m_Target = target as SkillTreeHelper;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            AssignControllerReferenceButton();
            AssignNodeIdButton();
            AssignConnectedNode();
            DeleteConnexionButton();
            GenerateConnexion();
            //DistanceTest();
        }

        private void DeleteConnexionButton()
        {
            if (GUILayout.Button("Delete Connexion"))
            {
                DeleteConnexion();
            }
        }

        private void DeleteConnexion()
        {
            int childCount = m_Target.ConnexionHolder.childCount;

            for (int i = 0; i < childCount; i++)
            {
                DestroyImmediate(m_Target.ConnexionHolder.GetChild(0).gameObject);
            }
        }

        private void GenerateConnexion()
        {
            if (GUILayout.Button("Generate Connexion"))
            {
                DeleteConnexion();
                
                List<Vector2Int> connexions = new List<Vector2Int>();
                BaseSkillTreeNode[] nodes = m_Target.NodeHolder.GetComponentsInChildren<BaseSkillTreeNode>();

                foreach (BaseSkillTreeNode node in nodes)
                {
                    foreach (BaseSkillTreeNode connectedNode in node.ConnectedNodes)
                    {
                        Vector2Int connexion = SkillTreeUtils.GetConnexion(node, connectedNode);

                        if (!connexions.Contains(connexion))
                        {
                            CreateConnexion(node,connectedNode);
                            connexions.Add(connexion);
                        }
                    }
                }
            }
        }

        private void CreateConnexion(BaseSkillTreeNode node1, BaseSkillTreeNode node2)
        {
            //Instantiate at node 1//
            //Rotate toward node 2//
            //Apply new Height//
            GameObject connexion = (GameObject)PrefabUtility.InstantiatePrefab(m_Target.ConnexionPrefab, m_Target.ConnexionHolder);
            connexion.transform.position = node1.transform.position;
            SpriteUtils.RotateTowardPoint(node1.transform.position,node2.transform.position,connexion.transform,-90);

            float distance = Vector3.Distance(node1.transform.position, node2.transform.position);
            
            RectTransform rectTransform = connexion.transform as RectTransform;
            rectTransform.sizeDelta =new Vector2(rectTransform.sizeDelta.x,distance * 10);
        }
        
        private void AssignConnectedNode()
        {
            if (GUILayout.Button("Assign Connected Node"))
            {
                BaseSkillTreeNode[] nodes = m_Target.NodeHolder.GetComponentsInChildren<BaseSkillTreeNode>();

                foreach (BaseSkillTreeNode node in nodes)
                {
                    List<BaseSkillTreeNode> closeNodes = new List<BaseSkillTreeNode>();

                    foreach (BaseSkillTreeNode treeNode in nodes)
                    {
                        if(node == treeNode)
                            continue;

                        if (Vector3.Distance(node.transform.position, treeNode.transform.position) <= m_Target.ConnectedNodeDistance)
                        {
                            closeNodes.Add(treeNode);    
                        }
                    }
                    
                    node.AssignConnected(closeNodes.ToArray());
                    EditorUtility.SetDirty(node);
                }
                
                EditorUtility.SetDirty(this);
            }
        }

        //private void DistanceTest()
        //{
        //    if (GUILayout.Button("Test Distance "))
        //    {
        //        float distance = Vector3.Distance(m_Target.TestNode.transform.position,
        //        m_Target.TestNode2.transform.position);
        //        Debug.Log("Distance :" + distance);
        //    }
        //}
        
        private void AssignControllerReferenceButton()
        {
            if (GUILayout.Button("Assign Controller Reference"))
            {
                BaseSkillTreeNode[] nodes = m_Target.NodeHolder.GetComponentsInChildren<BaseSkillTreeNode>();

                foreach (BaseSkillTreeNode node in nodes)
                {
                    node.AssignController(m_Target.NodeController);
                    EditorUtility.SetDirty(node);
                }
                
                EditorUtility.SetDirty(this);
            }
        }
        
        private void AssignNodeIdButton()
        {
            if (GUILayout.Button("Assign Node Id"))
            {
                BaseSkillTreeNode[] nodes = m_Target.NodeHolder.GetComponentsInChildren<BaseSkillTreeNode>();

                for (int i = 0; i < nodes.Length; i++)
                {
                    BaseSkillTreeNode node = nodes[i];
                    node.AssignNodeId(i);
                    EditorUtility.SetDirty(node);
                }

                EditorUtility.SetDirty(this);
            }
        }
    }
}