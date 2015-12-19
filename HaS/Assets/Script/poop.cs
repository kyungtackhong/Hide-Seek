using UnityEngine;
using System.Collections;

public class poop : MonoBehaviour {

	public SphereCollider col;
	private int count=0;

	// Update is called once per frame
	void Update () {
		if (Variable.state != 1)
			return;
		if (count <= 600) 
		{
			++count;
			col.radius += 0.1f;
		}
	}
}
