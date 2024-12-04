using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    private float reachDistance = 3f;
    void LateUpdate()
    {
        Debug.DrawLine(cameraPosition.position, cameraPosition.position + cameraPosition.transform.forward * reachDistance, Color.green);

		// Highlight();

		if (StatusManager.Singleton.GetFreeze())
			return;

        // Get interact input
       	if (Input.GetKeyDown("e")
            || Input.GetMouseButtonDown(0))
            Interact();

    }
    void Interact()
    {
        if (Physics.Raycast(cameraPosition.position, cameraPosition.transform.forward, out RaycastHit hit, reachDistance))
        {
            Interactable i = hit.collider.GetComponentInParent<Interactable>();
            if (i != null)
                i.Action();
        }
    }
	void Highlight() {
		if (Physics.Raycast(cameraPosition.position, cameraPosition.transform.forward, out RaycastHit hit, reachDistance))
        {
            Interactable i = hit.collider.GetComponentInParent<Interactable>();
            if (i != null)
                CanvasManager.Singleton.Highlight(i.highlightText);
			else
				CanvasManager.Singleton.Highlight(""); // Clear
        }
		else
			CanvasManager.Singleton.Highlight(""); // Clear

	}

    public Transform cameraPosition;
}