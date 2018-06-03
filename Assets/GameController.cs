using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameController : MonoBehaviour {

	public int DaysProgressed = 0;
	public int WeekCount = 1;
	public int TotalWeekCount = 1;
	public GameObject TeamSelectionBox;
	public System.Random GameRandom;
	public FightManager FightManager;
	public FighterDatabase CurrentLeagueFighterDatabase;
	public ActivityManager ActivityManager;
	public TeamManager TeamManager;
	public Date RealDate;
	public BookingManager BookingManager;
	public PlayerManager PlayerManager;
	public bool GameplayTickPause = true;
	public GameObject UIController;

	private bool TimeForTick;
	public int GameSpeed = 3;
	private float waitPerTickSpeedOne = 2f;
	private float waitPerTickSpeedTwo = 1f;
	private float waitPerTickSpeedThree = 0.5f;
	private float waitPerTickSpeedFour = 0.25f;
	private float waitPerTickSpeedFive = 0.1f;
	private float waitPerTick; //Seconds to wait per tick
	private float timeWaitedForTick;
	public bool fightMode = false;

	private InputController InputController;

	void Start(){
		InputController = GameObject.Find ("InputController").GetComponent<InputController> ();
		waitPerTick = waitPerTickSpeedThree; //Set the game speed to default
		SetUpGame ();
	}

	void Update(){
		if (InputController.pauseGame == false && TimeForTick == true && GameplayTickPause == false) {
			WorldMethods.DailyTick ();
			TimeForTick = false;
		}
		if (TimeForTick == false) {
			timeWaitedForTick += Time.deltaTime;
			if (timeWaitedForTick > waitPerTick) {
				TimeForTick = true;
				timeWaitedForTick = 0;
			}
		}
	}

	void SetUpGame()
	{
		GameRandom = new System.Random();

		FightManager = new FightManager();
		ActivityManager = new ActivityManager();
		CurrentLeagueFighterDatabase = new FighterDatabase();
		TeamManager = new TeamManager();
		BookingManager = new BookingManager();
		PlayerManager = new PlayerManager();

		RealDate = new Date();

		RealDate.Day = 1;
		RealDate.Month = 1;
		RealDate.Year = 1;

		WorldMethods.SetUpWorld();

		GameObject.Find ("FighterListContent").GetComponent<FighterListContent> ().SetUp (CurrentLeagueFighterDatabase.AllFighters);
		GameObject.Find ("TeamListContent").GetComponent<TeamListContent> ().SetUp (TeamManager.TeamsInLeague);
		GameObject.Find ("FightsPlannedListContent").GetComponent<FightPlanListContent> ().SetUp (BookingManager.Calendar);
	}

	public void ChangeGameSpeed(int newSpeed){
		switch (newSpeed) {
		case 1:
			waitPerTick = waitPerTickSpeedOne;
			GameSpeed = 1;
			break;
		case 2:
			waitPerTick = waitPerTickSpeedTwo;
			GameSpeed = 2;
			break;
		case 3:
			waitPerTick = waitPerTickSpeedThree;
			GameSpeed = 3;
			break;
		case 4:
			waitPerTick = waitPerTickSpeedFour;
			GameSpeed = 4;
			break;
		case 5:
			waitPerTick = waitPerTickSpeedFive;
			GameSpeed = 5;
			break;
		}
	}

	public void PauseTicks(){
		GameplayTickPause = true;
	}


	public void UnPauseTicks(){
		GameplayTickPause = false;
	}
}
