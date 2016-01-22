using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

  public float maxSpeed = 10f;

	void Start () {
	}
	
	void Update () {
    float move = Input.GetAxis("Horizontal");

    Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

    rigidBody.velocity = new Vector2(move * maxSpeed, rigidBody.velocity.y);
	}
}
