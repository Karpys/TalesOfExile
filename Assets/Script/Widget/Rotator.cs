using System;
using UnityEngine;

namespace KarpysDev.Script.Widget
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Transform m_Transform = null;
        [SerializeField] private float m_RotateSpeed = 30f;
        
        private void Update()
        {
            float newAngle = m_Transform.eulerAngles.z + Time.deltaTime * m_RotateSpeed;
            m_Transform.eulerAngles = new Vector3(0, 0, newAngle);
        }
    }
}