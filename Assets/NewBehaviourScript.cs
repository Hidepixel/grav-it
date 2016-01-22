using UnityEngine;

namespace Assets
{
    public class NewBehaviourScript : MonoBehaviour {

        public float maxSpeed = 10f;

        void Start () {
        }
	
        void Update () {
            float move = Input.GetAxis("Horizontal");
            float fly = Input.GetAxis("Vertical");

            Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

            rigidBody.velocity = new Vector2(move * maxSpeed, rigidBody.velocity.y);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, fly * maxSpeed);

        }
    }
}
