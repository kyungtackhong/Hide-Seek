using UnityEngine;
using System.Collections;

public class food : MonoBehaviour {
	public static float x1,z1;
	public static float x2, z2;
	public static float squre_distance=2800f;
	// Use this for initialization
	void Start () {
		x2 = this.transform.position.x;
		z2 = this.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if (squre_distance <= 2700) {
			Destroy (gameObject);
			//Debug.Log (squre_distance);
			print ("파괴");
		}
			


	}
	public static void food_destroy(float x,float z)
	{
		x1 = x;
		z1 = z;
		squre_distance = (x2 - x1) * (x2 - x1) + (z2 - z1) * (z2 - z1);
	}
}
