using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBaseStat : MonoBehaviour
{
    // 피해량
    public float DMG_per { get; set; }
    // 최대 체력
    public int MaxHP { get; set; }
    // 체력 재생
    public int HPS { get; set; }
    // 방어력
    public float DEF { get; set; }
    // 이동 속도
    public float SPD_per { get; set; }
    // 투사체 수 - 보조무기
    public int PJT { get; set; }
    // 공격 범위 - 보조 무기
    public float AtkScale_per { get; set; }
    // 지속 시간 - 보조 무기
    public float AtkDue_per { get; set; }
    // 획득 반경
    public float Magnet_per { get; set; }
    // 쿨타임 감소
    public float Cooldown_per { get; set; }
    // 경험치 획득량
    public float EXP_per { get; set; }
    // 골드 획득량
    public float Gold_per { get; set; }
    // 치명타 확률
    public float CriPro_per { get; set; }
    // 치명타 피해
    public float CriDMG_per { get; set; }
    // 기본 공격 속도 - 주 무기
    public float AtkSPD_per { get; set; }
    // 재장전 속도 - 주 무기
    public float Reload_per { get; set; }
    public StatType Type { get; set; }
}
