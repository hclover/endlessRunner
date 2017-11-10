using UnityEngine;
using System.Collections;

public class playerHandler_05 : MonoBehaviour {

    public float jumpHeight = 3000f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	public void jump(){
        //Shorthand for writing Vector3(0, 1, 0).
        this.GetComponent<Rigidbody2D>().AddForce (Vector2.up * jumpHeight);
		
	}
	
	
}
