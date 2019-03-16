using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using OculusSampleFramework;

public class crossbow : MonoBehaviour
{
    public float damage = 1.0f; //damage to be taken in by enemy script passed to function like? takedamage(damage);???
    // public float distance = 200.0f; //max distance for a hit
    public float speedAffect = 10.0f;

    // public Camera fpsCamera; //must be set to player camera.....might look at changing to ray from hand

    // public ParticleSystem boltImpactSmoke;
    // public LineRenderer lineRenderer;
    public GameObject projectile;
    public float projectileLifeTime = 2.0f;
    private DistanceGrabbable grabbableRef = GetComponent<DistanceGrabbable>() as DistanceGrabbable; //referenece to grabbable script component of crossbow

    // Start is called before the first frame update
    void Start()
    {
        // if (lineRenderer)
        // {
        //     lineRenderer.enabled = true;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote) && grabbableRef.isGrabbed == false)
        {
            if (OVRInput.GetDown(OVRInput.Touch.PrimaryIndexTrigger))
            {
                shoot();
            }
        }
    }

    public void shoot()
    {
        // RaycastHit hit;
        // if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, distance))
        // {
        //     Debug.Log(hit.transform.name); //name of object hit
        // }
        if (projectile)
        {
            projectile.transform.localScale = transform.localScale; //scale projectiles to proportional size of the crossbow...still a little off 
            projectile.transform.rotation = new Quaternion();
            GameObject bullet = Instantiate(projectile, transform.GetChild(0).gameObject.transform.position, transform.GetChild(0).gameObject.transform.rotation) as GameObject; //instantiate in the scene
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponentInChildren<Collider>());
            bullet.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, 1.0f) * speedAffect, ForceMode.Impulse); //apply impulse to move
            Destroy(bullet, projectileLifeTime); //remove bullets after 3 seconds
        }
        else
        {
            Debug.Log("Crossbow-> No projectile assigned");
        }

    }
}
