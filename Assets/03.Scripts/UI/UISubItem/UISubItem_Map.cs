using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISubItem_Map : UISubItem, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button _btnSelect;
    [SerializeField] private Image _imgMapIcon;
    [SerializeField] private int _mapId;
    [SerializeField] private UISubItem_MapDetail _mapDetail;
    private UISubItem_Map_Presenter _presenter;
    protected override void Awake()
    {
        base.Awake();
        _presenter = new UISubItem_Map_Presenter(this);
        _presenter.Init(_mapDetail,_imgMapIcon, _btnSelect, _mapId);
        _btnSelect.onClick.AddListener(() =>
        {
            //UIManager.Instance.InstantiateUISubScene<UISubScene_Test>();
            _presenter.OnSelectMap();
        });
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _presenter.OnMouseOver(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _presenter.OnMouseOver(false);
    }
}

public class UISubItem_Map_Presenter : Presenter<UISubItem_Map>
{
    public UISubItem_Map_Presenter(UISubItem_Map view) : base(view) {}
    private Map _mapData;
    private GameObject _mapDetailObj;
    private Image _img;
    private int _idx;
    private bool _isSelect = false;
    public void Init(UISubItem_MapDetail mapDetail, Image mapIcon, Button btnSelect, int idx)
    {
        _idx = idx;
        _mapData = TempMapDataHandler.Instance.Maps[_idx];
        if(_mapData == null)
        {
            Debug.LogError($"[UISubItem_Map_Presenter]::::_mapData is null.");
            return;
        }
        if (_mapData.IsLock) btnSelect.interactable = false;
        mapIcon.sprite = _mapData.Icon;
        _img = mapIcon;
        SetMapDetail(mapDetail);
    }
    private void SetMapDetail(UISubItem_MapDetail mapDetail)
    {
        mapDetail.SetData(_mapData);
        _mapDetailObj = mapDetail.gameObject;
        _mapDetailObj.SetActive(false);
    }
    public void OnMouseOver(bool isOver)
    {
        _mapDetailObj.SetActive(isOver);
    }
    public void OnSelectMap()
    {
        if (_mapData.IsLock)
            return;
        _isSelect = !_isSelect;
        TempMapDataHandler.Instance.SelectMapIdx = _isSelect ? _idx : -1;
        Material mat = new Material(_img.material);
        _img.material = mat;
        mat.SetFloat("_IsOutline", _isSelect ? 1 : 0);
    }
}