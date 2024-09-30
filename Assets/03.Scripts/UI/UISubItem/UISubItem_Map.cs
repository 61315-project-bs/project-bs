using UnityEngine.UI;

public class UISubItem_Map : UISubItem
{
  protected override void Awake()
  {
    base.Awake();
    GetComponent<Button>().onClick.AddListener(() =>
    {
      UIManager.Instance.InstantiateUISubScene<UISubScene_Test>();
    });
  }
}