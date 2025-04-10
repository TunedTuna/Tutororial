using System;
using System.Collections;
using UnityEngine;


public class StoveCounter : BaseCounter,IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs: EventArgs
    {
        public State state;
    }
    public  enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    private State state;
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    private float fryingTimer;
    private float burninTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
        {
            case State.Idle:
             
                    break;
            case State.Frying:
                fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                {
                    //fried                    
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        state = State.Fried;
                        burninTimer = 0f;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                }
                break;
            case State.Fried:
                    burninTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burninTimer / burningRecipeSO.burningTimerMax
                    });

                    if (burninTimer > burningRecipeSO.burningTimerMax)
                    {
                        //fried                    
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }

                    break;
            case State.Burned:
                break;

        }
        
           
           
        }
        
    }
    public override void Interact(IKitchenObjectParent interactor)
    {
       
        
        if (!HasKitchenObject())
        {
            //no kitchen BJ
            if (interactor.HasKitchenObject())
            {
                //carrying
                if (HasRecipeWithInput(interactor.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player carring somthin tat can be fried
                    interactor.GetKitchenObject().SetKitchenObjectParent(this);
                     fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    fryingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
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
            if (interactor.HasKitchenObject())
            {
                //carrying sumthin
                if (interactor.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                        state = State.Idle;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }


                }
            }
            else
            {
                //p not carrying
                
                GetKitchenObject().SetKitchenObjectParent(interactor);
                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }

    private KitchenObjectSO getOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }

    }

    private bool HasRecipeWithInput(KitchenObjectSO input)
    {//diffrernt name

        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(input);
        return fryingRecipeSO != null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO input)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {

            if (fryingRecipeSO.input == input)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO input)
    {
        foreach (BurningRecipeSO BurningRecipeSO in burningRecipeSOArray)
        {

            if (BurningRecipeSO.input == input)
            {
                return BurningRecipeSO;
            }
        }
        return null;
    }

}
