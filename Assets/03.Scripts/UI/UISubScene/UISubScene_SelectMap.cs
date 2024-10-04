using UniRx;
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
    public Button BtnDecision { get { return _decision; } }
    public Button OpenTestOverlap { get { return  OpenTestOverlapSubSceneButton; } }
    public Text TxtSetLevel { get { return _txtSetLevel; } }

    protected override void Awake()
    {
        base.Awake();
        presenter = new UISubScene_SelectMap_Presenter(this);
    }
}

public class UISubScene_SelectMap_Presenter : Presenter<UISubScene_SelectMap>
{
    public UISubScene_SelectMap_Presenter(UISubScene_SelectMap view) : base(view) 
    {
        TempMapDataHandler.Instance.ReactCurrLevel
            .AsObservable()
            .Subscribe(level => 
            {
                if (level < 0)
                    level = 0;
                if (level > TempMapDataHandler.Instance.MaxLevel)
                    level = TempMapDataHandler.Instance.MaxLevel;
                view.BtnSetLevel[0].interactable = level != 0;
                view.BtnSetLevel[1].interactable = level != TempMapDataHandler.Instance.MaxLevel;
                view.TxtSetLevel.text = TempMapDataHandler.Instance.STR_LEVEL[level];
            });
        view.BtnSetLevel[0].OnClickAsObservable()
        .Subscribe(_ =>
        {
            TempMapDataHandler.Instance.CurrLevel--;
        });
        view.BtnSetLevel[1].OnClickAsObservable()
        .Subscribe(_ =>
        {
            TempMapDataHandler.Instance.CurrLevel++;
        });
        view.OpenTestOverlap.OnClickAsObservable()
        .Subscribe(_ =>
        {
            UIManager.Instance.InstantiateUISubScene<UISubScene_Test>();
        });
    }

}