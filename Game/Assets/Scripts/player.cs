using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public int max_health = 3;
    private int health = 3;
    // Start is called before the first frame update
    void Start()
    {
        health = max_health;
        Physics.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer("Grabbable"));
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead())
        {
            Destroy(this, 0.0f);
        }
    }



    public void takeDamage(float damage)
    {
        int damage_taken = (int)damage;
        health -= damage_taken;
    }

    bool isDead()
    {
        if (health <= 0)
        {
            return true;
        }
        return false;
    }
}
