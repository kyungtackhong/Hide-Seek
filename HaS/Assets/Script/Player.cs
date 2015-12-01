using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Player : MonoBehaviour {

	public GameObject _camera;
	public GameObject _light;
	public GameObject poop;
	public GameObject player;
	public SpriteRenderer spr;

	public Slider[] desireSlider = new Slider[3];
	public Sprite[] upSpr = new Sprite[5];
	public Sprite[] downSpr = new Sprite[5];
	public Sprite[] sideSpr = new Sprite[5];
	public Sprite sleepSpr;
	public int velocity=10;
	public int sleep_desire=0;//수면욕
	public int appetite=0;//식욕
	public int excretion=0;//배설욕
	public bool run_flag = false;
	public bool runSw= false;
	public bool foodSw = false;
	public static bool sleep_flag = false;
	public bool excret_flag=false;
	public int timer=0;
	public int day=0;
	public float v = 2;
	public float x = 0;
	public float z = 0;
	public mainlight script;
	Vector3 vec = new Vector3 ();
	Vector3 ray_position;
	Vector3 ray_direction_R;
	public int pooptime=0;

	private int sprCount=0;

	RaycastHit hit_0;

	void Update () {
		v = 2; //속도 상수
		timer++;
		if (timer % 1200 == 0) { //20초마다 1씩 증가 
			sleep_desire++;
			if(sleep_flag)
			{
				sleep_desire-=6;
			}
			appetite++;
			excretion++;
			print ("update");
			desireSlider[0].value = appetite;
			desireSlider[1].value = excretion;
			desireSlider[2].value = sleep_desire;
		}
		if (timer > 10000) {//하루가 증가 했다.
			timer=0;
			day++;
		}
		//////수면욕///////////
		if (sleep_desire > 100) {
			sleep_desire=100;
		}
		if (sleep_desire >= 80) {//라이트 어둡게, 5초당 한번씩 10퍼확률로 잠들게
			if(timer%300==0)//5초에 한번씩
			{
				if(Random.Range(0,9)==0)
				{
					sleep_flag=true;
				}
			}
		}

		///////식욕////////////
		if (appetite >= 80) {//식욕이 80넘으면 이속 감소
			v = 4 / 5;
		} else {
			v = 1;
		}
		if (sleep_flag) {
			sleep ();
		}
		////////배설욕////

		if (Input.GetKeyDown ("c")) {
			
			if (timer % 1200 == 0) { //20초마다 1씩 증가 
				sleep_desire++;
				appetite++;
			}
			print ("달리기");
			run_flag=true;
			velocity*=2;
		}
		if (Input.GetKeyUp ("c")) {
			print ("걷기");
			velocity/=2;
			run_flag=false;
		}
		
		if (Input.GetKeyUp ("v")) { // 똥누기
			Instantiate(poop,this.transform.position,Quaternion.identity);
			excretion-=30;
			v=0;
			pooptime++;
		}
		if (pooptime > 0) {
			v=0;
			pooptime++;
		}
		if (pooptime > 60) {
			pooptime=0;
		}
		if (Input.GetKeyDown ("z")) {
			if(sleep_flag==true)
			{
				AudioListener.volume = 1F;
				mainlight.change_range (50);
				_light.GetComponent<SphereCollider>().radius*=2;
				print ("깨기");
				sleep_flag=false;
				spr.sprite=downSpr[sprCount/10%5];
			}
			else
			{
				AudioListener.volume = 2F;
				mainlight.change_range (25);
				_light.GetComponent<SphereCollider>().radius/=2;
				print ("잠자기");
				sleep_flag=true;
				spr.sprite=sleepSpr;
			}
		}
		Debug.Log (foodSw);
		if (foodSw == true) 
			foodSw = false;
		if (Input.GetKeyUp ("x")) {
			foodSw= true;
			/*x = 1f;
			z = 0;
			vec.x = x*Time.deltaTime*velocity*v;
			vec.z = z*Time.deltaTime*velocity*v;
			ray_position=this.transform.position;
			ray_direction_R=vec;
			
			//foodcollider();
			x = -1f;
			z = 0;
			vec.x = x*Time.deltaTime*velocity*v;
			vec.z = z*Time.deltaTime*velocity*v;
			ray_position=this.transform.position;
			ray_direction_R=vec;
			//foodcollider();
			
			x = 0;
			z = 1;
			
			vec.x = x*Time.deltaTime*velocity*v;
			vec.z = z*Time.deltaTime*velocity*v;
			ray_position=this.transform.position;
			ray_direction_R=vec;
			//foodcollider();
			
			x = 0;
			z = -1;
			
			vec.x = x*Time.deltaTime*velocity*v;
			vec.z = z*Time.deltaTime*velocity*v;
			ray_position=this.transform.position;
			ray_direction_R=vec;
			//foodcollider();*/
			
		}
		//if(Input.GetButton

		if ((Input.GetButton ("Horizontal") || Input.GetButton ("Vertical")) && sleep_flag == false) {
			runSw = run_flag;
			++sprCount;
			x = Input.GetAxisRaw ("Horizontal");
			z = Input.GetAxisRaw ("Vertical");
			if (x > 0) {
				spr.sprite = sideSpr [(sprCount / 10) % 5];
				player.transform.localScale = new Vector3 (3, 3, 3);
			} else if (x < 0) {
				spr.sprite = sideSpr [(sprCount / 10) % 5];
				player.transform.localScale = new Vector3 (-3, 3, 3);
			}
			if (z > 0) {
				spr.sprite = upSpr [(sprCount / 10) % 5];
				player.transform.localScale = new Vector3 (3, 3, 3);
			} else if (z < 0) {
				spr.sprite = downSpr [(sprCount / 10) % 5];
				player.transform.localScale = new Vector3 (3, 3, 3);
			}
			vec.x = x * Time.deltaTime * velocity * v;
			vec.z = z * Time.deltaTime * velocity * v;
			ray_position = this.transform.position;
			ray_direction_R = vec;
			if (Physics.Raycast (ray_position, ray_direction_R, out hit_0, 2.5f)) {
				if (hit_0.collider.tag == "Wall") {
					vec.x = 0;
					vec.z = 0;
				}
				
			}
			
			if (x != 0 && z != 0) {
				for (int i=0; i<10; i++) {
					float j = (float)i / 10 + 3f;
					if (Physics.Raycast (ray_position, ray_direction_R, out hit_0, j)) {
						if (hit_0.collider.tag == "Wall") {
							vec.x = 0;
							vec.z = 0;
						}
					}
				}
			} else {
				if (Physics.Raycast (ray_position, ray_direction_R, out hit_0, 2.5f)) {
					if (hit_0.collider.tag == "Wall") {
						vec.x = 0;
						vec.z = 0;
					}
					
				}
			}
			
			this.transform.position += vec;
		} else
			runSw = false;
		Vector3 pos = transform.position;
		pos.y = 50;
		_camera.transform.position = pos;
		_light.transform.position = pos;
		if (sleep_desire > 100) 
			sleep_desire = 100;
		else if (sleep_desire < 0)
			sleep_desire = 0;
		if (appetite > 100) 
			appetite = 100;
		else if (appetite < 0)
			appetite = 0;
		if (excretion > 100) 
			excretion = 100;
		else if (excretion < 0)
			excretion = 0;

		desireSlider[0].value = appetite/100f;
		desireSlider[1].value = excretion/100f;
		desireSlider[2].value = sleep_desire/100f;
	}
	void sleep()
	{
		v = 0;
		timer++;
	}
	/*public void foodcollider()
	{
		if(Physics.Raycast (ray_position, ray_direction_R,out hit_0,4f))
		{
			if(hit_0.collider.tag=="rice")
			{
				print ("먹기");
				food.food_destroy(this.transform.position.x,this.transform.position.z);
			}
			
		}
		
	}*/
}
