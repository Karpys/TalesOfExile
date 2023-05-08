using System;
using UnityEngine;

public class InputManager:SingletonMonoBehavior<InputManager>
{
    [SerializeField] private float m_MouvementKeyHoldTimer = 0;
    private bool m_MouvementKeyHold = false;
    public bool IsControlPressed => Input.GetKey(KeyCode.LeftControl);
    public bool IsMovementKeyHold => m_MouvementKeyHold;

    private float currentMouvementKeyTimer = 0;

    private void Update()
    {
        MouvementKeyHoldUpdate();
    }

    private void MouvementKeyHoldUpdate()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Z))
        {
            currentMouvementKeyTimer += Time.deltaTime;

            if (currentMouvementKeyTimer > m_MouvementKeyHoldTimer)
                m_MouvementKeyHold = true;
        }
        else
        {
            if (!(currentMouvementKeyTimer > 0)) return;
            
            currentMouvementKeyTimer = 0;
            m_MouvementKeyHold = false;
        }
    }
}
