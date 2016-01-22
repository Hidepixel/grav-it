using UnityEngine;

namespace Assets
{
    public class CameraFollow : MonoBehaviour
    {

        public Camera Camera;
        public GameObject Player;
        private GameObject _newPlayer;

        // Use this for initialization
        void Start ()
        {

            Camera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Camera.transform.position.z);
            _newPlayer = Instantiate(Player, transform.position, Quaternion.identity) as GameObject;
        }
	
        // Update is called once per frame
        void Update ()
        {
            Camera.transform.position = new Vector3(_newPlayer.transform.position.x, _newPlayer.transform.position.y, Camera.transform.position.z); 
        }
    }
}
