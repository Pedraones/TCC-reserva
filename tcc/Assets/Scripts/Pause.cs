using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public bool IsPaused = false;
    public GameObject PausePanel;

    void Start()
    {
        PausePanel.SetActive(false);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        
        if (Input.GetButtonDown("Cancel"))
        {
            if (!IsPaused) 
            {
                Paused();
            }
            else
            {
                Resume();
            }
        }

    }
    public void Resume()
    {
        
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }
    public void Paused()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        IsPaused = true;

    }
}
