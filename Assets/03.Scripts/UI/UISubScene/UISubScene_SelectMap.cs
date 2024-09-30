using UnityEngine;
using UnityEngine.UI;

public class UISubScene_SelectMap : UISubScene
{
  [SerializeField] Button OpenTestOverlapSubSceneButton;
  private UISubScene_SelectMap_Presenter presenter;

  protected override void Awake()
  {
    base.Awake();
    presenter = new UISubScene_SelectMap_Presenter(this);
    InitializeButtons();
  }

  private void InitializeButtons()
  {
    OpenTestOverlapSubSceneButton.onClick.AddListener(presenter.OpenTestOverlapSubScene);
  }
}

public class UISubScene_SelectMap_Presenter : Presenter
{
  public UISubScene_SelectMap_Presenter(UIBase view) : base(view) { }

  public void OpenTestOverlapSubScene()
  {
    UIManager.Instance.InstantiateUISubScene<UISubScene_Test>();
  }
}