using UnityEngine;
using System.Collections;

public class Guard_Move : MonoBehaviour {

	public Enemy_Trace trace;
	public GameObject enemy;
	public SpriteRenderer spr;
	public Sprite[] upSpr = new Sprite[5];
	public Sprite[] downSpr = new Sprite[5];
	public Sprite[] sideSpr = new Sprite[5];
//	private SphereCollider col;  
	private int sprCount = 0;
	private int type=0;
	private int count=0;
	private int step=0; // 0 - 티비보기, 1 - 자러가기, 2 - 자는중, 3- 기상, 냉장고로, 4 - 음식 꺼내기, 5 - 티비보러
						// 6 - 순찰 중, 7 - 쓰레기 정리 , 8 - 순찰 중 소리가 난곳으로, 10 - 집에서 소리 들림, 11 - 집에서 뭔가 보임
						// 12 - 수색
	private int detectStep = 0;
	private int patrolStep=0;
	private int trashStep=0;
	private Vector3 targetPos;
	private NavMeshAgent _agent;
	private AudioSource _audio;
	private bool isWalk = false;

	// Use this for initialization
	void Start () {
		trace = GetComponent<Enemy_Trace> ();
//		col = GetComponent<SphereCollider> ();
		_agent = GetComponent<NavMeshAgent> ();
		_audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	//오전 6시~오전 8시 생활공간
	//오전 8시~오후 4시 잠
	//오후 6시 출근 쓰레기 정리
	//오후 10시 순찰
	void Update () {
		if (Variable.state == 0)
			return;
		Vector3 pos = transform.position;
		pos.y = 1;
		transform.position = pos;
		if (trace.playerInSound == true && (step == 6 || step == 8)) { // 순찰 중 소리가 들림
			step = 8;
			_agent.destination = trace.targetSoundPos;
		} else if (trace.playerInSight == true && step == 6 && trace.player.GetComponent<Player> ().downSw == true) { // 순찰 중 쓰러진 꼬맹이 발견
			Debug.Log ("쓰러졌다!");
		} else if (trace.playerInSight == true && patrolStep == 0 && trashStep == 0 && step!=2) { // 집에서 플레이어 발견
			step = 11;
			_agent.destination=trace.targetSightPos;
			isWalk = true;
		}else if (trace.playerInSound == true && patrolStep == 0 && trashStep == 0 && step!=2 && step!=11) { // 집에서 소리 들림
			step = 10;
			_agent.destination = trace.targetSoundPos;
			isWalk = true;
		}
		else if(Variable.timer>=60*30*22 && step==0) // 오후 10시 순찰
		{
			step =6;
			patrolStep = 0;
		}
		else if(Variable.timer>=60*30*18 && Variable.timer<60*30*19 && step==0) // 오후 6시 쓰레기 정리
		{
			step = 7;
			trashStep = 0;
		}
		else if(Variable.timer>=60*30*8 && Variable.timer<60*30*16 && step==0) // 오전6시~오전 8시 생활공간
		{
			step=1;
			targetPos = new Vector3(-15+90,1,5-150);
			_agent.destination=targetPos;
			isWalk = true;
		}
		else if(Variable.timer>=60*30*16 && step==2) // 오전8시~오후4시 잠
		{
			step=3;
			transform.localPosition = new Vector3(-20,1,5);
			_agent.enabled=true;
			targetPos = new Vector3(25+90,1,-25-150);
			_agent.destination=targetPos;
			isWalk = true;
		}
		if (step == 1 && Vector3.Distance (transform.position, targetPos) < 0.1f) { // 1 - 자러가기
			isWalk = false;
			_agent.Stop ();
			_agent.enabled = false;
			transform.localPosition = new Vector3 (-20, 1, 15);
			transform.eulerAngles = new Vector3 (0, 90, 0);
			step = 2;
		} else if (step == 3 && Vector3.Distance (transform.position, targetPos) < 0.1f) { // 3 - 기상 , 냉장고로
			step = 4;
			count = 0;
			isWalk = false;
		} else if (step == 4) { // 4 - 음식 꺼내기
			++count;
			if (count == 30) {
				targetPos = new Vector3 (5 + 90, 1, 15 - 150);
				_agent.destination = targetPos;
				isWalk = true;
				step = 5;
			}
		} else if (step == 5 && Vector3.Distance (transform.position, targetPos) < 0.1f) { // 5 - 티비보러
			step = 0;
			isWalk = false;
		} else if (step == 6) { // 순찰
			Patrol ();
		} else if (step == 7) { // 쓰레기 정리
			Trash ();
		} else if (step == 8 && Vector3.Distance (transform.position, trace.targetSoundPos) < 1) { // 순찰 중 소리가 들림
			step = 6;
			_agent.destination = targetPos;
		} else if (step == 10 && Vector3.Distance (transform.position, trace.targetSoundPos) < 1) { // 집 안에서 소리 들림
			targetPos = new Vector3 (5 + 90, 1, 15 - 150);
			_agent.destination = targetPos;
			isWalk = true;
			step = 5;
		} else if (step == 11 & Vector3.Distance (transform.position, trace.targetSightPos) < 3) { // 집 안에서 뭔가 보임
			step = 12;
			detectStep=0;
		} else if(step==12)
		{
			Detect();
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

	void Patrol()
	{
		Vector3 pos = transform.position;
		pos.y = 1;
		transform.position = pos;
		if(patrolStep == 0) // 문으로 가기
		{
			targetPos = new Vector3 (5 + 90, 1, 35 - 150);
			_agent.destination = targetPos;
			isWalk = true;
			patrolStep=1;
		}
		else if(patrolStep == 1 && Vector3.Distance(transform.position,targetPos)<0.1f) // 밖으로 나가서 2층으로
		{
			_agent.Stop ();
			_agent.enabled=false;
			transform.localPosition = new Vector3(-25,1,155);
			_agent.enabled=true;
			targetPos=new Vector3(20+90,1,115-150);
			_agent.destination=targetPos;
			patrolStep = 2;
		}
		else if(patrolStep == 2 && Vector3.Distance(transform.position,targetPos)<0.1f) // 2층에서 3층으로
		{
			_agent.Stop ();
			_agent.enabled=false;
			transform.localPosition = new Vector3(-410,1,120);
			_agent.enabled=true;
			targetPos=new Vector3(-430+90,1,120-150);
			_agent.destination=targetPos;
			patrolStep = 3;
		}
		else if(patrolStep == 3 && Vector3.Distance(transform.position,targetPos)<0.1f) // 3층에서 복도 끝으로
		{
			_agent.Stop ();
			_agent.enabled=false;
			transform.localPosition = new Vector3(-810,1,120);
			_agent.enabled=true;
			targetPos=new Vector3(-1075+90,1,165-150);
			_agent.destination=targetPos;
			patrolStep = 4;
		}
		else if(patrolStep ==4 && Vector3.Distance(transform.position,targetPos)<0.1f) // 복도 끝에서 3층 계단으로
		{
			targetPos = new Vector3(-810+90,1,120-150);
			_agent.destination=targetPos;
			patrolStep = 5;
		}
		else if(patrolStep == 5 && Vector3.Distance(transform.position,targetPos)<0.1f) // 3층에서 2층 복도 끝으로
		{
			_agent.Stop ();
			_agent.enabled=false;
			transform.localPosition = new Vector3(-430,1,120);
			_agent.enabled=true;
			targetPos=new Vector3(-675+90,1,165-150);
			_agent.destination=targetPos;
			patrolStep = 6;
		}
		else if(patrolStep ==6 && Vector3.Distance(transform.position,targetPos)<0.1f) // 2층 복도 끝에서 2층 계단으로
		{
			targetPos = new Vector3(-410+90,1,120-150);
			_agent.destination=targetPos;
			patrolStep = 7;
		}
		else if(patrolStep == 7 && Vector3.Distance(transform.position,targetPos)<0.1f) // 2층 계단에서 1층 복도 끝으로
		{
			_agent.Stop ();
			_agent.enabled=false;
			transform.localPosition = new Vector3(20,1,120);
			_agent.enabled=true;
			targetPos=new Vector3(-225+90,1,165-150);
			_agent.destination=targetPos;
			patrolStep = 8;
		}
		else if(patrolStep == 8 && Vector3.Distance(transform.position,targetPos)<0.1f) // 1층 복도 끝에서 문으로
		{
			targetPos=new Vector3(-25+90,1,155-150);
			_agent.destination=targetPos;
			patrolStep = 9;
		}
		else if(patrolStep == 9 && Vector3.Distance(transform.position,targetPos)<0.1f)	// 집으로 들어가기
		{
			_agent.Stop ();
			_agent.enabled=false;
			transform.localPosition = new Vector3(5,1,35);
			_agent.enabled=true;
			targetPos=new Vector3(5+90,1,15-150);
			_agent.destination=targetPos;
			patrolStep = 10;
		}
		else if(patrolStep == 10 && Vector3.Distance(transform.position,targetPos)<0.1f) // 티비보러
		{
			isWalk=false;
			step = 0;
			patrolStep=0;
		}
	}

	void Trash() // 쓰레기 정리하러가기
	{
		Vector3 pos = transform.position;
		pos.y = 1;
		transform.position = pos;
		if(trashStep == 0) // 문으로 가기
		{
			targetPos = new Vector3 (5 + 90, 1, 35 - 150);
			_agent.destination = targetPos;
			isWalk = true;
			trashStep=1;
		}
		else if(trashStep==1 && Vector3.Distance(transform.position,targetPos)<0.1f) // 밖으로 나가기
		{
			_agent.Stop ();
			_agent.enabled=false;
			transform.localPosition = new Vector3(-25,1,155);
			_agent.enabled=true;
			targetPos=new Vector3(45+90,1,165-150);
			_agent.destination=targetPos;
			trashStep = 2;
		}
		else if(trashStep ==2 && Vector3.Distance(transform.position,targetPos)<0.1f) // 건물 밖으로 나가기
		{
			count =0;
			trashStep = 3;
			isWalk=false;
			trace.enabled =false;
			enemy.SetActive(false);
		}
		else if(trashStep == 3) // 분리수거중
		{
			++count;
			if(count==60*30) // 분리수거 끝나고 건물 안으로
			{
				isWalk=true;
				trace.enabled = true;
				enemy.SetActive(true);
				count = 0;
				trashStep = 4;
				targetPos = new Vector3(-25+90,1,155-150);
				_agent.destination=targetPos;
			}
		}
		else if(trashStep ==4 && Vector3.Distance(transform.position,targetPos)<0.1f) // 집으로 들어가기
		{
			_agent.Stop ();
			_agent.enabled=false;
			transform.localPosition = new Vector3(5,1,35);
			_agent.enabled=true;
			targetPos=new Vector3(5+90,1,15-150);
			_agent.destination=targetPos;
			trashStep = 5;
		}
		else if(trashStep == 5 && Vector3.Distance(transform.position,targetPos)<0.1f) // 티비보러
		{
			isWalk=false;
			step = 0;
			trashStep=0;
		}
	}

	public void Detect()
	{
		Vector3 pos = transform.position;
		pos.y = 1;
		transform.position = pos;
		if (detectStep == 0) {
			targetPos = new Vector3(-25+90,1,25-150);
			_agent.destination=targetPos;
			detectStep = 1;
		}
		else if (detectStep == 1 && Vector3.Distance(transform.position,targetPos)<0.1f) {
			targetPos = new Vector3(35+90,1,25-150);
			_agent.destination=targetPos;
			detectStep = 2;
		}
		else if (detectStep == 2 && Vector3.Distance(transform.position,targetPos)<0.1f) {
			targetPos = new Vector3(35+90,1,-15-150);
			_agent.destination=targetPos;
			detectStep = 3;
		}
		else if (detectStep == 3 && Vector3.Distance(transform.position,targetPos)<0.1f) {
			targetPos = new Vector3(15+90,1,-25-150);
			_agent.destination=targetPos;
			detectStep = 4;
		}
		else if (detectStep == 4 && Vector3.Distance(transform.position,targetPos)<0.1f) {
			targetPos = new Vector3(35+90,1,-35-150);
			_agent.destination=targetPos;
			detectStep = 5;
		}
		else if (detectStep == 5 && Vector3.Distance(transform.position,targetPos)<0.1f) {
			targetPos = new Vector3(-35+90,1,-35-150);
			_agent.destination=targetPos;
			detectStep = 6;
		}
		else if (detectStep == 6 && Vector3.Distance(transform.position,targetPos)<0.1f) {
			targetPos = new Vector3(-35+90,1,-25-150);
			_agent.destination=targetPos;
			detectStep = 7;
		}
		else if (detectStep == 7 && Vector3.Distance(transform.position,targetPos)<0.1f) {
			targetPos = new Vector3(-32+90,1,5-150);
			_agent.destination=targetPos;
			detectStep = 8;
		}
		else if (detectStep == 8 && Vector3.Distance(transform.position,targetPos)<0.1f) {
			targetPos = new Vector3(-15+90,1,5-150);
			_agent.destination=targetPos;
			detectStep = 9;
		}
		else if (detectStep == 9 && Vector3.Distance(transform.position,targetPos)<0.1f) {
			step=6;
			detectStep=0;
			patrolStep=0;
		}
	}
}
