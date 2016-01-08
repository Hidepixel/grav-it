using UnityEngine;
using System.Collections;

public class OpacityBugScript : MonoBehaviour {
	void Awake () {
    Color color = GetComponent<SpriteRenderer>().color;
    GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.5f);
	}
}
