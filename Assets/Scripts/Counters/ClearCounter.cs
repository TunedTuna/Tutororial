using UnityEngine;

public class ClearCounter : BaseCounter
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(IKitchenObjectParent interactor)
    {
        // Works for both Player and RobotController
        if (!HasKitchenObject())
        {
            if (interactor.HasKitchenObject())
            {
                interactor.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (interactor.HasKitchenObject())
            {
                if (interactor.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(interactor.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            interactor.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(interactor);
            }
        }
    }



}
