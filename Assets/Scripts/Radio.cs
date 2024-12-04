using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Radio : MonoBehaviour
{
    public void Action(Action onComplete = null)
    {
        source.Play();
        this.onComplete = onComplete; // Store the callback
        Invoke(nameof(DonePlay), source.clip.length + 1);
    }

    private void DonePlay() => onComplete?.Invoke();

    void Awake() => source = GetComponent<AudioSource>();

    private AudioSource source;
    private Action  onComplete;
}
