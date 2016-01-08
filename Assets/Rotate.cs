using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

  public float velocity;

  void Start() {
    GetComponent<Rigidbody2D>().angularVelocity = velocity;
  }

	void Update () {
	}
}
