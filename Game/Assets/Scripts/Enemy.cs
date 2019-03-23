using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UStateMachine;

public class Enemy : MonoBehaviour
{
    public StateMachine<Enemy> stateMachine { get; set; }
    public float damage = 1.0f;
    public int max_health = 3;
    private int health = 3;
    public ParticleSystem death_particles = null;
    public float death_delay_time = 1.0f; //for testing;
    public float death_particles_delay_time = 1.0f; //time before particles are destroyed
    public GameObject targetPlayerController = null;
    public bool shielded_enemy = false;
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(shielded_enemy);
        health = max_health;
        stateMachine = new StateMachine<Enemy>(this);
        stateMachine.changeState(EnemyPatrolState.Singleton); //set beggining state to EnemyPatolState
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
        stateMachine.Update();
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Player")
        {
            GameObject playerObj = collisionInfo.gameObject as GameObject;
            playerHealth playerIns = playerObj.GetComponent<playerHealth>() as playerHealth;
            playerIns.takeDamage(damage);

            //need to get normal of collision and move along it to prevent consecutive hits
            //this.GetComponent<Rigidbody>().AddForce(collisionInfo.GetContact(0).normal, ForceMode.Impulse);
        }
    }

    //switches to hunt player if conditions are met
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player") && stateMachine.currentState != EnemyHuntState.Singleton)
        {
            Debug.Log("Player Might Be in Sight");
            //makes ray hit for callbacks, makes ray, sets, its origin to this enemy, 
            //casts to the vec diference of player pos and enemy pos(this is the direction from enemy to player)
            RaycastHit hit;
            Ray ray = new Ray();
            ray.origin = this.transform.position;
            ray.direction = targetPlayerController.transform.position - this.transform.position;
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.tag == "PlayerBody")
                {
                    Debug.Log("EXTERMINATE-> " + hit.collider.gameObject.name);
                    stateMachine.changeState(EnemyHuntState.Singleton);
                }
            }
        }
    }

    public GameObject getTarget()
    {
        return targetPlayerController;
    }
    public void takeDamage(float damage)
    {
        if (Mathf.Cos(Time.time) > 0 && shielded_enemy)
        {
            int damage_taken = (int)damage;
            health -= damage_taken;
            Debug.Log(name + " health-> " + getHealth());
        }
        else if (shielded_enemy == false)
        {
            int damage_taken = (int)damage;
            health -= damage_taken;
            Debug.Log(name + " health-> " + getHealth());
        }
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
