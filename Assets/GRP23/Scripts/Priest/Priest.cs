using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRP23
{
    namespace AxolotlWrath
    {
        public class Priest : MonoBehaviour
        {
            [SerializeField] Rigidbody priestBody;

            [Header("Basic Moves")]
            [SerializeField] float priestWalkSpeed;

            [Header("Dash")]
            [SerializeField] float priestDashSpeed;
            [SerializeField] float priestDashDuration;
            float currentPriestDashDuration;
            Vector3 currentDashDirection;
            bool dashing;
            [SerializeField] float dashCooldown;
            float currentDashCooldown;

            [Header("Gravity")]
            [SerializeField] GravityHandler gravityHandler;
            [SerializeField] Transform[] gravityCheckers;
            Vector3[] gravityCheckersPositions;

            [Header("References")]
            [SerializeField] MeshRenderer priestRenderer;
            InputManager inputManager;

            private void Start()
            {
                inputManager = GameManager.gameManager.IptManager;
                SetUpCheckersPositions();
            }

            public void SetUpCheckersPositions()
            {
                gravityCheckersPositions = new Vector3[gravityCheckers.Length];

                for(int i = 0; i < gravityCheckers.Length; i++)
                {
                    gravityCheckersPositions[i] = gravityCheckers[i].transform.localPosition;
                }
            }

            private void FixedUpdate()
            {
                if (dashing)
                    Dashing();
                else if (!gravityHandler.CheckForOnGround(transform.position, gravityCheckersPositions))
                    priestBody.velocity = gravityHandler.GetCurrentGravityForce();
                else
                    MoveTowardCursor();
            }

            private void Update()
            {
                IInteractible interactible = inputManager.CheckForInteractibleObject(Input.mousePosition);

                if (Input.GetMouseButtonDown(0) && gravityHandler.CheckForOnGround(transform.position, gravityCheckersPositions) && currentDashCooldown == 0)
                {
                    if(interactible == null)
                    {
                        Debug.Log("may be dash");
                        if(currentPriestDashDuration == 0 && inputManager.GetMouseDirection(Input.mousePosition, transform.position) != PlayerDirection.None)
                            StartDash();
                    }
                    else
                    {
                        interactible.Interact();
                    }
                }

                if (currentDashCooldown > 0)
                    currentDashCooldown -= Time.deltaTime;
                else if (currentDashCooldown < 0)
                    currentDashCooldown = 0;

                if (dashing)
                    priestRenderer.material.color = Color.cyan;
                else if (currentDashCooldown > 0)
                    priestRenderer.material.color = Color.red;
                else
                    priestRenderer.material.color = Color.green;
            }

            /// <summary>
            /// Makes the Priest move left or right, depending on the position of the cursor.
            /// </summary>
            public void MoveTowardCursor()
            {
                PlayerDirection playerDirection = inputManager.GetMouseDirectionHorizontal(Input.mousePosition, transform.position);

                Vector3 walkDirection = playerDirection == PlayerDirection.Right ? new Vector3(1, 0, 0) : playerDirection == PlayerDirection.Left ? new Vector3(-1, 0, 0) : Vector3.zero;

                priestBody.velocity = walkDirection * priestWalkSpeed * 100 * Time.deltaTime;
            }

            #region Dash
            /// <summary>
            /// Starts the Dash action.
            /// </summary>
            public void StartDash()
            {
                dashing = true;
                currentPriestDashDuration = priestDashDuration;
                PlayerDirection playerDirection = inputManager.GetMouseDirection(Input.mousePosition, transform.position);
                currentDashDirection = playerDirection == PlayerDirection.Right ? new Vector3(1, 0, 0) : playerDirection == PlayerDirection.Left ? new Vector3(-1, 0, 0) : new Vector3(0, 1, 0);
                currentDashCooldown = dashCooldown;
            }

            /// <summary>
            /// Calls this function while the priest is Dashing to make him move.
            /// </summary>
            public void Dashing()
            {
                if (currentPriestDashDuration > 0)
                    currentPriestDashDuration -= Time.deltaTime;
                else if (currentPriestDashDuration <= 0 && dashing)
                    EndDash();

                priestBody.velocity = currentDashDirection * priestDashSpeed * 100 * Time.deltaTime;
            }

            /// <summary>
            /// Call this function at the end of the Dash, to reset the values.
            /// </summary>
            public void EndDash()
            {
                dashing = false;
                currentPriestDashDuration = 0;
            }
            #endregion
        }

        public enum PlayerDirection
        {
            None,
            Right,
            Left,
            Up
        }
    }
}