using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ScarToolkit.Scenarios
{
    public abstract class GameScenarioBase : MonoBehaviour
    {
        protected CancellationTokenSource _cancellationTokenSource;
        protected bool _isCompleted;
        public CancellationToken Token => _cancellationTokenSource.Token;
        

    }
}
