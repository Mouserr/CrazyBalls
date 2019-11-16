using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using UnityEngine;

namespace Assets.Scripts.Core.SyncCodes
{
	public static class SyncCodeExtensions
	{
	    public static ISyncScenarioItem PlayAndReturnSelf(this ISyncScenarioItem item)
	    {
	        item.Play();
	        return item;
	    }

        public static bool IsNullOrComplete(this ISyncScenarioItem item)
        {
            return item == null || item.IsComplete();
        }

		public static Coroutine WaitOperations(this MonoBehaviour behaviour, List<SimulateScenarioItem> operations)
		{
			return behaviour.StartCoroutine(waitSyncOperations(behaviour, operations.ToArray()));
		}

		public static Coroutine WaitOperations(this MonoBehaviour behaviour, params ISyncOperation[] operations)
		{
			return behaviour.StartCoroutine(waitSyncOperations(behaviour, operations));
		}

		public static Coroutine WaitOperations(this MonoBehaviour behaviour, IList<ISyncOperation> operations)
		{
			return behaviour.StartCoroutine(waitSyncOperations(behaviour, operations));
		}

		private static IEnumerator waitSyncOperations(MonoBehaviour behavior, IList<ISyncOperation> operations)
		{
			while (isOperationsInProgress(behavior, operations))
			{
				yield return null;
			}
		}

		private static bool isOperationsInProgress(MonoBehaviour behavior, IList<ISyncOperation> operations)
		{
			//Profiler.BeginSample(string.Format("{0}[{1}].isOperationsInProgress({2})", behavior.GetType().Name, behavior.name, operations != null ? operations.Count : 0));
         	 
			bool result = false;
         	if (null != operations)
			{
				for (int i = 0; i < operations.Count; i++)
				{
					ISyncOperation operation = operations[i];
					if (operation == null)
						continue;

					//Profiler.BeginSample(string.Format("{0}.IsComplete()", operation.GetType().Name));
					if (null != operations[i] && !operations[i].IsComplete())
					{
						result = true;
					//	Profiler.EndSample();
						break;
					}
					//Profiler.EndSample();
				}
			}
			//Profiler.EndSample();
			return result;
		}

        
	}
}