using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

[Serializable]

public class User
{
   public string userName;
   public int userScore;


   public User(){
       userName = GameManager.playerName;
       userScore = GameScore.score;
   }
}
