using UnityEngine;
using UnityEngine.UI;

public class UIPopup_Setting : UIPopup
{
    [SerializeField] Button OpenTestOverlapPopupButton;
    private UIPopup_Setting_Presenter presenter;

    protected override void Awake()
    {
        base.Awake();
        presenter = new UIPopup_Setting_Presenter(this);
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        OpenTestOverlapPopupButton.onClick.AddListener(presenter.OpenTestOverlapPopup);
    }
}

public class UIPopup_Setting_Presenter : Presenter<UIPopup_Setting>
{
    public UIPopup_Setting_Presenter(UIPopup_Setting view) : base(view) { }

    public void OpenTestOverlapPopup()
    {
        UIManager.Instance.InstantiateUIPopup<UIPopup_Test>();
    }
}