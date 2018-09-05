using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Audience
{
	public float AudienceAppreciation { get; set; }
	public bool AudienceFrenzy { get; set; }
	public float LackOfEnergyFactor { get; set; }//The value determining adjustments to Audience Appreciation based on lack of energy
	public float ExceptionalFeatFactor { get; set; }
    public float LastIncrease = 0f;
    public string LastIncreaseSource = "Hype before fight";
	public Character AudienceFavour { get; set; }

    public Audience()
    {
		AudienceFrenzy = false;
		LackOfEnergyFactor = 0.5f;
		ExceptionalFeatFactor = 2;
        AudienceAppreciation = 0;
    }

    public void ChangeAudienceFavourTarget (Character newTarget)
    {
        AudienceFavour = newTarget;
    }

    public void RegularAudienceAppreciationAdjustment(Character attacker, Character defender, float damageDone, bool specialsUsed)
    {
        float totalChange = 10f; //Any value entered here will serve as the base change of the method
        
        if (damageDone >= attacker.Attack)
        {
            totalChange = totalChange + 10f;
        }

        if (attacker.CurrentEnergy <= 0 && damageDone < attacker.Attack) //If you're out of energy, audience is less keen
        {
            totalChange = totalChange * LackOfEnergyFactor;
        }
        else if (attacker.CurrentEnergy <= 0 && damageDone >= attacker.Attack) //If you do well while out of energy, audience is very keen
        {
            totalChange = totalChange * ExceptionalFeatFactor;
        }

        LastIncrease = totalChange;
        LastIncreaseSource = "Strike by " + attacker.FirstName;

        AudienceAppreciation = AudienceAppreciation + totalChange;
    }

    public void SecondWindAppreciationIncrease(Character sWReceiver)
    {
        float AudienceAppreciationBonus = 200f;

        AudienceAppreciation = AudienceAppreciation + AudienceAppreciationBonus;

        //TODO: Add notifications that this is happening to the fight screen
        LastIncrease = AudienceAppreciationBonus;
        LastIncreaseSource = sWReceiver.FirstName + " received Second Wind!";

        //TODO_P: Make this a random chance, rather than guaranteed. Maybe based on Comebackitude?
        AudienceFavour = sWReceiver;
    }
}