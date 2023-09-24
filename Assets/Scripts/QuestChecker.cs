using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestChecker : MonoBehaviour
{

    [SerializeField] private GameObject dialogueBox, finishedText, unfinishedText;
    [SerializeField] private int questGoal = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerMovement>().coinsCollected >= questGoal)
            {
                dialogueBox.SetActive(true);
                finishedText.SetActive(true);
                // TODO: Change Level

            }
            else
            {
                dialogueBox.SetActive(true);
                unfinishedText.SetActive(true);
            }

        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueBox.SetActive(false);
            finishedText.SetActive(false);
            unfinishedText.SetActive(false);
        }
    }
}
