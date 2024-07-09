using UnityEngine;

class AudioCurlingSlide : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;
    private AudioClip curlingSlideClip;

    // 假設的最大速度，用於計算音量
    [SerializeField]
    private float maxSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.AddComponent<AudioSource>();
        curlingSlideClip = Resources.Load<AudioClip>("Audio/Curling2");
        audioSource.clip = curlingSlideClip;
        
    }

    void Update()
    {
        float speed = rb.velocity.magnitude;
        if (speed > 0.1f)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            // 使用指數衰減方式來逐漸減小音量
            audioSource.volume = Mathf.Clamp01(speed*4/maxSpeed);
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Time.deltaTime * 1f); // 逐漸減小音量
                if (audioSource.volume < 0.01f)
                {
                    audioSource.Stop();
                }
            }
        }
    }
}

