using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text NameText;
    public GameObject GameOverText;
    public GameUIHandler GameUIHandler;

    private bool m_Started = false;
    private bool m_GameOver = false;
    public int m_Points;
    
    private void Start()
    {
        //If there is a SaveInfo instance from menu screen set the player name to CurrentPlayer var
        if (SaveInfo.Instance != null)
        {
            NameText.text = SaveInfo.Instance.CurrentPlayer;
        }
        else
        //otherwise name the player N/A if skipped the menu screen
        {
            NameText.text = "N/A";
        }


        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
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

    private void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        //If current points are greater than the HiScore replace the HiScorePlayer and HiScore in the SaveInfo script with current info
        if (SaveInfo.Instance != null)
        {
            if (m_Points > SaveInfo.Instance.HiScore)
            {
                SaveInfo.Instance.HiScore = m_Points;
                SaveInfo.Instance.HiScorePlayer = SaveInfo.Instance.CurrentPlayer;

                //Runs method from GameUIHandler script to update the HiScore text box
                GameUIHandler.UpdateHiScore();

                //Runs method to save the new name and score to JSON
                SaveInfo.Instance.SaveName();
            }
        }
        //Runs method from GameUIHandler script to update the HiScore text box
        GameUIHandler.UpdateHiScore();

        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    private void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
