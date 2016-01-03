using UnityEngine;
using System.Collections;

public class UpDownPlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public float speed = 7f;
	public float direction = -1f;

	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity  = new Vector2 (rigidbody2D.velocity.x, speed * direction );
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Ground")
			direction *= -1f;
	}
}
