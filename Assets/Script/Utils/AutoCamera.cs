namespace Script.Utils
{
    using System;
    using UnityEngine;

    public class AutoCamera : MonoBehaviour
    {
        [SerializeField] private float _PPU = 64;
        [SerializeField] Camera _Camera = null;

        private void Awake()
        {
            _Camera.orthographicSize = Screen.height / (2f * _PPU);
        }
    }
}