using UnityEngine;

public enum TableState {
	idle,
	waitingToClean,
}
public class Table : Interactable
{
	TableState state = TableState.waitingToClean;
	public override void Action()
	{
		base.Action();
		switch (state)
		{
			case TableState.waitingToClean:
				state = TableState.idle;
				DialogueManager.Singleton.StartDialogue(new string[] {
					"Okay I've cleaned this table.",
					"What is this?",
					"The customer forgot something.",
					"It looks like a note",
					"It says...",
					"\"This is your last night.\"",
				}, () => {
					TaskManager.Singleton.CompleteTask(1);
				});
				break;
		}
	}

}
