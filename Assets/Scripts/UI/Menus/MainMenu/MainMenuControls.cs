using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuControls : MonoBehaviour
{
    [SerializeField] private Button[] mainButtons = new Button[3];
    [SerializeField] private int[] buttonsToScale = new int[2];
    [SerializeField] private int selectedButton = 1;
    private RectTransform[] buttonsRects = new RectTransform[3];

    [SerializeField] private TextMeshProUGUI contextText;
    [SerializeField] private GameObject discImage;

    [SerializeField] private AudioSource MusicAudio;
    [SerializeField] private AudioSource EffectAudio;
    [SerializeField] private AudioClip StopAudio;
    [SerializeField] private bool paused = false;

    [SerializeField] private TextMeshProUGUI playingText;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip exitHoverSound;
    [SerializeField] private GameObject SliderHandle;

    [SerializeField] private float curVolume = 0.3f;

    [Header("scale values:")]
    [SerializeField] private float originalSize;
    [SerializeField] private float largeSize;
    [SerializeField] private float smallSize;

    [Header("Display settings")]
    [SerializeField] private string[] buttonContext = new string[3];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetButtonRects();
        StartCoroutine(PlayingTextBlink());
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < buttonsRects.Length; i++)
        {
            if (i == buttonsToScale[0] || i == buttonsToScale[1])
            {
                buttonsRects[i].localScale = Vector3.Lerp(
                    buttonsRects[i].localScale,
                    Vector3.one * 0.8f,
                    Time.deltaTime * 10f
                );
            }
            else
            {
                buttonsRects[i].localScale = Vector3.Lerp(
                    buttonsRects[i].localScale,
                    Vector3.one * 1.2f,
                    Time.deltaTime * 10f
                );
            }
        }

        if (!paused)
        {
            discImage.transform.Rotate(new Vector3(0f, 0f, 2f));
        }
    }

    public void ExitButton()
    {
        EffectAudio.PlayOneShot(exitHoverSound);
    }

    private void GetButtonRects()
    {
        int i = 0;
        foreach(Button b in mainButtons)
        {
            buttonsRects[i] = b.GetComponent<RectTransform>();
            i++;
        }
    }

    public int[] HoverButtonEffect(int index)
    {
        EffectAudio.PlayOneShot(hoverSound);

        int[] values = new int[2];

        if(index == 0)
        {
            values[0] = 1;
            values[1] = 2;

            selectedButton = 0;
        }else if(index == 1)
        {
            values[0] = 0;
            values[1] = 2;

            selectedButton = 1;
        }else if(index == 2)
        {
            values[0] = 0;
            values[1] = 1;

            selectedButton = 2;
        }

        return values;
    }

    public void HoverOverButton(int index)
    {
        buttonsToScale = HoverButtonEffect(index);
        contextText.text = buttonContext[selectedButton];
    }

    public void ClickButtonEffect(int index)
    {
        GameObject g = mainButtons[index].gameObject;
        StartCoroutine(SpinBeforeClick(g, index));
    }

    private IEnumerator SpinBeforeClick(GameObject g, int index)
    {
        float f = 1f;
        int t = 0;

        while (true)
        {
            yield return new WaitForFixedUpdate();
            g.transform.Rotate(new Vector3(0f, 2f, 0f));

            float s = g.transform.localScale.x;
            g.transform.localScale = new Vector3(
                s * f,
                s * f,
                s * f
            );

            f += 0.005f;
            t += 1;

            if(t == 100)
            {
                if(index == 1)
                {
                    LoadScene(1);
                }
            }
        }
    }

    private void LoadScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
        return;
    }

    public void ToggleMusic()
    {
        if (!paused)
        {
            MusicAudio.Stop();
            MusicAudio.PlayOneShot(StopAudio);

            paused = true;
        }
        else
        {
            MusicAudio.Play();
            playingText.enabled = true;
            
            paused = false;
        }
    }

    private IEnumerator PlayingTextBlink()
    {
        while (true)
        {

            if (playingText.enabled)
            {
                if (paused)
                {
                    playingText.enabled = false;
                }
            }
            else
            {
                playingText.enabled = true;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ChangeVolume(float value)
    {
        Debug.Log(value);

        SliderHandle.transform.rotation = Quaternion.Euler(0f, 0f, 360 - (value * 360f));
        MusicAudio.volume = value;
    }
}
