using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GravityHandler {
    /// <summary>
    /// The direction of the gravity force.
    /// </summary>
    [SerializeField] Vector3 gravityDirection;
    [SerializeField] float gravityAcceleration;
    float currentVerticalSpeed;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCurrentGravityForce()
    {
        Vector3 gravityForce = new Vector3();

        currentVerticalSpeed += gravityAcceleration * Time.deltaTime;

        gravityForce = gravityDirection * currentVerticalSpeed * 100 * Time.deltaTime;

        return gravityForce;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="characterCenter"></param>
    /// <param name="gravityCheckPositions"></param>
    /// <returns></returns>
    public bool CheckForOnGround(Vector3 characterCenter, Vector3[] gravityCheckPositions)
    {
        bool onGround = false;

        Debug.Log("check");

        foreach(Vector3 checkOrigin in gravityCheckPositions)
        {
            Ray ray = new Ray();
            ray.origin = characterCenter + checkOrigin;
            ray.direction = new Vector3(0, -1, 0);

            RaycastHit[] hits = Physics.RaycastAll(ray, 0.15f);

            foreach(RaycastHit hit in hits)
            {
                Debug.Log("hit");
                Ground hitGround = hit.collider.GetComponent<Ground>();
                if(hitGround != null)
                {
                    onGround = true;
                    currentVerticalSpeed = 0;
                    break;
                }
            }

            if (onGround)
                break;
        }

        return onGround;
    }
}
