using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRP23
{
    namespace AxolotlWrath
    {
        public class BossWeakSpot : MonoBehaviour, IInteractible
        {
            public void Interact()
            {
                Debug.Log("weakpoint");
            }
        }
    }
}