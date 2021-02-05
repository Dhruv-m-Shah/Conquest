using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class optionsLoader : MonoBehaviour
{
    void switchSceneCreateGame()
    {
        SceneManager.LoadScene(sceneName: "createGame");
    }
    void switchSceneJoinGame()
    {
        SceneManager.LoadScene(sceneName: "joinGame");
    }
    // Start is called before the first frame update.
    void Start()
    {
        UnityEngine.UI.Button createGameButton = GameObject.FindGameObjectWithTag("createGame").GetComponent<Button>();
        UnityEngine.UI.Button joinGameButton = GameObject.FindGameObjectWithTag("joinGameButton").GetComponent<Button>();
        createGameButton.onClick.AddListener(() => switchSceneCreateGame());
        joinGameButton.onClick.AddListener(() => switchSceneJoinGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
