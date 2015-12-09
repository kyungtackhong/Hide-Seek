using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Player : MonoBehaviour {
	
	public GameObject _camera;
	public AudioSource _audio;
	public GameObject _light;
	public GameObject poop;
	public GameObject player;
	public SpriteRenderer spr;
	public bool hideSw = false;
	public Slider[] desireSlider = new Slider[3];
	public static Vector3 playerposition;
	public Sprite[] upSpr = new Sprite[5];
	public Sprite[] downSpr = new Sprite[5];
	public Sprite[] sideSpr = new Sprite[5];
	public Sprite sleepSpr;
	public int velocity=10;
	public bool run_flag = false;
	public bool runSw= false;
	public bool foodSw = false;
	public bool inventory_flag=false;
	public bool map_flag = false;
	public static bool sleep_flag = false;
	public bool excret_flag=false;
	public int day=0;
	public float v = 2;
	public float x = 0;
	public float z = 0;
	public mainlight script;
	Vector3 vec = new Vector3 ();
	Vector3 ray_position;
	Vector3 ray_direction_R;
	public int pooptime=0;
	public int sight=0;
	private int sprCount=0;
	
	RaycastHit hit_0;
	
	static bool save;
	
	void Save(){
		SaveLoad.info [0] = Variable.timer+"";
		SaveLoad.info [1] = this.transform.position.x + "";
		SaveLoad.info [2] = this.transform.position.z + "";
		SaveLoad.info [3] = this.transform.position.y + "";
		SaveLoad.info [4] = Application.loadedLevel+"";
		SaveLoad.info [5] = Variable.scene+"";
		SaveLoad.info [6] = Variable.prevScene+"";
		SaveLoad.info [7] = Variable.sleep_desire + "";
		SaveLoad.info [8] = Variable.appetite + "";
		SaveLoad.info [9] = Variable.excretion + "";
		
	}
	void Load(){
		string[] info = new string[3];
		info = SaveLoad.info;
		Variable.timer = int.Parse(info [0]);
		vec.x = float.Parse(info [1]);
		vec.z = float.Parse(info [2]);
		vec.y = float.Parse (info [3]);
		this.transform.position = vec;
		vec.y = 0;
		Variable.scene = int.Parse (SaveLoad.info [5]);
		Variable.prevScene = int.Parse (SaveLoad.info [6]);
		Variable.sleep_desire= int.Parse (SaveLoad.info [7]);
		Variable.appetite= int.Parse (SaveLoad.info [8]);
		Variable.excretion= int.Parse (SaveLoad.info [9]);
		
	}
	
	
	
	void Update () {
		if (save==true) {
			Load();
			save=false;
		}
		if(Input.GetKey(KeyCode.Escape)){
			SaveLoad.scene = Application.loadedLevel;
			save=true;
			Save ();
			Application.LoadLevel("save");
		}
		v = 2; //속도 상수
		Variable.timer++;
		if (Variable.timer % 1200 == 0) { //20초마다 1씩 증가 
			Variable.sleep_desire++;
			if(sleep_flag)
			{
				Variable.sleep_desire-=6;
			}
			Variable.appetite++;
			Variable.excretion++;
			desireSlider[0].value = Variable.appetite;
			desireSlider[1].value = Variable.excretion;
			desireSlider[2].value = Variable.sleep_desire;
		}
		if (Variable.timer > 10000) {//하루가 증가 했다.
			Variable.timer=0;
			day++;
		}
		//////수면욕///////////
		if (Variable.sleep_desire > 100) {
			Variable.sleep_desire=100;
		}
		if (Variable.sleep_desire >= 80) {//라이트 어둡게, 5초당 한번씩 10퍼확률로 잠들게
			if(Variable.timer%300==0)//5초에 한번씩
			{
				if(Random.Range(0,9)==0)
				{
					sleep_flag=true;
				}
			}
		}
		if (Variable.timer >= 0 && Variable.timer < 3000) {
			sight=50;
		}
		if (Variable.timer >= 3000 && Variable.timer < 6000) {
			sight=40;
		}
		if (Variable.timer >= 6000 && Variable.timer < 9000) {
			sight=30;
		}
		if (Variable.timer >= 9000 && Variable.timer < 12000) {
			sight=40;
		}
		if ((Variable.timer % 3000) == 0) {
			mainlight.change_range (sight);
		}
		///////식욕////////////
		if (Variable.appetite >= 80) {//식욕이 80넘으면 이속 감소
			v = 4 / 5;
		} else {
			v = 1;
		}
		if (sleep_flag) {
			sleep ();
		}
		////////배설욕////
		
		if (Input.GetKeyDown ("left shift")) {
			
			if (Variable.timer % 1200 == 0) { //20초마다 1씩 증가 
				Variable.sleep_desire++;
				Variable.appetite++;
			}
			run_flag=true;
			velocity*=2;
		}
		if (Input.GetKeyUp ("left shift"))  {
			velocity/=2;
			run_flag=false;
		}
		
		if (Input.GetKeyUp ("s")) { // 똥누기
			Instantiate(poop,this.transform.position,Quaternion.identity);
			Variable.excretion-=30;
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
		if (Input.GetKeyDown ("a")) {
			if(sleep_flag==true)
			{
				AudioListener.volume = 1F;
				mainlight.change_range (sight);
				_light.GetComponent<SphereCollider>().radius*=2;
				print ("깨기");
				sleep_flag=false;
				spr.sprite=downSpr[sprCount/10%5];
			}
			else
			{
				AudioListener.volume = 2F;
				mainlight.change_range (sight/2);
				_light.GetComponent<SphereCollider>().radius/=2;
				print ("잠자기");
				sleep_flag=true;
				spr.sprite=sleepSpr;
			}
		}
		if (foodSw == true) 
			foodSw = false;
		if (Input.GetKeyUp ("c")) {
			foodSw = true;
			/*if (Physics.Raycast (ray_position, ray_direction_R, out hit_0, 3f)) {
				if (hit_0.collider.tag == "knife") {
					print ("inventory");
					inventory.addItem ("knife");
				}
				if (hit_0.collider.tag == "hammer") {
					print ("inventory");
					inventory.addItem ("망치");
				}
				if (hit_0.collider.tag == "watch") {
					print ("inventory");
					inventory.addItem ("시계");
				}
			}*/
		}
		if (Input.GetKeyDown ("x")) {//숨기
			if(hideSw ==false)
			{
				if (Physics.Raycast (ray_position, ray_direction_R, out hit_0, 3f)) {
					if (hit_0.collider.tag == "Hide") {
						print ("숨기");
						hideSw = true;
						player.SetActive(false);
						//this.transform.localScale=new Vector3(0.0f,0.0f,0.0f);
						//Instantiate(poop,this.transform.position+vec*30,Quaternion.identity);
					}
					
				}
			}
			else{
				//똥 파괴 해야함
				player.SetActive(true);
				//this.transform.localScale=new Vector3(6.0f,2.0f,6.0f);
				hideSw=false;
			}
		}
		if (hideSw == true)
			v = 0;
		if (Input.GetKeyUp ("i")) {
			_audio.Stop();
			inventory.showinventory ();
			if (inventory_flag == false) {
				inventory_flag = true;
				mainlight.change_range (0);
				Variable.timer--;
				if (sleep_flag == true)
					Variable.timer--;
			} else {
				inventory_flag = false;
				mainlight.change_range (sight);
			}
			
		}
		if (Input.GetKeyDown ("m")) {
			_audio.Stop();
			mapmap.showmap ();
			if (map_flag == false) {
				map_flag=true;
				mainlight.change_range (0);
				Variable.timer--;
				inventory_flag = true;
				if (sleep_flag == true)
					Variable.timer--;
			} else {
				inventory_flag = false;
				map_flag=false;
				mainlight.change_range (sight);
			}
			
		}
		if (inventory_flag == false && hideSw == false) {
			if ((Input.GetButton ("Horizontal") || Input.GetButton ("Vertical")) && sleep_flag == false) {
				if (_audio.isPlaying == false)
					_audio.Play ();
				runSw = run_flag;
				++sprCount;
				x = Input.GetAxisRaw ("Horizontal");
				z = Input.GetAxisRaw ("Vertical");
				vec.x = x * Time.deltaTime * velocity * v;
				vec.z = z * Time.deltaTime * velocity * v;
				if (x > 0) {
					spr.sprite = sideSpr [(sprCount / 10) % 5];
					player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
				} else if (x < 0) {
					spr.sprite = sideSpr [(sprCount / 10) % 5];
					player.transform.localScale = new Vector3 (-1.5f, 1.5f, 3);
				}
				if (z > 0) {
					spr.sprite = upSpr [(sprCount / 10) % 5];
					player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
				} else if (z < 0) {
					spr.sprite = downSpr [(sprCount / 10) % 5];
					player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
				}
				
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
				} 
				this.transform.position += vec;
				playerposition=this.transform.position;
			} else {
				_audio.Stop ();
				runSw = false;
			}
		}
		Vector3 pos = transform.position;
		pos.y = 50;
		_camera.transform.position = pos;
		_light.transform.position = pos;
		if (Variable.sleep_desire > 100) 
			Variable.sleep_desire = 100;
		else if (Variable.sleep_desire < 0)
			Variable.sleep_desire = 0;
		if (Variable.appetite > 100) 
			Variable.appetite = 100;
		else if (Variable.appetite < 0)
			Variable.appetite = 0;
		if (Variable.excretion > 100) 
			Variable.excretion = 100;
		else if (Variable.excretion < 0)
			Variable.excretion = 0;
		
		desireSlider[0].value = Variable.appetite/100f;
		desireSlider[1].value = Variable.excretion/100f;
		desireSlider[2].value = Variable.sleep_desire/100f;
	}
	void sleep()
	{
		v = 0;
		Variable.timer++;
	}
	
}
