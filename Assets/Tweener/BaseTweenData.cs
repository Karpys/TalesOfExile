using UnityEngine;

[System.Serializable]
public class BaseTweenData
{
    public Transform TargetTransform = null;
    public Vector3 EndValue = Vector3.one;
    public float Duration = 1f;
}
