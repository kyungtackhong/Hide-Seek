using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetValue : MonoBehaviour {
	public InputField nameField;
	public static string nameText="";
	public Text textField;

	void Start(){
		//DontDestroyOnLoad (gameObject);

	}
	void Update(){
		if (textField != null) {
			textField.text = nameText;
		}
	}

	void getName(){
		nameText = nameField.text;

	}
	void setName(){
		textField.text = nameText;
	}
}


	




