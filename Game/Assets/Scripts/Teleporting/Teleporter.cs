using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Teleporter : MonoBehaviour
{
    static public float heightCovered = 0.6F;
    public Camera cam;
    public CharacterController controller;
    public GameObject sphere;
    Transform sphereTransform;
    Component[] childrenComponents;
    Transform handTransform;
    private Camera m_Camera;

    // Start is called before the first frame update
    void Start()
    {
        sphere = GameObject.FindWithTag("Sphere");
        if (sphere != null)
        {
            sphereTransform = sphere.GetComponent<Transform>();
        }
        cam = GetComponentInChildren<Camera>();
        controller = GetComponentInChildren<CharacterController>();
        childrenComponents = GetComponentsInChildren(typeof(Component));

        foreach (Component t in childrenComponents)
        {
            if (t.name == ("r_hand_skeletal_lowres"))
            {
                handTransform = t.transform;
            }
        }

        if (handTransform == null)
        {
            Debug.Log("No right hand transform");
        }

        if (cam == null)
        {
            Debug.Log("No Camera");
        }

        if (controller == null)
        {
            Debug.Log("No Controller");
        }
    }

    //returns the coordinates of the reticule teleport location
    //NOTE: will not work to teleport above a ledge that the player cannot see
    public Vector3 teleport(float maxDistance, CharacterController originalCollider, Vector3 direction, Vector3 handPos)
    {
        //set the top to the top of the caller's capsule collider
        float topY = originalCollider.bounds.center.y + originalCollider.height / 2;

        //set the bottom to the bottom of the caller's capsule collider
        float bottomY = originalCollider.bounds.center.y - originalCollider.height / 2;

        //set the height of the bottom ray in relation to the caller's capsule collider's height
        float fractionHeight = originalCollider.height * heightCovered;

        //set the origins of the top and bottom rays. set their direction to be the same
        Vector3 bottomRayLoc = new Vector3(handPos.x, topY - fractionHeight, handPos.z);
        Vector3 topRayLoc = new Vector3(handPos.x, handPos.y, handPos.z);
        Ray bottomRay = new Ray(bottomRayLoc, direction);
        Ray topRay = new Ray(topRayLoc, direction);

        //Fire a ray from the top and another from the bottom. The shortest distance is prioritized.
        RaycastHit bottomHit;
        RaycastHit topHit;
        Vector3 teleportLoc = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); //initialized to an impossible value
        float heightDifference = handPos.y - (topY - fractionHeight);

        if (Physics.Raycast(bottomRay, out bottomHit, maxDistance))
        {
            Vector3 point = new Vector3(bottomHit.point.x, bottomHit.point.y, bottomHit.point.z);
            point.y += heightDifference;
            teleportLoc = point;
        }
        if (Physics.Raycast(topRay, out topHit, maxDistance))
        {

            Vector3 point = new Vector3(topHit.point.x, topHit.point.y, topHit.point.z);
            float topDist = Math.Abs(Vector3.Distance(handPos, point));
            float bottomDist = Math.Abs(Vector3.Distance(handPos, teleportLoc));

            if (Math.Abs(Vector3.Distance(handPos, point)) < Math.Abs(Vector3.Distance(handPos, teleportLoc)))
            {
                teleportLoc = point;
            }
        }
        if (teleportLoc.x == float.PositiveInfinity)
        {
            Vector3 origin = new Vector3(handPos.x, handPos.y, handPos.z);
            Vector3 endPoint = new Vector3(handPos.x, handPos.y, handPos.z);
            endPoint.x += direction.x * maxDistance;
            endPoint.y += direction.y * maxDistance;
            endPoint.z += direction.z * maxDistance;
            teleportLoc = endPoint;
        }
        sphereTransform.position = teleportLoc;
        return teleportLoc;
    }

    // Update is called once per frame
    void Update()
    {
        //Ray cameraRay = cam.ScreenPointToRay(new Vector3(cam.pixelWidth /2, cam.pixelHeight / 2, 0));
        //Vector3 cameraDirection = cameraRay.direction;

        Vector3 teleportLoc = teleport(20F, controller, handTransform.forward, handTransform.position);



        if (Input.GetKeyDown(KeyCode.E))
        {
            //this.SendMessage("Teleported");
            //this.transform.position = teleportLoc;
        }
    }
}
