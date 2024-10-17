using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Data
{
    public float Speed { get; private set; }
    public float Att { get; private set; }
    public Vector3 InitPos { get; private set; }
    public Vector3 TargetPos { get; private set; }
    public Bullet_Data(Vector3 initPos, Vector3 targetPos, float speed, float att)
    {
        InitPos = initPos;
        Speed = speed;
        Att = att;
        TargetPos = targetPos;
    }
}
/// <summary>
/// Temp Class
/// </summary>

public class Bullet : MonoBehaviour
{
    private Rigidbody _rbody;
    private Bullet_Data _data;
    private void Init()
    {
        _rbody = GetComponent<Rigidbody>();
    }
    
    public void SetBullet(Bullet_Data data)
    {
        _data = data;
        transform.position = _data.InitPos;
        Init();
    }

    // mouse pos (target) 
    public void OnFire()
    {
        Vector3 newPos = (_data.TargetPos - transform.position).normalized;
        newPos.y = 0;
        Debug.Log($"OnFire::::{newPos}");
        _rbody.velocity = newPos * _data.Speed;
    }
}
