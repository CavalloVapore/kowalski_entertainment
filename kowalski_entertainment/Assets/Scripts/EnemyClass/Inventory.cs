using UnityEngine;
using System.Collections;
using System.Collections.Generic;



//Inventory Script that does everything in the inventory



public class Inventory : MonoBehaviour
{
    //Variables and lists needed
    private int slotsX = 3, slotsY = 3;             //Number of slots in x and y direction
    public Texture backgroundTexture;               //background image
    public GUISkin skin;                            //skin for layout and style
    public List<Item> inventory = new List<Item>(); //list for all items in the inventory
    public List<Item> slots = new List<Item>();     //list for all the slots
    public static bool showInventory;                     //bool if true, inventory is shown
    private ItemDatabase database;                  //database of all the items

    private bool showTooltip;                       //if true, shows tooltip
    private string tooltip;                         //string shown in the tooltip

    private bool draggingItem;      //if true, item is being dragged
    private Item draggedItem;       // all the infos of the dragged item
    private int prevIndex;          //slot index, where the item came from

    private int backgroundXOffset = 0, backgroundYOffset = 0;   //screen offset for the background image to scale everything right
    private float ratio = 0f;                                   //scaling ratio
    private int backgroundWidth = 0, backgroundHeight = 0;       //width and hight of the currently displayed background

    private bool displayCloseUp;
    private Item closeUpItem;

    // Use this for initialization
    void Start()
    {
        //calculate size for background
        ratio = (float)(Screen.height) / (float)(backgroundTexture.height);
        backgroundHeight = (int)(backgroundTexture.height * ratio);
        backgroundWidth = (int)(backgroundTexture.width * ratio);
        backgroundXOffset = (int)((Screen.width / 2) - (backgroundWidth / 2));

        //creates grid for inventory
        for (int i = 0; i < (slotsX * slotsY); i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }

        //adds items to the inventory from the database
        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
        AddItem(1);
        AddItem(2);
        AddItem(0);
    }

    //called each frame
    void Update()
    {
        //checkes if inventory is accessed
        if (Input.GetButtonDown("Inventory"))
        {
            showInventory = !showInventory;
        }
    }

    //draws gui layer
    void OnGUI()
    {
        //tooltip info is empty at each call of OnGUI
        tooltip = "";

        //asigns the GUI skin
        GUI.skin = skin;

        //if true, inventory is displayed
        if (showInventory)
        {
            //draw background
            GUI.DrawTexture(new Rect(backgroundXOffset, backgroundYOffset, backgroundWidth, backgroundHeight), backgroundTexture);

            //draw inventory
            DrawInventory();

            //if true, tooltip is displayed
            if (showTooltip)
            {
                float tooltipYOffset = 0f;
                if (Event.current.mousePosition.y + 220 > Screen.height)
                {
                    tooltipYOffset = (float)(Screen.height) - Event.current.mousePosition.y - 220;
                }
                GUI.Box(new Rect(Event.current.mousePosition.x + 15, Event.current.mousePosition.y + tooltipYOffset, 200, 200), tooltip, skin.GetStyle("Tooltip"));
            }

            //button to close inventory
            if (GUI.Button(new Rect(Screen.width - 70, 20, 50, 50), "X"))
            {
                showInventory = false;
                displayCloseUp = false;
            }

            //button to save inventory
            if (GUI.Button(new Rect(40, 400, 100, 40), "Save"))
            {
                SaveInventory();
            }

            //button to load inventory
            if (GUI.Button(new Rect(40, 450, 100, 40), "Load"))
            {
                LoadInventory();
            }

            //if true, dragging item is displayed
            if (draggingItem)
            {
                //display dragged icon with center at the mouse
                float draggedWidth = 0f, draggedHeight = 0f;    //width and hight of the items in the slot
                float draggedRatioX = 0f;                       //ratio for the hight and the width of the item

                //calculate the ratio for the image, if the bottom is 50 wide
                //this ensures that the image is scaled correctly when dragged
                draggedRatioX = 50f / (float)(draggedItem.itemIcon.width);
                draggedWidth = (float)(draggedItem.itemIcon.width) * draggedRatioX;
                draggedHeight = (float)(draggedItem.itemIcon.height) * draggedRatioX;

                GUI.DrawTexture(new Rect(Event.current.mousePosition.x - draggedWidth / 2, Event.current.mousePosition.y - draggedHeight / 2, draggedWidth, draggedHeight), draggedItem.itemIcon);
            }

            //if true, close-up of item is displayed
            if (displayCloseUp)
            {
                DrawCloseUp(closeUpItem);
            }
        }

    }

    //function that removes an item from the list
    void RemoveItem(int id)
    {
        //goes through all items until it finds the first item with the corresponding id and then stops
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemID == id)
            {
                inventory[i] = new Item();

                break;
            }
        }
    }

    //draw inventory function
    void DrawInventory()
    {
        Event e = Event.current;

        int i = 0;
        //creates all the fields
        for (int y = 0; y < slotsY; y++)
        {
            for (int x = 0; x < slotsX; x++)
            {
                //creates slots
                Rect slotRect;
                slotRect = CreateSlots(i);
                GUI.Box(slotRect, "", skin.GetStyle("Slot"));

                slots[i] = inventory[i];

                //if slot is not empty
                if (slots[i].itemSave != null)
                {
                    //display corresponding icon in slot
                    float itemWidth = 0f, itemHeight = 0f;       //width and hight of the items in the slot
                    float itemRatioX = 0f, itemRatioY = 0f;     //ratio for the hight and the width of the item
                    float itemXOffset = 0f, itemYOffset = 0f;   //the offset from the box of the inventory

                    //calculate and check which side is the smaller one and thus controles the size of the image
                    //this ensures that the image is scaled correctly in the inventory
                    itemRatioY = (float)(slotRect.height) / (float)(slots[i].itemIcon.height);
                    itemRatioX = (float)(slotRect.width) / (float)(slots[i].itemIcon.width);
                    if (itemRatioX < itemRatioY)
                    {
                        itemWidth = (float)(slots[i].itemIcon.width) * itemRatioX;
                        itemHeight = (float)(slots[i].itemIcon.height) * itemRatioX;
                    }
                    else
                    {
                        itemWidth = (float)(slots[i].itemIcon.width) * itemRatioY;
                        itemHeight = (float)(slots[i].itemIcon.height) * itemRatioY;
                    }

                    //calculate the offset of the box of the inventory to the image, so that it is located in the center of the slot
                    itemXOffset = (slotRect.width - itemWidth) / 2;
                    itemYOffset = (slotRect.height - itemHeight) / 2;

                    GUI.DrawTexture(new Rect(slotRect.xMin + itemXOffset, slotRect.yMin + itemYOffset, itemWidth, itemHeight), slots[i].itemIcon);

                    //if mouse is over that icon
                    if (slotRect.Contains(e.mousePosition) && !displayCloseUp)
                    {
                        //check if currently an item is being dragged
                        //if not
                        //show tooltip for that item the cursor is over
                        if (!draggingItem)
                        {
                            tooltip = CreateTooltip(slots[i]);
                            showTooltip = true;
                        }

                        //if left mouse button is pressed and the mouse is dragged and there is no item already dragged
                        //drag item the cursor was pressed over
                        if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                        {
                            draggingItem = true;
                            prevIndex = i;
                            draggedItem = slots[i];
                            inventory[i] = new Item();
                        }

                        //if mouse is released over an slot with an item in it
                        //switch the places with the item and place item and old spot from the dragged item
                        if (e.type == EventType.mouseUp && draggingItem)
                        {
                            inventory[prevIndex] = inventory[i];
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }

                        //if an item is right clicked
                        if (e.isMouse && e.type == EventType.mouseDown && e.button == 1)
                        {
                            //check if it is an consumable and executes corresponding function
                            if (slots[i].firstType == Item.ItemType.Info || slots[i].secondType == Item.ItemType.Info)
                            {
                                displayCloseUp = true;
                                closeUpItem = slots[i];
                            }
                        }
                    }
                }
                // if slot is empty
                else
                {
                    //check if mouse is over that slot
                    if (slotRect.Contains(e.mousePosition))
                    {
                        //check if mouse was released and and item was being dragged prior to this
                        //place item in empty slot and delete dragged item
                        if (e.type == EventType.mouseUp && draggingItem)
                        {
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                    }
                }

                //if there is nothing diplayed in the tooltip
                //set tooltip invisible (false)
                if (tooltip == "")
                {
                    showTooltip = false;
                }

                //go to next slot
                i++;
            }
        }

        //if mouse is released and it was dragging an item prior to this
        //but it was not released above a slot, return item to slot it was dragged from
        if (e.type == EventType.mouseUp && draggingItem)
        {
            inventory[prevIndex] = draggedItem;
            draggingItem = false;
            draggedItem = null;
        }
    }

    //function that sets the boundarys for all the slots
    Rect CreateSlots(int slotID)
    {
        Rect slotRect;

        switch (slotID)
        {
            case 0:
                slotRect = new Rect(backgroundXOffset + (1082 * ratio), backgroundYOffset + (78 * ratio), 315 * ratio, 308 * ratio);
                break;

            case 1:
                slotRect = new Rect(backgroundXOffset + (1420 * ratio), backgroundYOffset + (77 * ratio), 225 * ratio, 309 * ratio);
                break;

            case 2:
                slotRect = new Rect(backgroundXOffset + (1678 * ratio), backgroundYOffset + (76 * ratio), 243 * ratio, 312 * ratio);
                break;

            case 3:
                slotRect = new Rect(backgroundXOffset + (1081 * ratio), backgroundYOffset + (415 * ratio), 303 * ratio, 276 * ratio);
                break;

            case 4:
                slotRect = new Rect(backgroundXOffset + (1415 * ratio), backgroundYOffset + (413 * ratio), 229 * ratio, 279 * ratio);
                break;

            case 5:
                slotRect = new Rect(backgroundXOffset + (1672 * ratio), backgroundYOffset + (411 * ratio), 248 * ratio, 283 * ratio);
                break;

            case 6:
                slotRect = new Rect(backgroundXOffset + (1080 * ratio), backgroundYOffset + (718 * ratio), 301 * ratio, 298 * ratio);
                break;

            case 7:
                slotRect = new Rect(backgroundXOffset + (1411 * ratio), backgroundYOffset + (718 * ratio), 225 * ratio, 296 * ratio);
                break;

            case 8:
                slotRect = new Rect(backgroundXOffset + (1667 * ratio), backgroundYOffset + (721 * ratio), 256 * ratio, 287 * ratio);
                break;

            default:
                slotRect = new Rect();
                break;
        }

        return slotRect;
    }

    //function, that creates a tooltip
    string CreateTooltip(Item item)
    {
        //creates tooltip title and descritption of the item
        tooltip = "<color=#4DA4BF>" + item.itemTitle + "</color>\n\n" + "<color=#F2F2F2>" + item.itemDesc + "</color>";

        //if it is an item of the type info, display following text
        if (item.firstType == Item.ItemType.Info || item.secondType == Item.ItemType.Info)
        {
            tooltip += "\n\n<color=#F2F2F2>Right-click item to get a closer look.</color>";
        }

        return tooltip;
    }

    //function that adds an item to the inventory at the next free place
    void AddItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemSave == null)
            {
                for (int j = 0; j < database.items.Count; j++)
                {
                    if (database.items[j].itemID == id)
                    {
                        inventory[i] = database.items[j];
                    }
                }
                break;
            }
        }
    }

    //function that checks if a certain item is in the inventory
    bool InventoryContains(int id)
    {
        bool result = false;

        for (int i = 0; i < inventory.Count; i++)
        {
            result = inventory[i].itemID == id;

            if (result)
            {
                break;
            }
        }

        return result;
    }

    //function that is called when an item is displayed as an closeup
    private void DrawCloseUp(Item item)
    {
        Rect closeRect = new Rect((Screen.width - Screen.height) / 2, 0, Screen.height, Screen.height);

        //draw background
        GUI.Box(closeRect, "", skin.GetStyle("CloseUp"));

        //display corresponding icon in big slot
        float itemWidth = 0f, itemHeight = 0f;       //width and hight of the items in the slot
        float itemRatioX = 0f, itemRatioY = 0f;     //ratio for the hight and the width of the item
        float itemXOffset = 0f, itemYOffset = 0f;   //the offset from the box of the inventory

        //calculate and check which side is the smaller one and thus controles the size of the image
        //this ensures that the image is scaled correctly in the inventory
        itemRatioY = (float)(closeRect.height) / (float)(item.itemIcon.height);
        itemRatioX = (float)(closeRect.width) / (float)(item.itemIcon.width);
        if (itemRatioX < itemRatioY)
        {
            itemWidth = (float)(item.itemIcon.width) * itemRatioX;
            itemHeight = (float)(item.itemIcon.height) * itemRatioX;
        }
        else
        {
            itemWidth = (float)(item.itemIcon.width) * itemRatioY;
            itemHeight = (float)(item.itemIcon.height) * itemRatioY;
        }

        //calculate the offset of the box of the inventory to the image, so that it is located in the center of the slot
        itemXOffset = (closeRect.width - itemWidth) / 2;
        itemYOffset = (closeRect.height - itemHeight) / 2;

        GUI.DrawTexture(new Rect(closeRect.xMin + itemXOffset, closeRect.yMin + itemYOffset, itemWidth, itemHeight), item.itemHRImage);

        //button to close close-up
        if (GUI.Button(new Rect(closeRect.xMax - 70, 20, 50, 50), "X"))
        {
            displayCloseUp = false;
        }
    }

    //function to save inventory
    void SaveInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            PlayerPrefs.SetInt("Inventory " + i, inventory[i].itemID);
        }
    }

    //function to load inventory
    void LoadInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            inventory[i] = PlayerPrefs.GetInt("Inventory " + i, -1) >= 0 ? database.items[PlayerPrefs.GetInt("Inventory " + i)] : new Item();
        }
    }
}

