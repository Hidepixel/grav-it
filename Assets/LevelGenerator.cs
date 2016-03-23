using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets
{
    public class LevelGenerator : MonoBehaviour
    {
        public GameObject Player;
        private List<GameObject> _playerList;
        private int _numberOfPlayers;

        private const int MapWidth = 25;
        private const int MapHeight = 10;

        private int[,] _levelCoordinateSystem;
        private GameObject[,] _blockPool;

        [Serializable]
        public struct BlockType
        {
            public int BlockId;
            public GameObject Block;
            public bool ProcColor;
            public Color Color;
            public float MinOffset;
            public float MaxOffset;
        }
        public BlockType[] BlockTypes;

        void Awake()
        {
            _playerList = new List<GameObject>();
            _numberOfPlayers = 0;
            GenerateLevel();
        }

        private void GenerateLevel()
        {
            _levelCoordinateSystem = new int[MapWidth, MapHeight]
            {
                {1,1,1,1,1,1,1,1,1,1 },
                {1,1,1,1,1,1,1,1,1,1 },
                {1,1,1,2,1,1,1,1,1,1 },
                {1,0,0,2,0,0,0,0,0,1 },
                {1,0,0,2,0,1,0,0,0,1 },
                {1,0,0,0,0,1,1,0,0,1 },
                {1,0,50,0,0,0,0,0,0,1 },
                {1,0,0,0,0,0,0,0,0,1 },
                {1,0,0,4,0,0,0,0,0,1 },
                {1,0,0,0,3,3,3,0,0,1 },
                {1,0,0,0,0,0,0,0,0,1 },
                {1,0,0,0,0,4,0,0,0,1 },
                {1,0,0,0,0,0,0,0,0,1 },
                {1,0,0,2,0,0,50,0,0,1 },
                {1,0,0,2,0,1,0,0,0,1 },
                {1,0,0,0,0,1,1,0,0,1 },
                {1,0,0,0,0,0,0,0,0,1 },
                {1,0,0,1,1,0,0,0,0,1 },
                {1,0,0,0,0,50,0,0,0,1 },
                {1,0,0,0,0,0,0,0,0,1 },
                {1,0,0,0,0,1,0,0,0,1 },
                {1,0,0,0,0,1,0,0,0,1 },
                {1,0,0,0,0,0,0,0,0,1 },
                {1,1,1,1,1,1,1,1,1,1 },
                {1,1,1,1,1,1,1,1,1,1 },
            };
            _blockPool = new GameObject[MapWidth, MapHeight];

            for (int x = 0; x < _levelCoordinateSystem.GetLength(0); x++)
            {
                for (int y = 0; y < _levelCoordinateSystem.GetLength(1); y++)
                {
                    if (_levelCoordinateSystem[x, y] == 50)
                    {
                        GameObject newPlayer = Instantiate(Player, new Vector3(x, y), Quaternion.identity) as GameObject;
                        newPlayer.GetComponent<PlayerController>().Id = _numberOfPlayers;
                        _playerList.Add(newPlayer);
                        _numberOfPlayers++;
                    }
                    else if (_levelCoordinateSystem[x, y] != 0)
                    {
                        BlockType blockType = BlockTypes.FirstOrDefault(b => b.BlockId == _levelCoordinateSystem[x, y]);
                        GameObject newBlockObject = blockType.Block;
                        GameObject newBlock = Instantiate(newBlockObject, new Vector3(x, y), Quaternion.identity) as GameObject;
                        if (blockType.ProcColor && newBlock != null)
                        {
                            newBlock.GetComponent<SpriteRenderer>().color = GenerateColors(blockType.Color, blockType.MinOffset, blockType.MaxOffset);
                        }
                        _blockPool[x, y] = newBlock;
                    }
                }
            }
            switch (_playerList.Count)
            {
                case 1:
                    _playerList[0].GetComponent<PlayerController>().PlayerCamera.rect = new Rect(0, 0, 1, 1);
                    break;
                case 2:
                    _playerList[0].GetComponent<PlayerController>().PlayerCamera.rect = new Rect(0, 0, 0.5f, 1);
                    _playerList[1].GetComponent<PlayerController>().PlayerCamera.rect = new Rect(0.5f, 0, 0.5f, 1);
                    break;
                case 3:
                    _playerList[0].GetComponent<PlayerController>().PlayerCamera.rect = new Rect(0, 0, 0.5f, 1);
                    _playerList[1].GetComponent<PlayerController>().PlayerCamera.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                    _playerList[2].GetComponent<PlayerController>().PlayerCamera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                    break;
                case 4:
                    _playerList[0].GetComponent<PlayerController>().PlayerCamera.rect = new Rect(0, 0, 0.5f, 0.5f);
                    _playerList[1].GetComponent<PlayerController>().PlayerCamera.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                    _playerList[2].GetComponent<PlayerController>().PlayerCamera.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                    _playerList[3].GetComponent<PlayerController>().PlayerCamera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                    break;
            }
            
        }

        private Color GenerateColors(Color color,float minOffset, float maxOffset)
        {
            float value = (color.r + color.g + color.b) / 3;
            float newValue = value + 2 * UnityEngine.Random.Range(minOffset, maxOffset) * minOffset - maxOffset;
            float valueRatio = newValue / value;
            Color newColor = new Color
            {
                r = color.r*valueRatio,
                g = color.g*valueRatio,
                b = color.b*valueRatio,
                a = color.a
            };
            return newColor;
        }

        /*[CustomEditor(typeof (LevelGenerator))]
        public class LevelGeneratorEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                LevelGenerator levelGenerator = target as LevelGenerator;
                for (int x = 0; x < levelGenerator.BlockTypes.Length; x++)
                {
                    levelGenerator.BlockTypes[x].ProcColor = GUILayout.Toggle(levelGenerator.BlockTypes[x].ProcColor, "ProcColor");

                    levelGenerator.BlockTypes[x].BlockId = EditorGUILayout.IntField("BlockId", levelGenerator.BlockTypes[x].BlockId);

                    levelGenerator.BlockTypes[x].Block = EditorGUILayout.ObjectField("Block", levelGenerator.BlockTypes[x].Block, typeof (GameObject), true) as GameObject;

                    if (levelGenerator.BlockTypes[x].ProcColor)
                    {
                        levelGenerator.BlockTypes[x].Color = EditorGUILayout.ColorField("ProcColor",levelGenerator.BlockTypes[x].Color);
                        levelGenerator.BlockTypes[x].Offset = EditorGUILayout.FloatField("Offset",levelGenerator.BlockTypes[x].Offset);
                    }
                }
            }
        }*/
    }
}
