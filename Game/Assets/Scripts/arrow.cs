using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using OculusSampleFramework;

public class arrow : MonoBehaviour
{
    private bool grabbed = false;
    // Start is called before the first frame update
    public float arrow_damage = 1.0f;
    public float destroy_life = 0.0f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DistanceGrabbable grabbable = GetComponent<DistanceGrabbable>() as DistanceGrabbable;
        grabbed = grabbable.isGrabbed;
    }

    //this may seem wierd as damage is thought of as taken, but reflecting the metaphor of giving damage is easier to handle
    void OnCollisionEnter(Collision collisionInfo)
    {
        float damage = arrow_damage;
        // Debug.Log(name + " Hit-> " + collisionInfo.collider.name);
        if (collisionInfo.collider.tag == "Enemy")
        {
            //get collider as enemy and have it take damage
            GameObject enemyObj = collisionInfo.gameObject as GameObject;
            Enemy enemy = enemyObj.GetComponent<Enemy>() as Enemy;
            enemy.takeDamage(damage);
            //get parent to kill object
            if (!grabbed)
            {
                Destroy(this.gameObject, destroy_life);
            }
            else
            {
                destroy_life = 2.0f;
            }
        }
    }
}
