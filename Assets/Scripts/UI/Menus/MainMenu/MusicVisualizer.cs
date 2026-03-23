using UnityEngine;

public class MusicVisualizer : MonoBehaviour
{
	public AudioSource audioSource;
	public float updateStep = 0.1f;
	public int sampleDataLength = 1024;

	private float currentUpdateTime = 0f;

	private float clipLoudness;
	private float[] clipSampleData;

    [SerializeField] private RectTransform visualizerRect;

	void Awake () {
	
		if (!audioSource) {
			Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
		}
		clipSampleData = new float[sampleDataLength];

	}
	
	// Update is called once per frame
	void Update () {
	
		currentUpdateTime += Time.deltaTime;
		if (currentUpdateTime >= updateStep) {
			currentUpdateTime = 0f;
			audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); 
			clipLoudness = 0f;
			foreach (var sample in clipSampleData) {
				clipLoudness += Mathf.Abs(sample);
			}
			clipLoudness /= sampleDataLength; 

            visualizerRect.sizeDelta = new Vector2(
                150f * 1 + (clipLoudness * 100),
                150f * 1 + (clipLoudness * 100)
            );
		}

	}
}
