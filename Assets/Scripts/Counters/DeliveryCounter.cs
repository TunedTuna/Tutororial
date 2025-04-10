using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance {  get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public override void Interact(IKitchenObjectParent interactor)
    {

        if (interactor.HasKitchenObject())
        {
            if (interactor.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                //only accepts plates

                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                interactor.GetKitchenObject().DestroySelf();
            }
        }
    }

}
