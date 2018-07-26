using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// processes actions and tasks
// central component of dwarf, references all other important components on this dwarf
public class DwarfControl : MonoBehaviour {
    
    public DwarfMotion motor;       // Motion component, handles granular movement

    public DwarfSkills skills;      // Skills, holds info about occupation and skills

    public Resource carriedResource;// Resource this guy is currently hauling

    public Interactable stockPile;  // for testing remember to remove

    public Interactable focus;      // currently focused object

    public string task;             //specific task we're trying to perform 

    //flags for the job manager
    public bool isWorking = false;
    public bool isTasked = false;

    public bool hasInteracted = false; // flag for interaction

	void Start () 
    {
        // get components
        motor = GetComponent<DwarfMotion>();
        skills = GetComponent<DwarfSkills>();

        setTask("Wander", null);
	}

    void Update()
    {
        //if we're interacting, check distance until we're in range, then perform action
        if(task.Equals("Interact") && !hasInteracted)
        {
            float distance = Vector3.Distance(transform.position, focus.transform.position);

            if(distance <= focus.radius){
                hasInteracted = true;
                focus.Interact(this);
            }
        }
    }

    //set the task then figure out what to do
    //TODO make a task and object instead of a string
    public void setTask(string taskName, Interactable target) 
    {
        task = taskName;

        if(task.Equals("Wander")){
            motor.startWander();
            return;
        }

        if(task.Equals("Idle")){
            isTasked = false;
            //hasInteracted = false;
            return;
        }

        if(task.Equals("Follow") || task.Equals("Interact")){
            //set focus
            SetFocus(target);
        }

        if (task.Equals("Deposit"))
        {
            //set focus to the correct stockpile
            //TODO make an object/method that selects the correct stockpile
            task = "Interact";
            SetFocus(stockPile);
        }
    }

    void SetFocus (Interactable newFocus)
    {
        focus = newFocus;
        motor.FollowTarget(newFocus);
        hasInteracted = false;
    }

    public void RemoveFocus ()
    {
        focus = null;
        motor.StopFollowTarget();
        hasInteracted = false;
    }

    //Items and inventory

    // carried Resource
    public void CarryResource(Resource item){

        print("Gained: " + item.resourceName);

        //TODO make into method to handle conflicts and animations
        carriedResource = item;
    }
}
