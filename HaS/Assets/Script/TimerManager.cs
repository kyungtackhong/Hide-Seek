using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {


	public Image timeTable;
	public Text hourText;
	public Text minText;

	private int hour=0;
	private int min=0;

	private bool haveWatch=true;
	private static float total;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//시계 존재 유무
		if (haveWatch) {
			timeTable.gameObject.SetActive(true);
		} else {
			timeTable.gameObject.SetActive(false);
		}

		//테스트 부분
		if (min % 3 == 0) {
			haveWatch = false;
		} else {
			haveWatch = true;
		}
		//


		//시간출력
		total = Time.time*10;
		hour = ((int)total / 480)%24;
		min = ((int)total / 8)%60;

		hourText.text = hour + "";
		minText.text = min + "";

	}

	void setHavaWatch(bool b){
		haveWatch = b;
	}
}
