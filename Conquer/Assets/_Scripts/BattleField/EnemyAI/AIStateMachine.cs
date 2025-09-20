using Cysharp.Threading.Tasks;
using ModestTree;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BattleField
{
    public class AIStateMachine
    {
        public IEnemyAIState currentState;
        public IEnemyAIState moveToState;
        public Dictionary<IEnemyAIState, List<TransitionState>> transtions = new Dictionary<IEnemyAIState, List<TransitionState>>();
        public List<TransitionState> anyTransition = new List<TransitionState>();
        private EnemyAI _aiEnemy;
        private AiTransitionGraphBuilder _transitionBuilder;
        private float _stateSwitchColdown;
        public bool _isAllowedToSwitchState {  get; private set; }
        public AIStateMachine(EnemyAI aiEnemy, float stateSwitchColdown)
        {
            _aiEnemy = aiEnemy;
            _stateSwitchColdown = stateSwitchColdown;
            _isAllowedToSwitchState = true;
            _transitionBuilder = new AiTransitionGraphBuilder(this, _aiEnemy);
        }

        public void AddAnyTranstion(IEnemyAIState to, Func<bool> func)
        {
            anyTransition.Add(new TransitionState(to, func));
        }

        public void AddTranstion(IEnemyAIState from, IEnemyAIState to, Func<bool> func)
        {
            if (transtions.ContainsKey(from))
            {
                transtions[from].Add(new TransitionState(to, func));
            }
            else
            {
                List<TransitionState> list = new();
                list.Add(new TransitionState(to, func));
                transtions.Add(from, list);
            }
        }

        public void SwitchState(IEnemyAIState switchTo)
        {
            WaitStateSwitchColdown();
            currentState?.Exit(_aiEnemy);
            moveToState = switchTo;
            currentState = switchTo;
            currentState.Enter(_aiEnemy);
        }

        public void CheckAnyTransition()
        {
            for(int i = 0; i < anyTransition.Count; i++)
            {
                if (anyTransition[i].Condition())
                    SwitchState(anyTransition[i].TargetState);
            }
        }

        public void CheckTranstion()
        {
            if (currentState == null)
                Debug.Log("current state is null");
            if (!transtions.TryGetValue(currentState, out List<TransitionState> transitionList))
                return;
            for(int i =0; i < transitionList.Count; i++)
            {
                if (transitionList[i].Condition())
                    SwitchState(transitionList[i].TargetState);
            }
        }
        public async UniTask WaitStateSwitchColdown()
        {
            _isAllowedToSwitchState = false;
            await UniTask.Delay((int)(_stateSwitchColdown * 1000f));
            _isAllowedToSwitchState |= true;
        }

        public void CheckState()
        {
            CheckTranstion();
            CheckAnyTransition();
        }
    }
}