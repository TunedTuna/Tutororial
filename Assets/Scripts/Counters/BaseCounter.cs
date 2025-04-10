using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }
    public static event EventHandler OnAnyObjectPlacedHere;
    //[SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;
    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCOunter.Interact()");
    }
    public virtual void InteractAI(RobotController Robot)
    {
        Debug.LogError("BaseCOunter.Interact()");
    }
    public virtual void InteractAlternate(Player player)
    {
        Debug.LogError("BaseCOunter.InteractAlternate()");
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        
        if(kitchenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this,EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
