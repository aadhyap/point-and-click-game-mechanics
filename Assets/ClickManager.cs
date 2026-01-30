using UnityEngine;

public class ClickManager : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    public void ClickReaction(ItemData item)
    {
        TryGettingItem(item);
    }

    private void TryGettingItem(ItemData item)
    {
        bool canGetItem = item.requiredItemID == -1 ||  gameManager.selectedItemID == item.requiredItemID;
        if (canGetItem)
       {
        GameManager.collectedItems.Add(item);
        foreach (GameObject obj in item.objectsToRemove)
           {
            //print
            Debug.Log("Destroying object: " + obj.name);
               Destroy(obj);
           }
           gameManager.UpdateEquipmentCanvas();
       }
    }

}
