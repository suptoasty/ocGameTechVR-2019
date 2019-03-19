using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public float arrow_damage = 1.0f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

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
            Destroy(this.gameObject);
        }
    }
}
