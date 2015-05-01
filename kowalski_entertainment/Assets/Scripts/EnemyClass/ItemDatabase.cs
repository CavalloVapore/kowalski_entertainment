using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//Database for the items that can be found in the inventory



public class ItemDatabase : MonoBehaviour
{
    //creates List as a database for all items
    public List<Item> items = new List<Item>();

    //Adding the different Items to the List of all Items
    void Start()
    {
        items.Add(new Item("White Shirt", 0, "White Shirt", "A white shirt that is absolutly useless.", Item.ItemType.Info));
        items.Add(new Item("Amulet of Prayers", 1, "Amuelet of Prayers", "An amulet made from the best prayers in the world to pray even better.", Item.ItemType.Piece));
        items.Add(new Item("Power Potion", 2, "Power Potion", "This Potion gives you more power than you can handle. Trust me, don't try this one.", Item.ItemType.Equipable));

        for(int i = 0; i < items.Count; i++)
        {
            if (items[i].firstType == Item.ItemType.Info || items[i].secondType == Item.ItemType.Info)
            {
                items[i].LoadHRImage();
            }
        }
    }
}
