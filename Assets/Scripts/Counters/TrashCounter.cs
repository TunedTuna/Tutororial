using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    new public static void ResetStaticData()
    {
        OnAnyObjectTrash = null;
    }

    public static event EventHandler OnAnyObjectTrash;
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();

            OnAnyObjectTrash?.Invoke(this,EventArgs.Empty);
        }
    }
}
