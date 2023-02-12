using UnityEngine;

public class FloatingTextManager : SingletonMonoBehavior<FloatingTextManager>
{
    [SerializeField] private FloatingTextBurst m_FloatingPrefab = null;

    public void SpawnFloatingText(Transform transform,float value,Color? color = null,float delay = 0f)
    {
        Color targetColor = color ?? Color.white;
        FloatingTextBurst text = Instantiate(m_FloatingPrefab, transform);
        text.LaunchFloat(value,targetColor,delay);
    }
}
