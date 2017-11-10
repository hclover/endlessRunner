using UnityEngine;
using System.Collections;

public class levelCreator_0405 : MonoBehaviour {
	
	// Use this for initialization
	private GameObject collectedTiles;
	private const float tileWidth= 1.25f;
	public GameObject tilePos;
	private int heightLevel = 0;
	
	private GameObject gameLayer;
	private GameObject bgLayer;
	
	private GameObject tmpTile;
	
	private float startUpPosY;

    // Lv4
    public float gameSpeed = 2.0f;
	private float outofbounceX;
	private int blankCounter = 0;
	private int middleCounter = 0;
    public string lastTile = "right";
	
	
	
	
	void Start () 
	{
		gameLayer = GameObject.Find("gameLayer");
		bgLayer = GameObject.Find("backgroundLayer");
		collectedTiles = GameObject.Find("tiles");
		for(int i = 0; i<30; i++){
			GameObject tmpG1 = Instantiate(Resources.Load("ground_left", typeof(GameObject))) as GameObject;
			tmpG1.transform.parent = collectedTiles.transform.Find("gLeft").transform;
			tmpG1.transform.position = Vector2.zero;
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

		collectedTiles.transform.position = new Vector2 (-60.0f, -20.0f);
		
		tilePos = GameObject.Find("startTilePosition");
		startUpPosY = tilePos.transform.position.y;

        // Lv4
		outofbounceX = tilePos.transform.position.x - 5.0f; // trừ đi 5f sẽ đảy cái outofbunce này ra khỏi màn hình camera
		
		
		fillScene ();
	}
	
	
	
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		
		
		// Chỉnh tốc độ cho bg và platform thông qua chỉnh vị trí của x
		gameLayer.transform.position = new Vector2 (gameLayer.transform.position.x - gameSpeed * Time.deltaTime, 0);
		bgLayer.transform.position = new Vector2 (bgLayer.transform.position.x - gameSpeed/4 * Time.deltaTime, 0);

        foreach (Transform child in gameLayer.transform)
        {

            if (child.position.x < outofbounceX)
            {

                switch (child.gameObject.name)
                {
                    // Về tên có Clone do lúc chạy trog Start coi lại phần script 03
                    case "ground_left(Clone)":
                        child.gameObject.transform.position = collectedTiles.transform.Find("gLeft").transform.position;
                        child.gameObject.transform.parent = collectedTiles.transform.Find("gLeft").transform;
                        break;

                    case "ground_middle(Clone)":
                        child.gameObject.transform.position = collectedTiles.transform.Find("gMiddle").transform.position;
                        child.gameObject.transform.parent = collectedTiles.transform.Find("gMiddle").transform;
                        break;
                    case "ground_right(Clone)":
                        child.gameObject.transform.position = collectedTiles.transform.Find("gRight").transform.position;
                        child.gameObject.transform.parent = collectedTiles.transform.Find("gRight").transform;
                        break;
                    case "blank(Clone)":
                        child.gameObject.transform.position = collectedTiles.transform.Find("gBlank").transform.position;
                        child.gameObject.transform.parent = collectedTiles.transform.Find("gBlank").transform;
                        break;
                    case "Reward":
                        GameObject.Find("Reward").GetComponent<crateScript>().inPlay = false;
                        break;
                        
                    default:
                        Destroy(child.gameObject); // đi ra ngoài cái outofbounceX
                        break;

                }


            }




        }


        if (gameLayer.transform.childCount < 25)
            spawnTile();



    }

    //Fill start
    private void fillScene()
	{
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
		tmpTile.transform.parent = gameLayer.transform;
		tmpTile.transform.position = new Vector3(tilePos.transform.position.x+(tileWidth),startUpPosY+(heightLevel * tileWidth),0);
		tilePos = tmpTile;
        // Cứ sinh cái ra là thành last 

        // Lv4
        lastTile = type;
	}
	
	private void spawnTile(){
		
		if (blankCounter > 0) {
			//Sinh ra khoảng tróng nếu biến trên > 0 và khi nà nó lớn hơn thì đực định nghĩa ở dưới
			setTile("blank");
			blankCounter--;
			return;
			
		}
		if (middleCounter > 0) {
			// Tương tự blank
			setTile("middle");
			middleCounter--;
			return;
			
		}
		
		if (lastTile == "blank") {
			// cái cuối cùng là khoảng trắng thì  đỏi level cao ngẫu nhiên vs thêm tile tiếp là trái và một khoảng ngẫu nhiên middle
			changeHeight();
			setTile("left");
			middleCounter = (int)Random.Range(1,8);
			
		}else if(lastTile =="right"){
			
			blankCounter = (int)Random.Range(2,4);
		}else if(lastTile == "middle"){
			setTile("right");
		}
		
		
	}
	
	private void changeHeight()
	{
        // với code này sẽ hk nhảy quá 2 ô khi nào cả
		int newHeightLevel = (int)Random.Range(0,4);
		if(newHeightLevel<heightLevel)
			heightLevel--;
		else if(newHeightLevel>heightLevel)
			heightLevel++;
	}
	
	
}
