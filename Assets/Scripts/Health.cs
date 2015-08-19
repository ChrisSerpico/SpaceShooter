using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    // gives a game object health, which allows it to be destroyed by projectiles
    public int health = 5;

    // on update, check to see whether health has gone below zero
    // if it has, delete this gameobject
	void Update () 
    {
        if (health <= 0)
            Destroy(this.gameObject);
	}

    // decrement health by the passed amount
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
