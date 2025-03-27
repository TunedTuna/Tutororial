using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance { get;private set; }

    public float moveSpeed = 7f;
    private bool isWalking;
    private Vector3 lastInteractPosition;
    private ClearCounter selectedCounter;
    private KitchenObject kitchenObject;
    public event EventHandler<onSelectedCounterChangedEventArs> onSelectedCounterChanged;
    public class onSelectedCounterChangedEventArs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There is more than one Player instance");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }


    }

    void Update()
    {
        HandleMovement();
        HandleInteractions();

    }
    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractPosition = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractPosition, out RaycastHit raycasthit, interactDistance,counterLayerMask))
        {
          if( raycasthit.transform.TryGetComponent(out ClearCounter clearCounter)){
                // has clear counter
                //clearCounter.Interact();
                if(clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                 
                }

            }
            else
            {
                SetSelectedCounter(null);
                
            }
        }
        else {
            //Debug.Log("--");
            SetSelectedCounter(null);
        }
   
 
        
    }

    public void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);


        isWalking = moveDir != Vector3.zero;
        float moveDistance = Time.deltaTime * moveSpeed;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        float rotateSpeed = 10f;
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {

                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);




    }
    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this .selectedCounter = selectedCounter;
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
