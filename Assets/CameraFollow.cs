using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class CameraFollow : MonoBehaviour
    {
        public GameObject Player;
        private List<GameObject> _playerList;
        public int NumberOfPlayers;

        // Use this for initialization
        void Start ()
        {
            _playerList = new List<GameObject>();
            for (int x = 0; x < NumberOfPlayers; x++)
            {
                GameObject newPlayer = Instantiate(Player, transform.position, Quaternion.identity) as GameObject;
                _playerList.Add(newPlayer);
            }
           
        }
	
        // Update is called once per frame
        void Update ()
        {
            
        }
    }
}
