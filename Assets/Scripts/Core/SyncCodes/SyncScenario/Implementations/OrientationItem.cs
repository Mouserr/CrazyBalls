using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{
    public class OrientationItem : ISyncScenarioItem
    {
        #region Class fields
        private bool isCompleate;
        List<Transform> transforms;
        List<int> orientstions;
        #endregion

        #region Constructor
        public OrientationItem(List<Transform> transforms, List<int> orientstions)
        {
            this.transforms = transforms;
            this.orientstions = orientstions;
            isCompleate = false;
        }
        #endregion

        #region Methods
        public void Play()
        {
            if(transforms.Count == orientstions.Count)
            {
                for (int i = 0; i < transforms.Count; i++)
                {
                    if (orientstions[i] == 0) continue;

                    this.transforms[i].localEulerAngles = new Vector3(0, orientstions[i] > 0 ? 0 : 180, 0);
                    Camera cam = this.transforms[i].GetComponentInChildren<Camera>();
                    if (cam != null)
                    {
                        cam.transform.parent = this.transforms[i].parent;
                        cam.transform.localEulerAngles = new Vector3(0, 0, 0);
                        cam.transform.parent = this.transforms[i];
                    }
                }        
            }
            this.isCompleate = true;
        }

        public void Stop()
        {
            this.isCompleate = true;
        }

        public void Pause()
        {
            this.isCompleate = true;
        }

        public bool IsComplete()
        {
            return this.isCompleate;
        }
        #endregion
    }
}
