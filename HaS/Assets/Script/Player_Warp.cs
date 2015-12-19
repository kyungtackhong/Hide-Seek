using UnityEngine;
using System.Collections;

public class Player_Warp : MonoBehaviour {
	
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "106") {
			//Application.LoadLevel ("House1-1Scene");
			Variable.prevScene = Variable.scene;
			Variable.scene = 6;
			if(Variable.prevScene == 0)
				this.transform.position = new Vector3 (95, 1, -115);
			else if(Variable.prevScene == 5)
				this.transform.position = new Vector3 (55, 1, -185);
		} else if (col.gameObject.tag == "105") {
			//Application.LoadLevel ("House1-1Scene");
			Variable.prevScene = Variable.scene;
			Variable.scene = 5;
			if(Variable.prevScene == 0)
				this.transform.position = new Vector3 (-55, 1, -115);
			else if(Variable.prevScene == 6)
				this.transform.position = new Vector3 (-25, 1, -185);
		}else if (col.gameObject.tag == "101") {
			//Application.LoadLevel ("House1-2Scene");
			Variable.prevScene = Variable.scene;
			Variable.scene = 1;
			if(Variable.prevScene == 0)
				this.transform.position = new Vector3 (85, 1, 145);
			else if(Variable.prevScene == 2)
				this.transform.position = new Vector3 (55, 1, 215);
		} else if (col.gameObject.tag == "102") {
			//Application.LoadLevel ("House1-2Scene");
			Variable.prevScene = Variable.scene;
			Variable.scene = 2;
			if(Variable.prevScene == 0)
				this.transform.position = new Vector3 (-65, 1, 145);
			else if(Variable.prevScene == 1)
				this.transform.position = new Vector3 (-25, 1, 215);
		} else if (col.gameObject.tag == "Hall1") {
			Variable.prevScene = Variable.scene;
			Variable.scene = 0;
			if (Variable.prevScene == 1) 
				this.transform.position = new Vector3 (65, 1, 25);
			else if(Variable.prevScene == 2) 
				this.transform.position = new Vector3 (-15, 1, 25);
			else if(Variable.prevScene==5)
				this.transform.position = new Vector3 (-15, 1, 5);
			else if (Variable.prevScene == 6)
				this.transform.position = new Vector3 (65, 1, 5);
			else if (Variable.prevScene == 10)
				this.transform.position = new Vector3 (110, 1, -25);
			else if (Variable.prevScene == -1)
				this.transform.position = new Vector3 (125, 1, -25);
		} else if (col.gameObject.tag == "Hall2") {
			Variable.prevScene = Variable.scene;
			Variable.scene = 10;
			if (Variable.prevScene == 0)
				this.transform.position = new Vector3 (-320, 1, -25);
			else if (Variable.prevScene == 20)
				this.transform.position = new Vector3 (-340, 1, -25);
		} else if (col.gameObject.tag == "Hall3") {
			Variable.prevScene = Variable.scene;
			Variable.scene = 20;
			if (Variable.prevScene == 10)
				this.transform.position = new Vector3 (-720, 1, -25);
			else if(Variable.prevScene==30)
				this.transform.position = new Vector3(-740,1,-25);
		} else if (col.gameObject.tag == "BlackMarket" && Variable.timer <= 60 * 30 * 9) {
			Variable.prevScene = Variable.scene;
			Variable.scene = -1;
			if (Variable.prevScene == 0)
				this.transform.position = new Vector3 (285, 1, -5);
		} else if (col.gameObject.tag == "401") {
			Variable.prevScene = Variable.scene;
			Variable.scene = 30;
			if(Variable.prevScene==20)
				this.transform.position = new Vector3(-1085,1,-50);
		}
	}

}
