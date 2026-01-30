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
    public Image blockingImage;
    public GameObject[] localScenes;
    int activeLocalScene = 2;


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
            StartCoroutine(ChangeScene(3, 0));
            break;
        case -2:
            StartCoroutine(ChangeScene(1, 0));
            break;
        
       }
    }

    public IEnumerator ChangeScene(int scenenumber, float delay)
    {
        Debug.Log("Changing scene");
        Color c = blockingImage.color;
        blockingImage.enabled = true;
        while(blockingImage.color.a < 1)
        {
            c.a += Time.deltaTime;
            blockingImage.color = c;
        }
        //hide the old one 
        localScenes[activeLocalScene].SetActive(false);
        //show the new one
        localScenes[scenenumber].SetActive(true);
        //save which one is currently used 
        activeLocalScene = scenenumber;
        //show the new screen an enable clicking
        
        while(blockingImage.color.a > 0)
        {
            c.a -= Time.deltaTime;
            blockingImage.color = c;
        }
        blockingImage.enabled = false;


        yield return null;
    }


}
