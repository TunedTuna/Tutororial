using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {

        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                //only accepts plates

                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }

}
