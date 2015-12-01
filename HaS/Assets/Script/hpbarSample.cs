using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class hpbarSample : MonoBehaviour {

	public Image hpbar1;//식욕
	public Image hpbar2;//수면욕
	public Image hpbar3;//배설욕


	//테스트 부분
	private int hpMax = 100;
	private int hp1 = 80;
	private int hp2 = 60;
	private int hp3 = 30;

	// Use this for initialization
	void Start () {

		hpbar1.fillAmount = (float)hp1 / hpMax;
		hpbar2.fillAmount = (float)hp2 / hpMax;
		hpbar3.fillAmount = (float)hp3 / hpMax;
	
	}
	
	// Update is called once per frame
	void Update () {
		//한계치는 80% 부분
		hpbar1.fillAmount = (float)hp1 / hpMax;
		hpbar2.fillAmount = (float)hp2 / hpMax;
		hpbar3.fillAmount = (float)hp3 / hpMax;

	
	}
	//TEST
	public void UpHp(){
		hp1 ++;
		hp2 ++;
		hp3 ++;
	}
	public void DownHp(){
		hp1 --;
		hp2 --;
		hp3 --;
	}
}
