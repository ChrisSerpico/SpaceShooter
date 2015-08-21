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

    // default GUI color
    Color defaultGUI = Color.white; // set to white to avoid errors (Color is non-nullable)

    // GUIStyles for cooldown bar
    GUIStyle cdBar;
    GUIStyle cdText;

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
            toAdd.GetComponent<HeldItem>().Initialize(i.GetComponent<SpriteRenderer>().sprite, i.cooldown);
            toAdd.GetComponent<HeldItem>().projectile = i.projectile;

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

    // use the currently selected item
    public void UseSelected()
    {
        // only works if the player has at least one item
        if (!IsEmpty())
        {
            inv[selectedItem].GetComponent<HeldItem>().Use();
        }
    }

    // draw items in inventory on screen
    // WARNING: This method currently uses a ton of magic numbers.
    //          Eventually, I'd like to go through it and clean it up,
    //          but for now just consider yourself warned
    void OnGUI()
    {
        // Get the default color if we haven't already
        if (defaultGUI == Color.white) defaultGUI = GUI.color;

        // Set up some GUI styles (only if they haven't been already)
        if (cdBar == null || cdText == null) InitStyles();

        // reset color before doing anything
        GUI.color = defaultGUI;

        // used to calculate each item's cooldown bar
        float cdownPercent;

        // GUIstyle for centering text
        GUIStyle centeredText = GUI.skin.GetStyle("Box");
        centeredText.alignment = TextAnchor.MiddleCenter;

        for (int i = 0; i < currentItems; i++)
        {
            // temporary item variable
            HeldItem temp = inv[i].GetComponent<HeldItem>();

            // calculate how much of this item's cooldown bar should be drawn
            cdownPercent = (1 - (float)(inv[i].GetComponent<HeldItem>().GetCooldown()) / (float)(inv[i].GetComponent<HeldItem>().maxCooldown));

            // draw all items in upper left-hand corner
            GUI.DrawTexture(new Rect(8, 8 + i * 36, 32, 32), temp.GetImg().texture);

            if (i == selectedItem)
            {
                // Draw a box in the bottom right that shows what item the player has equipped 
                GUI.Box(new Rect(Screen.width - 152, Screen.height - 152, 144, 144), "");

                // draw a box around the currently selected item
                GUI.Box(new Rect(6, (8 + i * 36) - 2, 108, 36), "");

                // also redraw it, much larger, in the bottom right
                GUI.DrawTexture(new Rect(Screen.width - 144, Screen.height - 144, 128, 128), temp.GetImg().texture);

                // now draw a box above the larger item display with the item's name
                GUI.Box(new Rect(Screen.width - 152, Screen.height - 184, 144, 32), temp.name, centeredText);

                // draw a bar to the left of the item image that represents cooldown/ammo (not sure which to use yet)
                // bar is drawn as a proportion of the currently selected item's cooldown
                float boxHeight = 144 * cdownPercent;
                GUI.color = Color.red;
                GUI.Box(new Rect(Screen.width - 168, Screen.height - 152 + (144 - boxHeight), 16, boxHeight), "", cdBar);
                GUI.color = defaultGUI;
                GUI.Label(new Rect(Screen.width - 168, Screen.height - 152, 13, 144), "C\no\no\nl\nd\no\nw\nn", cdText);
            }

            // draw smaller cooldown bars next to the items in the upper left corner
            // only draw the bar if the item is on cooldown
            if (cdownPercent < .99)
            {
                GUI.color = Color.red;
                GUI.Box(new Rect(41, (8 + i * 36) + 15, 71 * cdownPercent, 15), "", cdBar);
                GUI.color = defaultGUI;
            }
        }
    }

    // Initializing GUIStyles for various things
    protected void InitStyles()
    {
        // GUI Style for cooldown bar
        // Just like a button, but with no hover effect
        cdBar = new GUIStyle(GUI.skin.button);
        cdBar.hover = GUI.skin.box.hover;

        // GUI Style for cooldown text
        // Just like a label, but with especially centered text
        cdText = new GUIStyle(GUI.skin.label);
        cdText.alignment = TextAnchor.MiddleCenter;
    }
}
