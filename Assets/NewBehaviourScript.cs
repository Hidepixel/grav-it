using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NewBehaviourScript : NetworkBehaviour {

  public float maxSpeed = 10f;

	void Start () {
	}
	
	void Update () {
    if(!isLocalPlayer) return;

    float move = Input.GetAxis("Horizontal");

    Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

    rigidBody.velocity = new Vector2(move * maxSpeed, rigidBody.velocity.y);
	}
}
