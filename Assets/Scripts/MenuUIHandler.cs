using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Execute this UI script last in case other scripts might need to initialize first
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public Text playerNameText;
    public Text HiScoreTextInMenu;
    
    public void Start()
    {
        //Sets the hi score text to previous high score
        SetName();
    }

    public void ChangeName(string playerName)
    {
        //created method to carry player name between scenes
        playerNameText.text = playerName;
        SaveInfo.Instance.CurrentPlayer = playerName;
    }

    public void SetName()
    {
        HiScoreTextInMenu.text = "Best Score : " + SaveInfo.Instance.HiScorePlayer + " : " + SaveInfo.Instance.HiScore;
    }

    public void ResetScore()
    {
        SaveInfo.Instance.ClearName();
        SaveInfo.Instance.LoadName();
        SetName();
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitAppButton()
    {
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}
