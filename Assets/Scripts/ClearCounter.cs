using UnityEngine;

public class ClearCounter : BaseCounter
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private KitchenObjectSO kitchenObjectSO;


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


}
