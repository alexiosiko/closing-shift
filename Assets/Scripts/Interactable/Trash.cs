using UnityEngine;

public class Trash : Interactable
{
	public override void Action()
	{
		base.Action();
		GetComponent<MeshRenderer>().enabled = false;
		GetComponent<Collider>().enabled = false;
		DialogueManager.Singleton.StartDialogue(new string[] {
			"Yuck this trash is filthy.",
			"I should throw it out in the bin outside behind the kitchen."
		}, () => {
			FindFirstObjectByType<TrashBin>().SetState(TrashBinState.waitingForTrash);
		});
	}
}
