using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    // handles player inventory

    // how many items the player is allowed to hold
    public int size = 6;
    // currently held number of items
    public int currentItems = 0;

    // array that holds all the currently held items
    protected GameObject[] inv;

	// Use this for initialization
	void Start () 
    {
	    // initialize inventory array
        inv = new GameObject[size];
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    // add an item to the inventory
    public void AddItem(GroundItem i)
    {
        // only add items if there's room in the inventory
        if (!IsFull())
        {
            // create a new game object and give it a held item script
            GameObject toAdd = new GameObject(i.name);
            toAdd.AddComponent<HeldItem>();
            toAdd.GetComponent<HeldItem>().Initialize(i.GetComponent<SpriteRenderer>().sprite);

            // add the new item to the inventory
            inv[currentItems] = toAdd;
            currentItems++;
            Destroy(i.gameObject);
            Debug.Log("Item \"" + toAdd.ToString() + "\" added to inventory");
        }
    }

    // returns true if the inventory is full
    public bool IsFull()
    {
        if (size == currentItems)
            return true;
        else
            return false;
    }

    // draw items in inventory on screen
    void OnGUI()
    {
        for (int i = 0; i < currentItems; i++)
        {
            // temporary item variable
            HeldItem temp = inv[i].GetComponent<HeldItem>();
            GUI.DrawTexture(new Rect(8, 8 + i * 36, 32, 32), temp.GetImg().texture);
        }
    }
}
