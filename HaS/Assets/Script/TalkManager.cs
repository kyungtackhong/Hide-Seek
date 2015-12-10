using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TalkManager : MonoBehaviour {

	public GameObject _player;
	public GameObject _blackTalk;
	public GameObject _talkImg;
	public Text _name;
	public Text _context;

	private int count = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Variable.state == 1)
		{
			if(Vector3.Distance (_blackTalk.transform.position, _player.transform.position) < 5 && Input.GetKeyDown ("z"))
			{
				Variable.state = 0;
				count = 0;
			}
			else
			{
				return;
			}
		}
		if (Variable.questSw == false) {
			if(count == 0)
			{
				_talkImg.SetActive(true);
				_name.text = "암상인";
				_context.text = "『빨리가게나』";
			}
			if(Input.GetKeyDown("z"))
			{
				++count;
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
		case 0:
			if(Input.GetKeyDown("z"))
			{
				++count;
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
					_context.text = "아이템은 c를 통해 주울 수 있습니다.";
					break;
				case 14:
					_talkImg.SetActive(false);
					Variable.quest = 1;
					Variable.questSw=false;
					Variable.state=1;
					count = 0;
					break;
				}
			}
			break;
		case 1:
			_talkImg.SetActive(true);
			_name.text = "시스템";
			_context.text = "반지 획득";
			if(Input.GetKeyDown("z"))
			{
				_talkImg.SetActive(false);
				Variable.quest = 2;
				Variable.state=1;
				count = 0;
			}
			break;
		case 2:
			if(count == 0)
			{
				_talkImg.SetActive(true);
				_name.text = "암상인";
				_context.text = "『반지를 구해오다니 훌륭하군.』";
			}
			if(Input.GetKeyDown("z"))
			{
				++count;
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
					Variable.questSw=false;
					Variable.mapSw=true;
					Variable.quest = 3;
					Variable.state=1;
					count = 0;
					break;
				}
			}
			break;
		}

	}
}

