using Cinemachine;
using System;
using System.Collections;
using UniRx;
using UnityEngine;

// 추후 부사수(trainer)와 분리될 가능성이 있음.
public class GunController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePos;

    private WaitForSeconds _waitReloadTime;
    private CinemachineBrain _cameraBrain;
    private IEnumerator IE_OnReload_Handle = null;
    private Vector3 _mousePosition;
    private float _rotOffset = 180;
    private float _distanceToCamera;

    public IntReactiveProperty CurrMagazine { get; private set; }
    public int MaxMagazine { get; set; } = 100;
    
    public void InitGun()
    {
        SetCamera();
        InitData();
        InitInput();
    }
    private void SetCamera()
    {
        // 만약  Cinemachine 카메라를 사용하면서 ScreenToWorldPoint를 사용해야 한다면 반드시 아래 로직을 적용해줘야 한다.
        _cameraBrain = Camera.main.GetComponent<CinemachineBrain>();
        _cameraBrain.m_CameraCutEvent.AddListener((brain) =>
        {
            if (brain != null)
            {
                if (brain.ActiveVirtualCamera != null)
                {
                    // if virtual camera changed
                    _distanceToCamera = Vector3.Distance(transform.position, brain.ActiveVirtualCamera.VirtualCameraGameObject.transform.position);
                    Debug.Log($"on cut event {brain.ActiveVirtualCamera.Name} {_distanceToCamera}");
                }
            }
        });
        // init distance
        _distanceToCamera = Vector3.Distance(transform.position, Camera.main.transform.position);
    }

    private void InitData()
    {
        _waitReloadTime = new WaitForSeconds(_player.TrainerData.HandleGun.ReloadTime);
        MaxMagazine -= _player.TrainerData.HandleGun.MaxMagazine;
        CurrMagazine = new IntReactiveProperty(_player.TrainerData.HandleGun.MaxMagazine);
        CurrMagazine
            .AsObservable()
            .Subscribe(mag =>
            {
                if (mag <= 1)
                    OnReload();
                Debug.Log($"Gun::::MaxMagazine::::{MaxMagazine}");
                Debug.Log($"Gun::::CurrMagazine::::{mag}");
            }).AddTo(gameObject);

    }

    private void InitInput()
    {
        Observable.EveryUpdate()
            .AsObservable()
            .Subscribe(pos =>
            {
                _distanceToCamera = Vector3.Distance(transform.position, Camera.main.transform.position);
                _mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _distanceToCamera));
                Vector2 newPos = _mousePosition - transform.position;
                float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg + _rotOffset;
                gameObject.transform.localRotation = Quaternion.Euler(AimDirection_LR(rotZ) == 1 ? 0 : -180, 0, AimDirection_LR(rotZ) == 1 ? rotZ : -rotZ);
                Vector2 aimDirection = AimDirection(rotZ);

                // [TODO] 플레이어 애니메이션은 다른 곳에서 호출 가능할 수 있게 수정 해야 함.
                _player.Animator.SetFloat("xDir", aimDirection.x);
                _player.Animator.SetFloat("yDir", aimDirection.y);
            }).AddTo(gameObject);

        Observable.EveryUpdate()
            .Where(click => Input.GetMouseButton(0) && CurrMagazine.Value > 0)
            .ThrottleFirst(TimeSpan.FromSeconds(_player.TrainerData.HandleGun.AttackSpeed * _player.PlayerBaseData.AttackSpeed))
            .Subscribe(pos =>
            {
                Bullet bullet = Instantiate(_bullet).GetComponent<Bullet>();
                bullet.SetBullet(new Bullet_Data(_firePos.position, _mousePosition, 10, 10));
                bullet.OnFire();
                CurrMagazine.Value--;
            }).AddTo(gameObject);
    }


    private Vector2 AimDirection(float rot)
    {
        if (rot > 315 || rot < 45) return Vector2.left;
        else if (rot > 45 && rot < 135) return Vector2.down;
        else if (rot > 135 && rot < 225) return Vector2.right;
        else if (rot > 225 && rot < 315) return Vector2.up;
        else
        {
            Debug.LogError("AimDirection::::Invalid.");
            return Vector2.zero;
        }
    }
    private int AimDirection_LR(float rot)
    {
        if (rot > 270 || rot < 90) return 1;
        else if (rot > 90 || rot < 270) return 2;
        else return -1;
    }

    private void OnReload()
    {
        if (IE_OnReload_Handle != null)
            return;
        StartCoroutine(IE_OnReload_Handle = IE_OnReload());
    }
    private IEnumerator IE_OnReload()
    {
        Debug.Log($"Gun::::Start::::Reload");
        yield return _waitReloadTime;
        if (MaxMagazine > _player.TrainerData.HandleGun.MaxMagazine)
        {
            MaxMagazine -= _player.TrainerData.HandleGun.MaxMagazine;
            CurrMagazine.Value = _player.TrainerData.HandleGun.MaxMagazine;
        }
        else
        { 
            CurrMagazine.Value = MaxMagazine;
            MaxMagazine = 0;
        }
        Debug.Log($"Gun::::Complete::::Reload");
        IE_OnReload_Handle = null;
    }
}
