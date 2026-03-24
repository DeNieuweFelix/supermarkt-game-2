using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControls : MonoBehaviour
{


    public MenuStates m_States = new MenuStates();

    [SerializeField] private GameObject selectedButton;
    [SerializeField] private List<GameObject> allButtons;

    [SerializeField] private GameObject pauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        StartCoroutine(SpinEffect());
    }

    public void PauseGame()
    {
        if (!m_States.IsPaused)
        {
            TogglePause(true);

            pauseMenu.SetActive(true);
            Debug.Log("Paused game!");
        }
        else
        {
            TogglePause(false);

            pauseMenu.SetActive(false);
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

    public void EnterButton(int index)
    {
        selectedButton = allButtons[index];
    }

    public void ExitButton()
    {
        selectedButton = null;
    }


    private IEnumerator SpinEffect()
    {
        int i = 0;
        while (true)
        {
            if(selectedButton != null)
            { 
                float r = Mathf.Cos(Time.realtimeSinceStartup * 5f);
                selectedButton.transform.GetChild(0).transform.localScale = new Vector3(
                    r,
                    1f + (r / 6),
                    1f + (r / 6)
                );
            }

            foreach(GameObject b in allButtons)
            {
                if(b.transform.GetChild(0).transform.localScale != Vector3.one)
                {
                    Transform t = b.transform.GetChild(0).transform;
                    
                    t.localScale = Vector3.Lerp(t.localScale, Vector3.one, 10f * Time.unscaledDeltaTime);
                }
            }

            i++;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
}
[System.Serializable]
public struct MenuStates
{
    public bool IsPaused;
}
