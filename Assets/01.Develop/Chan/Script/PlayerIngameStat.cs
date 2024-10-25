using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIngameStat : PlayerBaseStat, IStat_Single<PlayerIngameStat>
{
    public int Index { get; set; }
    public float Value { get; set; }
    private int[] _upgradeCount = new int[16];

    public int GetUpgradeCount(int index) => _upgradeCount[index];
    // 직접 Add하는게 아니면 해당 메서드를 반드시 호출해야 함.
    public void SetInfo(int index, float value)
    {
        Index = index;
        Value = value;
    }

    public void Add(int index, float value)
    {
        switch (index)
        {
            case 0: DMG_per += value; break;
            case 1: MaxHP += (int)value; break;
            case 2: HPS += (int)value; break;
            case 3: DEF += value; break;
            case 4: SPD_per += value; break;
            case 5: PJT += (int)value; break;
            case 6: AtkScale_per += value; break;
            case 7: AtkDue_per += value; break;
            case 8: Magnet_per += value; break;
            case 9: Cooldown_per += value; break;
            case 10: EXP_per += value; break;
            case 11: Gold_per += value; break;
            case 12: CriPro_per += value; break;
            case 13: CriDMG_per += value; break;
            case 14: AtkSPD_per += value; break;
            case 15: Reload_per += value; break;
        }
        _upgradeCount[index]++;
    }
    

    public PlayerIngameStat DeepCopy()
    {
        return Instantiate(this);
    }

    public void Multiply(int index, float value)
    {
        switch (index)
        {
            case 0: DMG_per *= value; break;
            case 1: MaxHP *= (int)value; break;
            case 2: HPS *= (int)value; break;
            case 3: DEF *= value; break;
            case 4: SPD_per *= value; break;
            case 5: PJT *= (int)value; break;
            case 6: AtkScale_per *= value; break;
            case 7: AtkDue_per *= value; break;
            case 8: Magnet_per *= value; break;
            case 9: Cooldown_per *= value; break;
            case 10: EXP_per *= value; break;
            case 11: Gold_per *= value; break;
            case 12: CriPro_per *= value; break;
            case 13: CriDMG_per *= value; break;
            case 14: AtkSPD_per *= value; break;
            case 15: Reload_per *= value; break;
        }
        _upgradeCount[index]++;
    }

    public void Remove(int index)
    {
        _upgradeCount[index]--;
    }

}
