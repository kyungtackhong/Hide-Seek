using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class inventory : MonoBehaviour
{
    public GameObject thing;
    public int i = 0;
    public Texture bg;
    public Texture tex;
    public Texture select;
    public static int cursor = 0;
    private int timer = 0;
//    private int count = 0;
    private static int maxitem = 4;
    private static bool inventoryWindowToggle = false;
    private Rect inventoryWindowRect;
	public Player com;
    // Use this for initialization
    private int width;
    private int height;
    private int a = 0;
    public struct Item
    {
        public string Name;
        public Texture tex;
        //	public bool Stackable=false;
    }

    private static Item temp_item;

    //public static item[] items = new item[maxitem];
    void Start()
    {
        for (int j = 0; j < maxitem; j++)
        {
        }
        width = (int)(Screen.width * 2 / 10);
        height = (int)(Screen.height * 2 / 10);
    }

    // Update is called once per frame
    void Update()
    {

        if (timer == 0 && inventoryWindowToggle == true)
        {

            if (Input.GetKey(KeyCode.RightArrow))
            {

                cursor += 1;
                if (cursor > maxitem - 1)
                    cursor -= 1;
                inventoryWindowToggle = false;
                inventoryWindowToggle = true;

                timer++;
            }


            if (Input.GetKey(KeyCode.LeftArrow))
            {

                cursor -= 1;
                if (cursor < 0)
                    cursor = 0;

                inventoryWindowToggle = false;
                inventoryWindowToggle = true;

                timer++;

            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                cursor -= 2;
                if (cursor < 0)
                    cursor += 2;

                inventoryWindowToggle = false;
                inventoryWindowToggle = true;

                timer++;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                cursor += 2;
                if (cursor > maxitem - 1)
                    cursor -= 2;

                inventoryWindowToggle = false;
                inventoryWindowToggle = true;

                timer++;
            }
            if (Input.GetKeyUp("z"))//키누르면 slot바꾸기 
            {
				if(Variable.items[cursor].Name=="고기")
				{
					Variable.appetite -= 50;
					if (Variable.appetite < 0)
						Variable.appetite = 0;
					Variable.current_item.Name=null;
					Variable.current_item.tex = (Texture)Resources.Load(Variable.current_item.Name);
					Player.food_timer++;
					Player.food_count++;
					discard_item();
					inventoryWindowToggle = false;
					inventoryWindowToggle = true;
				}
                else if (cursor < Variable.item_count)
                {
                    print("discardItem");
                    discard_item();
                    inventoryWindowToggle = false;
                    inventoryWindowToggle = true;
                }
            }
			if (Input.GetKeyDown("space") && cursor < Variable.item_count)
			{
				if(Variable.items[cursor].Name=="방망이"||Variable.items[cursor].Name=="망치"||Variable.items[cursor].Name=="")
				{
					if (a != 0&&Variable.current_item.Name!=null)
					{
						temp_item.Name = Variable.current_item.Name;
						temp_item.tex = Variable.current_item.tex;
						Variable.current_item.Name = Variable.items[cursor].Name;
						Variable.current_item.tex = Variable.items[cursor].tex;
						Variable.items[cursor].Name = temp_item.Name;
						Variable.items[cursor].tex = temp_item.tex;
						Debug.Log(temp_item.Name);
						
					}
					else
					{
						Variable.current_item.Name = Variable.items[cursor].Name;
						Variable.current_item.tex = Variable.items[cursor].tex;
						deleteItem(Variable.items[cursor].Name);
						
					}
					a++;				

				}
				else{
				//thing.GetComponent<SpriteRenderer>().sprite = Resources.Load(Variable.items[cursor].Name, typeof(Sprite)) as Sprite;
				
				}
			}
        }
        if (timer > 0)
        {
            timer++;
            if (timer > 5)
                timer = 0;
        }
    }

    void OnGUI()
    {
        width = (int)(Screen.width * 4 / 10);
        height = (int)(Screen.height * 4 / 10);
        inventoryWindowRect = new Rect((int)(Screen.width * 3 / 10), (int)(Screen.height * 2 / 10), (int)(Screen.width * 5 * 1.1f / 10), (int)(Screen.height * 6 * 1.1f / 10));
        if (inventoryWindowToggle)
        {
            inventoryWindowRect = GUI.Window(0, inventoryWindowRect, InventoryWindowMethod, "");
        }
    }

    public static void showinventory()
    {
        if (inventoryWindowToggle == true)
        {
            inventoryWindowToggle = false;

        }
        else
        {
            inventoryWindowToggle = true;
            cursor = 0;
        }
    }
    void InventoryWindowMethod(int windowId)
    {
        int boxwidth = (int)(width / 2);
        int boxheight = (int)(width / 2);
        int w = (int)(width / 16);
        int h = (int)(height / 16);
        GUIStyle myStyle = new GUIStyle(GUI.skin.box);
        myStyle.margin = new RectOffset(0, 0, 0, 0);
        myStyle.padding = new RectOffset(0, 0, 0, 0);
        /////배경화면 넣기///////////////////////
        GUI.Box(new Rect(0, 0, width * 5, width * 5), bg);
        //

        GUILayout.BeginArea(new Rect(0, 0, width, (int)(height / 20)));
        GUILayout.BeginVertical();

        GUI.Box(new Rect(0, (int)(height / 20), width, (int)(height / 10)), "inventory");
        GUILayout.EndVertical();
        GUILayout.EndArea();
        GUILayout.BeginArea(new Rect((int)(width / 10), (int)(height / 10), width * 1.2f, (int)(height * 1.6f)));
        GUILayout.BeginHorizontal();
        if (cursor >= 0 && cursor < 2)
        {
            GUI.Box(new Rect(0 + cursor * boxwidth + cursor * w + 5, 5, boxwidth + 10, boxheight + 10), select);
        }
        if (cursor >= 2 && cursor < 4)
        {
            GUI.Box(new Rect(0 + (cursor - 2) * boxwidth + (cursor - 2) * w + 5, boxheight + 1 * h + 5, boxwidth + 10, boxheight + 10), select);
        }
        int x = 0;
        //		int y = 0;


        if (Variable.item_count >= 1)
        {
            GUI.Box(new Rect(0 + x * boxwidth + x * w + 10, 10, boxwidth, boxheight), Variable.items[0].tex);
        }
        else
        {
            GUI.Box(new Rect(0 + x * boxwidth + x * w + 10, 10, boxwidth, boxheight), "");
        }
        x++;
        if (Variable.item_count >= 2)
        {
            //GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
            GUI.Box(new Rect(0 + x * boxwidth + x * w + 10, 10, boxwidth, boxheight), Variable.items[1].tex);
        }
        else
        {
            //GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
            GUI.Box(new Rect(0 + x * boxwidth + x * w + 10, 10, boxwidth, boxheight), "");
        }
        x = 0;
        if (Variable.item_count >= 3)
        {
            GUI.Box(new Rect(0 + x * boxwidth + x * w + 10, boxheight + 1 * h + 10, boxwidth, boxheight), Variable.items[2].tex);
            //GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
        }
        else
        {
            GUI.Box(new Rect(0 + x * boxwidth + x * w + 10, boxheight + 1 * h + 10, boxwidth, boxheight), "");
            //GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
        }
        x++;
        if (Variable.item_count >= 4)
        {
            GUI.Box(new Rect(0 + x * boxwidth + x * w + 10, boxheight + 1 * h + 10, boxwidth, boxheight), Variable.items[3].tex);
            //GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
        }
        else
        {
            GUI.Box(new Rect(0 + x * boxwidth + x * w + 10, boxheight + 1 * h + 10, boxwidth, boxheight), "");
            //GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
        }
        x++;
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
    public static void addItem(string str1)
    {
        Debug.Log("addd");
        if (Variable.item_count != 4)
        {
            Variable.items[Variable.item_count].Name = str1;

            Variable.items[Variable.item_count].tex = (Texture)Resources.Load(str1);
            Variable.item_count++;
			if(str1 == "반지")
			{
				Variable.questSw[1] = true;
			}
			else if(str1 == "열쇠뭉치")
			{
				Variable.questSw[2] = true;
			}
			else if(str1 == "금괴")
			{
				Variable.questSw[3]=true;
			}
			else if(str1 == "102열쇠")
			{
				GameObject.Find ("/Hall1/Doors/door2/Lock2").SetActive(false);
				deleteItem1("102열쇠");
			}
        }
    }
    public void discard_item()
    {
		if (Variable.items [cursor].Name == "반지" || Variable.items [cursor].Name == "열쇠뭉치" || Variable.items [cursor].Name == "코드북") {
			print ("못버림");
		} else {
			for (int j = cursor; j < Variable.item_count; j++) {
				Variable.items [j].tex = Variable.items [j + 1].tex;
				Variable.items [j].Name = Variable.items [j + 1].Name;

			}
			Variable.item_count--;
		}
    }
    public void deleteItem(string str)
    {
        for (int j = 0; j < Variable.item_count; j++)
        {
            if (Variable.items[j].Name == str)
            {
                for (int z = j; z < Variable.item_count; z++)
                {
                    Variable.items[z].tex = Variable.items[z + 1].tex;
                    Variable.items[z].Name = Variable.items[z + 1].Name;
                }
                Variable.item_count--;
                break;
            }
        }
    }
    public static void deleteItem1(string str)
    {
        for (int j = 0; j < Variable.item_count; j++)
        {
            if (Variable.items[j].Name == str)
            {
                for (int z = j; z < Variable.item_count; z++)
                {
                    Variable.items[z].tex = Variable.items[z + 1].tex;
                    Variable.items[z].Name = Variable.items[z + 1].Name;
                }
                Variable.item_count--;
                break;
            }
        }
    }
}