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

            private void Awake()
            {
                gameManager = this;
            }
        }
    }
}