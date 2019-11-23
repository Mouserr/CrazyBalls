using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Screens;
using UnityEngine;

public class StoryTellScreen : AbstractScreen
{
   
    public PlayerController Player;
    
    public StoryPointController[] StoryPoints;

    private List<EpisodeData> _episodes;
    
    public StoryPointController CurrentStoryPoint;

    

    // Start is called before the first frame update
    void Start()
    {
        _episodes = new List<EpisodeData>();
        _episodes = StoryPoints.Select(c => c.Episode).ToList();
        CurrentStoryPoint = StoryPoints.First(); 
        CurrentStoryPoint.IsActive = true;
        Player.CurrentEpisode = CurrentStoryPoint.Episode;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextStoryPoint()
    {
        CurrentStoryPoint.IsActive = false;
        var currentIndex = StoryPoints.ToList().IndexOf(CurrentStoryPoint);
        if (currentIndex >= StoryPoints.Length) return;
        
        currentIndex++;

        CurrentStoryPoint = StoryPoints[currentIndex];
        Player.CurrentEpisode = CurrentStoryPoint.Episode;
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
