using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Vuforia;

namespace Assets.Scripts
{
    public class VuforiaCustomTrackAdapter : ITrackableEventHandler
    {

        private VuforiaTag arTrackInfo;
        private TrackableBehaviour trackController;        

        public VuforiaCustomTrackAdapter(VuforiaTag arTrackInfo, TrackableBehaviour trackController)
        {
            this.trackController = trackController;
            this.arTrackInfo = arTrackInfo;
            this.trackController.RegisterTrackableEventHandler(this);
        }

        public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
        {
            IsTracked = newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED;
        }

        public string GetTrackedID()
        {
            return arTrackInfo.TrackingObjID;
        }

        public Transform GetArObjPostion()
        {
            return arTrackInfo.transform;
        }

        public bool IsTracked { get; private set; }
    }
}
