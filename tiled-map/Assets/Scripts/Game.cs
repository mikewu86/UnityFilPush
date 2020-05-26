using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
public class Game : MonoBehaviour {

	[SerializeField]
	Button startBtn;

	[SerializeField]
	Button saveBtn;


	[SerializeField]
	InputField stageIdTxt;


	[SerializeField]
	InputField widthTxt;



	[SerializeField]
	InputField heightTxt;

	[SerializeField]
	InputField timeTxt;

	[SerializeField]
	InputField stepTxt;

	[SerializeField]
	InputField clearTxt;

	[SerializeField]
	InputField initBlockTxt;

	[SerializeField]
	Text alertxt;


	[SerializeField]
	Vector2Int boardSize = new Vector2Int(11, 11);

	[SerializeField]
	GameBoard board = default;

	[SerializeField]
	GameTileContentFactory tileContentFactory = default;

	Ray TouchRay => Camera.main.ScreenPointToRay(Input.mousePosition);

	GameTileContentType currentType = GameTileContentType.Empty;

	private int stageid;
	private int width;

	private int height;

	private int time;

	private int step;

	private int clear;

	private int initBlock;
	
	void Awake () {
		startBtn.onClick.AddListener(Begin);
		saveBtn.onClick.AddListener(Save);
	}

	void Begin(){
		stageid = int.Parse(stageIdTxt.text);
		width = int.Parse(widthTxt.text);
		height = int.Parse(heightTxt.text);
		time = int.Parse(timeTxt.text);
		step = int.Parse(stepTxt.text);
		clear = int.Parse(clearTxt.text);
		initBlock = int.Parse(initBlockTxt.text);
		

		boardSize.x = width;
		boardSize.y = height;
		this.startBtn.gameObject.SetActive(false);
		board.Initialize(boardSize, tileContentFactory);
		board.ShowGrid = true;

		saveBtn.onClick.AddListener(Save);
	}

	void Save(){
		stageid = int.Parse(stageIdTxt.text);
		MySave(stageid);
	}

	void OnValidate () {
		if (boardSize.x < 2) {
			boardSize.x = 2;
		}
		if (boardSize.y < 2) {
			boardSize.y = 2;
		}
	}
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			HandleTouch();
			alertxt.text = "";
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha0)) {
			MyKeyDown(GameTileContentType.Wall);
		}

		if (Input.GetKeyDown(KeyCode.W)) {
			MyKeyDown(GameTileContentType.Wall_1);
		}

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			MyKeyDown(GameTileContentType.Content_1);
		}

		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			MyKeyDown(GameTileContentType.Content_2);
		}

		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			MyKeyDown(GameTileContentType.Content_3);
		}

		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			MyKeyDown(GameTileContentType.Content_4);
		}

		if (Input.GetKeyDown(KeyCode.Alpha5)) {
			MyKeyDown(GameTileContentType.Content_5);
		}

		// if (Input.GetKeyDown(KeyCode.Alpha6)) {
		// 	MyKeyDown(GameTileContentType.ContentType_6);
		// }

		if (Input.GetKeyDown(KeyCode.V)) {
			board.ShowPaths = !board.ShowPaths;
		}
		if (Input.GetKeyDown(KeyCode.G)) {
			board.ShowGrid = !board.ShowGrid;
		}
	}

	

	void HandleTouch () {
		if(this.currentType == GameTileContentType.Empty)
			return;
		GameTile tile = board.GetTile(TouchRay);
		if (tile != null) {
			board.ToggleWall(tile,this.currentType);
			//this.currentType = GameTileContentType.Empty;
		}
	}

	void MyKeyDown(GameTileContentType type){
		this.currentType = type;
	}

	void MySave(int id){
		this.stageid = int.Parse(stageIdTxt.text);
		this.time = int.Parse(timeTxt.text);
		this.step = int.Parse(stepTxt.text);
		this.clear = int.Parse(clearTxt.text);
		this.initBlock = int.Parse(initBlockTxt.text);
		string filePath = Path.Combine(Application.dataPath, "stage_"+id+".txt");

		//string filePath = Application.persistentDataPath + "/Resources/configs/stage_"+id+".txt";

		 Debug.Log(filePath);
	     StageInfo info = new StageInfo(id,this.boardSize.x,this.boardSize.y,this.time,this.step,this.clear,this.initBlock,this.board.Tiles);
		 string json = JsonUtility.ToJson(info);


		StreamWriter writer = new StreamWriter(File.Open(filePath, FileMode.Create));
   
        //将转换好的字符串存进文件，
        writer.WriteLine(json);
        //注意释放资源
        writer.Close();
        writer.Dispose();

		alertxt.text = filePath;
	}
}