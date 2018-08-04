using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPanelFighter : MonoBehaviour {

	public Character fighter;

	public Text FighterName;
	public Text Level;
	public Text Race;
	public Text Class;
	public Text health;
	public Text maxHealth;
	public Text energy;
	public Text maxEnergy;
	public Text teamName;
	public Image healthBar;
	public Image energyBar;
    public Image cooldownImage;
	public bool extraDamageModifier = false;

	public void UpdatePanelInfo(){
		FighterName.text = fighter.FirstName + " " + fighter.LastName;
		Level.text = fighter.Level.ToString ();
		Race.text = fighter.Race.RaceName;
		Class.text = fighter.MyClass.Name;
		maxHealth.text = fighter.MaxHealth.ToString();
		maxEnergy.text = fighter.MaxEnergy.ToString();
		teamName.text = fighter.Team.TeamName;
		if (fighter.Team.PlayerTeam) {
			teamName.fontStyle = FontStyle.Bold;
			gameObject.GetComponent<Image> ().color = new Color (0.4f, 0.6f, 1f, 0.5f);
		}
	}

	// Update is called once per frame
	void Update () {
		health.text = fighter.CurrentHealth.ToString();
		energy.text = fighter.CurrentEnergy.ToString();
		if (healthBar != null) {
			healthBar.fillAmount = fighter.CurrentHealth / fighter.MaxHealth;
		}
		if (energyBar != null) {
			energyBar.fillAmount = fighter.CurrentEnergy / fighter.MaxEnergy;
		}
        if (cooldownImage.isActiveAndEnabled)
        {
            cooldownImage.fillAmount = fighter.CurrentCooldown / fighter.FullCooldown;
        }

    }

	public void UpdateHealthAndEnergy(){
		/*health.text = fighter.CurrentHealth.ToString();
		energy.text = fighter.CurrentEnergy.ToString();
		if (healthBar != null) {
			healthBar.fillAmount = fighter.CurrentHealth / fighter.MaxHealth;
		}
		if (energyBar != null) {
			energyBar.fillAmount = fighter.CurrentEnergy / fighter.MaxEnergy;
		}*/
	}

	public void SetPanelToDefaultColour (){
		gameObject.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 0.5f);
	}
}
