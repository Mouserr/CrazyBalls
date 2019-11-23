using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Screens;
using UnityEngine;

public class StoryTellScreen : AbstractScreen
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
    
    public void ToBattle()
    {
        ScreensManager.Instance.OpenScreen(ScreenType.Battle);
    }
    
    public void ToMainMenu()
    {
        ScreensManager.Instance.OpenScreen(ScreenType.MainMenu);
    }
    
    public void TeamManagement()
    {
        ScreensManager.Instance.OpenScreen(ScreenType.TeamManagement);
    }
}
