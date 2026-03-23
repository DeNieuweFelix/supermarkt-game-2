using UnityEngine;

public class MenuControls : MonoBehaviour
{


    public MenuStates m_States = new MenuStates();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        if (!m_States.IsPaused)
        {
            TogglePause(true);
            Debug.Log("Paused game!");
        }
        else
        {
            TogglePause(false);
            Debug.Log("Unpaused game!");
        }

        m_States.IsPaused = !m_States.IsPaused;
    }

    private void TogglePause(bool toggle)
    {
        if (toggle)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
}
[System.Serializable]
public struct MenuStates
{
    public bool IsPaused;
}
