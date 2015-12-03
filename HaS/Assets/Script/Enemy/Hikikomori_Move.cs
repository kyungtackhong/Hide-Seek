using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hikikomori_Move : MonoBehaviour {
	
	public Enemy_Trace trace;
	public GameObject enemy;
	public SpriteRenderer spr;
	public Sprite[] upSpr = new Sprite[5];
	public Sprite[] downSpr = new Sprite[5];
	public Sprite[] sideSpr = new Sprite[5];
	private SphereCollider col;  
	private int sprCount = 0;
	private int type=0;
	
	
	// Use this for initialization
	void Start () {
		trace = GetComponent<Enemy_Trace> ();
		col = GetComponent<SphereCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!trace.playerInSight) {
			if(Variable.timer%1800>=600)
			{
				Vector3 ang = enemy.transform.eulerAngles;
				ang.x = 0;
				ang.y = 0;
				ang.z = 0;
				enemy.transform.eulerAngles=ang;
				col.radius=1;
			}
		} else {
			col.radius=4;
			++sprCount;
		}
		if (transform.eulerAngles.y >= 270) {
			type = 3;
			enemy.transform.localScale=new Vector3(-1.5f,1.5f,3);
		} 
		else if (transform.eulerAngles.y >= 180) {
			type = 2;
			enemy.transform.localScale=new Vector3(1.5f,1.5f,3);
		} 
		else if (transform.eulerAngles.y >= 90) {
			type = 1;
			enemy.transform.localScale=new Vector3(1.5f,1.5f,3);
		} 
		else {
			type = 0;
			enemy.transform.localScale=new Vector3(1.5f,1.5f,3);
		}
		if (type == 0) 
			spr.sprite = upSpr [(sprCount / 10) % 5];
		else if (type == 2)
			spr.sprite = downSpr [(sprCount / 10) % 5];
		else
			spr.sprite = sideSpr [(sprCount / 10) % 5];
	}
}
