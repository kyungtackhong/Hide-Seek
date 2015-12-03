using UnityEngine;
using System.Collections;

public class Player_Warp : MonoBehaviour {

	void Start()
	{

	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag=="House 1-1")
		{
			Application.LoadLevel ("House1-1Scene");
		}
		else if(col.gameObject.tag=="Hall1")
		{
			Application.LoadLevel ("Hall1Scene");
		}
	}

}
