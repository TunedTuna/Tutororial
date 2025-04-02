using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler onPlateSpawned;
    public event EventHandler onPlateRemoved;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax=4f;
    private int plateSpawnedAmount;
    private int plateSpawnedAmountMax=4;
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            if(plateSpawnedAmount<plateSpawnedAmountMax)
            {
                plateSpawnedAmount++;

                onPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //Playr is empty handed
            if (plateSpawnedAmount > 0)
            {
                //at least 1 plate here 
                plateSpawnedAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                onPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
