using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Names: Nelson Scott

namespace SwagSword
{
    class TribalCharacter : Character
    {
        public TribalCharacter(int x, int y, Texture2D texture, Game1 mainMan): base(x, y, texture, mainMan)
        {
            //Set config file here

            CurrentAbility = new Warp(mainMan, this);

            Init();
        }

        public override void Init()
        {
            Type = Faction.Tribal;
            NormalColor = Color.Purple;

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

        protected override void InitStats()
        {
            health = 125;
            maxHealth = 125;
            strength = 40f;
            damage = 5;
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
                        if (mainMan.GameMan.Players[0].CharacterState != CharacterState.Dead)
                        {
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
                        }
                        else
                        {
                            SwitchAIState(AIState.Idle);
                        }

                        AIStateTimer -= (float)mainMan.GameTime.ElapsedGameTime.TotalSeconds;
                        if (AIStateTimer <= 0f)
                        {
                            SwitchAIState(AIState.Idle);
                        }
                        break;

                    case AIState.Swing:
                        AIStateTimer -= (float)mainMan.GameTime.ElapsedGameTime.TotalSeconds;

                        if (AIStateTimer <= 0f)
                        {
                            SwitchAIState(AIState.Idle);
                        }
                        break;

                    case AIState.Defend:

                        break;

                    case AIState.Idle:
                        AIStateTimer -= (float)mainMan.GameTime.ElapsedGameTime.TotalSeconds;

                        if (AIStateTimer <= 0f)
                        {
                            //random movement?
                            if (!mainMan.GameMan.Players[0].NoCharacter && mainMan.GameMan.Players[0].Character.Type != Type && mainMan.GameMan.Players[0].CharacterState == CharacterState.Active)
                            {
                                if (DistanceToPlayer(0) < SightRange)
                                {
                                    //Get random
                                    SwitchAIState(GetRandomAIState(EncounterAIStates));
                                }
                            }
                            else
                            {
                                AIStateTimer = AITimers[AIState];
                            }
                        }
                        break;

                    case AIState.Switch:
                        //Move to sword
                        MoveToPoint(mainMan.GameMan.Players[0].X, mainMan.GameMan.Players[0].Y);
                        
                        //Check if picked up
                        if (HitBox.Contains(mainMan.GameMan.Players[0].Position))
                        {
                            //Pick it up!!!
                            mainMan.GameMan.Players[0].SwitchBlade(this);
                        }
                        break;

                    case AIState.Ability:
                        //Warp to player
                        CurrentAbility.AIUse();
                        SwitchAIState(AIState.Idle);
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