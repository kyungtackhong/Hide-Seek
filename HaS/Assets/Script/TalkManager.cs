using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TalkManager : MonoBehaviour {

	public GameObject _player;
	public GameObject _blackTalk;
	public GameObject _talkImg;
	public Text _name;
	public Text _context;
	public AudioSource _audio;
	private int count = 0;


	// Update is called once per frame
	void Update () {
		if (Variable.state == 2)
		{
			return;
		}
		if (Variable.state == 1)
		{
			if(Vector3.Distance (_blackTalk.transform.position, _player.transform.position) < 5 && Input.GetKeyDown(KeyCode.Space))
			{
				Variable.state = 0;
				count = 0;
			}
			else
			{
				return;
			}
		}
		if (Variable.itemSw == true) { // 아이템 습득
			if(count == 0)
			{
				_talkImg.SetActive(true);
				_name.text = "시스템";
				_context.text = Variable.item + " 획득!";
			}
			if(Input.GetKeyDown(KeyCode.Space))
			{
				_audio.Stop ();
				_audio.Play ();
				++count;
				if(count==1)
				{
					_talkImg.SetActive(false);
					Variable.state = 1;
					Variable.itemSw=false;
					Variable.item="";
					count = 0;
				}
			}
			return;
		}
		if (Variable.normalSw == true) { // 노말 아이템 탐색
			if(count == 0)
			{
				_talkImg.SetActive(true);
				_name.text = "시스템";
				_context.text = "평범한 " + Variable.item + "이다.";
			}
			if(Input.GetKeyDown(KeyCode.Space))
			{
				++count;
				_audio.Stop ();
				_audio.Play ();
				if(count==1)
				{
					_talkImg.SetActive(false);
					Variable.state = 1;
					Variable.normalSw=false;
					Variable.item="";
					count = 0;
				}
			}
			return;
		}
		if (Variable.hideSw == true) { // 노말 아이템 탐색
			if(count == 0)
			{
				_talkImg.SetActive(true);
				_name.text = "시스템";
				_context.text = "숨을 수 있는 " + Variable.item + "이다.";
			}
			if(Input.GetKeyDown(KeyCode.Space))
			{
				++count;
				_audio.Stop ();
				_audio.Play ();
				if(count==1)
				{
					_talkImg.SetActive(false);
					Variable.state = 1;
					Variable.hideSw=false;
					Variable.item="";
					count = 0;
				}
			}
			return;
		}
		if(Variable.toutSw==true) // 삐끼한테 잡힘
		{
			if(count==0)
			{
				_talkImg.SetActive(true);
				_name.text = "삐끼";
				_context.text = "『호오 여기로 찾아오다니 너 내 제자가 되라!』";
				++count;
			}
			if(Input.GetKeyDown(KeyCode.Space))
			{
				++count;
				_audio.Stop ();
				_audio.Play ();
				switch(count)
				{
				case 2:
					_name.text = "시스템";
					_context.text = "삐끼의 제자가 되어 영원히 삐끼질을 하게 되었습니다";
					break;
				case 3:
					_context.text = " GAME OVER";
					break;
				case 4:
					_context.text = "space를 한 번 더누르면 자동으로 재시작 됩니다.";
					break;
				case 5:
					Application.LoadLevel("Floor1Scene");
					break;
				}
			}
			return;
		}
		if(Variable.spySw==true) // 간첩한테 잡힘
		{
			if(count==0)
			{
				_talkImg.SetActive(true);
				_name.text = "간첩";
				_context.text = "『네 놈은 누구냐!』";
				++count;
			}
			if(Input.GetKeyDown(KeyCode.Space))
			{
				++count;
				_audio.Stop ();
				_audio.Play ();
				switch(count)
				{
				case 2:
					_name.text = "";
					_context.text = "(퍽퍽퍽퍽퍽퍽)";
					break;
				case 3:
					_name.text = "시스템";
					_context.text = "간첩한테 발각되어 봉변을 당했습니다.";
					break;
				case 4:
					_context.text = "GAME OVER";
					break;
				case 5:
					_context.text = "space를 한 번 더누르면 자동으로 재시작 됩니다.";
					break;
				case 6:
					Application.LoadLevel("Floor1Scene");
					break;
				}
			}
			return;
		}
		if(Variable.hikiSw==true) // 히키코모리와의 대화
		{
			if(count == 0)
			{
				_talkImg.SetActive(true);
				_name.text = "히키코모리";
				_context.text = "『당장 꺼져!』";
				++count;
			}
			if(Input.GetKeyDown(KeyCode.Space))
			{
				++count;
				_audio.Stop ();
				_audio.Play ();
				switch(count)
				{
				case 2:
					_name.text = "시스템";
					_context.text = "101호가 문을 바꿔서 문으로 입장할 수 없게 됩니다.";
					break;
				case 3:
					_context.text = "반지를 먹지 못했다면 다시 시작해주세요.";
					break;
				case 4:
					_talkImg.SetActive(false);
					Variable.state = 1;
					Variable.hikiSw=false;
					count = 0;
					break;	
				}
			}
			return;
		}
		if (Variable.guardSw == 1) { // 경비아저씨와의 대화
			if (count == 0) {
				_talkImg.SetActive (true);
				_name.text = "경비아저씨";
				_context.text = "『쓰러져 있던 걸 우리집에서 보살펴 주었단다.』";
				++count;
			}
			if (Input.GetKeyDown (KeyCode.Space)) {
				++count;
				_audio.Stop ();
				_audio.Play ();
				switch (count) {
				case 2:
					_context.text = "『회복된 것 같으니 얼른 집으로 돌아가렴.』";
					break;
				case 3:
					_context.text = "『다음에 또 그러고 있으면 경찰아저씨보고 데려가라고 할거야.』";
					break;
				case 4:
					_talkImg.SetActive (false);
					Variable.state = 1;
					Variable.guardSw = 0;
					count = 0;
					break;	
				}
			}
			return;
		} else if (Variable.guardSw == 2) { //경비 아저씨와 대화 2
			if (count == 0) {
				_talkImg.SetActive (true);
				_name.text = "경비아저씨";
				_context.text = "『인명피해도 재산피해도 없으니 한 번은 봐줄게.』";
				++count;
			}
			if (Input.GetKeyDown (KeyCode.Space)) {
				++count;
				_audio.Stop ();
				_audio.Play ();
				switch (count) {
				case 2:
					_context.text = "『하지만 다음에 또 걸리면 바로 경찰아저시보고 데려가라고 할거야.』";
					break;
				case 3:
					_talkImg.SetActive (false);
					Variable.state = 1;
					Variable.guardSw = 0;
					count = 0;
					break;	
				}
			}
			return;
		} else if (Variable.guardSw == 3) {
			if (count == 0) {
				_talkImg.SetActive (true);
				_name.text = "경비아저씨";
				_context.text = "『이 녀석 또 걸리면 경찰아저씨 부른다고 했지!.』";
				++count;
			}
			if (Input.GetKeyDown (KeyCode.Space)) {
				++count;
				_audio.Stop ();
				_audio.Play ();
				switch (count) {
				case 2:
					_name.text = "시스템";
					_context.text = "경비 아저씨가 경찰 아저씨에게 "+Variable.name + "를 넘겼습니다.";
					break;
				case 3:
					_context.text = " GAME OVER";
					break;
				case 4:
					_context.text = "space를 한 번 더누르면 자동으로 재시작 됩니다.";
					break;
				case 5:
					Application.LoadLevel("Floor1Scene");
					break;
				}
			}
			return;
		}
		if (Variable.questSw[Variable.quest] != true) { // 퀘스트 아직 못 깼을 때 
			if(count == 0)
			{
				_talkImg.SetActive(true);
				_name.text = "암상인";
				_context.text = "『빨리가게나』";
			}
			if(Input.GetKeyDown(KeyCode.Space))
			{
				++count;
				_audio.Stop ();
				_audio.Play ();
				if(count==2)
				{
					_talkImg.SetActive(false);
					Variable.state = 1;
					count = 0;
				}
			}
			return;
		}
		switch(Variable.quest)
		{
		case 0: // 퀘스트 1
			if(Input.GetKeyDown(KeyCode.Space))
			{
				++count;
				_audio.Stop ();
				_audio.Play ();
				switch(count)
				{
				case 0:
					_context.text = "『저기 밖에 쓰럭져있는것을 여기로 데려온게 나다.』";
					break;
				case 1:
					_context.text = "『밖을 보아하니 도망친 고아원에서 너를 애타게 찾고 있더군.』";
					break;
				case 2:
					_context.text = "『내 부탁을 들어주면 다시 고아원에 들어가지 않도록 도와주겠다.』";
					break;
				case 3:
					_name.text=Variable.name;
					_context.text = "『정말?』";
					break;
				case 4:
					_name.text = "암상인";
					_context.text = "『물론이지. 이 열쇠를 받아라.』";
					break;
				case 5:
					_name.text = "시스템";
					_context.text = "101호 열쇠 획득!";
					break;
				case 6:
					_name.text = "암상인";
					_context.text = "『저기 101호로 가서 반지를 훔쳐와라.』";
					break;
				case 7:
					_name.text = "시스템";
					_context.text = "퀘스트 : 101호에서 반지를 훔쳐와라!";
					break;
				case 8:
					_name.text = Variable.name;
					_context.text = "(끄덕끄덕)";
					break;
				case 9:
					_name.text = "암상인";
					_context.text = "『자 이걸 주지.』";
					break;
				case 10:
					_context.text = "『일을 하는데 도움이 될꺼야.』";
					break;
				case 11:
					_name.text = "시스템";
					_context.text = "시계 획득!!!";
					break;
				case 12:
					_context.text = "암상인의 방은 0~9시에만 들어올 수 있습니다.";
					break;
				case 13:
					_context.text = "아이템은 z를 통해 주울 수 있습니다.";
					break;
				case 14:
					_talkImg.SetActive(false);
					Variable.quest = 1;
					Variable.state=1;
					count = 0;
					break;
				}
			}
			break;
		case 1: // 퀘스트 2 주는 대화
			if(count == 0)
			{
				_talkImg.SetActive(true);
				_player.GetComponent<inventory>().deleteItem("반지");
				_name.text = "암상인";
				_context.text = "『반지를 구해오다니 훌륭하군.』";
			}
			if(Input.GetKeyDown(KeyCode.Space))
			{
				++count;
				_audio.Stop ();
				_audio.Play ();
				switch(count)
				{
				case 2:
					_context.text = "『자 다음 일거리다.』";
					break;
				case 3:
					_name.text = Variable.name;
					_context.text = "(암상인을 째려본다.)";
					break;
				case 4:
					_name.text = "암상인";
					_context.text = "『왜 그런 눈으로 쳐다보는거냐?』";
					break;
				case 5:
					_context.text = "『나는 부탁이 하나라고 한 적이 없다.』 ";
					break;
				case 6:
					_context.text = "『이번엔 이걸 받거라.』";
					break;
				case 7:
					_name.text = "시스템";
					_context.text = "106호 열쇠 획득!";
					break;
				case 8:
					_name.text = "암상인";
					_context.text = "『106호로 가서 열쇠뭉치를 가져와라.』";
					break;
				case 9:
					_name.text = "시스템";
					_context.text = "퀘스트 : 열쇠뭉치를 훔쳐와라!";
					break;
				case 10:
					_name.text = "암상인";
					_context.text = "『내가 한 부탁을 잘 해결했으니 이걸주마.』";
					break;
				case 11:
					_name.text = "시스템";
					_context.text = "지도 획득!";
					break;
				case 12:
					_context.text = "m키를 통해 현재 있는 위치의 지도를 볼 수 있습니다.";
					break;
				case 13:
					_talkImg.SetActive(false);
					GameObject.Find ("/Hall1/Doors/door6/Lock6").SetActive(false);
					Variable.mapSw=true;
					Variable.quest = 2;
					Variable.state=1;
					count = 0;
					break;
				}
			}
			break;
		case 2: // 퀘스트 2 주는 대화
			if(count == 0)
			{
				_talkImg.SetActive(true);
				_player.GetComponent<inventory>().deleteItem("열쇠뭉치");
				_name.text = "암상인";
				_context.text = "『열쇠뭉치를 구해오다니 잘 했군.』";
			}
			if(Input.GetKeyDown(KeyCode.Space))
			{
				++count;
				_audio.Stop ();
				_audio.Play ();
				switch(count)
				{
				case 2:
					_context.text = "『다음 임무다.』";
					break;
				case 3:
					_context.text = "『102호에 가서 금괴를 훔쳐와라.』";
					break;
				case 4:
					_name.text = Variable.name;
					_context.text = "『열쇠는?』";
					break;
				case 5:
					_name.text = "암상인";
					_context.text = "『이제 열쇠는 주지 않는다.』 ";
					break;
				case 6:
					_context.text = "『대신 이걸 받거라.』";
					break;
				case 7:
					_name.text = "시스템";
					_context.text = "망치 획득!";
					break;
				case 8:
					_name.text = "암상인";
					_context.text = "『어디에 쓸 지는 알아서 생각해.』";
					break;
				case 9:
					_name.text = "시스템";
					_context.text = "Hint : 벽 앞에서 망치를 사용해 보세요.";
					break;
				case 10:
					_name.text = "암상인";
					_context.text = "『빨리 102호에 가서 금괴를 훔쳐오거라.』";
					break;
				case 11:
					_name.text = "시스템";
					_context.text = "퀘스트 : 금괴를 훔쳐와라!";
					break;
				case 12:
					_talkImg.SetActive(false);
					inventory.addItem("망치");
					Variable.quest = 3;
					Variable.state=1;
					count = 0;
					break;
				}
			}
			break;
		case 3: // 퀘스트 2 주는 대화
			if(count == 0)
			{
				_talkImg.SetActive(true);
				_player.GetComponent<inventory>().deleteItem("금괴");
				_name.text = "암상인";
				_context.text = "『금괴를 훔쳐오다니 잘 했군.』";
			}
			if(Input.GetKeyDown(KeyCode.Space))
			{
				++count;
				_audio.Stop ();
				_audio.Play ();
				switch(count)
				{
				case 2:
					_context.text = "『다음 임무다.』";
					break;
				case 3:
					_context.text = "『105호에 간첩이 사는 것 같다.』";
					break;
				case 4:
					_context.text = "『간첩이 숨겨놓은 코드북을 가져와라.』";
					break;
				case 5:
					_context.text = "『그걸로 신고가 되서 간첩이 잡혀가면 그 집을 쓸 수 있을지도 모르지.』 ";
					break;
				case 6:
					_name.text = "시스템";
					_context.text = "퀘스트 : 간첩의 코드북을 훔쳐와라!";
					break;
				case 7:
					_name.text = "암상인";
					_context.text = "『이건 저번 임무에 대한 보상이다.』";
					break;
				case 8:
					_name.text = "암상인";
					_context.text = "『이거 먹고 빨리 가서 일해라.』";
					break;
				case 9:
					_name.text = "시스템";
					_context.text = "고기 획득!";
					break;
				case 10:
					_talkImg.SetActive(false);
					inventory.addItem("고기");
					Variable.quest = 4;
					Variable.state=1;
					count = 0;
					break;
				}
			}
			break;
		}
	}
}

