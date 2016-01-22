using UnityEngine;

namespace Assets
{
    public class FloorMovement : MonoBehaviour {

        public Vector3 startPosition;
        public Vector3 endPosition;
        public float speed;

        private float startTime;
        private float journeyLength;

        void Awake()
        {
            startPosition = new Vector3(transform.position.x , transform.position.y - 5);
            endPosition = new Vector3(transform.position.x, transform.position.y + 5);
        }

        void Start () {
            startTime = Time.time;
            journeyLength = Vector3.Distance(startPosition, endPosition);
        }

        void FixedUpdate () {
            float distanceCovered = (Time.time - startTime) * speed;
            float frac = distanceCovered / journeyLength;

            transform.position = Vector3.Lerp(startPosition, endPosition, frac);

            float distance = Vector3.Distance(transform.position, endPosition);
            if(distance == 0) {
                Flip();
            }
        }

        void Flip() {
            Vector3 aux = startPosition;
            startPosition = endPosition;
            endPosition = aux;
            startTime = Time.time;
        }
    }
}
