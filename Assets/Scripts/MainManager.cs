using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text NameText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    public TMP_Text HighScoreName;
    public TMP_Text HighScore;
    public int HighScoreVal;
    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }


        /* START OF MY ADDED CODE */
        //Show the player name by the score
        ScoreText.text = PlayerData.Instance.PlayerName + $"'s Score : {m_Points}";

        //grab the high score & name and add it to the game object
        UpdateHighScore();

    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void UpdateHighScore(){
           //check to make sure we actually have a high score
           if(m_Points > PlayerData.Instance.Score_HighScore){
               //change on screen display if they have the high score
               HighScoreName.text = PlayerData.Instance.PlayerName;
               HighScore.text = m_Points.ToString();
                //turn the score field object back on
                HighScore.gameObject.SetActive(true);
               
               //change the global high score
               PlayerData.Instance.Name_HighScore = PlayerData.Instance.PlayerName;
               PlayerData.Instance.Score_HighScore = m_Points;

               PlayerData.Instance.SaveHighScore();

            } else if(PlayerData.Instance.Score_HighScore != 0){
                HighScoreName.text = PlayerData.Instance.Name_HighScore;
                HighScore.text = PlayerData.Instance.Score_HighScore.ToString();
            } else {
                //set hide the score#'s and set the current high score to none
                HighScoreName.text = "Be the first!";
                HighScore.gameObject.SetActive(false);
            }
    }
    
    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = PlayerData.Instance.PlayerName + $"'s Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        UpdateHighScore();
    }
}
