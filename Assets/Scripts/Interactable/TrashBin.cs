
public enum TrashBinState {
	idle,
	waitingForTrash
}
public class TrashBin : Interactable
{
	public TrashBinState state = TrashBinState.idle;
	public void SetState(TrashBinState state) => this.state = state;
	public override void Action()
	{
		base.Action();
		switch (state)
		{
			case TrashBinState.waitingForTrash:
			state = TrashBinState.idle;
			DialogueManager.Singleton.StartDialogue(new string[] {
				"Ew that was smelly.",
				"Well that done and over with."
			}, () => {
				TaskManager.Singleton.CompleteTask(0);
			});
				break;
		}
	
	}
}
