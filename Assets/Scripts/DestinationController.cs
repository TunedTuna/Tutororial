using System;
using UnityEngine;

public class DestinationController : MonoBehaviour
{
    public event EventHandler OnPlateDetected;
    [SerializeField] private Transform destinationShere;

    public static DestinationController instance;
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPos= Input.mousePosition;
            //create a ray that goes thru screenPos using a camera
            Ray cursorRay = Camera.main.ScreenPointToRay(screenPos);
            bool rayHitSomething =Physics.Raycast(cursorRay, out RaycastHit hitInfo);
            if (rayHitSomething)
            {
                
                destinationShere.position = hitInfo.point;
                if (hitInfo.transform.TryGetComponent(out ClearCounter clearCounter))
                {
                    if (clearCounter.HasKitchenObject())
                    {
                        if (clearCounter.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                        {
                            Debug.Log("its a plate!");
                            OnPlateDetected?.Invoke(this, EventArgs.Empty);


                        }
                    }
                    

                }
            }
        }
    }
}
