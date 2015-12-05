using UnityEngine;
using System.Collections;

public class mapmap : MonoBehaviour {
	public static GameObject menu;
	public int i=0;
	
	public Texture tex;
	public Texture bg;
	public Texture select;
	public static Texture2D map2;
	public Texture fmap;
	public static Camera rt;
	public RenderTexture map1;
	private static bool mapWindowToggle= false;
	private Rect mapWindowRect= new Rect(15,20,400,300);
	public static int maptimer=0;
	public static bool capturemap=false;
	private static Canvas CanvasObject;
	private static Vector3 vec;
	// Use this for initialization
	void Start () {
		menu = GameObject.FindWithTag("UI");
		//item_count = 5;
		menu.SetActive(false);
		StartCoroutine (capture ());
		//CanvasObject = GetComponent<Canvas> ();
		//menu = GameObject.FindWithTag("UI");
	}
	
	// Update is called once per frame
	void Update () {
		if (maptimer > 0) {
			maptimer++;
			if(maptimer>5)
			{
				mapWindowToggle = true;
				maptimer=0;
			}
		}
		if (capturemap == true) {
			menu.SetActive(false);
			Camera.main.orthographicSize =50;
			vec=Player.playerposition;
			Player.playerposition -=Player.playerposition;
			Camera.main.transform.position -=vec;
			StartCoroutine (capture ());
			capturemap = false;
		}
	}
	void OnGUI()
	{
		if (mapWindowToggle) {
			mapWindowRect =GUI.Window(0,mapWindowRect,mapWindowMethod,"map");
			
			
		}
	}
	public static void showmap()
	{
		if (mapWindowToggle == true) {
			mapWindowToggle = false;
		} 
		else {
			maptimer++;
			capturemap=true;
			
		}
	}
	void mapWindowMethod(int windowId)
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
		
		
		///map 그리기
		
		GUILayout.BeginArea (new Rect (10, 30, 400, 300));
		GUILayout.BeginHorizontal ();
		GUI.DrawTexture(new Rect(0, -20, 620,320), map2);
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
		
	}
	static IEnumerator capture()
	{
		yield return new WaitForEndOfFrame ();
		//this.camera.orthographicSize += 70;
		map2 = new Texture2D (Screen.width, Screen.height, TextureFormat.ARGB32, false);
		//RenderTexture.active = map1;
		map2.ReadPixels (new Rect (220,0, Screen.width, Screen.height), 0, 0);
		
		map2.Apply ();
		Camera.main.orthographicSize = 30;
		menu.SetActive(true);
		Debug.Log (vec.x);
		Debug.Log (vec.z);
		
		Player.playerposition +=vec;
		Camera.main.transform.position +=vec;
	}
	
}