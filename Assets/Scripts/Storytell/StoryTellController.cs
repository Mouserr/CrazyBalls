using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class StoryTellController : MonoBehaviour
{
    private int _storypointIndex = 0;
    
    public StoryPointController[] StoryPoints;

    public StoryPointController CurrentStoryPoint => StoryPoints[_storypointIndex];



    // Start is called before the first frame update
    void Start()
    {
        CurrentStoryPoint.IsActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextStoryPoint()
    {
        CurrentStoryPoint.IsActive = false;
        if (_storypointIndex + 1 < StoryPoints.Length) _storypointIndex++;
        CurrentStoryPoint.IsActive = true;
    }
}
