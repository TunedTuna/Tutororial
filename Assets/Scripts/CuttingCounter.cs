using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField]   private CuttingRecipeSO[] cuttingRecipeSOarray;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //no kitchen BJ
            if (player.HasKitchenObject())
            {
                //carrying
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    //player carring somthin w cut recipe
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
                
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
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //there is a kitchen object here
            KitchenObjectSO outputKitchenObjectSO = getOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }


    private KitchenObjectSO getOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOarray)
        {

            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }

    private bool HasRecipeWithInput (KitchenObjectSO input)
    {//diffrernt name
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOarray)
        {

            if (cuttingRecipeSO.input == input)
            {
                return true;
            }
        }
        return false;
    }
}
