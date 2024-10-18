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
    public Text Txt_Reloading;
}

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerGunUI _playerGunUi;


    void Start()
    {
        Init();
    }

    private void Init()
    {
        // MVP가 적용되지 않은 임시 로직입니다.
        _player.GunController.Act_OnShot += OnShot;
        _player.GunController.Act_OnReload += OnReload;
        _player.GunController.Act_RealodTime += OnReloadTime;
    }

    private void OnShot(int currMag, int maxMag)
    {
        _playerGunUi.Txt_Mag.text = $"{currMag} / {maxMag}";
    }
    private void OnReload()
    {
        _playerGunUi.Img_Reloading.gameObject.SetActive(true);
        _playerGunUi.Img_Reloading.fillAmount = 1.0f;
    }
    private void OnReloadTime(float currTime, float maxTime)
    {
        float progress = 1.0f - currTime / maxTime;
        _playerGunUi.Img_Reloading.fillAmount = progress;
    }

}
