using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class FollowMouse : MonoBehaviour
    {
        private void Update()
        {
            transform.position = Input.mousePosition;
        }
    }
}