using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy_Move : MonoBehaviour {

	public float fieldOfViewAngle = 110f;
	public bool playerInSight;
	public Vector3 targetPos;
	public GameObject enemy;
	public SpriteRenderer spr;
	public Sprite[] upSpr = new Sprite[5];
	public Sprite[] downSpr = new Sprite[5];
	public Sprite[] sideSpr = new Sprite[5];
	public GameObject player;
	private NavMeshAgent agent;
	private SphereCollider col;  
	private int sprCount = 0;
	private int type=0;


	// Use this for initialization
	void Start () {
		targetPos = new Vector3(0,2,0);
		agent = GetComponent<NavMeshAgent> ();
		col = GetComponent<SphereCollider> ();
	}

	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(player.transform.position,transform.position)<player.GetComponent<Player>()._light.GetComponent<SphereCollider>().radius*4)
		{
			enemy.SetActive(true);
		}
		else
		{
			enemy.SetActive(false);
		}
		Vector3 pos = this.transform.position;
		pos.y = 1;
		transform.position = pos;
		if (!playerInSight) {
			col.radius=3;
			transform.Rotate (0, 1f, 0);
		} else {
			col.radius=6;
			++sprCount;
		}
		Vector3 ang = enemy.transform.eulerAngles;
		ang.y = 0;
		ang.z=0;
		enemy.transform.eulerAngles=ang;
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

	void OnTriggerStay (Collider other)
	{
		// If the player has entered the trigger sphere...
		if(other.gameObject.tag == "Player")
		{
			// By default the player is not in sight.
			playerInSight = false;
			
			// Create a vector from the enemy to the player and store the angle between it and forward.
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);

			// If the angle between forward and where the player is, is less than half the angle of view...
			if(angle < fieldOfViewAngle * 0.5f)
			{
				RaycastHit hit;
				// ... and if a raycast towards the player hits something...
				Physics.Raycast(transform.position + new Vector3(0,0.5f,0), direction.normalized, out hit, Mathf.Infinity,1<<8);
				Debug.DrawLine(transform.position,hit.point);
				if(Physics.Raycast(transform.position + new Vector3(0,0.5f,0), direction.normalized, out hit, Mathf.Infinity,1<<8))
				{
					// ... and if the raycast hits the player...
					if(hit.collider.gameObject.tag == "Player")
					{
						// ... the player is in sight.
						playerInSight = true;
						// Set the last global sighting is the players current position.
						targetPos = player.transform.position;
						agent.destination=targetPos;
						return;
					}
				}
			}
			if(player.GetComponent<Player>().runSw==true || player.GetComponent<Player_Collision>().soundSw==true)
			{
				targetPos = player.transform.position;
				agent.destination=targetPos;
			}
		}
	}

	void OnTriggerExit (Collider other)
	{
		// If the player leaves the trigger zone...
		if (other.gameObject.tag == "Player") 
		{
			playerInSight = false;
		}
	}
}
