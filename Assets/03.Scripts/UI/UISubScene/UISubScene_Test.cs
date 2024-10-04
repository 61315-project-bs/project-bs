public class UISubScene_Test : UISubScene
{
  private UISubScene_Test_Presenter presenter;

  protected override void Awake()
  {
    base.Awake();
    presenter = new UISubScene_Test_Presenter(this);
  }
}

public class UISubScene_Test_Presenter : Presenter<UISubScene_Test>
{
  public UISubScene_Test_Presenter(UISubScene_Test view) : base(view) { }
}