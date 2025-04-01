using UnityEngine;

public class ContainerCounter : BaseCounter
{

    private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
     
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        

    }

}
