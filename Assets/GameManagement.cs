using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{

    public static int round = 1;//1;
    int zombiesNumber = 3;//3;
    int zombiesSpawn = 0;
    float zombiesTimer = 0;

    public Transform[] zombiesPoints;
    //public GameObject zombieEnemy;
    public GameObject[] zombieArray;

    public Text scoreText;
    public Text cashText;
    public Text roundText;
    public Text remainingText;
    public static int playerScore = 0;//0;
    public static int playerCash = 0;//0;

    public static int zombiesRemaining = 3;//3;
    float countdown = 0;


    

    // Update is called once per frame
    void Update()
    {
        
        if (zombiesSpawn < zombiesNumber && countdown==0)
        {
            if (zombiesTimer > 3)
            {
                SpawnZombies();
                zombiesTimer = 0;
            }
            else zombiesTimer += Time.deltaTime;
        }
        else if(zombiesRemaining == 0)
        {
            StartNextRound();
        }

        scoreText.text = "SCORE " + playerScore;
        cashText.text = "CASH " + playerCash;
        roundText.text = "ROUND " + round;
        

        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            remainingText.text = "NEXT " + Mathf.RoundToInt(countdown);
        }
        else
        {
            countdown = 0;
            remainingText.text = "SPAWNED " + zombiesRemaining;
        }



    }


    void SpawnZombies()
    {
        int rand = Random.Range(0, zombieArray.Length);
        Vector3 randomSpawnPoint = zombiesPoints[Random.Range(0, zombiesPoints.Length)].position;
        Instantiate(zombieArray[rand], randomSpawnPoint, Quaternion.identity);
        zombiesSpawn++;
    }



    public static void AddPoints(int point)
    {
        playerScore += point;
        playerCash += point;
    }

    public static int PlayerScore()
    {
        return playerScore;
    }
    public static int PlayerCash()
    {
        return playerCash;
    }
    public static void AddCash(int cash)
    {
        playerCash += cash;
    }
    public static void SubCash(int cash)
    {
        playerCash -= cash;
    }

    void StartNextRound()
    {
        zombiesRemaining = round * 2 + 1;
        zombiesNumber = zombiesRemaining;
        
        zombiesSpawn = 0;
        countdown = 15;
        round++;

    }
}
