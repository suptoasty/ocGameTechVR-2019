using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    public float damage = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //this may seem wierd as damage is thought of as taken, but reflecting the metaphor of giving damage is easier to handle
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Enemy")
        {
            GameObject enemyObj = collisionInfo.gameObject as GameObject;
            Enemy enemy = enemyObj.GetComponent<Enemy>() as Enemy;
            enemy.takeDamage(damage);
        }
    }
}
