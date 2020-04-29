using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    public static ScoreManager Instance;
    public int currentScore {
        get { return mCurrentScore; }
    }
    private int mCurrentScore;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    public void AddToScore(int points)
    {
        mCurrentScore += points;

        scoreText.text = "Score: " + mCurrentScore;
    }
}
