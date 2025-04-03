using UnityEngine;
using System.Collections.Generic;

public class DeliveryManager : MonoBehaviour
{

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax =4f;
    private int waitingRecipeMax = 4;

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer < 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSo = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSo.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSo);
            }
            
        }
    }
    private void Awake()
    {
        waitingRecipeSOList = new List<RecipeSO>();
        Instance = this;
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            if(waitingRecipeSO.kitchenObjectList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //has same number of ingredients
                bool plateContentMatchesRecipe = true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectList)
                {
                    //cycle thru all ingredients in recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //cycle thru all ingredients in Plate
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //ingredients match
                            ingredientFound= true;
                            break;
                        }
                    }
                    //
                    if (!ingredientFound)
                    {
                        //this recipe ingredient was not found on Plate
                        plateContentMatchesRecipe= false;
                    }
                }
                if(plateContentMatchesRecipe)
                {
                    //player delivered the correct recipe
                    Debug.Log("Player delivered the correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }
        //no match is found
        //player did not deliver correct recipe
        Debug.Log("WRONG RECIPE!");
    }
}
