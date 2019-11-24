using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Screens;
using TMPro;
using UnityEngine;

public class StoryTellScreen : AbstractScreen
{
   
    public PlayerController Player;
    
    public StoryPointController[] StoryPoints;

    private List<EpisodeData> _episodes;
    
    public StoryPointController CurrentStoryPoint;
    public TextMeshProUGUI Description;

    

    // Start is called before the first frame update
    void Start()
    {
        _episodes = new List<EpisodeData>();
        _episodes = StoryPoints.Select(c => c.Episode).ToList();
        CurrentStoryPoint = StoryPoints.First(); 
        CurrentStoryPoint.Activate();
        Player.CurrentEpisode = CurrentStoryPoint.Episode;
        Description.text = CurrentStoryPoint.Episode.Description;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Focus()
    {
        if (Player.WonLastBattle)
        {
            Player.WonLastBattle = false;
            NextStoryPoint();
        }
        

    }
    
    public void NextStoryPoint()
    {
      //  CurrentStoryPoint.IsActive = false;
        var currentIndex = StoryPoints.ToList().IndexOf(CurrentStoryPoint);
        currentIndex++;
        if (currentIndex >= StoryPoints.Length) return;
        CurrentStoryPoint = StoryPoints[currentIndex];
        Player.CurrentEpisode = CurrentStoryPoint.Episode;
        CurrentStoryPoint.Activate();
        Description.text = CurrentStoryPoint.Episode.Description;
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
