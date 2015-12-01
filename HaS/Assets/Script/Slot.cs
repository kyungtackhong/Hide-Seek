using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Slot : MonoBehaviour {
	public Image slot1;
	public Image slot2;
	public Image slot3;
	public Image slot4;
	public Image handSlot;

	public Sprite item;

	private bool handStatus=false;
	// Use this for initialization
	void Start () {
		item= Resources.Load<Sprite> ("test");

	}
	
	// Update is called once per frame
	void Update () {

		if (handStatus) {
			//들고있는 이미지
			//setHandSlot(img);
		} else {
			setHandSlot(Resources.Load<Sprite>("hand"));
		}
	
	
	}

	public void quickSlotToHandSlot(Image quickSlot){
		handStatus=true;
		Sprite temp = handSlot.sprite;
		handSlot.sprite = quickSlot.sprite;
		quickSlot.sprite = temp;
	}


	void setSlot1(Sprite item){
		slot1.sprite = item;
	}
	void setSlot2(Sprite item){
		slot2.sprite = item;
	}
	void setSlot3(Sprite item){
		slot3.sprite = item;
	}
	void setSlot4(Sprite item){
		slot4.sprite = item;
	}
	void setHandSlot(Sprite img){
		handSlot.sprite = img;
	}
}
