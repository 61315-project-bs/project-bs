using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISubItem_AbilityCard : UISubItem
{
    private UISubItem_AbilityCard_Presenter _presenter;
    [SerializeField] private Button _btnSelect;
    [SerializeField] private Image _imgIcon;
    [SerializeField] private Text _txtLevel;
    [SerializeField] private Text _txtName;
    [SerializeField] private Text _txtDesc;
    protected override void Awake()
    {
        base.Awake();
        _presenter = new UISubItem_AbilityCard_Presenter(this);
    }
}

public class UISubItem_AbilityCard_Presenter : Presenter<UISubItem_AbilityCard>
{
    public UISubItem_AbilityCard_Presenter(UISubItem_AbilityCard view) : base(view) { }
}
