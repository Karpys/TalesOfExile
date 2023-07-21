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
        [SerializeField] private Vector2 m_JumpDelay = Vector2.zero;
        
        [Header("Parameters")]
        [SerializeField] protected Vector2 m_SpeedReference = new Vector2(5, 0.2f);

        private BaseTween m_MoveToTargetTween = null;
        private GoldManager m_GoldManager = null;
        private float m_GoldValue = 0;

        public void Initialize(GoldManager goldManager)
        {
            m_GoldManager = goldManager;
        }
        public void Launch(Vector3 spawnPosition,Transform target,float goldValue)
        {
            m_GoldValue = goldValue;
            transform.position = spawnPosition;
            Vector3 randomInsideSquare = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(1f,m_JumpDistance);
            float speed = Vector2.Distance(Vector2.zero, randomInsideSquare) * m_SpeedReference.y / m_SpeedReference.x;

            Jump(speed);
            transform.DoMove(randomInsideSquare + spawnPosition, speed).OnComplete(() =>
            {
                m_MoveToTargetTween = transform.DoLocalMove(Vector3.zero, speed).SetDelay(Random.Range(m_JumpDelay.x,m_JumpDelay.y)).OnStart(() => MoveToTarget(target,speed)).OnComplete(PoolReturn);
            });
        }

        private void PoolReturn()
        {
            m_GoldManager.ChangeGoldValue(m_GoldValue);
            m_GoldValue = 0;
            m_JumpContainer.transform.localPosition = Vector3.zero;
            transform.parent = m_GoldManager.transform;
            m_GoldManager.Return(this);
        }

        private void MoveToTarget(Transform targetTransform,float speed)
        {
            Jump(speed);
            Transform t = transform;
            t.parent = targetTransform;
            m_MoveToTargetTween.SetStartValue(t.localPosition);
        }

        private void Jump(float duration)
        {
            m_JumpContainer.DoLocalMove(new Vector3(0, m_JumpForce, 0), duration / 2).OnComplete(() =>
            {
                m_JumpContainer.DoLocalMove(Vector3.zero, duration / 2);
            });
        }
    }
}