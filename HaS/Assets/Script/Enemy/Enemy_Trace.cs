using UnityEngine;
using System.Collections;

public class Enemy_Trace : MonoBehaviour {

	public float fieldOfViewAngle;
	public bool playerInSight = false;
	public bool playerInSound = false;
	public bool downSw = false;
	public bool poopSw = false;
	public GameObject poop = null;
	public int downtimer = 0;
	public Vector3 targetSightPos;
	public Vector3 targetSoundPos;
	public GameObject enemy;
	public GameObject player;

	// Update is called once per frame
	void Update () {
		if (downSw == true) {
			--downtimer;
			if (downtimer == 0)
				downSw = false;
		}
		if(Vector3.Distance(player.transform.position,transform.position)<player.GetComponent<Player>()._light.GetComponent<Light>().spotAngle/10*4)
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
		if (Vector3.Distance (transform.position, player.transform.position) > 100) 
			GetComponent<AudioSource> ().mute = true;
		else
			GetComponent<AudioSource> ().mute = false;
	}


	void OnTriggerStay (Collider other)
	{
		// If the player has entered the trigger sphere...
		if(other.gameObject.tag == "Player" && player.GetComponent<Player>().hide_flag==false)
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
						if(Vector3.Distance (transform.position,player.transform.position)<50)
							targetSightPos = player.transform.position;
						//agent.destination=targetPos;
						return;
					}
					else
						playerInSight=false;
				}
				else
					playerInSight=false;
			}
			else
				playerInSight=false;
			if(player.GetComponent<Player>().runSw==true || player.GetComponent<Player_Collision>().soundSw==true)
			{
				if(Vector3.Distance (transform.position,player.transform.position)<50)
					targetSoundPos = player.transform.position;
				//agent.destination=targetPos;
				playerInSound = true;
				return;
			}
			else
			{
				playerInSound = false;
			}
		}
		if(other.gameObject.tag == "poop" && poop==null)
		{
			poopSw=true;
			poop=other.gameObject;
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		// If the player leaves the trigger zone...
		if (other.gameObject.tag == "Player") 
		{
			playerInSight=false;
			playerInSound=false;
		}
	}

}
