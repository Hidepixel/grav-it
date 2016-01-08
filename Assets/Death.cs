using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

  public Vector3 startPosition;

  void OnTriggerEnter2D(Collider2D other) {
    other.attachedRigidbody.velocity = Vector2.zero;
    other.attachedRigidbody.angularVelocity = 0.0f;

    other.transform.position = startPosition;
    other.attachedRigidbody.rotation = 0.0f;
  }
}
