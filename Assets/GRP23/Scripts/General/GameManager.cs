using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRP23
{
    namespace AxolotlWrath
    {       
        public class GameManager : MonoBehaviour
        {
            public static GameManager gameManager;

            [SerializeField] InputManager inputManager;
            public InputManager IptManager
            {
                get
                {
                    return inputManager;
                }
            }

            [SerializeField] IntDifficultyValues ints;
            [SerializeField] TestDifficultyValues tests;


            private void Awake()
            {
                gameManager = this;
            }
        }

        [System.Serializable]
        public struct TestStruct
        {
            public int Int;
            public float Float;
            public bool Bool;
            public Vector3 Vector;
        }
    }
}