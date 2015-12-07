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
	//private Rect inventoryWindowRect= new Rect(15,20,400,300);
	//private Rect inventoryWindowRect= new Rect(100,100,100,100);
	private Rect inventoryWindowRect;
	public static string[] items = new string[maxitem];
	//public static Item[] item;
	//public static List<string> Items = new List<string>();
	// Use this for initialization
	private int width;
	private int height;
	
	void Start () {
		//item_count = 5;
		//item = new Item[maxitem];
		// item = new Item[maxitem];
		width=Screen.width*7/10;
		height=Screen.height*7/10;
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
		inventoryWindowRect= new Rect(Screen.width/10,Screen.height/10, Screen.width*8/10, Screen.height*8/10);
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
		int boxwidth=width*7/5/10;
		int boxheight=width*7/5/10;
		
		GUIStyle myStyle = new GUIStyle(GUI.skin.box);
		myStyle.margin = new RectOffset (0, 0, 0, 0);
		myStyle.padding =new RectOffset (0, 0, 0, 0);
		/////배경화면 넣기///////////////////////
		GUI.Box (new Rect (0, 0, width*5, height*5), bg);
		//
		
		GUILayout.BeginArea (new Rect (0, 0, width, height/20));
		GUILayout.BeginVertical ();
		
		GUI.Box (new Rect (0,height/20,width,height/10), "inventory");
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
		
		GUILayout.BeginArea (new Rect (width/10,height/10, width*19/20, height*19/20));
		GUILayout.BeginHorizontal ();
		
		
		
		if (cursor >= 0 && cursor < 6) {
			GUI.Box (new Rect (0 + cursor * boxwidth + cursor * width/75, 0, boxwidth,boxheight), "");
		}
		if (cursor >= 6 && cursor < 12) {
			GUI.Box (new Rect (0 + (cursor - 6) * boxwidth + (cursor - 6) * width/75, boxheight+1*height/55,boxwidth,boxheight), "");
		}
		if (cursor >= 12 && cursor < 18) {
			GUI.Box (new Rect (0 + (cursor - 12) * boxwidth + (cursor - 12) * width/75, 2*boxheight+2*height/55, boxwidth,boxheight), "");
		}
		if (cursor >= 18 && cursor < 24) {
			GUI.Box (new Rect (0 + (cursor - 18) * boxwidth + (cursor - 18) * width/75, 3*boxheight+3*height/55, boxwidth,boxheight), "");
			
		}
		
		int x = 0;
		int y = 0;
		if (item_count >= 1) {
			//tex=item[0].getname;
			//tex = Resources.Load ("Resource/"+item[0].getname ()) as Texture;
			//tex=Resources.Load ("Resource/"+items[0]);
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 0, boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 0, boxwidth,boxheight), "");
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		x++;
		if (item_count >= 2) {
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 0, boxwidth,boxheight), tex);
		} else {
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 0, boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 3) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 0, boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 0, boxwidth,boxheight), "");
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		x++;
		if (item_count >= 4) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 0, boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 0, boxwidth,boxheight), "");
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		x++;
		if (item_count >= 5) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 0, boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 0, boxwidth,boxheight), "");
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		x++;
		if (item_count >= 6) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 0, boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 0, boxwidth,boxheight), "");
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		
		x = 0;
		y=1;
		
		if (item_count >= 7) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75,boxheight+1*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, boxheight+1*height/55,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 8) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, boxheight+1*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, boxheight+1*height/55,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 9) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, boxheight+1*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, boxheight+1*height/55,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 10) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, boxheight+1*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75,boxheight+1*height/55,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >=11) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75,boxheight+1*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75,boxheight+1*height/55,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 12) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, boxheight+1*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75,boxheight+1*height/55,boxwidth,boxheight), "");
		}
		x = 0;
		
		y++;
		if (item_count >= 13) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75,2*boxheight+2*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 2*boxheight+2*height/55,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 14) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 2*boxheight+2*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 2*boxheight+2*height/55,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 15) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 2*boxheight+2*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 2*boxheight+2*height/55,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 16) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 2*boxheight+2*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 2*boxheight+2*height/55,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >=17) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 2*boxheight+2*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 2*boxheight+2*height/55,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 18) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 2*boxheight+2*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 2*boxheight+2*height/55,boxwidth,boxheight), "");
		}
		x = 0;
		
		y++;
		if (item_count >= 19) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 3*boxheight+3*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 3*boxheight+3*height/55,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 20) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 3*boxheight+3*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 3*boxheight+3*height/55,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 21) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75,3*boxheight+3*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75,  3*boxheight+3*height/55,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 22) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75,  3*boxheight+3*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75,  3*boxheight+3*height/55,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >=23) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 3*boxheight+3*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75,  3*boxheight+3*height/55,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 24) {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 3*boxheight+3*height/55,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * width/75, 3*boxheight+3*height/55,boxwidth,boxheight), "");
		}
		x = 0;
		GUILayout.EndHorizontal ();
		y++;
		
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