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
    // Start is called before the first frame update.
    void Start()
    {
        UnityEngine.UI.Button createGameButton = GameObject.FindGameObjectWithTag("createGame").GetComponent<Button>();
        //asdasd
        createGameButton.onClick.AddListener(() => switchSceneCreateGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
