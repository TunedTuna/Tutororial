using UnityEngine;

public class ClearCounter : BaseCounter
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //no kitchen BJ
            if (player.HasKitchenObject())
            {
                //carrying
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //p has nothing
            }
        }
        else
        {
            //is kitcehn obj
            if (player.HasKitchenObject()) 
            { 
                //carrying sumthin
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                    

                }
                else
                {
                    //player not carrying plate, but sumthin else
                    if(GetKitchenObject().TryGetPlate(out  plateKitchenObject))
                    {
                        //counte is hold a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                //p not carrying
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
        public override void InteractAI(RobotController robot)
    {
        if (!HasKitchenObject())
        {
            //no kitchen BJ
            if (robot.HasKitchenObject())
            {
                //carrying
                robot.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //p has nothing
            }
        }
        else
        {
            //is kitcehn obj
            if (robot.HasKitchenObject()) 
            { 
                //carrying sumthin
                if(robot.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                    

                }
                else
                {
                    //player not carrying plate, but sumthin else
                    if(GetKitchenObject().TryGetPlate(out  plateKitchenObject))
                    {
                        //counte is hold a plate
                        if (plateKitchenObject.TryAddIngredient(robot.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            robot.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                //p not carrying
                GetKitchenObject().SetKitchenObjectParent(robot);
            }
        }
    }


}
