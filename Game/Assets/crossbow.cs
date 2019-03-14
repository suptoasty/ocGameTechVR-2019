using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class crossbow : MonoBehaviour
{
    public float damage = 1.0f; //damage to be taken in by enemy script passed to function like? takedamage(damage);???
    public float distance = 200.0f; //max distance for a hit
    public float speedAffect = 10.0f;

    public Camera fpsCamera; //must be set to player camera.....might look at changing to ray from hand

    public ParticleSystem boltImpactSmoke;
    public LineRenderer lineRenderer;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        if (lineRenderer)
        {
            lineRenderer.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
        {
            if (OVRInput.GetDown(OVRInput.Touch.PrimaryIndexTrigger))
            {
                shoot();
            }
        }
    }

    public void shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, distance))
        {
            Debug.Log(hit.transform.name); //name of object hit
        }
        if (projectile)
        {
            projectile.transform.localScale = transform.localScale;
            GameObject bullet = Instantiate(projectile, transform.GetChild(0).gameObject.transform.position, transform.GetChild(0).gameObject.transform.rotation) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * speedAffect);
            Destroy(bullet, 3.0f);
        }
        else
        {
            Debug.Log("Crossbow-> No projectile assigned");
        }

    }
}
