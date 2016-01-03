using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

	public float speed = 7f;
	float direction = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = new Vector2 ( speed * direction, rigidbody2D.velocity.y);
		transform.localScale = new Vector3 (direction, 1, 1);
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.name == "enemy_border")
			direction *= -1f;
	}
}
