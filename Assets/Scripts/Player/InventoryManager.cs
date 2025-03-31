using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	public Holdable[] inventoryItems;
	public Transform[] itemHoldPositions;
	public GameObject[] hold_buttons;
	public Holdable selectedObject;
	public GameObject placementWalls;

	void Awake() => inventoryItems = new Holdable[4];

	public void moveSelectedTo(int hold_pos) {
		if (selectedObject == null) {return;}
		selectedObject.GoTo(itemHoldPositions[hold_pos]);
		selectedObject.Deselect();
		for (int i = 0; i < inventoryItems.Length; i++) {
			if (inventoryItems[i] != null && inventoryItems[i] == selectedObject) {
				inventoryItems[i] = null;
				hold_buttons[i].SetActive(true);
			}
		}
		inventoryItems[hold_pos] = selectedObject;
		selectedObject.transform.SetParent(itemHoldPositions[hold_pos]);
		hold_buttons[hold_pos].SetActive(false);
		selectedObject = null;
	}

	void Update ()
	{
		placementWalls.SetActive(selectedObject != null);
		
		//if mouse button (left hand side) pressed instantiate a raycast
		if (Input.GetMouseButtonDown(0))
		{
			//create a ray cast and set it to the mouses cursor position in game
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, float.PositiveInfinity, 1 << 0 | 1 << 3)) 
			{
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
				IHolder holderPos;
				if (hit.collider.gameObject.TryGetComponent(out holderPos)) {
					if (holderPos.SubscribeObject(selectedObject)) {
						selectedObject.Deselect();
						for (int i = 0; i < inventoryItems.Length; i++) {
							if (inventoryItems[i] != null && inventoryItems[i] == selectedObject) {
								inventoryItems[i] = null;
								hold_buttons[i].SetActive(true);
							}
						}
						selectedObject.GoTo(holderPos.getTransform());
						selectedObject = null;
					} else {
						Holdable old = holderPos.UnsubscribeObject();
						selectedObject?.Deselect();
						int movTo = 0;
						for (int i = 0; i < inventoryItems.Length; i++) {
							if (inventoryItems[i] != null && inventoryItems[i] == selectedObject) {
								movTo = i;
								inventoryItems[i] = null;
								hold_buttons[i].SetActive(true);
							}
						}
						selectedObject?.GoTo(holderPos.getTransform());
						selectedObject = old;
						old?.Select();
					}
				}
			}	
		}
	}
}
