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
	private int step = 4;
	private int count = 0;
	private NavMeshAgent agent;
	private AudioSource _audio;
	public AudioClip _walk;
	public AudioClip _toilet;
	public SpriteRenderer toilet;
	public Sprite toiletOpen;
	public Sprite toiletClose;

	// Use this for initialization
	void Start () {
		trace = GetComponent<Enemy_Trace> ();
		col = GetComponent<SphereCollider> ();
		agent = GetComponent<NavMeshAgent> ();
		_audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!trace.playerInSight && !trace.playerInSound) {
			if(step == 1 || step == 3)
			{
				if(!_audio.isPlaying)
				{
					_audio.loop=true;
					_audio.Play ();
				}
				++sprCount;
			}
			if(Variable.timer%4800==0 && step ==4)
			{
				col.radius = 2;
				step = 1;
				agent.destination = new Vector3(-15,1,-25);
				Debug.Log (step);
			}
			else if(step==1 && transform.position == new Vector3(-15,1,-25))
			{
				step = 2;
				Debug.Log (step);
				_audio.Stop ();
				_audio.clip = _toilet;
				toilet.sprite=toiletOpen;
				_audio.loop = false;
				_audio.Play();
			}
			else if(step==2)
			{
				++count;
				if(count==200)
				{
					_audio.clip=_walk;
					step = 3;
					agent.destination = new Vector3(-5,1,25);
					Debug.Log (step);
					count =0;
					toilet.sprite=toiletClose;
				}
			}
			else if(step == 3 && transform.position == new Vector3(-5,1,25))
			{
				step = 4;
				_audio.Stop ();
				Debug.Log (step);
			}
			else if(step==4)
			{
				col.radius=1;
				transform.eulerAngles = new Vector3(0,0,0);
			}
			else if(step == 5)
			{
				_audio.Stop();
				++count;
				if(count==180)
				{
					step = 3;
					agent.destination = new Vector3(-5,1,25);
					count = 0;
				}
			}
		} else {
			if(!_audio.isPlaying)
			{
				_audio.loop=true;
				_audio.Play ();
			}
			step = 5;
			count = 0;
			col.radius=3;
			++sprCount;
		}
		Vector3 ang = enemy.transform.eulerAngles;
		ang.y = 0;
		ang.z = 0;
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
}
