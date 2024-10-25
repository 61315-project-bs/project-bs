using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainStat : PlayerBaseStat, IStat<PlayerBaseStat>
{

    public void Add(PlayerBaseStat other)
    {
        DMG_per += other.DMG_per;
        MaxHP += other.MaxHP;
        HPS += other.HPS;
        DEF += other.DEF;
        SPD_per += other.SPD_per;
        PJT += other.PJT;
        AtkScale_per += other.AtkScale_per;
        AtkDue_per += other.AtkDue_per;
        Magnet_per += other.Magnet_per;
        Cooldown_per += other.Cooldown_per;
        EXP_per += other.EXP_per;
        Gold_per += other.Gold_per;
        CriPro_per += other.CriPro_per;
        CriDMG_per += other.CriDMG_per;
        AtkSPD_per += other.AtkSPD_per;
        Reload_per += other.Reload_per;
    }
    public PlayerBaseStat DeepCopy()
    {
        return Instantiate(this);
    }

    public void Multiply(PlayerBaseStat other)
    {
        DMG_per *= other.DMG_per;
        MaxHP *= other.MaxHP;
        HPS *= other.HPS;
        DEF *= other.DEF;
        SPD_per *= other.SPD_per;
        PJT *= other.PJT;
        AtkScale_per *= other.AtkScale_per;
        AtkDue_per *= other.AtkDue_per;
        Magnet_per *= other.Magnet_per;
        Cooldown_per *= other.Cooldown_per;
        EXP_per *= other.EXP_per;
        Gold_per *= other.Gold_per;
        CriPro_per *= other.CriPro_per;
        CriDMG_per *= other.CriDMG_per;
        AtkSPD_per *= other.AtkSPD_per;
        Reload_per *= other.Reload_per;
    }

    public PlayerBaseStat StatCalculator(Func<float, int, float> calculator, int num)
    {
        throw new NotImplementedException();
    }
}
