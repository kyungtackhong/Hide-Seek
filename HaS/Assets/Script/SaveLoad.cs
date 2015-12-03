using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;


public class SaveLoad : MonoBehaviour {
	public static Player current; 

	public Text test;
	public static List<Player> savedGames = new List<Player>();

	public static string[] info = new string[7];

	public string[] getInfo(){
		return info;
	}


	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.dataPath+"/Save/SavedGame.gd");
		bf.Serialize (file, info);
		file.Close ();
	}

	public void Load(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.dataPath+"/Save/SavedGame.gd",FileMode.Open);
		info = (string[])bf.Deserialize (file);
		file.Close();

		Debug.Log ("Deserial" + info[0] + " " + info[1] + " " + info[2]);
	
	}

	public void Return(){
		Application.LoadLevel("scene");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

}
