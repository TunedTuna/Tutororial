using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField]   private KitchenObjectSO cutKitchenObjectSO;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //no kitchen BJ
            if (player.HasKitchenObject())
            {
                //carrying
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //p has nothing
            }
        }
        else
        {
            //is kitcehn obj
            if (player.HasKitchenObject())
            {
                //carrying sumthin
            }
            else
            {
                //p not carrying
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            //there is a kitchen object here
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }
}
