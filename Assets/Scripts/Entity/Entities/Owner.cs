using UnityEngine;
public enum OwnerState {
	idle,
	walkingToPlayer,
	walkingToOffice,
	finishedListeningToRadio,
	waitingToGiveYouTasks,
	leaving,
}
public class Owner : EntityController
{
	[SerializeField] OwnerState state = OwnerState.walkingToPlayer;
	public void SetState(OwnerState state) => this.state = state;
	public override void Action()
	{
		base.Action();
		switch (state) {
			case OwnerState.waitingToGiveYouTasks:
				LookAtPlayer();
				DialogueManager.Singleton.StartDialogue(new string[] {
					"Fuck me that customer did not seem to want to leave.",
					"He's been here for two hours sitting along watching people",
					"Well...",
					"I'm going home,",
					"and I'm leaving you alone to close the restaurant.",
					"You can hit TAB to see your task list.",
					"Make sure to clean up properly this time or you won't get your last check.",
					"Now get out of my way."
				}, () => {
					TaskManager.Singleton.SetActive();
					SetDestination(GameObject.Find("Exit").transform);
					LookAway();
				});
				break;
			case OwnerState.finishedListeningToRadio:
				LookAtPlayer();
				state = OwnerState.idle;
				DialogueManager.Singleton.StartDialogue(new string[] {
					"God this town is being cursed.",
					"People are getting lonely and crazy and start killing ...",
					"Anyways.",
					"Get the bill for that last guest so we can close.",

				}, () => {
					FindFirstObjectByType<Customer>().SetState(CustomerState.askingForCheck);
					LookAway();
					SetDestination(GameObject.Find("Behind Store").transform);
				});
				break;
			case OwnerState.walkingToPlayer:
				LookAtPlayer();
				state = OwnerState.walkingToOffice;
				DialogueManager.Singleton.StartDialogue(new string[] {
						"Again thanks for coming in today.",
						"I've been so fucking stressed at this place lately.",
						"I have to talk to you about something.",
						"Meet me in my office"}, () => {
							SetDestination(GameObject.Find("Office").transform);
							LookAway();
					});
				break;
			case OwnerState.walkingToOffice:
				if (DestinationDistance() > 5)
					return;
				state = OwnerState.idle;
				LookAtPlayer();
				DialogueManager.Singleton.StartDialogue(new string[] {
					"This restaurant cannot support itself, ",
					"And I'm going to have to let you off.", 
					"And there's nothing I can do, the money is just not enough.",
					"I've tried m-"
					}, () => {
						LookAway();
						FindFirstObjectByType<Radio>().Action(() => state = OwnerState.finishedListeningToRadio);
					}
				);
				break;
		}
	}
	void Start()
	{
		SetDestination(GameObject.Find("Player").transform);
	}
}
