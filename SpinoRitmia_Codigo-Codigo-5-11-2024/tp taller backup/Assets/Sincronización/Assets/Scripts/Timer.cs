using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public int escena = 10; 

    public Activador a;
    public Nota n;
    private TMP_Text timerText;
    enum TimerType{Countdown,Stopwatch}
    [SerializeField] private TimerType timerType;
    [SerializeField] public float timeToDisplay = 60.0f;
    private bool _isRunning;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake(){
        timerText = GetComponent<TMP_Text>();
    }

private void OnEnable(){
    EventManager.TimerStart += EventManagerOnTimerStart;
    EventManager.TimerStop += EventManagerOnTimerStop;
    EventManager.TimerUpdate += EventManagerOnTimerUpdate;

}
private void OnDisable(){
    EventManager.TimerStart -= EventManagerOnTimerStart;
    EventManager.TimerStop -= EventManagerOnTimerStop;
    EventManager.TimerUpdate -= EventManagerOnTimerUpdate;
}
private void EventManagerOnTimerStart() => _isRunning = true;
private void EventManagerOnTimerStop()=> _isRunning = false;
private void EventManagerOnTimerUpdate(float value)=> timeToDisplay += value;
    // Update is called once per frame
   private void Update()
    {
        if(!_isRunning) return;
        if((a.respawn - timeToDisplay) > 5){
            n.fuego = false;
            Debug.Log("Fuego reactivado");
        }
        if(timerType == TimerType.Countdown && timeToDisplay < 0.0f){ 
            EventManager.OnTimerStop();
            //BF.LoadScene(3);
            if(escena == 10){
                escena = 8;
                SceneManager.LoadScene(escena);
            }
             else if(escena == 8) {
                escena = 11;
                SceneManager.LoadScene(escena);
            }
            else if(escena == 11){
                escena = 9;
                SceneManager.LoadScene(escena);
            }
            else if (escena == 9){
                escena = 13;
                SceneManager.LoadScene(escena);
            }
            else if (escena == 13){
                escena = 12;
                SceneManager.LoadScene(escena);
            }
            else if (escena == 12){
                escena = 3;
                SceneManager.LoadScene(escena);
            }
            return;}
        timeToDisplay+= timerType == TimerType.Countdown ? -Time.deltaTime : Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);
        timerText.text = timeSpan.ToString(format:@"mm\:ss\:ff");
    }
}