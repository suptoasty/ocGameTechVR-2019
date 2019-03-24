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
    private LineRenderer line = null;
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
            projectile.transform.localScale = transform.localScale / 2; //scale projectiles to proportional size of the crossbow...still a little off 
            Quaternion projRot = Quaternion.Euler(Random.Range(45, 145), 90, Random.Range(80, 100)); //fix rotation of arrow
            GameObject bullet = Instantiate(projectile, transform.GetChild(0).gameObject.transform.position, projRot) as GameObject; //instantiate in the scene
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponentInChildren<Collider>()); //prevents arrow from colliding with crossbow and its siblings under the crossbow
            bullet.GetComponent<Rigidbody>().AddForce(transform.GetChild(0).forward * speedAffect, ForceMode.Impulse); //apply impulse to move
            Destroy(bullet, projectileLifeTime); //remove bullets after 3 seconds
        }
        else
        {
            Debug.Log("Crossbow-> No projectile assigned");
        }

    }

    Vector3 SampleCurve(Vector3 start, Vector3 end, Vector3 control, float t)
    {
        Vector3 q1 = Vector3.Lerp(start, end, t);
        Vector3 q2 = Vector3.Lerp(control, end, t);
        Vector3 q3 = Vector3.Lerp(q1, q2, t);
        return q3;
    }
}
