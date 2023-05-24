using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    public int goat;
    public int myStates = 0;
    public static GamePlayManager g;
    public Canvas Ui;
    public bool isPlaying = false;
    public GameObject cameraZoom;
    private void Awake()
    {
        GamePlayManager.g = this;
    }

    public void CheckWin()
    {
        Debug.Log("check");
        if (myStates == goat)
        {
            Debug.Log("thang");
            isPlaying = false;
            cameraZoom.GetComponent<CameraZoom>().zoomActive = false;
    
        }
        if (myStates == 0)
        {
            Debug.Log("thua");
            isPlaying = false;
            SceneManager.LoadScene("GamePlayScene");
        }
    }
    public void OnBtnPlay()
    {
        Ui.GetComponent<UiManager>().menu.gameObject.SetActive(false);
        StartCoroutine(Play());
    }
    IEnumerator Play()
    {
        Ui.GetComponent<UiManager>().goBanner.SetActive(true);
        cameraZoom.GetComponent<CameraZoom>().zoomActive = true;
        yield return new WaitForSeconds(1f);
        Ui.GetComponent<UiManager>().goBanner.SetActive(false);
        isPlaying = true;

    }
}
