using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

public static class BasicValues
{
    //Values for character creation
    public static float baseCooldown = 8f;
    public static float strategyCooldownFactor = 0.05f;

    //Values for basic attacks in combat
    public static float baseDamage = 20f;
    public static float basicStrikeEnergyCost = 100f;

    //Setupvalues
    public static int basicManagerFunds = 100000;

    public static int basicDailyCost = 3;
    public static int maxDailyCost = 5;

    //Fight conclusion values
    public static float audienceAppreciationToFightMoneyFactor = 10f;
    public static float winningPercentage = 55f;

    public static int minFightMoneyCut = 15;
    public static int maxFightMoneyCut = 20;

    //Level up values
    public static float fightMoneyCutLevelIncreaseChance = 25f;
}