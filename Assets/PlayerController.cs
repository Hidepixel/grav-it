using System.Collections;
using UnityEngine;

namespace Assets
{
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector]
        public int Id;

        public float maxSpeed = 10f;
        public bool jumped = false;
        public Camera PlayerCamera;

        void Start () {
            PlayerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, PlayerCamera.transform.position.z);
        }
	
        void Update () {
            PlayerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, PlayerCamera.transform.position.z);
            PlayerCamera.transform.rotation = Quaternion.identity;
            float move = Input.GetAxis("Horizontal");
            float jump = Input.GetAxis("Vertical");
            

            Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

            rigidBody.velocity = new Vector2(move * maxSpeed, rigidBody.velocity.y);
            //rigidBody.velocity = new Vector2(rigidBody.velocity.x, fly * maxSpeed);
            if (!jumped)
            {
                rigidBody.AddForce(Vector2.up * jump * 1000);
                jumped = true;
                StartCoroutine(WaitJumpAgain());
            }
            
        }

        private IEnumerator WaitJumpAgain()
        {
            yield return new WaitForSeconds(1.0f);
            jumped = false;
        }
    }
}
