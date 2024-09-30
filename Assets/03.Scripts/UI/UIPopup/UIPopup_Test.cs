using UnityEngine;
using UnityEngine.UI;

public class UIPopup_Test : UIPopup
{
    [SerializeField] Button AllExitButton;
    private UIPopup_Test_Presenter presenter;

    protected override void Awake()
    {
        base.Awake();
        presenter = new UIPopup_Test_Presenter(this);
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        AllExitButton.onClick.AddListener(presenter.AllExit);
    }
}

public class UIPopup_Test_Presenter : Presenter
{
    public UIPopup_Test_Presenter(UIBase view) : base(view) { }

    public void AllExit()
    {
        UIManager.Instance.DestroyAllUIPopups();
    }
}