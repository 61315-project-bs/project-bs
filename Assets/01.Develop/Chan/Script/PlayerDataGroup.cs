using System;
using UnityEngine;

public class PlayerBaseData
{
    public float Damage = 1.0f;
    public int MaxHp = 1;
    public float HpRecovery = 0.0f;
    public float MoveSpeed = 1.0f;
    public int ProjectileCount = 0;
    public float ItemGetRadious = 1.0f;
    public float AttackRange = 1.0f;
    public float CoolTimeDecrease = 1.0f;
    public float GetExp = 1.0f;
    public float GetGold = 1.0f;
    public float CriticalChance = 0.0f;
    public float CriticalDamage = 2.0f;
    public float AttackSpeed = 1.0f;
    public float ReloadSpeed = 1.0f;
}

[Serializable]
public class TrainerData
{
    public Gun HandleGun;
    public Action UltimateSkill;
}

[Serializable]
public class Gun
{
    public int MaxMagazine;
    public float Damage;
    public float ReloadTime;
    public float AttackSpeed;
}

public class Pistol : Gun
{

}

