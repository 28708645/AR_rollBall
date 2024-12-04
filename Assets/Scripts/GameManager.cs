using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
public enum GAMESTATE { play, start, finished}
public class GameManager : MonoBehaviour
{

    private GAMESTATE gs;
    private float startTime;
    private float time;
    private int collected;
    private SceneManager sc;
    [Header("Prefab References")]
    [SerializeField]
    private GameObject ringParent;
    // disable the spawner to avoid spawning multiples balls
    [SerializeField]
    PlaceObjectOnPlane spawner;
    private int target;
    [Header("UI References")]
    [SerializeField]
    GameObject startMenu;
    [SerializeField]
    GameObject playMenu;
    [SerializeField]
    GameObject restartMenu;
    [SerializeField]
    private TextMeshProUGUI countText;
    [SerializeField]
    private TextMeshProUGUI timerText;


    // Start is called before the first frame update
    void Start()
    {
        gs = GAMESTATE.start;
        collected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gs == GAMESTATE.play)
        {
            displayPlayUI();
            updateTimer();
        }
        if (gs == GAMESTATE.finished)
        {
            displayRestart();
        }
    }
    //Display functions
    private void displayPlayUI()
    {
        if (!playMenu.activeSelf)
        {
            playMenu.SetActive(true);
        }
        if(collected >= target)
        {
            finished();
        }
    }
    private void displayRestart()
    {
        if (!restartMenu.activeSelf)
        {
            restartMenu.SetActive(true);
        }
    }
    //update UI
    private void updateTimer()
    {
        time = Time.time - startTime;
        timerText.text = "Timer : " + time.ToString("F2");
    }
    public void updateCount(bool up=true)
    {
        if (up)
        {
            collected++;
        }
        countText.text = "Collected : " + collected.ToString()+"/"+target.ToString();
    }
    //Changing state functions
    public void startPlay()
    {
        gs = GAMESTATE.play;
        startTime = Time.time;
        target = ringParent.transform.childCount;
        startMenu.SetActive(false);
        spawner.enabled = false;
        updateCount(false);
    }
    private void finished()
    {
        Debug.Log("finished");
        gs = GAMESTATE.finished;
       
    }
    public void restart()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public GAMESTATE getGameState()
    {
        return gs;
    }
}