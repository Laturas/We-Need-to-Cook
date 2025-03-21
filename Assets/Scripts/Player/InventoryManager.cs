using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform[] inventoryItems;
    public Transform[] itemHoldPositions;
    public Holdable selectedObject;

    void Update () 
	{	
        //if mouse button (left hand side) pressed instantiate a raycast
        if(Input.GetMouseButtonDown(0))
		{
             //create a ray cast and set it to the mouses cursor position in game
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, float.PositiveInfinity)) 
			{
                Debug.DrawLine (ray.origin, hit.point);
                Holdable holder;
                if (hit.collider.gameObject.TryGetComponent(out holder)) {
                    holder.Select();
                    if (selectedObject != null) {
                        selectedObject.Deselect();
                    }
                    if (selectedObject == holder) {
                        selectedObject = null;
                    } else {
                        selectedObject = holder;
                    }
                }
			}    
        }
    }
}
