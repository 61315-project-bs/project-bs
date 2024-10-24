using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISubScene_SelectAbility : UISubScene
{
    private UISubScene_SelectAbility_Presenter presenter;

    protected override void Awake()
    {
        base.Awake();
        presenter = new UISubScene_SelectAbility_Presenter(this);
    }
}

public class UISubScene_SelectAbility_Presenter : Presenter<UISubScene_SelectAbility>
{
    public UISubScene_SelectAbility_Presenter(UISubScene_SelectAbility view) : base(view) { }
}