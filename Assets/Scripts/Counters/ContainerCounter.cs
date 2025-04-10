using UnityEngine;
using System;
using Unity.VisualScripting;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
   [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(IKitchenObjectParent interactor)
    {
        if (!interactor.HasKitchenObject())
        {
            //play hands are free
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, interactor);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
     
        
        

    }

}
