using UnityEngine;
using System.Collections;

public class Player_Warp : MonoBehaviour {
	
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "House 1-1") {
			//Application.LoadLevel ("House1-1Scene");
			this.transform.position = new Vector3 (95, 1, -115);
			Variable.prevScene = Variable.scene;
			Variable.scene = 1;
		} else if (col.gameObject.tag == "House 1-2") {
			//Application.LoadLevel ("House1-2Scene");
			this.transform.position = new Vector3 (85, 1, 145);
			Variable.prevScene = Variable.scene;
			Variable.scene = 2;
		} else if (col.gameObject.tag == "Hall1") {

			Variable.prevScene = Variable.scene;
			Variable.scene = 0;
			if (Variable.prevScene == 2) {
				this.transform.position = new Vector3 (65, 1, 25);
			} else if (Variable.prevScene == 1)
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
		} else if (col.gameObject.tag == "BlackMarket" && Variable.timer<=60*30*9){
			Variable.prevScene = Variable.scene;
			Variable.scene = -1;
			if (Variable.prevScene == 0)
				this.transform.position = new Vector3 (285, 1, -5);
		}
	}

}
