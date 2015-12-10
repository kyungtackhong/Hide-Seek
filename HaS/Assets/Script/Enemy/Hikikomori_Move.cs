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
	private int sleep_desire=0;//수면욕
	private int appetite=0;//식욕
	private int excretion=0;//배설욕
	private int sprCount = 0;
	private int type=0;
	private int step = 4; // 1 - 소변보러가기, 2 - 소변보기, 3 - 돌아가기, 4 - 컴퓨터,
						  // 5 - 소리가 들림, 6 - 둘러보기, 7 - 뭔가 보임, 8 - 수색하기
						  // 9 - 쫓아냄, 10 - 냉장고가기, 11 - 음식꺼내기, 12 - 침대가기
						  // 13 - 자기
	private int detectStep = 0;
	private int count = 0;
	private int sightCount = 0;
	private NavMeshAgent _agent;
	private AudioSource _audio;
	public AudioClip _walk;
	public AudioClip _toilet;
	public SpriteRenderer toilet;
	public Sprite toiletOpen;
	public Sprite toiletClose;
	private Vector3 targetPos;
	private bool[] sw = new bool[3]{true,true,true};


	// Use this for initialization
	void Start () {
		trace = GetComponent<Enemy_Trace> ();
		col = GetComponent<SphereCollider> ();
		_agent = GetComponent<NavMeshAgent> ();
		_audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Variable.state == 0)
			return;
		if (Variable.timer % 45 <= 20 && sw [0]) { //45
			++excretion;
			sw [0] = false;
		} else
			sw [0] = true;
		if (Variable.timer % 180 <= 20 && sw [1]) { //180
			++appetite;
			sw [1] = false;
		} else
			sw [1] = true;
		if (Variable.timer % 540 <= 20 && sw [2]) { //540
			++sleep_desire;
			sw [2] = false;
		} else
			sw [2] = true;
		Vector3 pos = transform.position;
		pos.y = 1;
		transform.position = pos;
		if (!trace.playerInSight && !trace.playerInSound) {
			if(step == 1 || step == 3 || step ==5 || step == 7 || step == 8 || step == 10 || step == 12)
			{
				if(!_audio.isPlaying)
				{
					_audio.loop=true;
					_audio.Play ();
				}
				++sprCount;
			}
			if(step ==4)	// 컴퓨터
			{
				if(excretion>=60)
				{
					_audio.clip = _walk;
					col.radius = 2;
					step = 1;
					targetPos = new Vector3(-15+90,1,-25+180);
					_agent.destination = targetPos;
				}
				else if(appetite>=60)
				{
					col.radius = 2;
					step = 10;
					targetPos = new Vector3(25+90,1,25+180);
					_agent.destination = targetPos;
				}
				else if(sleep_desire>=60)
				{
					col.radius=2;
					step = 12;
					targetPos = new Vector3(-25+90,1,35+180);
					_agent.destination = targetPos;
				}
			}
			else if(step==1 && Vector3.Distance(transform.position ,targetPos)<0.1f) // 소변보러가기
			{
				step = 2;
				_audio.Stop ();
				_audio.clip = _toilet;
				toilet.sprite=toiletOpen;
				_audio.loop = false;
				_audio.Play();
			}
			else if(step==2)	// 소변보기
			{
				++count;
				if(count==200)
				{
					excretion=0;
					_audio.clip=_walk;
					count =0;
					toilet.sprite=toiletClose;
					if(appetite>=60)
					{
						step = 10;
						targetPos = new Vector3(25+90,1,25+180);
					}
					else if(sleep_desire>=60)
					{
						step = 12;
						targetPos = new Vector3(-25+90,1,35+180);
					}
					else
					{
						step = 3;
						targetPos=new Vector3(-5+90,1,25+180);
					}
					_agent.destination = targetPos;
				}
			}
			else if(step == 3 && Vector3.Distance(transform.position ,targetPos)<0.1f) // 돌아가기
			{
				step = 4;
				_audio.Stop ();
				col.radius=1;
				transform.eulerAngles = new Vector3(0,0,0);
			}
			else if(step == 5 && Vector3.Distance(transform.position,trace.targetSoundPos)<1)// 뭔가 들림
			{
				_audio.Stop();
				step=6;
				count=0;
			}
			else if(step==6)	// 둘러보기
			{
				++count;
				transform.Rotate (0, 1f, 0);
				if(count==180)
				{
					step = 3;
					targetPos=new Vector3(-5+90,1,25+180);
					_agent.destination = targetPos;
					count = 0;
				}
			}
			else if(step == 7 && Vector3.Distance(transform.position,trace.targetSightPos)<1) // 뭔가 보임
			{
				_audio.Stop();
				step=6;
				count=0;
			}
			else if(step==8) // 수색하기
			{
				Debug.Log ("!!");
				Detect();
			}
			else if(step==9) // 쫓아내기
			{
				++count;
				if(count==10)
				{
					_agent.enabled=false;
					transform.localPosition = new Vector3(-5,1,25);
					_agent.enabled=true;
					step=4;
				}
			}
			else if(step==10 && Vector3.Distance(transform.position,targetPos)<0.1f) // 냉장고가기
			{
				step = 11;
				count =0;
				_audio.Stop();
			}
			else if(step==11) // 음식꺼내기
			{
				++count;
				if(count==120)
				{
					_audio.clip=_walk;
					_audio.loop=true;
					_audio.Play();
					appetite=0;
					count =0;
					if(excretion>=60)
					{
						col.radius = 2;
						step = 1;
						targetPos = new Vector3(-15+90,1,-25+180);
						_agent.destination = targetPos;
					}
					else if(sleep_desire>=60)
					{
						step = 12;
						targetPos = new Vector3(-25+90,1,35+180);
					}
					else
					{
						step = 3;
						targetPos=new Vector3(-5+90,1,25+180);
					}
					_agent.destination = targetPos;
				}
			}
			else if(step == 12 && Vector3.Distance(transform.position,targetPos)<0.1f) // 잠자러가기
			{
				_agent.enabled=false;
				this.transform.position = new Vector3(-35+90,1,30+180);
				step = 13;
				col.radius=0;
				_audio.Stop ();
			}
			else if(step==13)
			{
				++count;
				if(count==14400)
				{
					this.transform.position = new Vector3(-25+90,1,35+180);
					_agent.enabled=true;
					_audio.clip=_walk;
					_audio.loop=true;
					_audio.Play();
					appetite=0;
					count =0;
					if(excretion>=60)
					{
						col.radius = 2;
						step = 1;
						targetPos = new Vector3(-15+90,1,-25+180);
						_agent.destination = targetPos;
					}
					else if(excretion>=60)
					{
						col.radius = 2;
						step = 1;
						targetPos = new Vector3(-15+90,1,-25+180);
						_agent.destination = targetPos;
					}
					else
					{
						step = 3;
						targetPos=new Vector3(-5+90,1,25+180);
					}
					_agent.destination = targetPos;
				}
			}
		} 
		else if(step!=9){
			if(trace.playerInSight==true)
			{
				step = 7;
				_agent.destination=trace.targetSightPos;
				_audio.clip=_walk;
				if(!_audio.isPlaying)
				{
					_audio.loop=true;
					_audio.Play ();
				}
				++sprCount;
				++sightCount;
				if(sightCount>=30)
				{
					step = 8;
					if(!_audio.isPlaying)
					{
						_audio.loop=true;
						_audio.Play ();
					}
					++sprCount;
				}
			}
			else if(trace.playerInSound==true)
			{
				step=5;
				_agent.destination=trace.targetSoundPos;
				_audio.clip=_walk;
				if(!_audio.isPlaying)
				{
					_audio.loop=true;
					_audio.Play ();
				}
				++sprCount;
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
		if (type == 0) 
			spr.sprite = upSpr [(sprCount / 10) % 5];
		else if (type == 2)
			spr.sprite = downSpr [(sprCount / 10) % 5];
		else
			spr.sprite = sideSpr [(sprCount / 10) % 5];
	}
	void Detect()
	{
		Vector3 pos = transform.position;
		pos.y = 1;
		transform.position = pos;
		if(detectStep == 0)
		{
			targetPos = new Vector3(-35+90,1,-25+180);
		}
		if(detectStep == 0 && Vector3.Distance(transform.position ,targetPos)<0.1f)
		{
			targetPos=new Vector3(-25+90,1,35+180);
			detectStep = 1;
		}
		else if(detectStep == 1 && Vector3.Distance(transform.position ,targetPos)<0.1f)
		{
			targetPos = new Vector3(-5+90,1,25+180);
			detectStep = 2;
		}
		else if(detectStep == 2 && Vector3.Distance(transform.position ,targetPos)<0.1f)
		{
			targetPos = new Vector3(-5+90,1,-5+180);
			detectStep=3;
		}
		else if(detectStep == 3 && Vector3.Distance(transform.position ,targetPos)<0.1f)
		{
			targetPos = new Vector3(5+90,1,25+180);
			detectStep=4;
		}
		else if(detectStep == 4 && Vector3.Distance(transform.position ,targetPos)<0.1f)
		{
			targetPos = new Vector3(35+90,1,25+180);
			detectStep=5;
		}
		else if(detectStep == 5 && Vector3.Distance(transform.position ,targetPos)<0.1f)
		{
			targetPos = new Vector3(35+90,1,-25+180);
			detectStep=6;
		}
		else if(detectStep == 6 && Vector3.Distance(transform.position ,targetPos)<0.1f)
		{
			targetPos = new Vector3(-5+90,1,-35+180);
			detectStep=7;
		}
		else if(detectStep == 7 && Vector3.Distance(transform.position ,targetPos)<0.1f)
		{
			targetPos = new Vector3(-5+90,1,25+180);
			detectStep=0;
			step=3;
		}
		++sprCount;
		_agent.destination= targetPos;
	}
	void OnTriggerStay(Collider coll)
	{
		if(coll.gameObject.tag=="Player" && Vector3.Distance(transform.position,coll.transform.position)<5)
		{
			GameObject.Find ("/Hall1/Doors/door1/Lock1").SetActive(true);
			step = 9;
			count = 0;
			_agent.Stop();
			_agent.enabled=false;
			transform.position = new Vector3(-25+90,1,-150+180);
			Variable.prevScene = Variable.scene;
			Variable.scene = 0;
			transform.eulerAngles=new Vector3(0,0,0);
			_agent.enabled=true;
			coll.transform.position = new Vector3(65,1,25);
			Player com = coll.GetComponent<Player>();
			com.hideSw=false;
			com.player.SetActive(true);
		}
	}
}
