using UniRx;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class UISubScene_SelectMap : UISubScene
{
    [SerializeField] Button OpenTestOverlapSubSceneButton;
    [Tooltip("0 : left button \n 1 : right button")]
    [SerializeField] private Button[] _btnsSetLevel;
    [SerializeField] private Text _txtSetLevel;
    [SerializeField] private Button _decision;
    private UISubScene_SelectMap_Presenter presenter;
    public Text TxtSetLevel { get { return _txtSetLevel; } }

    // Component 자체를 전달하는 것 보단, stream을 전달하는것이 추후 코드를 볼 때 명확하게 이해할 수 있을 것 같아서 수정 했습니다.
    public IObservable<Unit> OnClick_SetLevel_Left { get { return _btnsSetLevel[0].OnClickAsObservable(); } }
    public IObservable<Unit> OnClick_SetLevel_Right { get { return _btnsSetLevel[1].OnClickAsObservable(); } }
    public IObservable<Unit> OnClick_Decision { get { return _decision.OnClickAsObservable(); } }
    public IObservable<Unit> OnClick_OpenTestOverlap { get { return  OpenTestOverlapSubSceneButton.OnClickAsObservable(); } }

    protected override void Awake()
    {
        base.Awake();
        presenter = new UISubScene_SelectMap_Presenter(this);
    }
    protected override void OnDestory()
    {
        base.OnDestory();
        presenter.DisopseStreams();
        presenter = null;
    }
    public void SetLevelActivater(bool isLeft, bool isRight)
    {
        _btnsSetLevel[0].interactable = isLeft;
        _btnsSetLevel[1].interactable = isRight;
    }
}

public class UISubScene_SelectMap_Presenter : Presenter<UISubScene_SelectMap>
{
    public UISubScene_SelectMap_Presenter(UISubScene_SelectMap view) : base(view) 
    {
        TempMapDataHandler.Instance.ReactCurrLevel
        .AsObservable()
        .Subscribe(level=>
        {
            if (level < 0)
                level = 0;
            if (level > TempMapDataHandler.Instance.MaxLevel)
                level = TempMapDataHandler.Instance.MaxLevel;
            view.SetLevelActivater(level != 0, level != TempMapDataHandler.Instance.MaxLevel);
            view.TxtSetLevel.text = TempMapDataHandler.Instance.STR_LEVEL[level];
        }).EnqueueDispose(_disposables);

        view.OnClick_SetLevel_Left
        .Subscribe(_ =>
        {
            TempMapDataHandler.Instance.CurrLevel--;
        }).EnqueueDispose(_disposables);

        view.OnClick_SetLevel_Right
        .Subscribe(_ =>
        {
            TempMapDataHandler.Instance.CurrLevel++;
        }).EnqueueDispose(_disposables);

        view.OnClick_OpenTestOverlap
        .Subscribe(_ =>
        {
            UIManager.Instance.InstantiateUISubScene<UISubScene_Test>();
        }).EnqueueDispose(_disposables);

    }

}

