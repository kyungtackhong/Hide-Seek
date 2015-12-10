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
		if(col.gameObject.tag=="Object" || col.gameObject.tag=="Wall" || col.gameObject.tag=="Hide")
		{
			soundSw=true;
		}
	}

	void OnCollisionStay(Collision col)
	{
		if(col.gameObject.tag=="Object" || col.gameObject.tag=="Wall" || col.gameObject.tag=="Hide")
		{
			soundSw=false;
		}
	}

	void OnCollisionExit(Collision col)
	{
		if(col.gameObject.tag=="Object" || col.gameObject.tag=="Wall" || col.gameObject.tag=="Hide")
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
		if (col.gameObject.layer==9 && com.foodSw == true)
		{
			Destroy (col.gameObject);
			inventory.addItem (col.gameObject.name);
			if(col.gameObject.name == "ring")
			{
				Variable.state=0;
				Variable.questSw=true;
			}
		}
	}
}
