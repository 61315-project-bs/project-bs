using UnityEngine;
using UnityEngine.UI;

public class UIPopup : UIBase
{
  [SerializeField] protected Button ExitButton;
  protected override void Awake()
  {
    base.Awake();
    UIManager.Instance.SetUIPopup(gameObject);
    ExitButton.onClick.AddListener(() => UIManager.Instance.DestroyUIPopup(this));
  }
}