using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {

	public int test;
	public void OnMenuStartButtonClicked(){
		Application.LoadLevel ("startMenu");
	}
	public void OnMenuExitButtonClicked(){
		Application.Quit ();
	}
	public void OnStartGoButtonClicked(){
		Application.LoadLevel ("Floor1Scene");
	}
}
