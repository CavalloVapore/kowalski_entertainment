using UnityEngine;
using System.Collections;

//Items subparts and descriptions



[System.Serializable]
public class Item
{
    //Info that needs to be stored to the different items
    public string itemSave;         //save file name of the corresponding images
    public int itemID;              //Id of items for faster code
    public string itemTitle;        //item title for tooltip
    public string itemDesc;         //item description for tooltip
    public Texture2D itemIcon;      //2D Texture for image in inventory
    public Texture2D itemHRImage;   //2D High resolution image for closeup
    public ItemType firstType;      //Item type
    public ItemType secondType;     //secondary item type

    //types for the items for actions
    public enum ItemType
    {
        Weapon,         //can be used as weapon
        Equipable,      //can be active to interact with surrounding
        Quest,          //is needed to fullfill quest requirements
        Piece,          //is part of a puzzle piece -- needs to be combined
        Info,           //item has information on it -- there is a highres image
        Interactive,    //highres image can be interacted with
        None            //no type -- only used for secondary if there is none
    }

    //Constructor for Items
    public Item(string save, int id, string title, string desc, ItemType type1, ItemType type2)
    {
        itemSave = save;
        itemID = id;
        itemTitle = title;
        itemDesc = desc;
        itemIcon = Resources.Load<Texture2D>("Item Icons/" + save);
        firstType = type1;
        secondType = type2;
    }

    //Constructor for Items with no secundary type
    public Item(string save, int id, string title, string desc, ItemType type1)
    {
        itemSave = save;
        itemID = id;
        itemTitle = title;
        itemDesc = desc;
        itemIcon = Resources.Load<Texture2D>("Item Icons/" + save);
        firstType = type1;
        secondType = ItemType.None;
    }

    //Empty Constructor
    public Item()
    {
        itemID = -1;
    }

    //loads the high res image -- call only if there exists an high res image
    public void LoadHRImage()
    {
        itemHRImage = Resources.Load<Texture2D>("Item HR Image/" + itemSave);
    }
}
