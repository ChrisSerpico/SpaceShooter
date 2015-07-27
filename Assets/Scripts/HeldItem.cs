using UnityEngine;
using System.Collections;

public class HeldItem : MonoBehaviour {

    // generic held item class
    // this represents items held in player's inventory 

    // image displayed in the inventory bar
    protected Sprite img;

    // initialize this item
    public void Initialize(Sprite image)
    {
        img = image;
    }

    // get the sprite image
    public Sprite GetImg()
    {
        return img;
    }

    // use this item
    // can be extended by child classes
    public virtual void Use()
    {
        Debug.Log("Item \"" + this.name + "\" was used.");
    }
}
