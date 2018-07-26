using UnityEngine;

public class Interactable : MonoBehaviour {

    public float radius = 3.0f;

    bool isFocus = false;

    public bool harvested = false;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, radius);
    }


    public virtual void Interact(DwarfControl entity)
    {
        //meant to be overwritten
        print("Interacting!");
    }

}
