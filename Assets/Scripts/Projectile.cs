using UnityEngine;
using System.Collections;

// describes how the projectile it's attached to should behave
// gives speed and damage values
public class Projectile : MonoBehaviour 
{
    // how fast this projectile should move
    public float speed = 1f;

    // how much damage this projectile should deal
    public int damage = 1;
}
