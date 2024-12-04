using UnityEngine;
public enum CustomerState {
	idle,
	askingForCheck,
	waitingForCheck,
}
public class Customer : EntityController
{
	[SerializeField] CustomerState state = CustomerState.idle;
	public void SetState(CustomerState state) => this.state = state;
	public override void Action()
	{
		base.Action();
		switch (state)
		{

			case CustomerState.waitingForCheck:
				LookAtPlayer();
				state = CustomerState.idle;
				DialogueManager.Singleton.StartDialogue(new string[] {
					"Thank you so much for your time and service.",
					"I'm sure we'll meet again shortly.",
					"Very... shortly...",
				}, () => {
					LookAway();
					animator.CrossFade("Idle", 0.5f);
					Owner owner = FindAnyObjectByType<Owner>();
					owner.SetState(OwnerState.waitingToGiveYouTasks);
					owner.SetDestination(GameObject.Find("Player").transform);
					SetDestination(GameObject.Find("Exit").transform);
				});
				break;
			case CustomerState.askingForCheck:
				LookAtPlayer();
				state = CustomerState.idle;
				DialogueManager.Singleton.StartDialogue(new string[] {
					"The food was incredible.",
					"The shrimp was so tasty I could not believe it.",
					"By the way, I've noticed the manager has left you all alone to close the resturant.",
					"I hope you're not afraid of being alone...",
					"Anyways...",
					"I'll have the check please and thank you."
				}, () => {
					FindFirstObjectByType<Cashier>().SetState(CashierState.waitingToPickUpBill);
					LookAway();
				});
				break;
		}
	}
	void Start()
	{
		animator.Play("Sit");
	}
}
