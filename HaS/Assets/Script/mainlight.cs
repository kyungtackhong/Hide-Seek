using UnityEngine;
using System.Collections;

public class mainlight : MonoBehaviour {
	public static Light lt;
	// Use this for initialization
	void Start () {
		lt=GetComponent<Light>();
		lt.type = LightType.Spot;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public static void change_range(int i)
	{
		lt.spotAngle = i;
	}
}
