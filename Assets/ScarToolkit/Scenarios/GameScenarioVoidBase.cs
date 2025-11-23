using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ScarToolkit.Scenarios
{
    public abstract class GameScenarioVoidBase : GameScenarioBase
    {
        public void Init()
        {
            OnInit();
        }

        public async UniTask Run(CancellationToken token)
        {
            _isCompleted = false;
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
        
            await UniTask.WhenAny(OnBeforeScenario(Token), CompleteScenario(Token));
            await UniTask.WhenAny(RunInternal(Token), CompleteScenario(Token));
            OnScenarioEnd();
        }


        public void StopScenario()
        {
            _isCompleted = true;
        }

        protected virtual UniTask OnBeforeScenario(CancellationToken token)
        {
            return UniTask.CompletedTask;
        }

        protected UniTask RunInternal(CancellationToken token)
        {
            return UniTask.CompletedTask;
        }

        private async UniTask CompleteScenario(CancellationToken token)
        {
            await UniTask.WaitUntil(() => _isCompleted, cancellationToken: token);
        }

        protected virtual void OnInit()
        {
        }

        protected virtual void OnScenarioEnd()
        {
        }
    }
}