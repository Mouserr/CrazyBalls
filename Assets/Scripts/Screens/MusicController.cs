using Assets.Scripts.Core;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class MusicController : Singleton<MusicController>
    {
        private EventInstance _battleEvent;
        private EventInstance _mainThemeEvent;
        private bool _battleWasStarted;
        
        [SerializeField]
        private StudioEventEmitter BattleTheme;
        [SerializeField]
        private StudioEventEmitter BattleThemeEnd;
        [SerializeField]
        private StudioEventEmitter MenuTheme;

        public void ChangeTheme(bool battle)
        {
            if (!battle)
            {
                if (_battleWasStarted)
                {
                    _battleEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    BattleThemeEnd.Play();
                    _battleWasStarted = false;
                }
                
                MenuTheme.Play();
                _mainThemeEvent = MenuTheme.EventInstance;
            }
            else
            {
                _battleWasStarted = true;
                _mainThemeEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                BattleTheme.Play();
                _battleEvent = BattleTheme.EventInstance;
            }
        }
    }
}