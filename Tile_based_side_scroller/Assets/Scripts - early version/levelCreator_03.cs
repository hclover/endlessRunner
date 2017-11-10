using UnityEngine;
using System.Collections;

public class levelCreator_03 : MonoBehaviour {
	
	// Use this for initialization
	private GameObject collectedTiles;
	private const float tileWidth= 1.25f;
	private GameObject tilePos;
	private int heightLevel = 0;
	
	private GameObject gameLayer;
	private GameObject bgLayer;
	
	private GameObject tmpTile;
	
    // lưu vị trí bắt đầu up của y, sẽ dùng để chỉnh chiều ca của địa hình về sau
	private float startUpPosY;
	
	private float gameSpeed = 2.0f;
	private float outofbounceX;
	private int blankCounter = 0;
	private int middleCounter = 0;
	private string lastTile = "right";
	
	
	void Start () 
	{
		gameLayer = GameObject.Find("gameLayer");
		bgLayer = GameObject.Find("backgroundLayer");
		collectedTiles = GameObject.Find("tiles");

		for(int i = 0; i<20; i++){

            // Tạo ra trong Tile > gleft: 20 child nữa có resource là ground_left, tương tự cho mấy thằng kia
			GameObject tmpG1 = Instantiate(Resources.Load("ground_left", typeof(GameObject))) as GameObject;
            // Changing the parent will modify the parent-relative position,
            // scale and rotation but keep the world space position, rotation and scale the same.
            tmpG1.transform.parent = collectedTiles.transform.Find("gLeft").transform;
			tmpG1.transform.position = Vector2.zero; //Set về 0


			GameObject tmpG2 = Instantiate(Resources.Load("ground_middle", typeof(GameObject))) as GameObject;
			tmpG2.transform.parent = collectedTiles.transform.Find("gMiddle").transform;
			tmpG2.transform.position = Vector2.zero;
			GameObject tmpG3 = Instantiate(Resources.Load("ground_right", typeof(GameObject))) as GameObject;
			tmpG3.transform.parent = collectedTiles.transform.Find("gRight").transform;
			tmpG3.transform.position = Vector2.zero;
			GameObject tmpG4 = Instantiate(Resources.Load("blank", typeof(GameObject))) as GameObject;
			tmpG4.transform.parent = collectedTiles.transform.Find("gBlank").transform;
			tmpG4.transform.position = Vector2.zero;
		}

        //đặt cái tile ở lưng chừng đâu đó ngoài màn hình
		collectedTiles.transform.position = new Vector2 (-60.0f, -20.0f);
		
		tilePos = GameObject.Find("startTilePosition");
		startUpPosY = tilePos.transform.position.y;
		
		
		// Xong những cái phía trên vẫn chưa fill tile mới tạo ra cái child ở trong Tile> hôi
		fillScene ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		
		
		
	}
	
	private	void fillScene()
	{
		//Fill start
		for (int i = 0; i<15; i++)
		{
			setTile("middle");
		}
		setTile("right");
	}
	
	private void setTile(string type)
	{
		switch (type){
		case "left":
			tmpTile = collectedTiles.transform.Find("gLeft").transform.GetChild(0).gameObject;
			break;
		case "middle":
			tmpTile = collectedTiles.transform.Find("gMiddle").transform.GetChild(0).gameObject;
			break;
		case "right":
			tmpTile = collectedTiles.transform.Find("gRight").transform.GetChild(0).gameObject;
			break;
		case "blank":
			tmpTile = collectedTiles.transform.Find("gBlank").transform.GetChild(0).gameObject;
			break;
		}

        // set cha của nó
		tmpTile.transform.parent = gameLayer.transform;
        // vị trí ; x : cộng thêm chiều rộng của tile thôi
        // y : sẽ set thêm theo mức chiều cao của khối platform 
        // Debug.Log(tilePos.transform.position.x); 
		tmpTile.transform.position = new Vector3(tilePos.transform.position.x+(tileWidth),startUpPosY+(heightLevel * tileWidth),0);
		tilePos = tmpTile; // đẩy tile pos lên trước một đơn vị
		
	}
	
	private void spawnTile(){
		
		
	}
	
	private void changeHeight()
	{
		int newHeightLevel = (int)Random.Range(0,4);
		if(newHeightLevel<heightLevel)
			heightLevel--;
		else if(newHeightLevel>heightLevel)
			heightLevel++;
	}
	
	
}
