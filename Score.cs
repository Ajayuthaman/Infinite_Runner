using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float score= 0.0f;

    private int difficultyLevel = 1;
    private int maxDifficaltyLevel = 10;
    private int scoreToNextLevel = 10;

    private bool isDead=false;

    public Text scoreText;
    public DeathMenu deathMenu;

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        if (score >= scoreToNextLevel)
        {
            LevelUp();
        }

        score += Time.deltaTime  * difficultyLevel;

        scoreText.text = ((int)score).ToString();
    }

    private void LevelUp()
    {
        if(difficultyLevel == maxDifficaltyLevel)
        {
            return;
        }
        scoreToNextLevel *= 3;

        difficultyLevel++;

        GetComponent<PlayerMovement>().SetSpeed(difficultyLevel);

    }

    public void OnDeath()
    {
        isDead = true;
        deathMenu.ToggleEndMenu(score);
    }
}
