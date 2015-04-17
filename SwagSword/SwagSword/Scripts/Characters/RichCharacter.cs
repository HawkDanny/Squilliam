using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Names: Nelson Scott

namespace SwagSword
{
    class RichCharacter : Character
    {
        public RichCharacter(int x, int y, Texture2D texture, Game1 mainMan): base(x, y, texture, mainMan)
        {
            //Set config file here

            CurrentAbility = new Minion(mainMan, this);

            Init();
        }

        public override void Init()
        {
            Type = Faction.Rich;
            NormalColor = Color.SteelBlue;

            //Set AI state prob
            AIProbs.Add(AIState.Attack, 0.5f);
            AIProbs.Add(AIState.Flank, 0.1f);
            AIProbs.Add(AIState.Ability, 0.5f);
            AIProbs.Add(AIState.Defend, 0.4f);
            AIProbs.Add(AIState.Cower, 0.3f);
            AIProbs.Add(AIState.Ready, 0.3f);
            AIProbs.Add(AIState.Idle, 0.4f);

            //Set AI timers
            AITimers.Add(AIState.Attack, 7f);
            AITimers.Add(AIState.Swing, 0.25f);
            AITimers.Add(AIState.Defend, 5f);
            AITimers.Add(AIState.Ability, 3f);
            AITimers.Add(AIState.Idle, 0.2f);

            //Encounter AI State list
            EncounterAIStates.Add(AIState.Attack);
            EncounterAIStates.Add(AIState.Ability);

            SightRange = 250f;
            AttackRange = 50f;

            base.Init();

            InitStats();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}