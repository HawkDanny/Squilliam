using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Names: Nelson Scott

namespace SwagSword
{
    class GoodCharacter : Character
    {
        public GoodCharacter(int x, int y, Texture2D texture, Game1 mainMan): base(x, y, texture, mainMan)
        {
            //Set config file here

            CurrentAbility = new Boomerang(mainMan, this);

            Init();
        }

        public override void Init()
        {
            Type = Faction.Good;
            NormalColor = Color.White;
            //NormalColor = Color.Purple;

            CustomCharacter custom = mainMan.GameMan.SpawnMan.GetCustomCharacter(Faction.Good);
            this.name = custom.Name;

            //Set AI state prob
            AIProbs.Add(AIState.Attack, 0f);
            AIProbs.Add(AIState.Flank, 0.1f);
            AIProbs.Add(AIState.Ability, (float)custom.Ability);
            AIProbs.Add(AIState.Defend, (float)custom.Defense);
            AIProbs.Add(AIState.Cower, (float)custom.Cowardice);
            AIProbs.Add(AIState.Ready, 0.3f);
            AIProbs.Add(AIState.Idle, 0.4f);

            //Set AI timers
            AITimers.Add(AIState.Attack, 2f);
            AITimers.Add(AIState.Swing, 0.25f);
            AITimers.Add(AIState.Defend, 2f);
            AITimers.Add(AIState.Ability, 4f);
            AITimers.Add(AIState.Idle, 0.2f);

            //Encounter AI State list
            EncounterAIStates.Add(AIState.Attack);
            EncounterAIStates.Add(AIState.Ability);
            EncounterAIStates.Add(AIState.Defend);

            SightRange = 300f;
            AttackRange = 50f;

            base.Init();

            InitStats();
        }

        protected override void InitStats()
        {
            name = "Peter";
            health = 100;
            maxHealth = 100;
            strength = 30f;
            damage = 4;
            attackSpeedMin = 7.0f;
            attackSpeedMax = 13.0f;
            movementSpeed = 5f;
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
                            if (!mainMan.GameMan.Players[0].NoCharacter && mainMan.GameMan.Players[0].Character.Type != Type && PlayerInArea() && mainMan.GameMan.Players[0].CharacterState == CharacterState.Active)
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
                        //Throw the sword
                        Weapon.Angle = (float)Math.Atan2(mainMan.GameMan.Players[0].X - X, mainMan.GameMan.Players[0].Y - Y) * 180f / (float)Math.PI;
                        CurrentAbility.Use();
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
