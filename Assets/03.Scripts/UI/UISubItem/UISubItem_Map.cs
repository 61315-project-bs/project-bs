using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using System;

public class UISubItem_Map : UISubItem
{
    [SerializeField] private Button _btnSelect;
    [SerializeField] private Image _imgMapIcon;
    [SerializeField] private int _mapId;
    [SerializeField] private UISubItem_MapDetail _mapDetail;
    private UISubItem_Map_Presenter _presenter;

    public UISubItem_MapDetail MapDetail { get { return _mapDetail; } }

    public IObservable<Unit> OnClick_BtnSelect { get { return _btnSelect.OnClickAsObservable(); } }
    public IObservable<PointerEventData> OnPointEnter_BtnSelect { get { return _btnSelect.OnPointerEnterAsObservable(); } }
    public IObservable<PointerEventData> OnPointExit_BtnSelect { get { return _btnSelect.OnPointerExitAsObservable(); } }

    //public Button BtnSelect { get { return _btnSelect; } }
    public Image ImgMapIcon { get { return _imgMapIcon; } }
    public int MapId { get { return _mapId; } }


    protected override void Awake()
    {
        base.Awake();
        _presenter = new UISubItem_Map_Presenter(this);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        _presenter.Dispose();
        _presenter = null;
    }
    public void SelectButtonActivater(bool isOn) => _btnSelect.interactable = isOn;
}

public class UISubItem_Map_Presenter : Presenter<UISubItem_Map>
{
    private GameObject _mapDetailObj = null;
    public UISubItem_Map_Presenter(UISubItem_Map view) : base(view)
    {
        Map mapData = TempMapDataHandler.Instance.Maps[_view.MapId];
        if (mapData == null)
        {
            Debug.LogError($"[UISubItem_Map_Presenter]::::_mapData is null.");
            return;
        }
        _view.ImgMapIcon.sprite = mapData.Icon;
        SetMapDetail(_view.MapDetail, mapData);

        view.OnPointEnter_BtnSelect
            .Subscribe(_ =>_mapDetailObj.SetActive(true))
            .EnqueueDispose(_disposables);
        view.OnPointExit_BtnSelect
            .Subscribe(_ =>_mapDetailObj.SetActive(false))
            .EnqueueDispose(_disposables);

        if (mapData.IsLock)
            view.SelectButtonActivater(false);
        else
        {
            view.SelectButtonActivater(true);
            view.OnClick_BtnSelect
                .Subscribe(_ =>TempMapDataHandler.Instance.SelectMapIdx = TempMapDataHandler.Instance.SelectMapIdx == -1 ? _view.MapId : -1)
                .EnqueueDispose(_disposables);
            TempMapDataHandler.Instance.ReactSelectMapIdx
                .Subscribe(idx => OnSelectMap())
                .EnqueueDispose(_disposables);
        }
    }
    private void SetMapDetail(UISubItem_MapDetail mapDetail, Map mapData)
    {
        mapDetail.SetData(mapData);
        _mapDetailObj = mapDetail.gameObject;
        _mapDetailObj.SetActive(false);
    }
    private void OnSelectMap()
    {
        Material mat = new Material(_view.ImgMapIcon.material);
        _view.ImgMapIcon.material = mat;
        mat.SetFloat("_IsOutline", TempMapDataHandler.Instance.SelectMapIdx == -1 ? 0 : 1);
    }
}