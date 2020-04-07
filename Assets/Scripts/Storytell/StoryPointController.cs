using Assets.Scripts;
using UnityEngine;

public class StoryPointController : MonoBehaviour
{
    public EpisodeData Episode;
    public GameObject Visual;
    
   
    // Start is called before the first frame update
    void Start()
    {
        Visual.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        Visual.SetActive(true);
    }
}
