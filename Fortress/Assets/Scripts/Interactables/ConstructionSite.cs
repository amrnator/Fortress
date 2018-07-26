using System.Collections;
using UnityEngine;

// Interactable object for a construction site
// once builders interact with it enough, it will be replaced with a building
public class ConstructionSite : Interactable {

    public int progress = 5;

    // TODO glitch where progress ends at -1
    public override void Interact(DwarfControl entity)
    {
        base.Interact(entity);

        progress--;             // chip at progress

        if (progress <= 0)
        {
            //we done
            harvested = true;
            //idle entity
            entity.setTask("Idle", null);
        }else{
            // wait a bit before reseting hasInteracted
            StartCoroutine(pause(1.0f, entity));
        }

        if(progress == -1)
        {
            //make the building
            construct();
        }
    }

    // make the building 
    private void construct()
    {
        print("Built");
    }

    private IEnumerator pause(float waitTime, DwarfControl entity)
    {
        yield return new WaitForSeconds(waitTime);
        entity.hasInteracted = false;
    }
}
