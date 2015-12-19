using UnityEngine;
using System.Collections;

public class Player_Collision : MonoBehaviour {

	public bool soundSw=false;
	public AudioSource _audio;
	public AudioClip _kung;

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag=="Object" || col.gameObject.tag=="Wall" || col.gameObject.tag=="Hide")
		{
			Debug.Log ("!!");
			soundSw=true;
			_audio.clip=_kung;
			_audio.Play ();
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
}
