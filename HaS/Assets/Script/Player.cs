using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Player : MonoBehaviour {

	public GameObject thing;/// <이거 추가해야하는 변수>
	public GameObject _retryBtn;
	public GameObject _camera;
	public AudioSource _audio;
	public GameObject _light;
	public GameObject poop;
	public GameObject player;
	public GameObject playerCol;
	public SpriteRenderer spr;
	public bool hideSw = false;
	public Slider[] desireSlider = new Slider[3];
	public Image[] fill = new Image[3];
	public static Vector3 playerposition;
	public Sprite[] upSpr = new Sprite[5];
	public Sprite[] downSpr = new Sprite[5];
	public Sprite[] sideSpr = new Sprite[5];
	public Sprite[] hammerupSpr = new Sprite[6];
	public Sprite[] stickupSpr = new Sprite[6];
	public Sprite[] nkifeupSpr = new Sprite[6];
	public Sprite[] hammerdownSpr = new Sprite[6];
	public Sprite[] stickdownSpr = new Sprite[6];
	public Sprite[] knifedownSpr = new Sprite[6];
	public Sprite[] hammersideSpr = new Sprite[6];
	public Sprite[] sticksideSpr = new Sprite[6];
	public Sprite[] knifesideSpr = new Sprite[6];
	public Sprite[] swingUpSpr = new Sprite[3];
	public Sprite[] swingDownSpr = new Sprite[3];
	public Sprite[] swingSideSpr = new Sprite[3];
	public Sprite[] hammerswingUpSpr = new Sprite[3];
	public Sprite[] hammerswingSideSpr = new Sprite[3];
	public Sprite[] stickswingUpSpr = new Sprite[3];
	public Sprite[] stickswingSideSpr = new Sprite[3];
	public Sprite poopSpr; 
	public Sprite sleepSpr;
	public int velocity=10;
	public bool run_flag = false;
	public bool runSw= false;
	public bool foodSw = false;
    public bool downSw = false;
    public bool sleepSw = false;
	public bool sleep_flag1=false;// 30될때까지 자는 플레그
	public bool sleep_flag2=false;
	public bool metrix_flag = false;
    public bool poop_flag = false;
	public bool poop_all=false;
	public static bool inventory_flag=false;
	public static bool object_inventory_flag=false;
	public bool map_flag = false;
    public bool poopSw = false;
	public bool itemSw = false;
	public bool sleep_flag = false;
	public static int food_timer=0;
	public static int food_count=0;
	public bool excret_flag=false;
	public int day=0;
	public float v = 1;
	public float x = 0;
	public float z = 0;
	public mainlight script;
	public Vector3 direction = new Vector3();
	public Vector3 vec = new Vector3 ();
	Vector3 ray_position;
	Vector3 ray_direction_R;
	public int pooptime=0;
	public int sight=50;
	private int sprCount=0;
	private int poop_count=0;
	RaycastHit hit_0;
	public int waitasecond = 0;
	static bool save;
	public int thingmove=0;
    public bool hide_flag=false;
    public bool item_flag = false;
	void Start()
	{
		Time.captureFramerate = 60;
		Variable.hikiSw = false;
		Variable.spySw = false;
		Variable.guardSw = 0;
		Variable.toutSw = false;
		Variable.questSw[0] = true;
		for(int i=1;i<5;++i)
			Variable.questSw[i]=false;
		Variable.itemSw = false;
		Variable.item = "";
		Variable.mapSw = false;
		Variable.guardSw = 0;
		Variable.quest = 0;
		Variable.state = 0; // 0 - PAUSE, 1 - PLAY
		Variable.prevScene = -1;
		Variable.scene = -1;// -1 - blackmarket, 0 - hall1, 6 - house 1-1(106호), 1 - house 1-2(101호)
		// 10 - hall2
		// 20 - hall3
		// 30 - hall4

		Variable.timer = 60*30*6; // 60*30*6
		for (int i=0; i<4; ++i)
			Variable.items[i]=new Variable.Item();
		Variable.item_count = 0;
		//Variable.bookshelf.Initialize ();
		Variable.current_item.Name = "";
	}

	public void Retry()
	{
		Application.LoadLevel("Floor1Scene");
	}


	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if(_retryBtn.activeSelf==false)
			{
				Variable.state = 2;
				_retryBtn.SetActive(true);
			}
			else
			{
				Variable.state=1;
				_retryBtn.SetActive(false);
			}
		}
		if (Variable.state != 1)
			return;

		Variable.timer++;
		desire_up ();
		
		//////수면욕///////////
		
		
		///////식욕////////////
		//velocity = 10;
		if (sleep_flag) {
			v = 0;
		} else {
			v = 1;
		}
		desire_control ();
		////////배설욕////
		if (v == 1) {
			if (Input.GetKeyDown ("left shift")) {
				run_flag = true;				
			}
			if (Input.GetKeyUp ("left shift")) {				
				run_flag = false;
			}
		}
		if (run_flag == true) {
			velocity = 15;
		} else {
			velocity = 10;
		}

        //////*****************************************************
        if (poopSw == true)
            poopSw = false;
        if (sleep_flag == false)
        {
            if (Input.GetKeyUp("s"))
            { // 똥누기                
				playerCol.transform.localPosition = new Vector3 (0.25f, 0, 0);
				spr.sprite = poopSpr;
				player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
                if (poop_flag == true)
                    poop_flag = false;
               

                ray_position = this.transform.position;
                ray_direction_R = vec;
                if (Physics.Raycast(ray_position, ray_direction_R, out hit_0,3.0f))
                {
                    poopSw = true;
                }

               else if (poop_flag == false)
                {
                    if (Variable.excretion >= 100)
                    {
                        Instantiate(poop, this.transform.position, Quaternion.identity);
                        Variable.excretion -= 200;
                        pooptime++;

                    }
                }
            }
        }
        //*************************************
		if (pooptime > 0) {
			v=0;
			pooptime++;
			if(pooptime%2==1){
				vec.x=1;
				vec.z=0;
				this.transform.position += vec;
				playerposition=this.transform.position;
			}
			else if(pooptime%2==0){
				vec.x=-1;
				vec.z=0;
				this.transform.position += vec;
				playerposition=this.transform.position;
			}
		}
        
		if (pooptime > 60) {
			pooptime=0;
            if(poop_flag)
            {
                Vector3 vec2 = new Vector3();
                vec2 = PlayerCol_Collision.metrix_vec;               
                this.transform.position -= vec2;
            }
		}
        if (sleepSw == true)
            sleepSw = false;
        if (map_flag == false && inventory_flag == false)
        {
            if (Input.GetKeyDown("a"))
            {
                if (sleep_flag == false)
                    sleepSw = true;
                if (metrix_flag)
                {
                    Vector3 vec2 = new Vector3();
                    vec2 = PlayerCol_Collision.metrix_vec;
                    metrix_flag = false;
                    this.transform.position -= vec2;
                    //  Player.playerposition -= vec;
                    print("침대에서 일어나기");
                    sleep_flag = false;
                    waitasecond++;
                    mainlight.change_range(sight);
                }
                else if (metrix_flag == false)
                {
                    if (sleep_flag == true)
                    {
                        if (sleep_flag1 == false && sleep_flag2 == false)
                        {
                            AudioListener.volume = 1F;
                            mainlight.change_range(sight);
                            _light.GetComponent<SphereCollider>().radius *= 2;
                            print("깨기");
                            sleep_flag = false;
                            downSw = false;
                            spr.sprite = downSpr[sprCount / 10 % 5];
                        }
                    }
                    else
                    {
                        Debug.Log(sight);
                        AudioListener.volume = 2F;

                        mainlight.change_range(sight / 2);
                        _light.GetComponent<SphereCollider>().radius /= 2;
                        print("잠자기");
                        sleep_flag = true;
                        downSw = true;
                        spr.sprite = sleepSpr;

                    }
                }
            }
        }
		if (waitasecond > 0) {
			++waitasecond;
			if (waitasecond >= 5)
				waitasecond = 0;
		}

		if (sleep_flag == true) {
			//spr.sprite=downSpr[sprCount/10%5];
			spr.sprite=sleepSpr;
		}
		if (foodSw == true) 
			foodSw = false;
        /*
		if (Input.GetKeyUp ("c")) {
			foodSw = true;
			if (metrix_flag)
			{
				Vector3 vec2 = new Vector3();
				vec2= PlayerCol_Collision.metrix_vec;
				metrix_flag = false;
				this.transform.position -= vec2;
				//  Player.playerposition -= vec;
				print("침대에서 일어나기");
				sleep_flag = false;
				waitasecond++;
				sight*=3;
				mainlight.change_range (sight);
			}
		}*/
        if (hideSw == true)
            hideSw = false;
        if (sleep_flag == false)//***********************************************************추가
        {           
            if (Input.GetKeyDown("x"))
            {//숨기               
                hideSw = true;
                if (hide_flag)
                {
                    /*Vector3 vec2 = new Vector3();
                    vec2 = PlayerCol_Collision.metrix_vec;
                    this.transform.position -= vec2;*/
                    waitasecond++;
                    hide_flag = false;
                    player.SetActive(true);
                    //this.transform.localScale=new Vector3(6.0f,2.0f,6.0f);
					mainlight.change_range (sight);
                    hideSw = false;
                }
            }
        }
        if (hide_flag == true)
            v = 0;
		
		//////object inventory button
		if (Input.GetKeyUp("i") && map_flag==false && sleep_flag==false&&object_inventory_flag==false)
		{
			_audio.Stop();
			inventory.showinventory();
			if (inventory_flag == false)
			{
				inventory_flag = true;
				mainlight.change_range(0);
				Variable.timer--;
			}
			else
			{
				inventory_flag = false;
				mainlight.change_range(sight);
			}
		}
        if (object_inventory_flag == true && Input.GetKeyUp("i"))
        {
            object_inventory.showinventory(PlayerCol_Collision.object_num);
        	object_inventory_flag = false;
            mainlight.change_range(sight);
        }
            if (map_flag == true)
            v = 0;
		if (Input.GetKeyDown ("m") && Variable.mapSw==true && inventory_flag==false && sleep_flag==false &&object_inventory_flag==false) {
			_audio.Stop();
			mapmap.showmap ();
			if (map_flag == false) {
				mainlight.change_range (0);
				Variable.timer--;
				map_flag = true;
				if (sleep_flag == true)
					Variable.timer--;
			} else {
				map_flag=false;
				mainlight.change_range (sight);
			}
			
		}
		if (itemSw == true)
			itemSw = false;
        if (item_flag == true)
            item_flag = false;
		if(Input.GetKeyDown("z")&&inventory_flag == false)
		{
            foodSw = true;
            if (Variable.current_item.Name=="고기")
			{
				Variable.appetite -= 50;
				if (Variable.appetite < 0)
					Variable.appetite = 0;
				Variable.current_item.Name=null;
				Variable.current_item.tex = (Texture)Resources.Load(Variable.current_item.Name);
				food_timer++;
				food_count++;
				//thing.GetComponent<SpriteRenderer>().sprite = Resources.Load(Variable.current_item.Name, typeof(Sprite)) as Sprite;
				
			}
            if (item_flag == false)
            {
                if (Variable.current_item.Name == "방망이" || Variable.current_item.Name == "칼" || Variable.current_item.Name == "망치")
                {
                    itemSw = true;
                    thingmove++;

                }

            }
               
            
			/*else 
			{
				itemSw=true;
				Debug.Log (Variable.current_item.Name);
			}*/
		}
		if (thingmove > 0) {
			
			thingmove++;
			if (thingmove > 29)
			{
				spr.sprite = downSpr [0];
				playerCol.transform.localPosition = new Vector3(0f,0,-0.03f);
				player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
				thingmove = 0;
			}
			//thingmovement ();
			if(Variable.current_item.Name=="망치")
			{
				if (direction.x > 0) {
					playerCol.transform.localPosition = new Vector3 (0.25f, 0, 0);
					spr.sprite = hammerswingSideSpr [thingmove / 10];
					player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
				} else if (direction.x < 0) {
					playerCol.transform.localPosition = new Vector3 (-0.25f, 0, 0);
					spr.sprite = hammerswingSideSpr [thingmove / 10];
					player.transform.localScale = new Vector3 (-1.5f, 1.5f, 3);
				} else if (direction.z > 0) {
					playerCol.transform.localPosition = new Vector3(0,0,0.3f);
					spr.sprite = swingUpSpr [thingmove/10];
					player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
				} else {
					playerCol.transform.localPosition = new Vector3(0f,0,-0.03f);
					spr.sprite = hammerswingUpSpr [thingmove/10];

					player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
				}

			}
			else if(Variable.current_item.Name=="방망이")
			{
				
				print ("방망이");
				if (direction.x > 0) {
					playerCol.transform.localPosition = new Vector3 (0.25f, 0, 0);
					spr.sprite = stickswingSideSpr [thingmove / 10];
					player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
				} else if (direction.x < 0) {
					playerCol.transform.localPosition = new Vector3 (-0.25f, 0, 0);
					spr.sprite = stickswingSideSpr [thingmove / 10];
					player.transform.localScale = new Vector3 (-1.5f, 1.5f, 3);
				} else if (direction.z > 0) {
					playerCol.transform.localPosition = new Vector3(0,0,0.3f);
					spr.sprite = swingUpSpr [thingmove/10];
					player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
				} else {
					playerCol.transform.localPosition = new Vector3(0f,0,-0.03f);
					spr.sprite = stickswingUpSpr [thingmove/10];

					player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
				}
			}
			else{
			if (direction.x > 0) {
				playerCol.transform.localPosition = new Vector3 (0.25f, 0, 0);
				spr.sprite = swingSideSpr [thingmove / 10];
				player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
			} else if (direction.x < 0) {
				playerCol.transform.localPosition = new Vector3 (-0.25f, 0, 0);
				spr.sprite = swingSideSpr [thingmove / 10];
				player.transform.localScale = new Vector3 (-1.5f, 1.5f, 3);
			} else if (direction.z > 0) {
				playerCol.transform.localPosition = new Vector3(0,0,0.3f);
				spr.sprite = swingUpSpr [thingmove/10];
				player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
			} else {
				playerCol.transform.localPosition = new Vector3(0f,0,-0.03f);
				spr.sprite = swingDownSpr [thingmove/10];
				player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
			}
			}
		}
		if (inventory_flag == false && hide_flag == false&&object_inventory_flag==false) {
			if ((Input.GetButton ("Horizontal") || Input.GetButton ("Vertical")) && sleep_flag == false) {
				if (_audio.isPlaying == false)
					_audio.Play ();
				runSw = run_flag;
				++sprCount;
				x = Input.GetAxisRaw ("Horizontal");
				z = Input.GetAxisRaw ("Vertical");
				vec.x = x * Time.deltaTime * velocity * v;
				vec.z = z * Time.deltaTime * velocity * v;
				if(thingmove==0)
				{
					if (x > 0) {
						direction.x=1;
						direction.z=0;
						direction.y=0;
						
						playerCol.transform.localPosition = new Vector3(0.25f,0,0);
						player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
						if(Variable.current_item.Name=="망치")
						{
							spr.sprite = hammersideSpr [(sprCount / 10) % 5];	
						}
						else if(Variable.current_item.Name=="방망이")
						{
							spr.sprite = sticksideSpr [(sprCount / 10) % 5];	
						}
						else{	
							spr.sprite = sideSpr [(sprCount / 10) % 5];						
						}
					} else if (x < 0) {
						direction.x=-1;
						direction.z=0;
						direction.y=0;
						
						if(Variable.current_item.Name=="망치")
						{
							spr.sprite = hammersideSpr [(sprCount / 10) % 5];	
						}
						else if(Variable.current_item.Name=="방망이")
						{
							spr.sprite = sticksideSpr [(sprCount / 10) % 5];	
						}
						else{
							spr.sprite = sideSpr [(sprCount / 10) % 5];						
						}
						player.transform.localScale = new Vector3 (-1.5f, 1.5f, 3);
						playerCol.transform.localPosition = new Vector3(-0.25f,0,0);
					}
					if (z > 0) {
						direction.x=0;
						direction.z=1;
						direction.y=0;
						
						if(Variable.current_item.Name=="망치")
						{
							spr.sprite = hammerupSpr [(sprCount / 10) % 5];	
							playerCol.transform.localPosition = new Vector3(0f,0,-0.03f);
							player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
						}
						else if(Variable.current_item.Name=="방망이")
						{
							spr.sprite = stickupSpr [(sprCount / 10) % 5];	
							playerCol.transform.localPosition = new Vector3(0f,0,-0.03f);
							player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
						}
						else{
						spr.sprite = upSpr [(sprCount / 10) % 5];
							playerCol.transform.localPosition = new Vector3(0,0,0.3f);
							player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
						}

					} else if (z < 0) {
						direction.x=0;
						direction.z=-1;
						direction.y=0;
						


						if(Variable.current_item.Name=="망치")
						{
							spr.sprite = hammerdownSpr [(sprCount / 10) % 5];	
							playerCol.transform.localPosition = new Vector3(0,0,0.3f);
							player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
						}
						else if(Variable.current_item.Name=="방망이")
						{
							spr.sprite = stickdownSpr [(sprCount / 10) % 5];	
							playerCol.transform.localPosition = new Vector3(0,0,0.3f);
							player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
						}
						else{
						spr.sprite = downSpr [(sprCount / 10) % 5];
							playerCol.transform.localPosition = new Vector3(0f,0,-0.03f);
							player.transform.localScale = new Vector3 (1.5f, 1.5f, 3);
						
						}
					}
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
		pos.y = 1;
		this.transform.position=pos;
		if (Variable.sleep_desire > 1000) 
			Variable.sleep_desire = 1000;
		else if (Variable.sleep_desire < 0)
			Variable.sleep_desire = 0;
		if (Variable.appetite > 1000) 
			Variable.appetite = 1000;
		else if (Variable.appetite < 0)
			Variable.appetite = 0;
		if (Variable.excretion > 1000) 
			Variable.excretion = 1000;
		else if (Variable.excretion < 0)
			Variable.excretion = 0;
		
		desireSlider[0].value = Variable.appetite/1000f;
		desireSlider[1].value = Variable.excretion/1000f;
		desireSlider[2].value = Variable.sleep_desire/1000f;
		for (int i=0; i<3; ++i) {
			if(desireSlider[i].value>=0.8f)
				fill[i].color = Color.red;
			else if(desireSlider[i].value>=0.6f)
				fill[i].color = Color.yellow;
			else
				fill[i].color = Color.green;
		}
	}
	void desire_up()
	{
        if (sleep_flag == true)
            Variable.sleep_timer++;
        if (sleep_flag == false)
            Variable.sleep_timer = 0;

        //똥차는 량///
        if (food_timer > 0) {
			food_timer++;
			if(food_timer>60*30*4*food_count)
			{
				food_timer=0;
				food_count=0;
			}
			if(food_timer%144==0)
			{
				Variable.excretion++;

			}
		}

        if (Variable.timer >= 43200)//60*30*24 하루 증가
        {
            Variable.timer = 0;
            Variable.day++;
        }
        if (Variable.timer % 60 == 0) { //1초마다 1씩 증가 
			if(sleep_flag==true&&Variable.sleep_timer>6)
			{
				Variable.sleep_desire-=5;
				if(metrix_flag)
					Variable.sleep_desire -= 5;
			}
			else{
				Variable.sleep_desire+=1;
			}
			if(run_flag)
			{
				Variable.sleep_desire+=2;
				Variable.appetite+=2;
			}
			Variable.appetite++;
			desireSlider[0].value = Variable.appetite;
			desireSlider[1].value = Variable.excretion;
			desireSlider[2].value = Variable.sleep_desire;
		}
	}
	void desire_control()
	{
        //식욕
       
        if (Variable.appetite >= 600&&	Variable.appetite < 800) {//라이트 어둡게, 5초당 한번씩 10퍼확률로 잠들게
			if(Variable.timer%300==0)//5초에 한번씩
			{
				if(Random.Range(0,99)==0)//1percent 꼬르륵
				{
					//추가함
					print ("꼬르륵");
				}
			}
		}
		if (Variable.appetite >= 800) {//식욕이 80넘으면 이속 감소
			v = 7f / 10f;
		} else {
			v = 1;
		}
		if (Variable.appetite >= 1000) {//식욕이 100넘으면 gameover
			//gameover
			print ("Game Over");
		}
		//수면욕
		if (Variable.sleep_desire >= 600 && Variable.sleep_desire < 80) {//라이트 어둡게, 5초당 한번씩 10퍼확률로 잠들게
			if(sleep_flag1==false&&sleep_flag2==false)
			{
				int sight1=sight*7/10;
				mainlight.change_range (sight1);
			}
		} else if (Variable.sleep_desire >= 800&&Variable.sleep_desire < 1000) {//라이트 어둡게, 5초당 한번씩 10퍼확률로 잠들게
			if (Variable.timer % 60 == 0) {//1초에 한번씩
				if (Random.Range (0, 99) == 0) {
					sleep_flag = true;
					downSw = true;
					sleep_flag1 = true;
					int sight1=sight/2;
					mainlight.change_range (sight1);
				}
			}
		} else if (Variable.sleep_desire >= 1000) {
			sleep_flag2 = true;
			sleep_flag = true;
			downSw = true;
			int sight1=sight/2;
			mainlight.change_range (sight1);
		} 
		if (Variable.sleep_desire < 300) {
			if(sleep_flag1==true)
			{
				sleep_flag1 = false;
				sight/=2;
			}
			spr.sprite=downSpr[sprCount/10%5];
		}
		if (Variable.sleep_desire == 0) {
			if(sleep_flag2==true)
			{
				sleep_flag2 = false;
				sight/=2;
			}
			spr.sprite=downSpr[sprCount/10%5];
		}
		//배설욕
		if (Variable.excretion >= 600&& Variable.excretion < 800) {
			if (Variable.timer % 300 == 0) {//5초에 한번씩
				if (Random.Range (0, 99) == 0) {
					pooptime++;
				}
			}
		}
		if (Variable.excretion >= 800&&Variable.excretion < 1000) {
			if (Variable.timer % 300 == 0) {//5초에 한번씩
				if (Random.Range (0, 99) == 0) {
					pooptime++;
					Instantiate(poop,this.transform.position,Quaternion.identity);
					Variable.excretion-=20;
				}
			}
		}
		if (Variable.excretion == 1000) {
			Variable.excretion -= 200;
			pooptime++;
			Instantiate (poop, this.transform.position, Quaternion.identity);
			poop_all=true;
		}
		if (poop_all == true && pooptime == 0) {
			pooptime++;
			poop_count++;
			for(int x=poop_count*-1;x<=poop_count;x++)
			{
				Instantiate(poop, this.transform.position + vec, Quaternion.identity);
				/*for (int y=poop_count*-1;y<=poop_count;y++)
				{
					vec.x=x*0.2f;
					vec.z=y*0.2f;
					Instantiate (poop, this.transform.position+vec, Quaternion.identity);

				}*/
			}
			Variable.excretion -= 200;
		}
		if (Variable.excretion == 0) {
			poop_all=false;
			poop_count=0;
		}
	}
}