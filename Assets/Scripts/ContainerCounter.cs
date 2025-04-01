using UnityEngine;
using System;
using Unity.VisualScripting;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
   [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //play hands are free
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
     
        
        

    }

}
