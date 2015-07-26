using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    // handles player inventory

    // how many items the player is allowed to hold
    public int size = 6;
    // currently held number of items
    public int currentItems = 0;
    // the currently selected item
    public int selectedItem = 0;

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
	    // make sure the player can't select an item they don't have yet
        if (!IsEmpty() && selectedItem >= currentItems)
            selectedItem = currentItems - 1;
        // also make sure that selected item isn't less than zero
        if (selectedItem < 0)
            selectedItem = 0;
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

    // methods for changing which item the player has selected
    // move the selection upwards (on screen)
    public void SelectUp()
    {
        // top item is selected, loop to bottom
        if (selectedItem <= 0)
            selectedItem = currentItems - 1;
        else
            selectedItem--;
    }

    // move the selection downwards (on screen)
    public void SelectDown()
    {
        // if bottom item is selected, loop to top
        if (selectedItem == currentItems - 1)
            selectedItem = 0;
        else
            selectedItem++;
    }

    // returns true if the inventory is full
    public bool IsFull()
    {
        if (size == currentItems)
            return true;
        else
            return false;
    }

    // returns true if the inventory is empty
    public bool IsEmpty()
    {
        if (currentItems == 0)
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
            if (i == selectedItem)
            {
                GUI.Box(new Rect(6, (8 + i * 36) - 2, 108, 36), temp.name);
            }
        }
    }
}
