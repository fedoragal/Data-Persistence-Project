using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour
{
    //declares an instance of this class
    public static PlayerData Instance;

    //string for storing entered player name & current score
    public string PlayerName;
    public int PlayerScore;

    //variables for storing highscore info
    public string Name_HighScore;
    public int Score_HighScore;

    private void Awake(){

        /*checking to see if there is already an instance loaded
        if so it will be deleted, this only happens on game load first ime */
        if(Instance != null){
            Destroy(gameObject);
            return;
        }

        //creates a reference to this instance so we can access from anywhere
        Instance = this;
        //sets the gameObject to not be destroyed between scenes
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    //creates the JSON class for storing the info
    [System.Serializable]
    class SaveData{
        public string SaveName;
        public int SaveScore;
    }


    public void SaveHighScore(){
        /*checks to see if the new score is better than the current high score
          then saves the new score if it is */
          if(PlayerScore > Score_HighScore){
              SaveData data = new SaveData();
              data.SaveScore = PlayerScore;
              data.SaveName = PlayerName;

              string json = JsonUtility.ToJson(data);

              File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
          }

    }
    public void LoadHighScore(){
        // will load the high score info from JSON file
        string path = Application.persistentDataPath + "/highscore.json";

        //checks that there is actually a highscore file
        if (File.Exists(path)){
            //loads the info to string
            string json = File.ReadAllText(path);
            //parses the string to classes
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            //sets our high score variables based on the info from the file
            Name_HighScore = data.SaveName;
            Score_HighScore = data.SaveScore;
        }
    }


}
