using UnityEngine;
using System.Collections;

public static class Variable{

	public static string name;
	public static bool hikiSw = false;
	public static bool hideSw = false;
	public static bool toutSw = false;
	public static bool spySw = false;
	public static bool[] questSw = new bool[5];
	public static bool mapSw = false;
	public static bool itemSw = false;
	public static bool normalSw = false;
	public static string item = "";
	public static int guardSw = 0;
	public static int quest = 0;
	public static int state = 0; // 0 - 대화, 1 - PLAY , 2 - PAUSE
	public static int prevScene = -1;
	public static int scene = -1;// -1 - blackmarket, 0 - hall1, 6 - house 1-1(106호), 1 - house 1-2(101호)
								 // 10 - hall2
								 // 20 - hall3
								 // 30 - hall4
	public static int sleep_desire=200;//수면욕
	public static int appetite=300;//식욕
	public static int excretion=100;//배설욕
	public static long timer = 60*30*6; // 60*30*6
    public static long sleep_timer =0;
	public static int hour;
	public static int min;
    public static int day = 0;
	public struct Item
	{
		public string Name;
		public Texture tex;
		//	public bool Stackable=false;
	}
	public struct objectItem
	{
		public Item[] It;
		public int count;
	}
	public static int sunapjang=20; /// <여기 변수 추가용ㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇ>
	public static int maxitem = 4;
	public static Item current_item;
	public static Item[] items = new Item[maxitem + 1];
	public static int item_count = 0;
	public static objectItem[] bookshelf;
	public static void addItem(objectItem item, string str1)
	{
        if (item.count < 4)
        {
            item.It[item.count].Name = str1;
            item.It[item.count].tex = (Texture)Resources.Load(str1);
            item.count++;
        }
	}
}
