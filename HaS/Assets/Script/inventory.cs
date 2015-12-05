using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class inventory : MonoBehaviour {
	public int i=0;
	public Texture bg;
	public Texture tex;
	public Texture select;
	public static int item_count = 0;
	public static int cursor = 0;
	private int timer=0;
	private int count = 0;
	private static int maxitem=23;
	private static bool inventoryWindowToggle= false;
	private Rect inventoryWindowRect= new Rect(15,20,400,300);
	public static string[] items = new string[maxitem];
	//public static Item[] item;
	//public static List<string> Items = new List<string>();
	// Use this for initialization
	void Start () {
		//item_count = 5;
		//item = new Item[maxitem];
		// item = new Item[maxitem];
		item_count = 0;
		//item [item_count].setname ("haha");
		
	}
	
	// Update is called once per frame
	void Update () {
		if (timer == 0&&inventoryWindowToggle == true) {
			
			if (Input.GetKey (KeyCode.RightArrow)) {
				
				cursor += 1;
				if(cursor>maxitem)
					cursor=maxitem;
				inventoryWindowToggle = false;
				inventoryWindowToggle = true;
				
				timer++;
			}
			
			
			if (Input.GetKey (KeyCode.LeftArrow)) {
				
				cursor -= 1;
				if(cursor <0)
					cursor=0;
				
				inventoryWindowToggle = false;
				inventoryWindowToggle = true;
				
				timer++;
				
			}
			
			if (Input.GetKey (KeyCode.UpArrow)) {
				cursor -= 6;
				if(cursor<0)
					cursor+=6;
				
				inventoryWindowToggle = false;
				inventoryWindowToggle = true;
				
				timer++;
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				cursor+=6;
				if(cursor>maxitem)
					cursor-=6;
				
				inventoryWindowToggle = false;
				inventoryWindowToggle = true;
				
				timer++;
				
				
			}
			if(Input.GetKey (KeyCode.Alpha1))//키누르면 slot바꾸기 
			{
				print ("111111111111");
			}
			if(Input.GetKey (KeyCode.Alpha2))
			{
				print ("222222222222");
			}
			if(Input.GetKey (KeyCode.Alpha3))
			{
				print ("333333333333");
			}
			if(Input.GetKey (KeyCode.Alpha4))
			{
				print ("444444444444");
			}
		}
		if (timer > 0) {
			timer++;
			if(timer>5)
				timer=0;
		}
	}
	
	void OnGUI()
	{
		if (inventoryWindowToggle) {
			inventoryWindowRect =GUI.Window(0,inventoryWindowRect,InventoryWindowMethod,"");
		}
	}
	public static void showinventory()
	{
		if (inventoryWindowToggle == true) {
			inventoryWindowToggle = false;
			
		} 
		else {
			inventoryWindowToggle = true;
			cursor=0;
		}
	}
	void InventoryWindowMethod(int windowId)
	{
		GUIStyle myStyle = new GUIStyle(GUI.skin.box);
		myStyle.margin = new RectOffset (0, 0, 0, 0);
		myStyle.padding =new RectOffset (0, 0, 0, 0);
		/////배경화면 넣기///////////////////////
		GUI.Box (new Rect (0, 0, 510, 410), bg);
		//
		GUILayout.BeginArea (new Rect (0, 0, 400, 300));
		
		//GUILayout.Box (bg, GUILayout.Height (300), GUILayout.Width (400));
		
		GUILayout.EndArea ();
		
		
		
		GUILayout.BeginArea (new Rect (0, 0, 400, 30));
		GUILayout.BeginVertical ();
		
		GUI.Box (new Rect (100,10, 200, 20), "inventory");
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
		
		GUILayout.BeginArea (new Rect (10, 40, 380, 300));
		GUILayout.BeginHorizontal ();
		
		if (cursor >= 0 && cursor < 6) {
			GUI.Box (new Rect (0 + cursor * 60 + cursor * 4, 0, 60, 60), "");
		}
		if (cursor >= 6 && cursor < 12) {
			GUI.Box (new Rect (0 + (cursor - 6) * 60 + (cursor - 6) * 4, 64, 60, 60), "");
		}
		if (cursor >= 12 && cursor < 18) {
			GUI.Box (new Rect (0 + (cursor - 12) * 60 + (cursor - 12) * 4, 128, 60, 60), "");
		}
		if (cursor >= 18 && cursor < 24) {
			GUI.Box (new Rect (0 + (cursor - 18) * 60 + (cursor - 18) * 4, 192, 60, 60), "");
			
		}
		if (item_count >= 1) {
			//tex=item[0].getname;
			//tex = Resources.Load ("Resource/"+item[0].getname ()) as Texture;
			//tex=Resources.Load ("Resource/"+items[0]);
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 2) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 3) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 4) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 5) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 6) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		if (item_count >= 7) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 8) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 9) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 10) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 11) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 12) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		if (item_count >= 13) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 14) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 15) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 16) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 17) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 18) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		if (item_count >= 19) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 20) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (75));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 21) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 22) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 23) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		if (item_count >= 24) {
			GUILayout.Box (tex, GUILayout.Height (60), GUILayout.Width (60));
		} else {
			GUILayout.Box ("", GUILayout.Height (60), GUILayout.Width (60));
		}
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
		
	}
	public static void addItem(string str1)
	{
		//	item [item_count].setname (str1);
		//item [item_count].setTexture ();
		items [item_count] = str1;
		//Items.Add (str1);
		item_count++;
		
	}
	
}