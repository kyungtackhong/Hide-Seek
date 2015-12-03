using UnityEngine;
using System.Collections;

public class Player_Collision : MonoBehaviour {

	public bool soundSw=false;
	private Player com;

	void Start()
	{
		com = GetComponent<Player>();
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag=="Object" || col.gameObject.tag=="Wall")
		{
			soundSw=true;
		}
	}

	void OnCollisionStay(Collision col)
	{
		if(col.gameObject.tag=="Object" || col.gameObject.tag=="Wall")
		{
			soundSw=false;
		}
	}

	void OnCollisionExit(Collision col)
	{
		if(col.gameObject.tag=="Object" || col.gameObject.tag=="Wall")
		{
			soundSw=false;
		}
	}

	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.tag=="food" && com.foodSw == true)
		{
			Destroy (col.gameObject);
			Variable.appetite-=5;
			if(Variable.appetite<0)
				Variable.appetite=0;
		}
	}
}
