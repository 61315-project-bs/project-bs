using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class UISubItem_AbilityCard : UISubItem
{
    private UISubItem_AbilityCard_Presenter _presenter;
    [SerializeField] private Button _btnSelect;
    [SerializeField] private Image _imgIcon;
    [SerializeField] private Text _txtLevel;
    [SerializeField] private Text _txtName;
    [SerializeField] private Text _txtDesc;
    public IObservable<Unit> OnClick_Select { get => _btnSelect.OnClickAsObservable(); }
    public Image ImgIcon { get => _imgIcon; }
    public Text TxtLevel { get => _txtLevel; }
    public Text TxtName { get => _txtName; }
    public Text TxtDesc { get => _txtDesc; }

    protected override void Awake()
    {
        base.Awake();
    }
    public void SetData(Test_Ability abilitySet)
    {
        _presenter = new UISubItem_AbilityCard_Presenter(this);
        _presenter.SetData(abilitySet);
    }
}

public class UISubItem_AbilityCard_Presenter : Presenter<UISubItem_AbilityCard>
{
    public UISubItem_AbilityCard_Presenter(UISubItem_AbilityCard view) : base(view) 
    {
        
    }

    public void SetData(Test_Ability abilitySet)
    {
        _view.ImgIcon.sprite = abilitySet.Icon;
        _view.TxtName.text = abilitySet.Name;
        _view.TxtDesc.text = abilitySet.Desc;
        _view.TxtLevel.text = abilitySet.Level == 0 ? "New" : $"·¹º§ : abilitySet.Level";
    }
}
