using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameOver : MonoBehaviour
{
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
