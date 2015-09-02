using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    // gives a game object health, which allows it to be destroyed by projectiles
    public int health = 5;

    // Tween used for invulnerability frames
    protected GoTween turnRed;

    void Start()
    {
        InitializeTweens();
    }

    // on update, check to see whether health has gone below zero
    // if it has, delete this gameobject
	void Update () 
    {
        if (health <= 0)
            Destroy(this.gameObject);
	}

    // decrement health by the passed amount
    // also make the gameobject taking dmaage flash red
    public void TakeDamage(int damage)
    {
        // decrement health
        health -= damage;

        // Play tween chain to make the sprite flash red
        Go.addTween(turnRed);
    }

    // set up the tweens used in health behavior
    protected void InitializeTweens()
    {
        // Tween configuration
        MaterialColorTweenProperty red = new MaterialColorTweenProperty(Color.red);
        GoTweenConfig rConfig = new GoTweenConfig();
        rConfig.addTweenProperty(red);
        rConfig.setIterations(6);
        rConfig.loopType = GoLoopType.PingPong;

        // Make a tween to turn the gameObject red
        turnRed = new GoTween(this.gameObject, .5f, rConfig);
    }
}
