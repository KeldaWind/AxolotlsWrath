using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRP23
{
    namespace AxolotlWrath
    {
        [System.Serializable]
        public class InputManager
        {
            /// <summary>
            /// The Main Camera used for the game.
            /// </summary>
            [SerializeField] Camera mainCamera;

            /// <summary>
            /// Returns the position of the cursor in the world.
            /// </summary>
            /// <param name="screenPosition">The position of the cursor on the screen.</param>
            /// <returns>The position of the cursor in the world.</returns>
            public Vector3 GetMouseWorldPosition(Vector3 screenPosition)
            {
                Vector3 mouseWorldPosition = new Vector3();

                Ray mouseRay = mainCamera.ScreenPointToRay(screenPosition);

                RaycastHit[] hits = Physics.RaycastAll(mouseRay);

                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.gameObject.GetComponent<InputSurface>() != null)
                        mouseWorldPosition = hit.point;
                }

                return mouseWorldPosition;
            }

            /// <summary>
            /// Checks if the mouse is touching an interactible object.
            /// </summary>
            /// <param name="screenPosition">The position of the cursor on the screen.</param>
            /// <returns>The hit interactible objetc, null if none was hit.</returns>
            public IInteractible CheckForInteractibleObject(Vector3 screenPosition)
            {
                Ray mouseRay = mainCamera.ScreenPointToRay(screenPosition);

                RaycastHit[] hits = Physics.RaycastAll(mouseRay);

                foreach (RaycastHit hit in hits)
                {
                    IInteractible interactible = hit.collider.gameObject.GetComponent<IInteractible>();

                    if (interactible != null)
                        return interactible;
                }

                return null;
            }

            /// <summary>
            /// Gets the direction of the mouse depending on the player's position.
            /// </summary>
            /// <param name="screenPosition">The position of the cursor on the screen.</param>
            /// <param name="priestPosition">The position of the priest in the world.</param>
            /// <returns></returns>
            public PlayerDirection GetMouseDirection(Vector3 screenPosition, Vector3 priestPosition)
            {
                Vector3 mouseWorldPosition = GetMouseWorldPosition(screenPosition);

                Debug.Log(Mathf.Abs(mouseWorldPosition.y - priestPosition.y));

                if (Mathf.Abs(mouseWorldPosition.y - priestPosition.y) < 0.2f)
                    return PlayerDirection.None;
                else if((mouseWorldPosition - priestPosition).y > Mathf.Abs((mouseWorldPosition - priestPosition).x))
                    return PlayerDirection.Up;

                if (Mathf.Abs(mouseWorldPosition.x - priestPosition.x) < 0.2f)
                    return PlayerDirection.None;
                else if (mouseWorldPosition.x > priestPosition.x)
                    return PlayerDirection.Right;
                else
                    return PlayerDirection.Left;
            }

            /// <summary>
            /// Gets the direction of the mouse depending on the player's position. Only returns Left or Right.
            /// </summary>
            /// <param name="screenPosition">The position of the cursor on the screen.</param>
            /// <param name="priestPosition">The position of the priest in the world.</param>
            /// <returns></returns>
            public PlayerDirection GetMouseDirectionHorizontal(Vector3 screenPosition, Vector3 priestPosition)
            {
                Vector3 mouseWorldPosition = GetMouseWorldPosition(screenPosition);

                if (Mathf.Abs(mouseWorldPosition.x - priestPosition.x) < 0.2f)
                    return PlayerDirection.None;
                else if (mouseWorldPosition.x > priestPosition.x)
                    return PlayerDirection.Right;
                else
                    return PlayerDirection.Left;
            }
        }
    }
}
