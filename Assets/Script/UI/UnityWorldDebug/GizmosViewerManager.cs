using UnityEngine;

namespace KarpysDev.Script.UI.UnityWorldDebug
{
    public class GizmosViewerManager : SingletonMonoBehavior<GizmosViewerManager>
    {
        [SerializeField] private CubeViewer m_CubeViewer = null;

        public void CreateCube(Vector3 position, Vector3 size,Color color,Transform parent = null)
        {
            CubeViewer cube = null;

            if (parent)
            {
                cube = Instantiate(m_CubeViewer,parent);
                cube.transform.localPosition = position;
            }
            else
            {
                cube = Instantiate(m_CubeViewer, position, Quaternion.identity);
            }
        
            cube.Initalize(size,color);
        }
    }
}