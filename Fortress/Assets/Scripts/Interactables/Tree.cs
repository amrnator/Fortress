using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Interactable {

    public Resource resource;

    public int quantity = 2;

    public override void Interact(DwarfControl entity)
    {
        base.Interact(entity);

        //give entity wood
        entity.CarryResource(resource);

        //send to stockpile
        entity.setTask("Deposit", null);

        quantity--;

        if(quantity <= 0){
            harvested = true;
        }
    }
}
