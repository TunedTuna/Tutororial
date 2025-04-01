using UnityEngine;

public class ClearCounter : BaseCounter
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

     private KitchenObjectSO kitchenObjectSO;
     private Transform counterTopPoint;

     private KitchenObject kitchenObject;

    public override void Interact(Player player)
    {
        if ((kitchenObject ==null))
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            //get object to player
            //Debug.Log(kitchenObject.GetClearCounter());
            kitchenObject.SetKitchenObjectParent(player);
        }
    }


}
