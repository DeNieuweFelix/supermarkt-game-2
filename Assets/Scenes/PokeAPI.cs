using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PokeAPI : MonoBehaviour
{

    public TMP_InputField input;
    public Image preview;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if(input.text.Length > 0)
            {
                StartCoroutine(CheckAPI());
            }
        }
    }

    private IEnumerator CheckAPI()
    {
        PokeData data;

        using(UnityWebRequest request = UnityWebRequest.Get("https://pokeapi.co/api/v2/pokemon/" + input.text))
        {
            yield return request.SendWebRequest();
            if(request.result != UnityWebRequest.Result.Success)
            {
                yield break;
            }
    
            data = JsonUtility.FromJson<PokeData>(request.downloadHandler.text);

            Debug.Log(data.sprites.front_default);
        }

        if(data != null)
        {
            using(UnityWebRequest request = UnityWebRequestTexture.GetTexture(data.sprites.front_default))
            {
                yield return request.SendWebRequest();
                if(request.result != UnityWebRequest.Result.Success)
                {
                    yield break;
                }
                Texture2D preview2d = DownloadHandlerTexture.GetContent(request);
                preview.sprite = Sprite.Create(preview2d, new Rect(0, 0, preview2d.width, preview2d.height), new Vector2(0.5f, 0.5f));
            }

            using(UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(data.cries.latest, AudioType.OGGVORBIS))
            {
                yield return request.SendWebRequest();
                if(request.result != UnityWebRequest.Result.Success)
                {
                    yield break;
                }

                AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }

    [System.Serializable]
    public class PokeData
    {
        public string name;
        public PokeSprite sprites;
        public PokeCry cries;
    }

    [System.Serializable]
    public class PokeSprite
    {
        public string front_default;
    }

    [System.Serializable]
    public class PokeCry
    {
        public string latest;
    }
}
