using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stockpile : Interactable {

    public ArrayList resources;

    private void Awake()
    {
        resources = new ArrayList();
    }

    public override void Interact(DwarfControl entity)
    {
        Resource x = entity.carriedResource;

        resources.Add(entity.carriedResource);

        entity.carriedResource = null;

        print("Resource: " + x.resourceName + " added to stockpile");

        //idle entity
        entity.setTask("Idle", null);
    }
}
