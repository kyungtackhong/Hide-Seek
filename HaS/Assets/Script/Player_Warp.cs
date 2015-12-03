using UnityEngine;
using System.Collections;

public class Player_Warp : MonoBehaviour {

	void Start()
	{
		if(Variable.scene==0)
		{
			if(Variable.prevScene==1)
			{
				this.transform.position = new Vector3(-165,1,205);
			}
			if(Variable.prevScene == 10)
			{
				this.transform.position = new Vector3(-120,1,175);
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag=="House 1-1")
		{
			Application.LoadLevel ("House1-1Scene");
			Variable.prevScene=Variable.scene;
			Variable.scene = 1;
		}
		else if(col.gameObject.tag=="Hall1")
		{
			Application.LoadLevel ("Hall1Scene");
			Variable.prevScene=Variable.scene;
			Variable.scene = 0;
		}
		else if(col.gameObject.tag=="Hall2")
		{
			Application.LoadLevel("Hall2Scene");
			Variable.prevScene=Variable.scene;
			Variable.scene = 10;
		}
	}

}
