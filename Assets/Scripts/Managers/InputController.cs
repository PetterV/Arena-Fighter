using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	public bool pauseGame = false;
	public bool pauseFight = false;
	public GameController GameController;
	public string pauseButton;
	public string pauseFightButton;
	public string speedUpButton;
	public string speedDownButton;
	public string debugUnpauseButton;
	public bool inFight = false;
	public bool debugInput = false;

	void Start(){
		GameController = GameObject.Find ("GameController").GetComponent<GameController> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (pauseButton)) {
			pauseGame = !pauseGame;
		}

		if (Input.GetKeyDown (pauseFightButton)){
			pauseFight = !pauseFight;
		}

		if (Input.GetKeyDown (speedUpButton)) {
			if (GameController.GameSpeed < 5) {
				int newSpeed = GameController.GameSpeed + 1;
				GameController.ChangeGameSpeed (newSpeed);
			} 
			else {
				MonoBehaviour.print ("At max speed already");
			}
		}
		if (Input.GetKeyDown (speedDownButton)) {
			if (GameController.GameSpeed > 1) {
				int newSpeed = GameController.GameSpeed - 1;
				GameController.ChangeGameSpeed (newSpeed);
			} 
			else {
				MonoBehaviour.print ("At slowest speed already");
			}
		}

		//Debug inputs
		if (debugInput){
			//Debug Unpause
			if (Input.GetKeyUp(debugUnpauseButton)){
				GameController.UnPauseTicks ();
			}	
		}

	}
}
