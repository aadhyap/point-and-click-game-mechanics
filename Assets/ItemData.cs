using UnityEngine;


public class ItemData : MonoBehaviour
{
    [Header("Set up")]
    public int itemID, requiredItemID;
    public string objectName;

    [Header("Success")]
    public GameObject[] objectsToRemove;
    public GameObject[] objectsToSetActive;
    public Sprite itemSlotSprite;

  
    

}
