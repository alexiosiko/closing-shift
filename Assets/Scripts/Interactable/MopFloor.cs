using UnityEngine;
public enum MopFloorState {
	idle,
	waitingToMop,
}
public class MopFloor : Interactable
{
	MopFloorState state = MopFloorState.waitingToMop;
	public override void Action()
	{
		base.Action();
		switch (state)
		{
			case MopFloorState.waitingToMop:
				GetComponent<MeshRenderer>().enabled = false;
				GetComponent<Collider>().enabled = false;
				state = MopFloorState.idle;
				DialogueManager.Singleton.StartDialogue(new string[] {
					"Who was the idiot that spilt their drink anyways...",
					"Anyways, it's not perfect but the floor is less sticky than before."
				}, () => {
					TaskManager.Singleton.CompleteTask(2);
				});
				break;
		}
	}
}
