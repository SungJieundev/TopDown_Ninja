using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private SaveSystem saveSystem;

    public UnityEvent<bool> OnResultData;

    private void Awake() {
        
        if(Instance == null){

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{

            Destroy(gameObject);
        }

        OnResultData?.Invoke(File.Exists(Application.dataPath + "/SaveData/SaveFile.txt"));
    }

    public void ClickStart(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1 );
    }

    public void ClickLoad(){

        StartCoroutine(LoadRoutine());
    }

    IEnumerator LoadRoutine(){

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex +1);

        while(!operation.isDone){

            yield return null;
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1 );

        saveSystem = FindObjectOfType<SaveSystem>();
        saveSystem.Load();
    }
}
