using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DebugOpinionMenu : MonoBehaviour {
	public GameObject TextBox;
	public GameObject Character1;
	public GameObject Character2;
	private Text Text;
	public GameController GameController;
	public int ID1;
	public int ID2;

	public void Start(){
		GameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		Text = TextBox.GetComponent<Text>();
	}

	public void UpdateText(){
		Text.text = 0.ToString();
		ID1 = Character1.GetComponent<Dropdown>().value;
		ID2 = Character2.GetComponent<Dropdown>().value;
		//int intID1 = int.Parse (ID1);
		//int intID2 = int.Parse (ID2);
		Opinion Opinion = GameController.OpinionManager;

		Text.text = Opinion.OpinionList [ID1, ID2].ToString ();
	}
}
