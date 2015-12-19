using UnityEngine;
using System.Collections;

public class Spy_Move : MonoBehaviour {

	public Enemy_Trace trace;
	public GameObject enemy;
	public SpriteRenderer spr;
	public Sprite[] upSpr = new Sprite[5];
	public Sprite[] downSpr = new Sprite[5];
	public Sprite[] sideSpr = new Sprite[5];
	private SphereCollider col;  
	private int sprCount = 0;
	private int type=0;
	private int count=0;
	private int step=0; // 0 - 티비보기, 1 - 냉장고 옆 소파, 2 - 명상, 3 - 화장실로, 4 - 배설, 5 - 컴퓨터 방
						// 6 - TV로, 7 - 침대로, 8 - 자기, 9 - 집에서 뭔가 발견, 10 - 집에서 소리 들림, 11 - 똥치우러
	private int computerStep=0;
	private Vector3 targetPos;
	private NavMeshAgent _agent;
	private AudioSource _audio;
	public AudioClip _walk;
	public AudioClip _toilet;
	public AudioClip _snore;
	private bool isWalk = false;

	// Use this for initialization
	void Start () {
		trace = GetComponent<Enemy_Trace> ();
		col = GetComponent<SphereCollider> ();
		_agent = GetComponent<NavMeshAgent> ();
		_audio = GetComponent<AudioSource> ();
	}
	
	void Update () {
		if (Variable.state != 1 || trace.downSw==true)
			return;
		Vector3 pos = transform.position;
		pos.y = 1;
		transform.position = pos;
		if (trace.playerInSight == true && step != 8) {
			_agent.destination = trace.targetSightPos;
			_audio.clip = _walk;
			step = 9;
			col.radius = 6;
			isWalk = true;
		} else if (trace.playerInSound == true && step != 8) {
			_agent.destination = trace.targetSoundPos;
			_audio.clip = _walk;
			step = 10;
			col.radius = 6;
			isWalk = true;
		} else if (trace.poopSw == true && step != 8 && step != 9 && step != 10) {
			_agent.destination=trace.poop.transform.position;
			_audio.clip = _walk;
			step = 11;
			isWalk=true;
		}
		else if (Variable.timer >= 60 * 30 * 20 && step == 8) {
			count = 0;
			targetPos = new Vector3 (35 - 60, 1, -25 - 150);
			_agent.destination = targetPos;
			isWalk = true;
			step = 3;
			_audio.clip=_walk;
			_audio.Stop ();
		}
		else if (Variable.timer >= 60 * 30 * 13 && Variable.timer<=60*30*20 && step == 0) {
			step = 7;
			targetPos = new Vector3 (-35 - 60, 1, 8 - 150);
			_agent.destination = targetPos;
			isWalk = true;
			count = 0;
		} else if (Variable.timer == 60 * 30 * 12 && Variable.timer<=60*30*13 && step == 0) {
			count = 0;
			targetPos = new Vector3 (35 - 60, 1, -25 - 150);
			_agent.destination = targetPos;
			isWalk = true;
			step = 3;
		} else if (Variable.timer >= 60 * 30 * 9 && Variable.timer<=60*30*10 && step == 0) {
			step = 5;
			isWalk = true;
			computerStep = 0;
		} else if (Variable.timer >= 60 * 30 * 6  && Variable.timer<=60*30*7 && step == 0) {
			step = 1;
			targetPos = new Vector3 (35 - 60, 1, 15 - 150);
			_agent.destination = targetPos;
			isWalk = true;
		} else if (Variable.timer >= 60 * 30 * 1 && Variable.timer<=60*30*2 && step == 0) {
			step = 5;
			isWalk=true;
			computerStep=0;
		}
		if (step == 0) {
			transform.eulerAngles = new Vector3 (0, 0, 0);
		} else if (step == 1 && Vector3.Distance (transform.position, targetPos) < 0.1f) { // 냉장고 옆 소파로
			step = 2;
			count = 0;
			isWalk = false;
		} else if (step == 2) { // 명상
			++count;
			transform.eulerAngles = new Vector3 (0, -90, 0);
			if (count == 90 * 30) { // 90*30
				count = 0;
				targetPos = new Vector3 (35 - 60, 1, -25 - 150);
				_agent.destination = targetPos;
				isWalk = true;
				step = 3;
			}
		} else if (step == 3 && Vector3.Distance (transform.position, targetPos) < 0.1f) { // 화장실로
			count = 0;
			step = 4;
			_audio.Stop ();
			_audio.clip = _toilet;
			_audio.loop = false;
			_audio.Play();
		} else if (step == 4) { // 배설
			transform.eulerAngles = new Vector3 (0, -90, 0);
			++count;
			if (count == 240) {
				_audio.Stop ();
				_audio.clip = _walk;
				_audio.loop = true;
				_audio.Play();
				count = 0;
				targetPos = new Vector3 (-15 - 60, 1, -25 - 150);
				_agent.destination = targetPos;
				isWalk = true;
				step = 6;
			}
		} else if (step == 5) { // 컴퓨터 방
			ComputerRoom ();
		} else if (step == 6 && Vector3.Distance (transform.position, targetPos) < 0.1f) { // TV로
			step = 0;
			isWalk = false;
		} else if (step == 7 && Vector3.Distance (transform.position, targetPos) < 0.1f) { // 자러
			step = 8;
			_audio.clip = _snore;
			_audio.loop = true;
			_audio.Stop();
			_audio.Play ();
			count = 0;
		}else if (step == 9 && Vector3.Distance (transform.position, trace.targetSightPos) < 3) { // 집에서 뭔가 발견
			targetPos = new Vector3 (-15 - 60, 1, -25 - 150);
			_agent.destination = targetPos;
			step = 6;
			col.radius=2;
			isWalk = true;
		}else if (step == 10 && Vector3.Distance (transform.position, trace.targetSoundPos) < 3) { // 집에서 소리 들림
			targetPos = new Vector3 (-15 - 60, 1, -25 - 150);
			_agent.destination = targetPos;
			step = 6;
			col.radius=2;
			isWalk = true;
		}else if(step==11 && Vector3.Distance(transform.position,trace.poop.transform.position)<5)
		{
			Destroy (trace.poop.gameObject);
			step = 6;
			trace.poopSw=false;
			targetPos = new Vector3 (-15 - 60, 1, -25 - 150);
			_agent.destination = targetPos;
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
		if (isWalk == true) {
			if(_audio.clip == _walk)
				++sprCount;
			if (_audio.isPlaying == false)
				_audio.Play ();
		} 
		else{
			if (_audio.isPlaying)
				_audio.Stop ();
		}
		if (type == 0) 
			spr.sprite = upSpr [(sprCount / 10) % 5];
		else if (type == 2)
			spr.sprite = downSpr [(sprCount / 10) % 5];
		else
			spr.sprite = sideSpr [(sprCount / 10) % 5];
	}
	void ComputerRoom()
	{
		if (computerStep == 0) { // 컴퓨터 방 서재1로
			targetPos = new Vector3 (-15 - 60, 1, 5 - 150);
			_agent.destination = targetPos;
			isWalk = true;
			computerStep = 1;
		} else if (computerStep == 1 && Vector3.Distance (transform.position, targetPos)<0.1f) { //서재 1 검사
			count = 0;
			computerStep = 2;
			isWalk = false;
			transform.eulerAngles=new Vector3(0,-90,0);
		} else if (computerStep == 2) { // 서재 1 검사
			++count;
			if (count == 180) {
				targetPos = new Vector3 (-15 - 60, 1, 15 - 150);
				_agent.destination = targetPos;
				isWalk = true;
				computerStep = 3;
			}
		} else if (computerStep == 3 && Vector3.Distance (transform.position, targetPos)<0.1f) { //서재 2 검사
			count = 0;
			computerStep = 4;
			isWalk = false;
			transform.eulerAngles=new Vector3(0,-90,0);
		} else if (computerStep == 4) { // 서재 2 검사
			++count;
			if (count == 180) {
				targetPos = new Vector3 (-15 - 60, 1, 25 - 150);
				_agent.destination = targetPos;
				isWalk = true;
				computerStep = 5;
			}
		} else if (computerStep == 5 && Vector3.Distance (transform.position, targetPos)<0.1f) { //서재 3 검사
			count = 0;
			computerStep = 6;
			isWalk = false;
			transform.eulerAngles=new Vector3(0,-90,0);
		} else if (computerStep == 6) { // 서재 3 검사
			++count;
			if (count == 180) {
				targetPos = new Vector3 (-15 - 60, 1, 35 - 150);
				_agent.destination = targetPos;
				isWalk = true;
				computerStep = 7;
			}
		} else if (computerStep == 7 && Vector3.Distance (transform.position, targetPos)<0.1f) { //서재 4 검사
			count = 0;
			computerStep = 8;
			isWalk = false;
			transform.eulerAngles=new Vector3(0,-90,0);
		} else if (computerStep == 8) { // 서재 4 검사
			++count;
			if (count == 180) {
				targetPos = new Vector3 (-5 - 60, 1, 25 - 150);
				_agent.destination = targetPos;
				isWalk = true;
				computerStep = 9;
			}
		} else if (computerStep == 9 && Vector3.Distance (transform.position, targetPos)<0.1f) { //컴퓨터
			count = 0;
			computerStep = 10;
			isWalk = false;
			transform.eulerAngles=new Vector3(0,0,0);
		} else if (computerStep == 10) { // 컴퓨터 중
			++count;
			if(count == 90*30)//90*30
			{
				count = 0;
				targetPos = new Vector3(-15-60,1,-25-150);
				_agent.destination=targetPos;
				isWalk = true;
				step = 6;
			}
		}
	}

	void OnTriggerStay(Collider coll)
	{
		
		if(coll.gameObject.tag=="Player" && Vector3.Distance(transform.position,coll.transform.position)<3)
		{
			Variable.spySw = true;
			Variable.state = 0;
		}
		
	}
}
