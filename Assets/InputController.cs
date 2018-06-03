using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	public bool pauseGame = false;
	public GameController GameController;
	public string pauseButton;
	public string speedUpButton;
	public string speedDownButton;
	public bool inFight = false;

	void Start(){
		GameController = GameObject.Find ("GameController").GetComponent<GameController> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (pauseButton)) {
			pauseGame = !pauseGame;
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
	}
}
