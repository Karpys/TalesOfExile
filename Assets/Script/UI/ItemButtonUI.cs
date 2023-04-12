using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonUI:MonoBehaviour
{
    [SerializeField] private Button m_Button = null;
    [SerializeField] private TMP_Text m_ButtonText = null;
    
    private InventoryObject m_AttachedItem = null;
    private ButtonAction m_FunctionCallOnClick = null;

    public void InitalizeButton(InventoryObject targetObject, ButtonAction action,string buttonText)
    {
        m_AttachedItem = targetObject;
        m_FunctionCallOnClick = action;
        m_ButtonText.text = buttonText;
        m_Button.onClick.AddListener(TriggerButtonAction);
    }

    private void TriggerButtonAction()
    {
        m_FunctionCallOnClick?.Invoke();
        ItemButtonOptionController.Instance.Clear();
    }
}

public class ItemButtonUIParameters
{
    public ButtonAction OnClickAction = null;
    public string ButtonText = null;

    public ItemButtonUIParameters(ButtonAction onClickAction,string buttonText)
    {
        OnClickAction = onClickAction;
        ButtonText = buttonText;
    }
}

public delegate void ButtonAction();