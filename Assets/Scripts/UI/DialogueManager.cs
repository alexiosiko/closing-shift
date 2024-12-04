using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Singleton;
    [SerializeField] TMP_Text text;
    Action onComplete;
    bool allowInput = true;
    string[] texts;
    int chatIndex = 0;
    bool busy = false;
    bool isAnimating = false;
    Coroutine textAnimationRoutine;

    public void OverrideBusy(bool busy) => this.busy = busy;

    void Awake()
    {
        Singleton = this;
    }

    // Method for starting a dialogue with multiple texts
    public void StartDialogue(string[] texts, Action onComplete = null)
    {
        if (busy)
            return;

        this.texts = texts;
        this.onComplete = onComplete;
        chatIndex = 0;
        busy = true;

        StatusManager.Singleton.Freeze(); // Freeze the game or player
        DisplayNextText(); // Display the first text
    }

    // Display the next text or end dialogue if no more text
    private void DisplayNextText()
    {
        if (chatIndex >= texts.Length) // If no more texts are left
        {
            EndDialogue();
            return;
        }

        if (textAnimationRoutine != null)
            StopCoroutine(textAnimationRoutine);

        text.text = ""; // Clear the current text
        isAnimating = true;
        textAnimationRoutine = StartCoroutine(AnimateTextRoutine(texts[chatIndex]));
        chatIndex++;
    }

    // End the dialogue and reset states
    private void EndDialogue()
    {
        if (textAnimationRoutine != null)
            StopCoroutine(textAnimationRoutine);

        text.text = ""; // Clear the dialogue text
        StatusManager.Singleton.UnFreeze(); // Unfreeze the game or player
        busy = false; // Set busy to false to prevent restarting
        onComplete?.Invoke(); // Call the callback if it exists
    }

    // Coroutine to animate text letter by letter
    private IEnumerator AnimateTextRoutine(string message)
    {
        for (int i = 0; i < message.Length; i++)
        {
            text.text = message.Substring(0, i + 1); // Add the next letter
            yield return new WaitForSeconds(0.05f); // Wait between each letter
        }

        isAnimating = false; // Animation complete
        allowInput = true; // Allow user to proceed
    }

    // Update method to listen for user input
    void Update()
    {
        if (busy && Input.GetMouseButtonDown(0))
        {
            if (isAnimating)
            {
                // If animating, instantly complete the text
                StopCoroutine(textAnimationRoutine);
                text.text = texts[chatIndex - 1]; // Show the full text
                isAnimating = false;
                allowInput = true; // Allow user to proceed
            }
            else if (allowInput)
            {
                // If animation is complete, show the next text
                DisplayNextText();
            }
        }
    }
}
