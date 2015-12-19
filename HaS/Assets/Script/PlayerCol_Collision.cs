using UnityEngine;
using System.Collections;

public class PlayerCol_Collision : MonoBehaviour {

	public GameObject _player;
	public AudioSource _audio;
	public Player com;
	public AudioClip wallBreak;
	public static Vector3 metrix_vec = new Vector3();
    public static int object_num = 0;
    public static string object_name;
	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.name == "세로침대" && com.sleepSw == true)//여기 추가 ***********************************************************************************************************
		{
			if (com.metrix_flag == false&&com.waitasecond==0)
			{
				metrix_vec.x = col.transform.position.x-this.transform.position.x;
				metrix_vec.z = col.transform.position.z-this.transform.position.z+1;
				metrix_vec.y = 10;
				/*if (com.run_flag)
				{
					metrix_vec *= 2;
					metrix_vec /= 3;
				}*/
				com.transform.position += metrix_vec;
				com.metrix_flag = true;
				com.sleep_flag = true;				
				mainlight.change_range (com.sight/3);
            }	
			return;
		}
		if (col.gameObject.name == "가로침대" && com.sleepSw == true)
		{
			if (com.metrix_flag == false&&com.waitasecond==0)
			{
				metrix_vec.x = col.transform.position.x-this.transform.position.x;
				metrix_vec.z = col.transform.position.z-this.transform.position.z+5;
				metrix_vec.y = 10;
				
				com.transform.position += metrix_vec;
				com.metrix_flag = true;			
				com.sleep_flag = true;
				
				mainlight.change_range (com.sight/3);
            }	
			return;
		}
        if (col.gameObject.tag == "Hide" && com.hideSw == true)
        {
            /*metrix_vec.x = col.transform.position.x - this.transform.position.x;
            metrix_vec.z = col.transform.position.z - this.transform.position.z;
            metrix_vec.y = 10;
            com.transform.position += metrix_vec;*/
			mainlight.change_range (0);
            com.hide_flag = true;
            com.player.SetActive(false);
            print("숨기");
            return;
        }
        if (col.gameObject.name == "변기" && com.poopSw == true)
		{
           
            if (Variable.excretion >= 10)
            {
                com.poopSw = false;
                com.poop_flag = true;
                Variable.excretion -= 20;
                com.pooptime++;
                print("변기에서 누기");

                metrix_vec.x = col.transform.position.x - this.transform.position.x;
                metrix_vec.z = col.transform.position.z - this.transform.position.z;
                metrix_vec.y = 10;
                com.transform.position += metrix_vec;
            }		
            return;
		}
		/*if (col.gameObject.tag == "food" && com.foodSw == true) {
			Destroy (col.gameObject);
			Variable.appetite -= 5;
			if (Variable.appetite < 0)
				Variable.appetite = 0;
		}*/
		if (col.gameObject.tag == "item" && com.foodSw == true) {
			Variable.itemSw = true;
			Variable.item = col.gameObject.name;
			Variable.state = 0;
			Destroy (col.gameObject);
			inventory.addItem (col.gameObject.name);
			com.foodSw=false;
            com.item_flag = true;
            com.thingmove = 0;
        }
		if (col.gameObject.tag == "Hide" && com.foodSw == true) {
			Variable.hideSw = true;
			Variable.item = col.gameObject.name;
			Variable.state = 0;
            com.item_flag = true;
            com.foodSw=false;
            com.thingmove = 0;
        }
		if (col.gameObject.tag == "Object" && com.foodSw == true) {
			Variable.normalSw = true;
			Variable.item = col.gameObject.name;
			Variable.state = 0;
            com.item_flag = true;
            com.foodSw=false;
            com.thingmove = 0;
        }
		if (col.gameObject.tag == "Seek" && com.foodSw == true)//여기 추가 ***********************************************************************************************************
		{
            com.item_flag = true;
            com.thingmove = 0;
            _audio.Stop();		
			if(col.gameObject.name=="신발장1-1")
			{
				object_inventory.showinventory(0);
                object_name = "신발장";
                object_num = 0;
			}
			else if(col.gameObject.name == "냉장고1-1")
			{
				object_inventory.showinventory(1);
                object_name = "냉장고";
                object_num = 1;
            }
			else if(col.gameObject.name == "수납장1-1")
			{
				object_inventory.showinventory(2);
				object_name = "수나방";
				object_num = 2;
			}
			else if(col.gameObject.name == "수납장1-2")
			{
				object_inventory.showinventory(3);
				object_name = "수납장";
				object_num = 3;
			}
			else if(col.gameObject.name == "서재1-1")
			{
				object_inventory.showinventory(4);
				object_name = "서재";
				object_num = 4;
			}
			else if(col.gameObject.name == "냉장고6-1")
			{
				object_inventory.showinventory(5);
                object_name = "냉장고";
                object_num = 5;
            }
			else if(col.gameObject.name == "신발장6-1")
			{
				object_inventory.showinventory(6);
                object_name = "신발장";
                object_num = 6;
            }
			else if(col.gameObject.name == "수납장6-2")
			{
				object_inventory.showinventory(7);
                object_name = "수납장";
                object_num = 7;
            }
			else if(col.gameObject.name == "수납장6-3")
			{
				object_inventory.showinventory(8);
                object_name = "수납장";
                object_num = 8;
            }
			else if(col.gameObject.name == "수납장6-4")
			{
				object_inventory.showinventory(9);
                object_name = "수납장";
                object_num = 9;
            }
			else if(col.gameObject.name == "서재6-1")
			{
				object_inventory.showinventory(10);
                object_name = "서재";
                object_num = 10;
            }
			else if(col.gameObject.name == "냉장고3-1")
			{
				object_inventory.showinventory(11);
                object_name = "냉장고";
                object_num = 11;
            }
			else if(col.gameObject.name == "장롱3-1")
			{
				object_inventory.showinventory(12);
                object_name = "장롱";
                object_num = 12;
            }
			else if(col.gameObject.name == "냉장고4-1")
			{
				object_inventory.showinventory(13);
                object_name = "냉장고";
                object_num = 13;
            }
			else if(col.gameObject.name == "서재4-1")
			{
				object_inventory.showinventory(14);
                object_name = "서재";
                object_num = 14;
            }
			else if(col.gameObject.name == "서재4-2")
			{
				object_inventory.showinventory(15);
                object_name = "서재";
                object_num = 15;
            }
			else if(col.gameObject.name == "서재4-3")
			{
				object_inventory.showinventory(16);
                object_name = "서재";
                object_num = 16;
            }
			else if(col.gameObject.name == "서재4-4")
			{
				object_inventory.showinventory(17);
                object_name = "서재";
                object_num = 17;
            }
			else 
			{
				object_inventory.showinventory(18);
                object_name = "기타";
                object_num = 18;
            }
			if (Player.object_inventory_flag == false)
			{
				Player.object_inventory_flag = true;			
				mainlight.change_range(0);
			}
			else
			{
				Player.object_inventory_flag = false;
				mainlight.change_range(com.sight);
                
			}
		}
		if (col.gameObject.tag == "Broken-Wall" && com.itemSw == true && Variable.current_item.Name == "망치") 
		{
			BoxCollider boxCol = col.gameObject.GetComponent<BoxCollider>();
			if(boxCol.isTrigger==false)
			{
				_audio.clip=wallBreak;
				_audio.Play();
				switch(Variable.scene)
				{
				case 1:
					GameObject.Find ("/102/Walls/Broken101").GetComponent<BoxCollider>().isTrigger=true;
					break;
				case 6:
					GameObject.Find ("/105/Walls/Broken106").GetComponent<BoxCollider>().isTrigger=true;
					break;
				}
				boxCol.isTrigger=true;
			}
		}
		if (col.gameObject.tag == "enemy" && com.itemSw == true && (Variable.current_item.Name == "방망이"||Variable.current_item.Name == "망치"||Variable.current_item.Name == "칼")) 
		{
			Enemy_Trace ent = col.gameObject.GetComponent<Enemy_Trace>();
			ent.downSw=true;
			ent.downtimer = 180;
		}
	}
}
