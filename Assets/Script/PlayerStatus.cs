using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;


public static class PlayerStatus
{
        [SerializeField] public static Vector3 playerPosition = new Vector3(235.63f, 1.55f, -15.76f);

        public static string questStatus;

        public static GameManager.QuestSign[] QuestList;

        public static string currentQuest = "Quest1_P";

        public static YarnProject currentYarnProject;

        public static string playerName = "Luna";





}
