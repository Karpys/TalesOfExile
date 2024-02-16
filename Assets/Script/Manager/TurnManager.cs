namespace KarpysDev.Script.Manager
{
    using System;
    using System.Collections.Generic;
    using KarpysUtils;
    using Map_Related;
    using UnityEngine;

    public class TurnManager : SingletonMonoBehavior<TurnManager>
    {
        [SerializeField] private GameManager m_GameManager = null;

        private List<ITurn> m_Turns = new List<ITurn>();
        private void Awake()
        {
            m_GameManager.A_OnEndTurn += UpdateTurn;
        }

        private void UpdateTurn()
        {
            for (int i = m_Turns.Count - 1; i >= 0; i--)
            {
                m_Turns[i].ReduceTurn();

                if (m_Turns[i].TurnLeft <= 0)
                {
                    m_Turns[i].OnEndLife();
                    RemoveTurn(m_Turns[i]);
                }
            }
        }

        public void AddTurn(ITurn turn)
        {
            m_Turns.Add(turn);
        }

        public void RemoveTurn(ITurn turn)
        {
            m_Turns.Remove(turn);
        }
    }
}