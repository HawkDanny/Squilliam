using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Names: Nelson Scott

namespace SwagSword
{
    class ThiefCharacter : Character
    {
        public ThiefCharacter(int x, int y, Texture2D texture, Game1 mainMan): base(x, y, texture, mainMan)
        {
            //Set config file here

            CurrentAbility = new Decoy(mainMan, this);

            Init();
        }

        public override void Init()
        {
            Type = Faction.Thief;
            NormalColor = Color.White;
            //NormalColor = Color.OrangeRed;

            CustomCharacter custom = mainMan.GameMan.SpawnMan.GetCustomCharacter(Faction.Thief);
            this.name = custom.Name;

            //Set AI state prob
            AIProbs.Add(AIState.Attack, (float)custom.Aggression);
            AIProbs.Add(AIState.Flank, 0.1f);
            AIProbs.Add(AIState.Ability, (float)custom.Ability);
            AIProbs.Add(AIState.Defend, (float)custom.Defense);
            AIProbs.Add(AIState.Cower, (float)custom.Cowardice);
            AIProbs.Add(AIState.Ready, 0.3f);
            AIProbs.Add(AIState.Idle, 0.4f);

            //Set AI timers
            AITimers.Add(AIState.Attack, 2f);
            AITimers.Add(AIState.Swing, 0.25f);
            AITimers.Add(AIState.Defend, 3f);
            AITimers.Add(AIState.Ability, 3f);
            AITimers.Add(AIState.Idle, 0.2f);

            //Encounter AI State list
            EncounterAIStates.Add(AIState.Attack);
            EncounterAIStates.Add(AIState.Ability);
            EncounterAIStates.Add(AIState.Defend);

            SightRange = 250f;
            AttackRange = 50f;

            base.Init();

            InitStats();
        }

        public override void Update()
        {
            if (!IsControlled && CharacterState == CharacterState.Active)
            {
                #region AI Behavior
                switch (AIState)
                {
                    case AIState.Attack:
                        if (mainMan.GameMan.Players[0].CharacterState != CharacterState.Dead && PlayerInArea())
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
                        if (mainMan.GameMan.Players[0].CharacterState != CharacterState.Dead && PlayerInArea())
                        {
                            //Check if player is too close and move back
                            if (DistanceToPlayer(0) < SightRange / 1.5)
                            {
                                SetDirectionToPoint(mainMan.GameMan.Players[0].X, mainMan.GameMan.Players[0].Y);
                                MoveToPoint(X + (float)(MovementSpeed * Math.Cos((270f - Direction) * Math.PI / 180f)), Y + (float)(MovementSpeed * Math.Sin((270f - Direction) * Math.PI / 180f)));
                            }
                            else
                            {
                                SwitchAIState(AIState.Idle);
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

                    case AIState.Idle:
                        AIStateTimer -= (float)mainMan.GameTime.ElapsedGameTime.TotalSeconds;

                        if (AIStateTimer <= 0f)
                        {
                            //random movement?
                            if (!mainMan.GameMan.Players[0].NoCharacter && PlayerInArea() && mainMan.GameMan.Players[0].Character.Type != Type && mainMan.GameMan.Players[0].CharacterState == CharacterState.Active)
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
                        //Decoy
                        if (!CurrentAbility.InUse)
                        {
                            CurrentAbility.AIUse();
                            CurrentAbility.InUse = true;
                        }
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