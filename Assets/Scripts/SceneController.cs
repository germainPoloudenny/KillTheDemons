using UnityEngine;


public class SceneController : MonoBehaviour
{
    public GameObject openWorld, fight, menu,death,cloak,pause;
    private GameObject _currentScene,_player;
    public EndlessTerrain endlessTerrain;
    public AudioClip openWorldClip,menuClip,fightClip,deathClip;
    public AudioSource audio;
    // Start is called before the first frame update
    void Awake()
    {
      
     
        pause.SetActive(false);
        cloak.SetActive(false);
        fight.SetActive(false);
        _player=GameObject.Find("Player");
        _player.SetActive(false);
        endlessTerrain.UpdateVisibleChunks(new Vector2(0, 0));
        audio.clip=menuClip;
        audio.Play();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& !menu.activeSelf)
        {
         
            pause.SetActive(true);
            pause.GetComponent<Canvas>().worldCamera = Camera.allCameras[0];
            Time.timeScale = 0;


        }
    }
    
    public void StartFight(GameObject opponent)
    {
        fight.GetComponent<Fight>().opponent = opponent;
        openWorld.SetActive(false);
        fight.SetActive(true);
        StartCoroutine(fight.GetComponent<Fight>().ToFight());
        SwitchClip(fightClip);
    }
    
    public void EndFight(GameObject opponent)
    {
        fight.SetActive(false);
        openWorld.SetActive(true);
        Destroy(opponent);
        SwitchClip(openWorldClip);
    }

    public void Play()
    {
        _player.SetActive(true);
        cloak.GetComponent<Cloak>().timeWithoutPlaying = Time.timeSinceLevelLoad;
        cloak.SetActive(true);
        menu.SetActive(false);
        SwitchClip(openWorldClip);
        
    }
    public void Die()
    {
        SwitchClip(deathClip);
        death.SetActive(true);
        fight.SetActive(false);
        
    }


    public void SwitchClip(AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();
    }

    public void SetVolume(float volume)
    {
        audio.volume = volume;
    }
}
