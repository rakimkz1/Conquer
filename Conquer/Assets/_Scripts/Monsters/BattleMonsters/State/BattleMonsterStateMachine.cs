using BattleField;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Monsters
{
    public class BattleMonsterStateMachine
    {
        public IBattleMonsterState currentState;
        public IBattleMonsterState movingToState;
        public ArmyCommandTypes currentArmyCommand = ArmyCommandTypes.Defence;
        public Dictionary<IBattleMonsterState, List<TransitionState>> transitions = new();
        public List<TransitionState> anyTransitions = new();
        private BattleMonster _monster;
        private TransitionGraphBuilder _transitionGraphBuilder;
        public BattleMonsterStateMachine(BattleMonster monster)
        {
            _monster = monster;
            _transitionGraphBuilder = new TransitionGraphBuilder(this, _monster);

        }
        

        public void SwichState(IBattleMonsterState swichTo)
        {
            movingToState = swichTo;
            currentState?.OnExit(_monster);
            currentState = swichTo;
            swichTo.OnEnter(_monster);
        }

        public void AddTransition(IBattleMonsterState from, IBattleMonsterState to, Func<bool> condition)
        {
            if(!transitions.ContainsKey(from))
                transitions[from] = new List<TransitionState>();
            transitions[from].Add(new TransitionState(to, condition));
        }

        public void AddAnyTransition(IBattleMonsterState to, Func<bool> condition)
        {
            anyTransitions.Add(new TransitionState(to, condition));
        }

        public void CheckAnyTransitions()
        {
            foreach (TransitionState state in anyTransitions)
            {
                if (state.Condition() && currentState != state.TargetState)
                {
                    //Debug.Log(state.TargetState.ToString());
                    SwichState(state.TargetState);
                    return;
                }
            }
        }

        public void CheckTransitions()
        {
            if(transitions.TryGetValue(currentState, out List<TransitionState> stateTransitions))
            {
                for(int i = 0; i < stateTransitions.Count; i++)
                {
                    if (stateTransitions[i].Condition())
                    {
                        Debug.Log($"Swich from {currentState.ToString()}  to {stateTransitions[i].TargetState.ToString()}");
                        SwichState(stateTransitions[i].TargetState);
                        return;
                    }
                }
            }
        }

        public void ListenArmyCommand(ArmyCommandTypes types)
        {
            currentArmyCommand = types;
        }
    }
}