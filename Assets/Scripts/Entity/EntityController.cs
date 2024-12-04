using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(IKAnimatorController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EntityAnimationController))]
[RequireComponent(typeof(EntityAudio))]
public class EntityController : Interactable
{
	[SerializeField] Transform destination;
	public void SetDestination(Transform destination){ 
		print(destination);
		this.destination = destination;
	}
	public virtual void Update()
	{
		if (destination)
			nav.SetDestination(destination.position);
	}
	public void LookAtPlayer() => ikAnimator.EnableIK();
	public void LookAway() => ikAnimator.DisableIK();
	public override void Action()
    {
		base.Action();
    }

	protected float DestinationDistance() => Vector3.Distance(transform.position, destination.position);
    protected virtual void Awake()
    {
		ikAnimator = GetComponent<IKAnimatorController>();
		nav = GetComponent<NavMeshAgent>();
		entityAudio = GetComponent<EntityAudio>();
		animator = GetComponent<Animator>();
    }
	protected IKAnimatorController ikAnimator;
	NavMeshAgent nav;
	public void PlayAudio(string audioName, Action onComplete = null)
	{
		StopAllCoroutines();
		StartCoroutine(entityAudio.PlayAudioEnum(audioName, onComplete));
	}
	protected EntityAudio entityAudio;
	protected Animator animator;
}
