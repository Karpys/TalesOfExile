using System;
using KarpysDev.Script.Manager;
using TweenCustom;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KarpysDev.Script.Map_Related
{
    public class GoldWorldHolder : MonoBehaviour
    {
        [SerializeField] private Transform m_JumpContainer = null;
        [SerializeField] private float m_JumpForce = 0;
        [SerializeField] private float m_JumpDistance = 0;
        
        [Header("Parameters")]
        [SerializeField] protected Vector2 m_SpeedReference = new Vector2(5, 0.2f);

        private void Animate()
        {
            Transform targetTransform = GameManager.Instance.PlayerEntity.transform;
            Vector3 start = targetTransform.position;
            m_JumpContainer.transform.localPosition = Vector3.zero;
            transform.position = start;
            
            Vector3 randomInsideSquare = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(1f,m_JumpDistance);
            float arrowSpeed = Vector2.Distance(Vector2.zero, randomInsideSquare) * m_SpeedReference.y / m_SpeedReference.x;

            Jump(arrowSpeed);
            transform.DoMove(randomInsideSquare + start, arrowSpeed);
            //Todo : OnComplete Move Towards (Set parent => localDoMove => Jump)targetTransform//
        }

        private void Jump(float duration)
        {
            m_JumpContainer.DoLocalMove(new Vector3(0, m_JumpForce, 0), duration / 2).OnComplete(() =>
            {
                m_JumpContainer.DoLocalMove(Vector3.zero, duration / 2);
            });
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Animate();
            }
        }
    }
}