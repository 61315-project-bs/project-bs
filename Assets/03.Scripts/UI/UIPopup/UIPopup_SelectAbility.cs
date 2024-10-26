using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public enum AbilityType
{
    None = -1,
    Stat = 0,
    AssistanceWeapon = 1
}

[Serializable]
public class Test_Ability
{
    public AbilityType AbilityType;
    // Player stat
    public int Index;
    public float IncreaseValue;
    public int Level;
    public string Desc;
    public string Name;
    public Sprite Icon;
}

public class UIPopup_SelectAbility : UIPopup
{
    private UISubScene_SelectAbility_Presenter presenter;
    [SerializeField] private UISubItem_AbilityCard[] _subItemCard;
    [SerializeField] private Test_Ability[] _abilitySet;
    public UISubItem_AbilityCard[] SubItemCard { get => _subItemCard; }
    public Test_Ability[] AbilitySet { get => _abilitySet; }

    protected override void Awake()
    {
        base.Awake();
        presenter = new UISubScene_SelectAbility_Presenter(this);
    }
}

public class UISubScene_SelectAbility_Presenter : Presenter<UIPopup_SelectAbility>
{
    public UISubScene_SelectAbility_Presenter(UIPopup_SelectAbility view) : base(view) 
    {
        SetData();
    }
    private void SetData()
    {
        int length = _view.AbilitySet.Length;
        int rand;
        bool[] isUse = new bool[length];
        int iterateCount = 0;
        while(iterateCount < 3)
        {
            rand = Random.Range(0, length - 1);
            if (isUse[rand] == false)
            {
                _view.SubItemCard[iterateCount].SetData(_view.AbilitySet[rand]);
                isUse[rand] = true;
                iterateCount++;
            }
        }
    }
}