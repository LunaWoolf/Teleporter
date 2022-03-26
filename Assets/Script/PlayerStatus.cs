using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;


public static class PlayerStatus
{
    //new Vector3(235.63f, 1.55f, -15.76f);

    [SerializeField] public static Vector3 playerPosition = new Vector3(233.65f, 1.55f, -21.5f);

        public static string questStatus;

        public static GameManager.QuestSign[] QuestList;

        public static string currentQuest = "Quest1_P";
        
        public static YarnProject currentYarnProject;

        public static string playerName = "Luna";





}
