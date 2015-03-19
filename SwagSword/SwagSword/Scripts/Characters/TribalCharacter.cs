using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwagSword
{
    class TribalCharacter : Character
    {
        public TribalCharacter(int x, int y, Texture2D texture, Game1 mainMan): base(x, y, texture, mainMan)
        {
            //Set config file here

            Init();
        }

        public override void Init()
        {
            Type = Faction.Tribal;
            NormalColor = Color.Purple;

            //Set AI state prob
            AIProbs.Add(AIState.Attack, 0.3f);
            AIProbs.Add(AIState.Ability, 0.2f);
            AIProbs.Add(AIState.Defend, 0.4f);
            AIProbs.Add(AIState.Cower, 0.3f);
            AIProbs.Add(AIState.Ready, 0.3f);
            AIProbs.Add(AIState.Idle, 0.4f);

            //Set AI timers
            AITimers.Add(AIState.Attack, 7f);
            AITimers.Add(AIState.Swing, 3f);
            AITimers.Add(AIState.Defend, 5f);
            AITimers.Add(AIState.Ability, 3f);

            SightRange = 300f;
            AttackRange = 70f;

            base.Init();

            InitStats();
        }

        protected override void InitStats()
        {
            health = 125;
            maxHealth = 125;
            strength = 50f;
            damage = 10;
            attackSpeedMin = 8.0f;
            attackSpeedMax = 15.0f;
            movementSpeed = 3.5f;
        }

        public override void Update()
        {
            if (!IsControlled && CharacterState == CharacterState.Active)
            {
                #region AI Behavior
                switch (AIState)
                {
                    case AIState.Attack:
                        //Move close to attack
                        if (DistanceToPlayer(0) > AttackRange)
                        {
                            MoveToPoint(mainMan.GameMan.Players[0].X, mainMan.GameMan.Players[0].Y);
                        }
                        else
                        {
                            //Fight them suckers
                            SwitchAIState(AIState.Swing);
                        }

                        AIStateTimer -= (float)mainMan.GameTime.ElapsedGameTime.TotalSeconds;
                        if (AIStateTimer <= 0f)
                        {
                            SwitchAIState(AIState.Idle);
                        }
                        break;

                    case AIState.Swing:

                        break;

                    case AIState.Defend:

                        break;

                    case AIState.Idle:
                        //Work out some random movement paths
                        if (DistanceToPlayer(0) < SightRange)
                        {
                            SwitchAIState(AIState.Attack);
                        }
                        break;

                    case AIState.Ability:

                        break;

                    case AIState.Cower:

                        break;

                    case AIState.Ready:

                        break;
                }
                #endregion
            }

            base.Update();
        }
    }
}