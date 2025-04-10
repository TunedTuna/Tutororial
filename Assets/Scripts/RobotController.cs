using System;
using UnityEngine;
using UnityEngine.AI;
using static Player;

public class RobotController : MonoBehaviour, IKitchenObjectParent
{

    public event EventHandler OnPlateDetected;
    [SerializeField] private Transform destinationShere;

    public static DestinationController instance;

    [SerializeField] private Transform destinationTransform;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private KitchenObject kitchenObject;
    private BaseCounter selectedCounter;
    public event EventHandler OnPickUpSomething;

    public event EventHandler<onSelectedCounterChangedEventArs> onSelectedCounterChanged;
    public class onSelectedCounterChangedEventArs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        DestinationController.instance.OnPlateDetected += DestinationController_OnPlateDetected;

    }

    private void DestinationController_OnPlateDetected(object sender, EventArgs e)
    {
        //pick up plate
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {


            selectedCounter.InteractAI(this);
        }

    }

    void Update()
    {
      
        agent.destination = destinationTransform.position;
        //gotta do something where destion something?

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPos = Input.mousePosition;
            //create a ray that goes thru screenPos using a camera
            Ray cursorRay = Camera.main.ScreenPointToRay(screenPos);
            bool rayHitSomething = Physics.Raycast(cursorRay, out RaycastHit hitInfo);
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

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        onSelectedCounterChanged?.Invoke(this, new onSelectedCounterChangedEventArs
        {
            selectedCounter = selectedCounter,
        });
    }


    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnPickUpSomething?.Invoke(this, EventArgs.Empty);
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
