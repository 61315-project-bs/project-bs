using UnityEngine;
using UnityEngine.UI;

public class UIScene_Lobby : UIScene
{
    [SerializeField] Button GoToLabButton;
    [SerializeField] Button GoToFoyerButton;
    [SerializeField] Button GoToSelectMapButton;
    [SerializeField] Button GoToPlayButton;
    [SerializeField] Button GoToSettingButton;

    private UIScene_Lobby_Presenter presenter;

    protected override void Awake()
    {
        base.Awake();
        presenter = new UIScene_Lobby_Presenter(this);
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        GoToLabButton.onClick.AddListener(presenter.OnGoToLab);
        GoToFoyerButton.onClick.AddListener(presenter.OnGoToFoyer);
        GoToSelectMapButton.onClick.AddListener(presenter.OnGoToSelectMap);
        GoToPlayButton.onClick.AddListener(presenter.OnGoToPlay);
        GoToSettingButton.onClick.AddListener(presenter.OnGoToSetting);
    }
}

public class UIScene_Lobby_Presenter : Presenter
{
    public UIScene_Lobby_Presenter(UIBase view) : base(view) { }

    public void OnGoToLab()
    {
    }

    public void OnGoToFoyer()
    {
    }

    public void OnGoToSelectMap()
    {
        UIManager.Instance.InstantiateUISubScene<UISubScene_SelectMap>();
    }

    public void OnGoToPlay()
    {
    }

    public void OnGoToSetting()
    {
        UIManager.Instance.InstantiateUIPopup<UIPopup_Setting>();
    }
}
