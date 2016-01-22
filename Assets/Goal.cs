using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

  public Vector3 startPosition;

  private bool isInside = false;

  void OnTriggerEnter2D(Collider2D other) {
    if(other.tag == "Player") {
      isInside = true;
      StartCoroutine(WaitToWin(other));
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    isInside = false;
  }

  private IEnumerator WaitToWin(Collider2D player) {
    yield return new WaitForSeconds(1.0f);

    if(isInside) {
      ResetPosition(player);
    }
  }

  private void ResetPosition(Collider2D player) {
    player.attachedRigidbody.velocity = Vector2.zero;
    player.attachedRigidbody.angularVelocity = 0.0f;

    player.transform.position = startPosition;
    player.attachedRigidbody.rotation = 0.0f;
  }

}
