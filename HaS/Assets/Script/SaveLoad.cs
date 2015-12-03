using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;


public class SaveLoad : MonoBehaviour {
	public static Player current; 
	
	public static int scene;
	public Text test;
	public static List<Player> savedGames = new List<Player>();
	
	public static string[] info = new string[10];
	
	public string[] getInfo(){
		return info;
	}
	
	
	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.dataPath+"/SavedGame.gd");
		bf.Serialize (file, info);
		file.Close ();
	}
	
	public void Load(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.dataPath+"/SavedGame.gd",FileMode.Open);
		info = (string[])bf.Deserialize (file);
		file.Close();
		Application.LoadLevel(int.Parse(info[4]));
		//Debug.Log ("Deserial" + info[0] + " " + info[1] + " " + info[2]);
		//Debug.Log (f[0] +"" + f[1] +""+f[2]);
	}
	
	public void Return(){
		Application.LoadLevel(scene);
	}
	
	// Use this for initialization
	void Start () {
		
		//DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
}
