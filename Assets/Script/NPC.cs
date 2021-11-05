using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
public class NPC : MonoBehaviour
{
    public YarnProgram currentDialogueYarnProgarm;
    public string CurrentStartNode = "Start";
    public DialogueRunner dialogueRunner;


    public void SetDialogue()
    {
        if (currentDialogueYarnProgarm != null && dialogueRunner != null)
        {
            dialogueRunner.yarnScripts[0] = currentDialogueYarnProgarm;
            dialogueRunner.StartDialogue();
        }
        
    }

   
}
