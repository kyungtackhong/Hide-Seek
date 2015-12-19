using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class object_inventory : MonoBehaviour
{
	public static int object_inventory_count = 1;
	public string[] objectitemstring = new string[maxitem];
	public GameObject thing;
	public int i = 0;
	public Texture bg;
	public Texture tex;
	public Texture select;
	
	
	private static int object_item_count = 0;
	public static int cursor = 0;
	private int timer = 0;
//	private int count = 0;
	private static int maxitem = 4;
	private static bool inventoryWindowToggle = false;
	private Rect inventoryWindowRect;
	private static int object_cursor = -1;
	// Use this for initialization
	private int width;
	private int height;
//	private int a = 0;
	public struct Item
	{
		public string Name;
		public Texture tex;
		//	public bool Stackable=false;
	}
	public static Item[] object_items = new Item[maxitem + 1];
	private static Item temp_item;
	
	//public static item[] items = new item[maxitem];
	void Start()
	{
		
		width = (int)(Screen.width * 2 / 10);
		height = (int)(Screen.height * 2 / 10);
		// objectaddItem(objectitemstring[0]);
		/*
        for(int j=0;j< object_inventory_count;j++)
        {
            objectaddItem(objectitemstring[j]);
        } 
        */
		Variable.bookshelf = new Variable.objectItem[Variable.sunapjang];
		for (int j=0; j<Variable.sunapjang; j++) {
			Variable.bookshelf[j].It = new Variable.Item[4];
			Variable.bookshelf[j].count = 0;
		}
		setItem ();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (timer == 0 && inventoryWindowToggle == true)
		{
			if (Input.GetKey(KeyCode.RightArrow))
			{
				
				if (object_cursor < 0)
				{
					cursor += 1;
					if (cursor > maxitem - 1)
					{
						cursor -= 1;
						object_cursor++;
					}
				}
				else
				{
					object_cursor++;
					if (object_cursor > maxitem - 1)
						object_cursor -= 1;
				}
				if (object_cursor > maxitem - 1)
					object_cursor -= 1;
				inventoryWindowToggle = false;
				inventoryWindowToggle = true;
				Debug.Log(cursor);
				timer++;
			}
			
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				if (object_cursor < 0)
				{
					cursor -= 1;
					if (cursor < 0)
						cursor = 0;
				}
				else
				{
					object_cursor -= 1;
				}
				inventoryWindowToggle = false;
				inventoryWindowToggle = true;
				Debug.Log(cursor);
				timer++;
				
			}
			
			if (Input.GetKey(KeyCode.UpArrow))
			{
				if (object_cursor < 0)
				{
					cursor -= 2;
					if (cursor < 0)
						cursor += 2;
				}
				else
				{
					object_cursor -= 2;
					if (object_cursor < -1)
						object_cursor -= 1;
				}
				
				inventoryWindowToggle = false;
				inventoryWindowToggle = true;
				Debug.Log(cursor);
				timer++;
			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
				if (object_cursor < 0)
				{
					cursor += 2;
					if (cursor > maxitem - 1)
						cursor -= 2;
				}
				else
				{
					object_cursor += 2;
					if (object_cursor > maxitem - 1)
						object_cursor -= 2;
				}
				
				inventoryWindowToggle = false;
				inventoryWindowToggle = true;
				Debug.Log(cursor);
				timer++;
			}
			
			if (Input.GetKeyDown("space") && object_cursor < object_item_count && object_cursor > -1)
			{
				if (Variable.item_count < Variable.maxitem)
				{
					inventory.addItem(object_items[object_cursor].Name);
					discard_item();
				}
			}
			if (Input.GetKeyDown("space") && object_cursor < 0)
			{
				if (object_item_count < Variable.maxitem)
				{
					Debug.Log("asdasdsad" + object_item_count);
					addItem(Variable.items[cursor].Name);
					inventory.deleteItem1(Variable.items[cursor].Name);
					
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
		inventoryWindowRect = new Rect((int)(Screen.width * 0.5f / 10), (int)(Screen.height * 0.5f / 10), (int)(Screen.width * 8.5f / 10), (int)(Screen.height * 8.5f / 10));
		if (inventoryWindowToggle)
		{
			inventoryWindowRect = GUI.Window(0, inventoryWindowRect, InventoryWindowMethod, "");
		}
	}
	
	public static void showinventory(int num)
	{
		if (inventoryWindowToggle == true)
		{
			Variable.bookshelf[num].count = object_item_count;
			for (int j = 0; j < object_item_count; j++)
			{
				Variable.bookshelf[num].It[j].Name = object_items[j].Name;
				Variable.bookshelf[num].It[j].tex = object_items[j].tex;
			}
			inventoryWindowToggle = false;
		}
		else
		{
			object_item_count = Variable.bookshelf[num].count;
			for (int j = 0; j < Variable.bookshelf[num].count; j++)
			{
				object_items[j].Name = Variable.bookshelf[num].It[j].Name;
				object_items[j].tex = Variable.bookshelf[num].It[j].tex;
			}
			inventoryWindowToggle = true;
			cursor = 0;
			object_cursor = -1;
		}
	}
	void InventoryWindowMethod(int windowId)
	{
		int boxwidth = (int)(width / 2.5f);
		int boxheight = (int)(width / 2.5f);
		int w = (int)(width / 16);
		int h = (int)(height / 16);
		GUIStyle myStyle = new GUIStyle(GUI.skin.box);
		myStyle.margin = new RectOffset(0, 0, 0, 0);
		myStyle.padding = new RectOffset(0, 0, 0, 0);
		/////배경화면 넣기///////////////////////
		GUI.Box(new Rect(-width * 1.2f, -width * 1.2f, width * 5, width * 5), bg);
		//
		
		GUILayout.BeginArea(new Rect(0, 0, width, (int)(height / 20)));
		GUILayout.BeginVertical();
		
		GUI.Box(new Rect(0, (int)(height / 20), width, (int)(height / 10)), "inventory");
		GUILayout.EndVertical();
		GUILayout.EndArea();
		GUILayout.BeginArea(new Rect((int)(width / 20), (int)(height / 20), width * 2f, (int)(height * 2f)));
		GUILayout.BeginHorizontal();
		// GUI.Box(new Rect(0 + 2.5f * boxwidth + 2.5f * w + 10, 30, 0 + 2.5f * boxwidth + 2.5f * w + 10 + boxwidth, 30 + boxheight), "인벤토리");
		GUI.Box(new Rect(0 + 0.5f * boxwidth + 0.5f * w + 10, boxheight + 1 * h + w * 4 + boxwidth, boxwidth, 30), "인벤토리");
		// GUI.Box(new Rect(0 + x * boxwidth + x * w + width * 1.1f, boxheight + 1 * h + w * 9.3f, boxwidth, boxheight), "");
		GUI.Box(new Rect(0 + 0.5f * boxwidth + 0.5f * w + width * 1.1f, boxheight / 2, boxwidth, 30), PlayerCol_Collision.object_name);
		if (object_cursor < 0)
		{
			if (cursor >= 0 && cursor < 2)
			{
				GUI.Box(new Rect(0 + cursor * boxwidth + cursor * w + 5, w * 2 - 5, boxwidth + 10, boxheight + 10), select);
			}
			if (cursor >= 2 && cursor < 4)
			{
				GUI.Box(new Rect(0 + (cursor - 2) * boxwidth + (cursor - 2) * w + 5, boxheight + 1 * h + w * 2 - 5, boxwidth + 10, boxheight + 10), select);
			}
		}
		else
		{
			if (object_cursor >= 0 && object_cursor < 2)
			{
				GUI.Box(new Rect(0 + object_cursor * boxwidth + object_cursor * w + width * 1.1f - 5, w * 9.3f - 5, boxwidth + 10, boxheight + 10), select);
			}
			if (object_cursor >= 2 && object_cursor < 4)
			{
				GUI.Box(new Rect(0 + (object_cursor - 2) * boxwidth + (object_cursor - 2) * w - 5 + width * 1.1f, boxheight + 1 * h + w * 9.3f - 5, boxwidth + 10, boxheight + 10), select);
			}
		}
		int x = 0;
		//		int y = 0;
		//my item
		
		if (Variable.item_count >= 1)
		{
			GUI.Box(new Rect(0 + x * boxwidth + x * w + 10, w * 2, boxwidth, boxheight), Variable.items[0].tex);
		}
		else
		{
			GUI.Box(new Rect(0 + x * boxwidth + x * w + 10, w * 2, boxwidth, boxheight), "");
		}
		
		
		x++;
		if (Variable.item_count >= 2)
		{
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
			GUI.Box(new Rect(0 + x * boxwidth + x * w + 10, w * 2, boxwidth, boxheight), Variable.items[1].tex);
		}
		else
		{
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
			GUI.Box(new Rect(0 + x * boxwidth + x * w + 10, w * 2, boxwidth, boxheight), "");
		}
		x = 0;
		if (Variable.item_count >= 3)
		{
			GUI.Box(new Rect(0 + x * boxwidth + x * w + 10, boxheight + 1 * h + w * 2, boxwidth, boxheight), Variable.items[2].tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		else
		{
			GUI.Box(new Rect(0 + x * boxwidth + x * w + 10, boxheight + 1 * h + w * 2, boxwidth, boxheight), "");
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		x++;
		if (Variable.item_count >= 4)
		{
			GUI.Box(new Rect(0 + x * boxwidth + x * w + 10, boxheight + 1 * h + w * 2, boxwidth, boxheight), Variable.items[3].tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		else
		{
			GUI.Box(new Rect(0 + x * boxwidth + x * w + 10, boxheight + 1 * h + w * 2, boxwidth, boxheight), "");
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		
		//inventory item
		x = 0;
		
		//		int y = 0;
		
		if (object_item_count >= 1)
		{
			GUI.Box(new Rect(0 + x * boxwidth + x * w + width * 1.1f, w * 9.3f, boxwidth, boxheight), object_items[0].tex);
		}
		else
		{
			GUI.Box(new Rect(0 + x * boxwidth + x * w + width * 1.1f, w * 9.3f, boxwidth, boxheight), "");
		}
		x++;
		if (object_item_count >= 2)
		{
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
			GUI.Box(new Rect(0 + x * boxwidth + x * w + width * 1.1f, w * 9.3f, boxwidth, boxheight), object_items[1].tex);
		}
		else
		{
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUIL/2ayout.Width (boxwidth));
			GUI.Box(new Rect(0 + x * boxwidth + x * w + width * 1.1f, w * 9.3f, boxwidth, boxheight), "");
		}
		x = 0;
		if (object_item_count >= 3)
		{
			GUI.Box(new Rect(0 + x * boxwidth + x * w + width * 1.1f, boxheight + 1 * h + w * 9.3f, boxwidth, boxheight), object_items[2].tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		else
		{
			GUI.Box(new Rect(0 + x * boxwidth + x * w + width * 1.1f, boxheight + 1 * h + w * 9.3f, boxwidth, boxheight), "");
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		x++;
		if (object_item_count >= 4)
		{
			GUI.Box(new Rect(0 + x * boxwidth + x * w + width * 1.1f, boxheight + 1 * h + w * 9.3f, boxwidth, boxheight), object_items[3].tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		else
		{
			GUI.Box(new Rect(0 + x * boxwidth + x * w + width * 1.1f, boxheight + 1 * h + w * 9.3f, boxwidth, boxheight), "");
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}
	public void addItem(string str1)
	{
		object_items[object_item_count].Name = str1;		
		
		object_items[object_item_count].tex = (Texture)Resources.Load(str1);
		object_item_count++;
		
		
	}
	public static void objectaddItem(Variable.objectItem it)
	{
		object_item_count = it.count;
		for (int j = 0; j < it.count; j++)
		{
			object_items[j].Name = it.It[j].Name;
			object_items[j].tex = it.It[j].tex;
			Debug.Log(object_items[j].Name);
		}
	}
	public void discard_item()
	{
		for (int j = object_cursor; j < object_item_count; j++)
		{
			object_items[j].tex = object_items[j + 1].tex;
			object_items[j].Name = object_items[j + 1].Name;
		}
		object_item_count--;
		
	}
	void setItem()//여기 추가 ***********************************************************************************************************
	{
		int n = 0;
		//신발장 1-1 0/////////
		Variable.addItem(Variable.bookshelf[n], "칼");
		Variable.bookshelf[n].count++;
		Variable.addItem(Variable.bookshelf[n], "고기");
		Variable.bookshelf[n].count++;
		//냉장고 1-1 1//////
		n++;
		Variable.addItem(Variable.bookshelf[n], "고기");
		Variable.bookshelf[n].count++;
		Variable.addItem(Variable.bookshelf[n], "고기");
		Variable.bookshelf[n].count++;
		Variable.addItem(Variable.bookshelf[n], "고기");
		Variable.bookshelf[n].count++;
		Variable.addItem(Variable.bookshelf[n], "고기");
		Variable.bookshelf[n].count++;
		//수납장 1-1 2//////
		n++;
		Variable.addItem(Variable.bookshelf[n], "통조림");
		Variable.bookshelf[n].count++;
		Variable.addItem(Variable.bookshelf[n], "통조림");
		Variable.bookshelf[n].count++;
		//수납장 1-2 3//////
		n++;
		Variable.addItem(Variable.bookshelf[n], "방망이");
		Variable.bookshelf[n].count++;
		//서재 1-1 4//////
		n++;
		Variable.addItem(Variable.bookshelf[n], "책1");
		Variable.bookshelf[n].count++;
		Variable.addItem(Variable.bookshelf[n], "책1");
		Variable.bookshelf[n].count++;
		//냉장고 6-1 5//////
		n++;
		Variable.addItem (Variable.bookshelf [n], "고기");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "고기");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "고기");
		Variable.bookshelf [n].count++;
		//신발장 6-1 6//////
		n++;
		Variable.addItem (Variable.bookshelf [n], "칼");
		Variable.bookshelf [n].count++;
		//수납장 6-2 7//////
		n++;
		Variable.addItem (Variable.bookshelf [n], "방망이");
		Variable.bookshelf [n].count++;
		//수납장 6-3 8//////
		n++;
		Variable.addItem (Variable.bookshelf [n], "통조림");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "통조림");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "통조림");
		Variable.bookshelf [n].count++;
		//수납장 6-4 9//////
		n++;
		Variable.addItem (Variable.bookshelf [n], "망치");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "열쇠뭉치");
		Variable.bookshelf [n].count++;
		//서재 6-1 10//////
		n++;
		Variable.addItem (Variable.bookshelf [n], "책1");
		Variable.bookshelf [n].count++;
		//냉장고 3-1 11//////
		n++;
		Variable.addItem (Variable.bookshelf [n], "고기");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "고기");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "고기");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "고기");
		Variable.bookshelf [n].count++;
		//장롱 3-1 12//////
		n++;
		Variable.addItem (Variable.bookshelf [n], "금괴");
		Variable.bookshelf [n].count++;
		//냉장고 4-1 13//////
		n++;
		Variable.addItem (Variable.bookshelf [n], "고기");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "고기");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "고기");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "고기");
		Variable.bookshelf [n].count++;
		//책장4-1 14//////
		n++;
		Variable.addItem (Variable.bookshelf [n], "책1");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "책1");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "책1");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "책1");
		Variable.bookshelf [n].count++;
		//책장 4-2 15//////
		n++;
		Variable.addItem (Variable.bookshelf [n], "책1");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "책1");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "책1");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "책1");
		Variable.bookshelf [n].count++;
		//책장 4-3 16//////
		n++;
		Variable.addItem (Variable.bookshelf [n], "책1");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "코드북");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "책1");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "책1");
		Variable.bookshelf [n].count++;
		//책장 4-4 17//////
		n++;
		Variable.addItem (Variable.bookshelf [n], "책1");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "책1");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "책1");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "책1");
		Variable.bookshelf [n].count++;
		//else 18//////
		n++;
		Variable.addItem (Variable.bookshelf [n], "몽둥이");
		Variable.bookshelf [n].count++;
		Variable.addItem (Variable.bookshelf [n], "칼");
		Variable.bookshelf [n].count++;
	}
}