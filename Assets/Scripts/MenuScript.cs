using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuScript : MonoBehaviour
{
    public TMP_InputField NameObj;
    public string DefaultName;
    public TMP_Text ErrorText;

    public TMP_Text HighScoreName;
    public TMP_Text HighScore;

    // Start is called before the first frame update
    void Start()
    {
        //turning the errortext off until it is needed
        ErrorText.gameObject.SetActive(false);

        //grab the default text of the Name box and set it in a variable to check against
        DefaultName = NameObj.GetComponent<TMP_InputField>().text;

        ShowHighScore();
    }

    void ShowHighScore(){
        //check to make sure we actually have a high score
        if(PlayerData.Instance.Score_HighScore != 0){
            HighScoreName.text = PlayerData.Instance.Name_HighScore;
            HighScore.text = PlayerData.Instance.Score_HighScore.ToString();
        } else {
            //set hide the score#'s and set the current high score to none
            HighScoreName.text = "Be the first!";
            HighScore.gameObject.SetActive(false);
        }

    }

   public void OnStart()
    {
        //check to see if they have entered a name
        string name = NameObj.GetComponent<TMP_InputField>().text;
        if(name == DefaultName){
            // if player has not set a name, show the error text
            ErrorText.gameObject.SetActive(true);
        } else {
            //set the playername in the playerdata instance
            PlayerData.Instance.PlayerName = name;
            //load the next scene
            SceneManager.LoadScene(1);
        }

    }

    public void OnExit()
    {
        //checking to see if we are inside unity or in a compiler, the # indicates an instruction for the compiler specifically
        //this is why it uses this format instead of the normal if/else statement. The compiler will automatically remove unused code
        //if done in this way

        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
