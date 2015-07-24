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

    // image get
    public Sprite GetImg()
    {
        return img;
    }
}
