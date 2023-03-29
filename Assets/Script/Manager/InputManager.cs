using UnityEngine;

public class InputManager:SingletonMonoBehavior<InputManager>
{
    public bool IsControlPressed => Input.GetKey(KeyCode.LeftControl);
}
