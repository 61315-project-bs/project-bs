public class UISubScene_Test : UISubScene
{
  private UISubScene_Test_Presenter presenter;

  protected override void Awake()
  {
    base.Awake();
    presenter = new UISubScene_Test_Presenter(this);
  }
}

public class UISubScene_Test_Presenter : Presenter
{
  public UISubScene_Test_Presenter(UIBase view) : base(view) { }
}