using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public int dayCount = 0;
	public int monthCount = 0;
	public int yearCount = 0;
	private Text DaysCounter;
	private Text MonthsCounter;
	private Text YearsCounter;
	private Text GameSpeedDisplay;
	private int GameSpeed;
	public Date RealDate;
	private GameController GameController;
	public GameObject FightPanel;
	public GameObject FighterSelectionBox;

	void Start(){
		GameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		GameSpeedDisplay = GameObject.Find ("GameSpeedDisplay").GetComponent<Text> ();
		DaysCounter = GameObject.Find ("DaysCounter").GetComponent<Text>();
		MonthsCounter = GameObject.Find ("MonthsCounter").GetComponent<Text>();
		YearsCounter = GameObject.Find ("YearsCounter").GetComponent<Text>();
	}

	void Update(){
		dayCount = RealDate.Day;
		monthCount = RealDate.Month;
		yearCount = RealDate.Year;
		GameSpeed = GameController.GameSpeed;
		DaysCounter.text = dayCount.ToString();
		MonthsCounter.text = monthCount.ToString();
		YearsCounter.text = yearCount.ToString();
		GameSpeedDisplay.text = GameSpeed.ToString ();
	}

	public void ActivateFightScreen(Character fighter1, Character fighter2, Fight1v1 fight){
		FightPanel.SetActive (true);
		FightPanel.GetComponent<FightPanel> ().SetUpFight (fighter1, fighter2, fight);
	}
	public void DeactivateFightScreen(){
		FightPanel.SetActive (false);
	}
}
