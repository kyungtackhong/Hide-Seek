using UnityEngine;
using System.Collections;

public static class Variable{

	public static int prevScene = -1;
	public static int scene = 0; // 0 - hall1, 1 - house 1-1, 2 - house 1-2
								 // 10 - hall2
								 // 20 - hall3
								 // 30 - hall4
	public static Vector3 playerPos;
	public static int sleep_desire=15;//수면욕
	public static int appetite=15;//식욕
	public static int excretion=15;//배설욕
	public static int timer = 0;


}
