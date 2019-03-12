using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossbow : MonoBehaviour
{
    public float damage = 1.0f; //damage to be taken in by enemy script passed to function like? takedamage(damage);???
    public float distance = 200.0f; //max distance for a hit

    public Camera fpsCamera; //must be set to player camera.....might look at changing to ray from hand

    public ParticleSystem boltImpactSmoke;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")) {
            shoot();
        }
    }

    public void shoot() {
        RaycastHit hit;
        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, distance)) {
            Debug.Log(hit.transform.name); //name of object hit
        }

    }
}
