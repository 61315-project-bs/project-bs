using UnityEngine;
using UnityEngine.UI;

public class UISubScene_SelectMap : UISubScene
{
    [SerializeField] Button OpenTestOverlapSubSceneButton;
    [Tooltip("0 : left button \n 1 : right button")]
    [SerializeField] private Button[] _btnsSetLevel;
    [SerializeField] private Text _txtSetLevel;
    [SerializeField] private Button _decision;
    private UISubScene_SelectMap_Presenter presenter;
    public Button[] BtnSetLevel { get { return _btnsSetLevel; } }

    protected override void Awake()
    {
        base.Awake();
        presenter = new UISubScene_SelectMap_Presenter(this);
        presenter.Init(_btnsSetLevel);
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        OpenTestOverlapSubSceneButton.onClick.AddListener(presenter.OpenTestOverlapSubScene);
        _btnsSetLevel[0].onClick.AddListener(() => presenter.SetLevel(_txtSetLevel, true));
        _btnsSetLevel[1].onClick.AddListener(() => presenter.SetLevel(_txtSetLevel, false));
        //_decision.onClick.AddListener(() => UIManager.Instance.InstantiateUIPopup<UIPopup>("ss"));
    }
}

public class UISubScene_SelectMap_Presenter : Presenter<UISubScene_SelectMap>
{
    public UISubScene_SelectMap_Presenter(UISubScene_SelectMap view) : base(view) { }

    /// <summary>
    /// 좋은 방법은 아닌듯..일단 임시로 구현
    /// </summary>
    private Button[] _btnSetLevel;

    public void Init(Button[] btnSetLevel)
    {
        _btnSetLevel = btnSetLevel;
    }
    public void SetLevel(Text text, bool isLeft)
    {
        if (isLeft)
            TempMapDataHandler.Instance.CurrLevel--;
        else
            TempMapDataHandler.Instance.CurrLevel++;
        //_view.BtnSetLevel[0].interactable = true;
        _btnSetLevel[0].interactable = TempMapDataHandler.Instance.CurrLevel != 0;
        _btnSetLevel[1].interactable = TempMapDataHandler.Instance.CurrLevel != TempMapDataHandler.Instance.MaxLevel;

        text.text = TempMapDataHandler.Instance.CurrLevelStr;
    }
    public void OpenTestOverlapSubScene()
    {
        UIManager.Instance.InstantiateUISubScene<UISubScene_Test>();
    }
}