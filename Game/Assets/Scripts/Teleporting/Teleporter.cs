using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Teleporter : MonoBehaviour
{
    static public float heightCovered = 0.6F;
    Camera cam;
    CharacterController controller;
    LineRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>();
        renderer = GetComponent<LineRenderer>();
        renderer.widthMultiplier = 0.2f;
        renderer.positionCount = 3;
    }

    //returns the coordinates of the reticule teleport location
    //NOTE: will not work to teleport above a ledge that the player cannot see
    public Vector3 teleport(float maxDistance, CharacterController originalCollider, Vector3 direction, Vector3 viewPos)
    {
        //set the top to the top of the caller's capsule collider
        float topY = originalCollider.bounds.center.y + originalCollider.height / 2;

        //set the bottom to the bottom of the caller's capsule collider
        float bottomY = originalCollider.bounds.center.y - originalCollider.height / 2;

        //set the height of the bottom ray in relation to the caller's capsule collider's height
        float fractionHeight = originalCollider.height * heightCovered;

        //set the origins of the top and bottom rays. set their direction to be the same
        Vector3 bottomRayLoc = new Vector3(originalCollider.bounds.center.x, topY - fractionHeight, originalCollider.bounds.center.z);
        Vector3 topRayLoc = new Vector3(originalCollider.bounds.center.x, viewPos.y, originalCollider.bounds.center.z);
        Ray bottomRay = new Ray(bottomRayLoc, direction);
        Ray topRay = new Ray(topRayLoc, direction);

        //Fire a ray from the top and another from the bottom. The shortest distance is prioritized.
        RaycastHit bottomHit;
        RaycastHit topHit;
        Vector3 teleportLoc = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); //initialized to an impossible value
        float heightDifference = (topY - fractionHeight) - bottomY;
        renderer.positionCount = 3;
        if (Physics.Raycast(bottomRay, out bottomHit, maxDistance))
        {
            Vector3 point = new Vector3(bottomHit.point.x, bottomHit.point.y, bottomHit.point.z);
            renderer.SetPosition(0, bottomRayLoc);
            renderer.SetPosition(1, point);
            point.y += heightDifference;
            teleportLoc = point;
            Debug.Log("Bottom");
        }
        if(Physics.Raycast(topRay, out topHit, maxDistance))
        {
            Vector3 point = new Vector3(topHit.point.x, topHit.point.y, topHit.point.z);
            if (teleportLoc.x != float.PositiveInfinity)
            {
                if (Math.Abs(Vector3.Distance(viewPos, point)) < Math.Abs(Vector3.Distance(viewPos, teleportLoc)))
                {
                    teleportLoc = point;
                    renderer.SetPosition(0, bottomRayLoc);
                    renderer.SetPosition(1, point);
                    Debug.Log("Top");
                }
            }
        }
        if (teleportLoc.x == float.PositiveInfinity)
        {
            Vector3 origin = new Vector3(viewPos.x, viewPos.y, viewPos.z);
            Vector3 endPoint = new Vector3(viewPos.x, viewPos.y, viewPos.z);
            endPoint.x += direction.x * maxDistance;
            endPoint.y += direction.y * maxDistance;
            endPoint.z += direction.z * maxDistance;
            teleportLoc = endPoint;
            renderer.SetPosition(0, bottomRayLoc);
            renderer.SetPosition(1, endPoint);
        }
        renderer.SetPosition(2, teleportLoc);
        return teleportLoc;
    }

    // Update is called once per frame
    void Update()
    {
        Ray cameraRay = cam.ScreenPointToRay(new Vector3(cam.pixelWidth /2, cam.pixelHeight / 2, 0));
        Vector3 cameraDirection = cameraRay.direction;
        teleport(20F, controller, cameraDirection, cam.transform.position);
    }
}
