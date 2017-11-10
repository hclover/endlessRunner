using UnityEngine;
using System.Collections;

public class playerHandler_06 : MonoBehaviour {
	
	private bool inAir = false;

	private int _animState = Animator.StringToHash("animState");
	private Animator _animator;
	public bool jumpPress = false; // biến làm chỉ nhảy được 1 lần duy nhất

	void Start () {
		_animator = this.transform.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(!inAir && this.GetComponent<Rigidbody2D>().velocity.y > 0.05f){

			_animator.SetInteger(_animState,1);
			inAir =true;

		}else if(inAir && this.GetComponent<Rigidbody2D>().velocity.y == 0.00f){

			_animator.SetInteger(_animState,0);
			inAir =false;
			if(jumpPress)jump ();
		}






	}


	public void jump(){
		jumpPress = true;
		if (inAir)return;
		this.GetComponent<Rigidbody2D>().AddForce (Vector2.up * 3000);
	}


}
