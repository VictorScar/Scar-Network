using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using ScarToolkit.Scenarios;
using UnityEngine;

public abstract class GameScenarioVoidWithResultBase<TResult> : GameScenarioBase
{
    protected TResult _scenarioResult;

    public void Init()
    {
        OnInit();
    }

    public async UniTask<TResult> Run(CancellationToken token)
    {
        _isCompleted = false;
        _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
        
        await UniTask.WhenAny(OnBeforeScenario(Token), CompleteScenario(Token));
        var result = await UniTask.WhenAny(RunInternal(Token), CompleteScenario(Token));
        OnScenarioEnd();

        if (result.winArgumentIndex == 0)
        {
            return result.result1;
        }
        else
        {
            return result.result2;
        }
    }


    public void StopScenario()
    {
        _isCompleted = true;
    }

    protected virtual UniTask OnBeforeScenario(CancellationToken token)
    {
        return UniTask.CompletedTask;
    }

    protected UniTask<TResult> RunInternal(CancellationToken token)
    {
        return UniTask.FromResult(_scenarioResult);
    }

    private async UniTask<TResult> CompleteScenario(CancellationToken token)
    {
        await UniTask.WaitUntil(() => _isCompleted, cancellationToken: token);
        return _scenarioResult;
    }

    protected virtual void OnInit()
    {
    }

    protected virtual void OnScenarioEnd()
    {
    }
}
