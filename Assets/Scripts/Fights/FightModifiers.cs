using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightModifiers: MonoBehaviour
{
	public float DamageIncrease(float baseDamage, int skillLevel){
		float extraDamage = baseDamage / 10 * skillLevel;
		return extraDamage;
	}

	public void EnergyRecovery(Character fighter, float recoveryRate, int skillLevel){
		float energyToRecover = fighter.MaxEnergy / 10 * skillLevel;
		fighter.CurrentEnergy = fighter.CurrentEnergy + energyToRecover;
	}
}
