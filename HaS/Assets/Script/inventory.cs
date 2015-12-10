using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class inventory : MonoBehaviour {
	public GameObject thing;
	public int i=0;
	public static int item_count;
	public Texture bg;
	public Texture tex;
	public Texture select;
	public static int cursor = 0;
	private int timer=0;
	private int count = 0;
	private static int maxitem=4;
	private static bool inventoryWindowToggle= false;
	private Rect inventoryWindowRect;

	// Use this for initialization
	private int width;
	private int height;
	public struct Item
	{
		public string Name;
		public Texture tex;
	//	public bool Stackable=false;
	}
	public static Item[] items =new Item[maxitem]; 
	//public static item[] items = new item[maxitem];
	void Start () {
		for (int j=0; j< maxitem; j++) { 
		}
		width=(int)(Screen.width*2/10);
		height=(int)(Screen.height*2/10);		
	}
	
	// Update is called once per frame
	void Update () {

		if (timer == 0&&inventoryWindowToggle == true) {
			
			if (Input.GetKey (KeyCode.RightArrow)) {
				
				cursor += 1;
				if(cursor>maxitem-1)
					cursor-=1;
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
				cursor -= 2;
				if(cursor<0)
					cursor+=2;
				
				inventoryWindowToggle = false;
				inventoryWindowToggle = true;
				
				timer++;
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				cursor+=2;
				if(cursor>maxitem-1)
					cursor-=2;
				
				inventoryWindowToggle = false;
				inventoryWindowToggle = true;
				
				timer++;
			}
			if (Input.GetKeyUp ("z"))//키누르면 slot바꾸기 
			{
				if(cursor<item_count)
				{
					print ("discardItem");
					discard_item();
					inventoryWindowToggle = false;
					inventoryWindowToggle = true;
				}
			}
			if (Input.GetKeyDown("space"))
			{
				//thing.
				//thing.renderer.
				//this.renderer.material.mainTexture = items [cursor].tex;
				thing.GetComponent<SpriteRenderer> ().sprite = Resources.Load (items[cursor].Name,typeof(Sprite))as Sprite;;
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
		width=(int)(Screen.width*4/10);
		height=(int)(Screen.height*4/10);
		inventoryWindowRect= new Rect((int)(Screen.width*3/10),(int)(Screen.height*2/10), (int)(Screen.width*5*1.1f/10), (int)(Screen.height*6*1.1f/10));
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
		int boxwidth=(int)(width/2);
		int boxheight=(int)(width/2);
		int w = (int)(width/16);
		int h = (int)(height / 16);
		GUIStyle myStyle = new GUIStyle(GUI.skin.box);
		myStyle.margin = new RectOffset (0, 0, 0, 0);
		myStyle.padding =new RectOffset (0, 0, 0, 0);
		/////배경화면 넣기///////////////////////
		GUI.Box (new Rect (-width*1.2f, -width*1.2f, width*5, width*5), bg);
		//
		
		GUILayout.BeginArea (new Rect (0, 0, width, (int)(height/20)));
		GUILayout.BeginVertical ();
		
		GUI.Box (new Rect (0,(int)(height/20),width,(int)(height/10)), "inventory");
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
		GUILayout.BeginArea (new Rect ((int)(width/10),(int)(height/10), width*1.2f, (int)(height*1.6f)));
		GUILayout.BeginHorizontal ();
		if (cursor >= 0 && cursor < 2) {
			GUI.Box (new Rect (0 + cursor * boxwidth + cursor * w+5, 5, boxwidth+10,boxheight+10), select);
		}
		if (cursor >= 2 && cursor < 4) {
			GUI.Box (new Rect (0 + (cursor - 2) * boxwidth + (cursor - 2) * w+5, boxheight+1*h+5,boxwidth+10,boxheight+10), select);
		}
		int x = 0;
//		int y = 0;

		if (item_count >= 1) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w+10, 10, boxwidth,boxheight), items [0].tex);
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w+10, 10, boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 2) {
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
			GUI.Box (new Rect (0 + x * boxwidth + x * w+10, 10, boxwidth,boxheight), items [1].tex);
		} else {
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
			GUI.Box (new Rect (0 + x * boxwidth + x * w+10, 10 , boxwidth,boxheight), "");
		}
		x = 0;
		if (item_count >= 3) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w+10,boxheight+1*h+10,boxwidth,boxheight), items [2].tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w+10,boxheight+1*h+10,boxwidth,boxheight), "");
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		x++;
		if (item_count >= 4) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w+10,boxheight+1*h+10,boxwidth,boxheight), items [3].tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w+10,boxheight+1*h+10,boxwidth,boxheight), "");
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		x++;
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
		/*
		GUILayout.BeginArea (new Rect ((int)(width/10),(int)(height/10), (int)(width*19/20), (int)(height*19/20)));
		GUILayout.BeginHorizontal ();
		
		
		
		if (cursor >= 0 && cursor < 6) {
			GUI.Box (new Rect (0 + cursor * boxwidth + cursor * w, 0, boxwidth,boxheight), "");
		}
		if (cursor >= 6 && cursor < 12) {
			GUI.Box (new Rect (0 + (cursor - 6) * boxwidth + (cursor - 6) * w, boxheight+1*h,boxwidth,boxheight), "");
		}
		if (cursor >= 12 && cursor < 18) {
			GUI.Box (new Rect (0 + (cursor - 12) * boxwidth + (cursor - 12) * w, 2*boxheight+2*h, boxwidth,boxheight), "");
		}
		if (cursor >= 18 && cursor < 24) {
			GUI.Box (new Rect (0 + (cursor - 18) * boxwidth + (cursor - 18) * w, 3*boxheight+3*h, boxwidth,boxheight), "");
			
		}
		//GUI.DrawTexture(new Rect(0, 0, 100,100), tex);
		int x = 0;
		int y = 0;
		if (item_count >= 1) {
			//tex=item[0].getname;
			//tex = Resources.Load ("Resource/"+item[0].getname ()) as Texture;
			//tex=Resources.Load ("Resource/"+items[0]);
		
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 0, boxwidth,boxheight), items [0].tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 0, boxwidth,boxheight), "");
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		x++;
		if (item_count >= 2) {
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 0, boxwidth,boxheight), tex);
		} else {
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 0, boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 3) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 0, boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 0, boxwidth,boxheight), "");
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		x++;
		if (item_count >= 4) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 0, boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 0, boxwidth,boxheight), "");
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		x++;
		if (item_count >= 5) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 0, boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 0, boxwidth,boxheight), "");
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		x++;
		if (item_count >= 6) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 0, boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 0, boxwidth,boxheight), "");
			//GUILayout.Box ("", GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		}
		
		x = 0;
		y=1;
		
		if (item_count >= 7) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w,boxheight+1*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, boxheight+1*h,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 8) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, boxheight+1*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, boxheight+1*h,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 9) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, boxheight+1*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, boxheight+1*h,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 10) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, boxheight+1*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w,boxheight+1*h,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >=11) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w,boxheight+1*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w,boxheight+1*h,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 12) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, boxheight+1*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w,boxheight+1*h,boxwidth,boxheight), "");
		}
		x = 0;
		
		y++;
		if (item_count >= 13) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w,2*boxheight+2*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 2*boxheight+2*h,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 14) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 2*boxheight+2*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 2*boxheight+2*h,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 15) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 2*boxheight+2*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 2*boxheight+2*h,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 16) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 2*boxheight+2*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 2*boxheight+2*h,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >=17) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 2*boxheight+2*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 2*boxheight+2*h,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 18) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 2*boxheight+2*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 2*boxheight+2*h,boxwidth,boxheight), "");
		}
		x = 0;
		
		y++;
		if (item_count >= 19) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 3*boxheight+3*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 3*boxheight+3*h,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 20) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 3*boxheight+3*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 3*boxheight+3*h,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 21) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w,3*boxheight+3*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w,  3*boxheight+3*h,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 22) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w,  3*boxheight+3*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w,  3*boxheight+3*h,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >=23) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 3*boxheight+3*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w,  3*boxheight+3*h,boxwidth,boxheight), "");
		}
		x++;
		if (item_count >= 24) {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 3*boxheight+3*h,boxwidth,boxheight), tex);
			//GUILayout.Box (tex, GUILayout.Height (boxheight), GUILayout.Width (boxwidth));
		} else {
			GUI.Box (new Rect (0 + x * boxwidth + x * w, 3*boxheight+3*h,boxwidth,boxheight), "");
		}
		x = 0;
		GUILayout.EndHorizontal ();
		y++;
		
		GUILayout.EndArea ();
		*/
	}
	public static void addItem(string str1)
	{
		string str;
		str = str1;
		Debug.Log (str);
		items [item_count].Name = str1;
		Debug.Log ("item:" + items [item_count].Name);
		items [item_count].tex = (Texture)Resources.Load (str1);
		item_count++;
		Debug.Log ("item count" + item_count);
	}
	public void discard_item()
	{
		for (int j=cursor; j<maxitem-1; j++) {
			items [j].tex=items [j+1].tex;
			items [j].Name=items [j+1].Name;
		}
		item_count--;

	}
}