using UnityEngine;
using System.Collections;

public class platform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public float speed = 7f;
	public float direction = -1f;


	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity  = new Vector2 ( speed * direction, rigidbody2D.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Ground")
			direction *= -1f;
	}
}
