using UnityEngine;
using System.Collections;

public class crateScript_07 : MonoBehaviour {
	
	private float maxY;
	private float minY;
	private int direction = 1;
	
	
	
	private SpriteRenderer crateRender;
	
	// Use this for initialization
	void Start () {
		
        // tạo ra để up down 
		maxY = this.transform.position.y + .5f;
		minY = maxY - 1.0f;
		
		crateRender = this.transform.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		// thằng x được giữ nguyên, y sẽ up down
        // tạm hiểu 0.05 này sẽ lá Speed của up down vậy
		this.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y +(direction * 0.05f));
		if (this.transform.position.y > maxY)
			direction = -1;
		if (this.transform.position.y < minY)
			direction = 1;
		
		
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		
		if (coll.gameObject.tag == "Player") {
			
			switch(crateRender.sprite.name){
				
			case "crates_0":
				GameObject.Find("Main Camera").GetComponent<levelCreator>().gameSpeed -=1.0f; // CHANGE V4 BEGINNING 8
				break;
			case "crates_1":
				GameObject.Find("Player").GetComponent<Rigidbody2D>().AddForce(Vector2.up*6000);
				break;
			case "crates_2":
				//add score
				break;
			}
			// sau va chạm di chuyển nó lên trời :3 phải destroy nó chứ ta :3 chắc để répawn
			this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 30.0f);
			
		}
		
		
	}
	
	
	
	
	
	
	
	
	
	
}
