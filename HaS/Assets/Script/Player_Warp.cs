using UnityEngine;
using System.Collections;

public class Player_Warp : MonoBehaviour {

	void Start()
	{
		Debug.Log ("prevscene"+Variable.prevScene);
		Debug.Log ("scene"+Variable.scene);
		if(Variable.scene==0)
		{
			if(Variable.prevScene == 10)
				this.transform.position = new Vector3(-120,1,175);
		}
		if(Variable.scene==10)
		{
			if(Variable.prevScene == 20)
				this.transform.position = new Vector3(-120,1,175);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag=="House 1-1")
		{
			//Application.LoadLevel ("House1-1Scene");
			this.transform.position = new Vector3(-185,1,25);
			Variable.prevScene=Variable.scene;
			Variable.scene = 1;
		}
		else if(col.gameObject.tag=="House 1-2")
		{
			//Application.LoadLevel ("House1-2Scene");
			this.transform.position = new Vector3(-145,1,345);
			Variable.prevScene=Variable.scene;
			Variable.scene = 2;
		}
		else if(col.gameObject.tag=="Hall1")
		{

			Variable.prevScene=Variable.scene;
			Variable.scene = 0;
			if(Variable.prevScene==2)
			{
				GameObject.Find ("/Hall1/Doors/door1/Lock1").SetActive(false);
				this.transform.position = new Vector3(-165,1,225);
			}
			else if(Variable.prevScene==1)
				this.transform.position = new Vector3(-165,1,205);
			else if(Variable.prevScene==10)
				Application.LoadLevel ("Floor1Scene");
		}
		else if(col.gameObject.tag=="Hall2")
		{
			Variable.prevScene=Variable.scene;
			Variable.scene = 10;
			Application.LoadLevel("Floor2Scene");
		}
		else if(col.gameObject.tag=="Hall3")
		{
			Variable.prevScene=Variable.scene;
			Variable.scene = 20;
			Application.LoadLevel("Floor3Scene");
		}
	}

}
