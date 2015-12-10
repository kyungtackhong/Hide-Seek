using UnityEngine;
using System.Collections;

public static class Variable{

	public static string name;
	public static bool questSw = true;
	public static bool mapSw = false;
	public static int quest = 0;
	public static int state = 0; // 0 - PAUSE, 1 - PLAY
	public static int prevScene = -1;
	public static int scene = -1;// -1 - blackmarket, 0 - hall1, 1 - house 1-1, 2 - house 1-2
								 // 10 - hall2
								 // 20 - hall3
								 // 30 - hall4
	public static int sleep_desire=20;//수면욕
	public static int appetite=30;//식욕
	public static int excretion=10;//배설욕
	public static long timer = 60*30*6; // 60*30*6


}
