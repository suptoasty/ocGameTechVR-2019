using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 1.0f;
    public int max_health = 3;
    private int health = 3;
    public ParticleSystem death_particles = null;
    public float death_delay_time = 1.0f; //for testing;
    public float death_particles_delay_time = 1.0f; //time before particles are destroyed
    // Start is called before the first frame update
    void Start()
    {
        health = max_health;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead())
        {
            //might need to access parent and instantiate death particles under that
            if (death_particles)
            {
                death_particles.transform.position = this.transform.position;
                death_particles.Play();
                Destroy(death_particles, death_particles_delay_time);
            }
            Destroy(this.gameObject, death_delay_time);
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Player")
        {
            GameObject playerObj = collisionInfo.gameObject as GameObject;
            player player = playerObj.GetComponent<player>() as player;
            player.takeDamage(damage);

            //need to get normal of collision and move along it to prevent consecutive hits
            this.GetComponent<Rigidbody>().AddForce(collisionInfo.GetContact(0).normal, ForceMode.Impulse);
        }
    }

    public void takeDamage(float damage)
    {
        int damage_taken = (int)damage;
        health -= damage_taken;
        Debug.Log(name + " health-> " + getHealth());
    }

    public int getHealth()
    {
        return health;
    }
    public bool isDead()
    {
        if (health <= 0)
        {
            return true;
        }
        return false;
    }
}
