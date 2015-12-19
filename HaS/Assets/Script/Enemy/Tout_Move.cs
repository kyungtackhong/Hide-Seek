using UnityEngine;
using System.Collections;

public class Tout_Move : MonoBehaviour {
	public Enemy_Trace trace;
	public GameObject enemy;
	public SpriteRenderer spr;
	public SpriteRenderer wep;
	public Sprite weapon;
	public Sprite[] upSpr = new Sprite[5];
	public Sprite[] downSpr = new Sprite[5];
	public Sprite[] sideSpr = new Sprite[5];
	private SphereCollider col;  
	private int soundCount =0;
	private int sleep_desire=0;//수면욕
	private int appetite=0;//식욕
	private int excretion=0;//배설욕
	private int type = 0;
	private int sprCount = 0;
	private int toiletcount=0;
	private int RFGcount = 0;
	private int Bedcount = 0;
//	private int soundsightCount = 0;
	private int readyCount = 0;
	private bool goBedRFGToilet = false;
	private int step = 0; // 1 - 소변보러가기, 2 - 소변보기, 3 - 돌아가기, 4 - 컴퓨터,
	private int prvStep=0;  // 5 - 소리가 들림, 6 - 둘러보기, 7 - 뭔가 보임, 8 - 수색하기
						  // 9 - 쫓아냄, 10 - 냉장고가기, 11 - 음식꺼내기, 12 - 침대가기
						  // 13 - 자기, 14 - 집안에서 출근 , 15 - 복도에서 출근 , 16 - 걷기

	private int commute=0; 		  //1 - 집 -> 현관, 2 - 복도 -> 중앙현관
	private int prvCommute; 	  //3 - 집 <- 현관, 4 - 복도 <- 중앙현관
								  //5 - 출퇴근중 이상징후 발견

//	private int enemyWhere=2;  //1 - 집안 , 2 - 복도 , 3 - 게임 밖
//	private int tempCommute;
	private bool playerSound = false;
	private bool playerSight = false;
	private NavMeshAgent _agent;
	private Vector3 targetPos;

	private bool goWork = false;
	private int goWorkTime =21;
//	private int backWorkTime = 9;
	private bool backCheck = false;

	private bool aiInHouse = false;
	private bool weaponState = false;
	private bool aiSeeInHouse = false;


	//집안 구조에 따라 좌표만 변경 해주면 될듯?
	private Vector3 v3frontDoor = new Vector3(-5-60,1,-35+180); // 집안 현관
	private Vector3 v3hallDoor = new Vector3(45-60,1,-155+180); // 복도 현관
	private Vector3 v3centerHall = new Vector3(195-60,1,-165+180); //출퇴근시 사라지는곳

	private Vector3 v3Computer = new Vector3(-15-60,1,5+180); //컴퓨터
	private Vector3 v3Toilet = new Vector3(-15-60,1,-25+180); //화장실
	private Vector3 v3RFG = new Vector3(15-60,1,15+180); //냉장고
	private Vector3 v3Bed = new Vector3(-5-60,1,20+180); //침대

	void Needcontrol(){

		if (goBedRFGToilet == false) {
			if (excretion >= 60) {
				col.radius = 2;
				step = 1;
				targetPos = v3Toilet;
				_agent.destination = targetPos;
				goBedRFGToilet=true;
			} else if (appetite >= 60) {
				col.radius = 2;
				step = 10;
				targetPos = v3RFG;
				_agent.destination = targetPos;
				goBedRFGToilet=true;
			} else if (sleep_desire >= 60) {
				col.radius = 2;
				step = 12;
				targetPos = v3Bed;
				_agent.destination = targetPos;
				goBedRFGToilet=true;
			}
		}

	}
	// Use this for initialization
	void Start () {
		wep.sprite = null;
		col = GetComponent<SphereCollider> ();
		_agent = GetComponent<NavMeshAgent> ();
		trace = GetComponent<Enemy_Trace> ();
		enemy.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {

		if (Variable.state != 1 || trace.downSw==true)
			return;
		//Debug.Log (Variable.timer+" " +excretion + " " + appetite + " " + sleep_desire);
	

		if (aiInHouse==true&&Variable.timer!=10800) {
			if (Variable.timer % 10 == 0) //90
				++excretion;
			if (Variable.timer % 40 == 0) //360
				++appetite;
			if (Variable.timer % 120 == 0) //1080
				++sleep_desire;
		}

		Vector3 pos = transform.position;
		pos.y = 1;
		transform.position = pos;
		//출퇴근 관리
		if (Variable.hour == goWorkTime && goWork==false) {//직장에 있는 시간 & 직장에 가야하는 시간
			Debug.Log("출근시간");
			commute =1;
			goWork = true;
			backCheck = false;
			aiInHouse=false;
			excretion=0;
			appetite=0;
			sleep_desire=0;

		} else if (Variable.hour ==0||Variable.hour==24) {//퇴근 시간
			if (backCheck == false) {// 퇴근 시작
				commute=4;
				backCheck = true;
				//enemy.SetActive(true);
			}
		}
		//Debug.Log (commute + " " + prvCommute);
	
		//출퇴근 행동 관리
		if (commute == 1) {
			targetPos = v3frontDoor;
			_agent.destination = targetPos;
			prvCommute =1;
			commute = 0;
			step=14;
		} else if (commute == 2) {
			targetPos = v3centerHall;
			_agent.destination = targetPos;
			prvCommute=2;
			commute = 0;
			step=15;
		} else if (commute == 3) {
			targetPos = v3Computer;
			_agent.destination = targetPos;
			step = 4;
			commute = 0;
		} else if (commute == 4) {
			targetPos = v3hallDoor;
			_agent.destination = targetPos;
			step=14;
			prvCommute = 4;
			commute = 0;
		} 
		if (commute == 0&&prvCommute!=0) {
			if(prvCommute==1 && Vector3.Distance (transform.position, targetPos) < 1.0f){
				aiInHouse = false;
				_agent.Stop();
				_agent.enabled=false;
				transform.position = v3hallDoor;
				_agent.enabled=true; 
				commute=2;
				prvCommute=0;
//				tempCommute = commute;
			} else if (prvCommute==2 && Vector3.Distance (transform.position, targetPos) < 1.0f){
				//enemy.SetActive(false);
				step=0;
				prvCommute=0;
//				tempCommute = commute;
			} else if (prvCommute==3 && Vector3.Distance (transform.position, targetPos) < 1.0f){
				step=0;
			}else if (prvCommute==4 && Vector3.Distance (transform.position, targetPos) < 1.0f) {
				_agent.Stop();
				_agent.enabled=false;
				transform.position = v3frontDoor;
				_agent.enabled=true;
				aiInHouse = true;
				commute = 3;
				prvCommute=0;
//				tempCommute = commute;
			}
		}

//		Debug.Log ("step : " + step + aiInHouse);
		//본능보다 다른일이 발생 시 
		if (step==0&&aiInHouse==true) {
//			Debug.Log("읽긴하냐");
			Needcontrol ();
		}
	//	Debug.Log (excretion + " " + sleep_desire + " " + appetite);
//		Debug.Log (commute + " " + prvCommute + " " +Vector3.Distance (transform.position, targetPos));
		//화장실가기 , 냉장고 가기 , 침대가기
		if (step == 1 || step ==3 || step == 10 || step == 12||step==14 || step==15) {
			sprCount++;
		}
			if (step == 1 && Vector3.Distance (transform.position, targetPos) < 1.0f) {
			step = 2;
			toiletcount = 0;
		} else if (step == 2) {
			toiletcount++;
			if (toiletcount > 150) {
				excretion = 0;
				toiletcount = 0;
				targetPos = v3Computer;
				_agent.destination = targetPos;
				goBedRFGToilet = false;
				step = 3;
			}
		} else if (step == 10 && Vector3.Distance (transform.position, targetPos) < 1.0f) {
			step = 11;
			RFGcount = 0;
		} else if (step == 11) {
			RFGcount++;
			if (RFGcount > 100) {
				appetite = 0;
				RFGcount = 0;
				targetPos = v3Computer;
				_agent.destination = targetPos;
				goBedRFGToilet = false;
				step = 3;
			}
		} else if (step == 12 && Vector3.Distance (transform.position, targetPos) < 1.0f) {
			step = 13;
			Bedcount = 0;
		} else if (step == 13) {
			Bedcount++;

			if (Bedcount > 3600) {
				sleep_desire = 0;
				Bedcount = 0;
				targetPos = v3Computer;
				_agent.destination = targetPos;
				goBedRFGToilet = false;
				step = 3;
			}
		} else if (step == 4) {
			targetPos = v3Computer;
			_agent.destination = targetPos;
			step = 3;
		} else if (step == 3 && Vector3.Distance (transform.position, targetPos) < 1.0f) {
			step = 0;
		} else if (step == 5 && Vector3.Distance (transform.position, targetPos) <1.0f) {
			if(playerSight==false){
				if(commute==0){
					AitargetPos(prvStep);
				} else {
					commute = prvCommute;
				}
			} else if (playerSight==true){
				step=6;
			}


		} else if (step == 6) { //경계
			readyCount++;
			transform.Rotate (0, 1f, 0);
			if (readyCount > 600) {
				wep.sprite = null;
				readyCount = 0;
				playerSight=false;
				playerSound=false;
				if(commute==0){
					step = prvStep;
					AitargetPos(step);
				} else {
					commute = prvCommute;
				}

			}
		
		} 




		if (aiSeeInHouse == true || aiInHouse == true) {

			if (trace.playerInSight == true) {
				aiSeeInHouse = true;
//				soundsightCount = 0;
				if (playerSight == false && playerSound == false) {
					if(commute!=0){
						prvCommute = commute;
					}
					prvStep = step;
				}
				weaponState = true;
				playerSight = true;
				step = 5;

				targetPos = trace.targetSightPos;
				_agent.destination = targetPos;

			} 
			if (trace.playerInSound == true) {
				soundCount++;
				if(soundCount>60){
					if (playerSight == false && playerSound == false) {
						if(commute!=0){
							prvCommute = commute;
						}
						prvStep = step;
					}
					playerSound = true;
					step = 5;

//					tempCommute = prvCommute;
//					Debug.Log ("의심하기 시작");

					targetPos = trace.targetSoundPos;
					_agent.destination = targetPos;
				}
			} 
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

		if (type == 0) {
			spr.sprite = upSpr [(sprCount / 10) % 5];
			if(weaponState==true) wep.sprite = weapon;
		} else if (type == 2) {
			spr.sprite = downSpr [(sprCount / 10) % 5];
			if(weaponState==true) wep.sprite = weapon;
		} else {
			spr.sprite = sideSpr [(sprCount / 10) % 5];
			if(weaponState==true) wep.sprite = weapon;
		}
			

	
	}
	void AitargetPos(int step){
		if (step == 1) {
			targetPos = v3Toilet;
			_agent.destination = targetPos;
		} else if (step == 4 || step==3) {
			targetPos = v3Computer;
			_agent.destination = targetPos;
		} else if (step == 10) {
			targetPos = v3RFG;
			_agent.destination = targetPos;
		} else if (step == 12) {
			targetPos = v3Bed;
			_agent.destination = targetPos;
		}


	}
	void OnTriggerStay(Collider coll)
	{
		
		if(coll.gameObject.tag=="Player" && Vector3.Distance(transform.position,coll.transform.position)<3&&aiSeeInHouse==true)
		{
			Variable.toutSw = true;
			Variable.state = 0;
		}
		
	}
}
