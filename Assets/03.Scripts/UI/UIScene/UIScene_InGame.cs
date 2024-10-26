using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;

[Serializable]
public class PlayerGunUI
{
    public Text Txt_Mag;
    public Image Img_Reloading;
}

[Serializable]
public class PlayerSkillUI
{
    public Image Img_CoolTime;
    public Text Txt_CoolTime;
}
public class UIScene_InGame : UIScene
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerGunUI _playerGunUi;
    [SerializeField] private PlayerSkillUI _playerSkillUi;
    private UIScene_InGame_Presenter _presenter;

    public Player Player { get => _player; }
    public PlayerGunUI PlayerGunUI { get => _playerGunUi; }
    public PlayerSkillUI PlayerSkillUI { get => _playerSkillUi; }

    protected override void Awake()
    {
        base.Awake();
        _presenter = new UIScene_InGame_Presenter(this);
    }

}

public class UIScene_InGame_Presenter : Presenter<UIScene_InGame>
{
    public UIScene_InGame_Presenter(UIScene_InGame view) : base(view) 
    {
        _view.Player.GunController.Act_OnShot += OnShot;
        _view.Player.GunController.Act_OnReload += OnReload;
        _view.Player.GunController.Act_RealodTime += OnReloadTime;
        _view.Player.IsSkillCooltime
            .AsObservable()
            .Subscribe(isCooltime =>
            {
                _view.PlayerSkillUI.Txt_CoolTime.text = isCooltime ? "Use Skill!" : "";
            }).AddTo(_view.gameObject);
        _view.Player.SkillCoolTime += OnSkillCoolTime;
    }

    private void OnShot(int currMag, int maxMag)
    {
        _view.PlayerGunUI.Txt_Mag.text = $"{currMag} / {maxMag}";
    }
    private void OnReload()
    {
        _view.PlayerGunUI.Img_Reloading.gameObject.SetActive(true);
        _view.PlayerGunUI.Img_Reloading.fillAmount = 1.0f;
    }
    private void OnReloadTime(float currTime, float maxTime)
    {
        float progress = 1.0f - currTime / maxTime;
        _view.PlayerGunUI.Img_Reloading.fillAmount = progress;
    }

    private void OnSkillCoolTime(float currTime, float maxTime)
    {
        float progress = 1.0f - currTime / maxTime;
        _view.PlayerSkillUI.Img_CoolTime.fillAmount = progress;
        _view.PlayerSkillUI.Txt_CoolTime.text = (maxTime - currTime).ToString("N2");
    }
}