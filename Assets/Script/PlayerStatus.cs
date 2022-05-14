using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;


public static class PlayerStatus
{
    //new Vector3(235.63f, 1.55f, -15.76f);
    //new Vector3(-35.7f, 21f, -99f)

    [SerializeField] public static Vector3 playerPosition = new Vector3(233.65f, 1.55f, -21.5f);
    //[SerializeField] public static Vector3 playerPosition = new Vector3(-18.9f, 21f, -99f);

    public static string questStatus;

        public static GameManager.QuestSign[] QuestList;

        public static string currentQuest = "Quest1_P";
        
        public static YarnProject currentYarnProject;

        public static string playerName = "Luna";





}
