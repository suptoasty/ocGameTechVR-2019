using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using OculusSampleFramework;

public class EnemyProjectile : MonoBehaviour
{
    private bool grabbed = false;
    // Start is called before the first frame update
    public float arrow_damage = 1.0f;
    public float destroy_life = 0.0f;
    public GameObject projectile;
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
        if (collisionInfo.collider.tag == "Player")
        {
            //get collider as enemy and have it take damage
            GameObject enemyObj = collisionInfo.gameObject as GameObject;
            playerHealth enemy = enemyObj.GetComponent<playerHealth>() as playerHealth;
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

    public void shoot()
    {
        if (projectile)
        {
            //a child is used for positions and rotations rather than the crossbow
            projectile.transform.localScale = transform.localScale; //scale projectiles to proportional size of the crossbow...still a little off 
            Quaternion projRot = Quaternion.Euler(Random.Range(45, 145), 90, Random.Range(80, 100)); //fix rotation of arrow
            GameObject bullet = Instantiate(projectile, transform.GetChild(0).gameObject.transform.position, projRot) as GameObject; //instantiate in the scene
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponentInChildren<Collider>()); //prevents arrow from colliding with crossbow and its siblings under the crossbow
            bullet.GetComponent<Rigidbody>().AddForce(transform.GetChild(0).forward * speedAffect, ForceMode.Impulse); //apply impulse to move
            Destroy(bullet, projectileLifeTime); //remove bullets after 3 seconds
        }
        else
        {
            Debug.Log("Enemy-> No projectile assigned");
        }
    }
}
