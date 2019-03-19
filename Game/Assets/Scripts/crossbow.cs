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
    public int max_arrow_inscene = 2;
    // public Camera fpsCamera; //must be set to player camera.....might look at changing to ray from hand

    // public ParticleSystem boltImpactSmoke;
    // public LineRenderer lineRenderer;
    public GameObject projectile;
    public float projectileLifeTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //below is for testing purposes...remove for final game
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
        DistanceGrabbable grabbable = GetComponent<DistanceGrabbable>() as DistanceGrabbable;
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote) && grabbable.isGrabbed == false)
        {
            if (OVRInput.GetDown(OVRInput.Touch.PrimaryIndexTrigger))
            {
                shoot();
            }
        }
    }

    public void shoot()
    {
        if (projectile)
        {
            //a child is used for positions and rotations rather than the crossbow
            projectile.transform.localScale = transform.localScale; //scale projectiles to proportional size of the crossbow...still a little off 
            projectile.transform.rotation = transform.GetChild(0).transform.rotation; //sets rotation of projectile
            GameObject bullet = Instantiate(projectile, transform.GetChild(0).gameObject.transform.position, transform.GetChild(0).gameObject.transform.rotation) as GameObject; //instantiate in the scene
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponentInChildren<Collider>()); //prevents arrow from colliding with crossbow and its siblings under the crossbow
            bullet.GetComponent<Rigidbody>().AddForce(transform.GetChild(0).forward * speedAffect, ForceMode.Impulse); //apply impulse to move
            Destroy(bullet, projectileLifeTime); //remove bullets after 3 seconds
        }
        else
        {
            Debug.Log("Crossbow-> No projectile assigned");
        }

    }
}
