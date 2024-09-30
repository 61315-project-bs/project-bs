using UnityEngine;
using UnityEngine.UI;

public class UISubScene : UIBase
{
  [SerializeField] protected Button ExitButton;

  protected override void Awake()
  {
    base.Awake();
    UIManager.Instance.SetUISubScene(gameObject);
    ExitButton.onClick.AddListener(() => UIManager.Instance.DestroyUISubScene(this));
  }
}