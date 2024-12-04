public enum OfficeDeskState {
	idle,
	waitingToCheck,
}
public class OfficeDesk : Interactable
{
	OfficeDeskState state = OfficeDeskState.waitingToCheck;
	public override void Action()
	{
		base.Action();
		switch (state)
		{
			case OfficeDeskState.waitingToCheck:
				state = OfficeDeskState.idle;
				DialogueManager.Singleton.StartDialogue(new string[] {
					"Okay, so I've logged todays sales done today",
				}, () => {
					TaskManager.Singleton.CompleteTask(3);
				});
				break;
		}
	}
}
