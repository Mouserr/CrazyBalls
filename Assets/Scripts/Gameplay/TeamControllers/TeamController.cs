﻿using System.Collections.Generic;

namespace Assets.Scripts.TeamControllers
{
    public abstract class TeamController
    {
        public UnitController CurrentUnit { get; private set; }
        public int PlayerId { get; }

        public TeamController(int playerId)
        {
            PlayerId = playerId;
        }

        public virtual void StartTurn(UnitController unit)
        {
            CurrentUnit = unit;
        }

        public abstract void Clear();
    }
}