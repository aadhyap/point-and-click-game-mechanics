using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;  


public class GameManager : MonoBehaviour
{
    public static List<ItemData> collectedItems = new List<ItemData>();
    [Header("Equipment")]
    public GameObject equipmentCanvas;
    public Image[] equipmentSlots, equipmentImages;
    public Sprite emptyItemSlotSprite;
    public Color selectedItemColor;
    public int selectedCanvasSlotID=0;
    public int selectedItemID = -1;


    public void SelectItem(int equipmentCanvasID)
    {

        Color c = Color.white;
        c.a = 1;
        // change alpha of prev slot of new slot to 0
        equipmentSlots[selectedCanvasSlotID].color = c;
        
        //save changes and stop if an empty slot is clicked and the last item is removed
        if (collectedItems.Count <= equipmentCanvasID || equipmentCanvasID < 0)
        {
            //no items selected
            selectedItemID = -1;
            selectedCanvasSlotID = 0;
            return;
        }
           
        // change alpha of new slot to x
        equipmentSlots[equipmentCanvasID].color = selectedItemColor;
        // save changes
        selectedCanvasSlotID = equipmentCanvasID;
        selectedItemID = collectedItems[selectedCanvasSlotID].itemID;

    }

    public void UpdateEquipmentCanvas()
    {
        //find out how many items we have and when to stop
        int itemsAmount = collectedItems.Count, itemSlotsAmount = equipmentSlots.Length;
        //replace no item sprites and old sprites with collectedItems[x].itemImage
        for (int i = 0; i < itemSlotsAmount; i++)
        {
            if (i < itemsAmount)
            {
                //choose between emptyItemSlotSprite and an item sprite
                equipmentImages[i].sprite = collectedItems[i].itemSlotSprite;
            }
            else
            {
                equipmentImages[i].sprite = emptyItemSlotSprite;
            }
        }
        //add special conditions for selecting items
        if (itemsAmount == 0)
        {
            SelectItem(-1);
        } else if (itemsAmount == 1)
        {
            SelectItem(0);
        }

        
    }

    public void ShowItemName(int equipmentCanvasID)
    {
        // Implementation for selecting an item based on equipmentCanvasID
        Debug.Log("Item selected with ID: " + equipmentCanvasID);
    }

    public void ChangeScene(string sceneName)
    {
        switch (sceneName)
        {
            case "MainScene":
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
                break;
            case "EndingScene":
                UnityEngine.SceneManagement.SceneManager.LoadScene("EndingScene");
                break;
            default:
                Debug.LogError("Scene " + sceneName + " not found!");
                break;
        }
    }

    public void CheckSpecialConditions(ItemData item)
    {
       switch (item.itemID)
       {
        case -4:
            StartCoroutine(ChangeScene(4, 0));
            break;
        case -2:
            StartCoroutine(ChangeScene(3, 0));
            break;
        
       }
    }

    public IEnumerator ChangeScene(int scenenumber, float delay)
    {
        Debug.Log("Changing scene");
        yield return null;
    }


}
