using UnityEngine;

public enum CashierState {
	idle,
	waitingToPickUpBill,
}
public class Cashier : Interactable
{
	public void SetState(CashierState state) => this.state = state;
	CashierState state = CashierState.idle;
	public override void Action()
	{
		base.Action();
		switch (state)
		{
			case CashierState.waitingToPickUpBill:
			 	FindFirstObjectByType<Customer>().SetState(CustomerState.waitingForCheck);
				DialogueManager.Singleton.StartDialogue(new string[] {
					"Okay I've git his bill.",
					"Weird, he only spent $30 and has been here for over 2 hours."
				});
				state = CashierState.idle;
				break;
		}
	}
}
