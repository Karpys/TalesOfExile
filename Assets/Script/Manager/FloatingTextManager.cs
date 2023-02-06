using UnityEngine;

public class FloatingTextManager : SingletonMonoBehavior<FloatingTextManager>
{
    [SerializeField] private FloatingTextBurst m_FloatingPrefab = null;

    public void SpawnFloatingText(Vector3 position,float value,Color? color = null)
    {
        Color targetColor = color ?? Color.white;
        FloatingTextBurst text = Instantiate(m_FloatingPrefab, position,Quaternion.identity);
        text.LaunchFloat(value,targetColor);
    }
}
