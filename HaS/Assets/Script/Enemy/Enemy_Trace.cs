using UnityEngine;
using System.Collections;

public class Enemy_Trace : MonoBehaviour {

	public float fieldOfViewAngle;
	public bool playerInSight = false;
	public bool playerInSound = false;
	public Vector3 targetPos;
	public GameObject enemy;
	public GameObject player;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		targetPos = new Vector3(0,2,0);
		agent = GetComponent<NavMeshAgent> ();
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
		if(playerInSound == true && transform.position == targetPos)
		{
			playerInSound = false;
		}
		else if(playerInSight == true && transform.position == targetPos)
		{
			playerInSight = false;
		}
	}


	void OnTriggerStay (Collider other)
	{
		// If the player has entered the trigger sphere...
		if(other.gameObject.tag == "Player")
		{
			// By default the player is not in sight.

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
				playerInSound = true;
			}
		}
	}
	
	/*void OnTriggerExit (Collider other)
	{
		// If the player leaves the trigger zone...
		if (other.gameObject.tag == "Player") 
		{
			playerInSight = false;
		}
	}*/

}
