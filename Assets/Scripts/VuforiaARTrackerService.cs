using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Vuforia;

namespace CyberCountry
{
    public class VuforiaARTrackerService : MonoBehaviour, ITrackerService
    {


        private List<VuforiaCustomTrackAdapter> trackableObjects;

        private void Awake()
        {
            ARTag[] arTags = FindObjectsOfType<ARTag>();

            trackableObjects = new List<VuforiaCustomTrackAdapter>();

            for (int i = 0; i < arTags.Length; i++)
            {
                ImageTargetBehaviour beh = arTags[i].gameObject.GetComponent<ImageTargetBehaviour>();

                VuforiaCustomTrackAdapter trAdapter = new VuforiaCustomTrackAdapter(arTags[i], beh);

                trackableObjects.Add(trAdapter);
            }
        }        

        public Transform GetCastlePos()
        {
            return GetTrackedObj("castle");
        }

        public Transform GetPortalPos()
        {
            return GetTrackedObj("portal");
        }

        public Transform GetTowerPos()
        {
            return GetTrackedObj("tower");
        }

        private Transform GetTrackedObj(string objId)
        {
            return trackableObjects.Where((x) => x.GetTrackedID() == objId).First().GetArObjPostion();
        }
        
        public bool IsCastleTracking()
        {
            return GetTrackingState("castle");
        }

        public bool IsPortalTracking()
        {
            return GetTrackingState("portal");
        }

        public bool IsTowerTracking()
        {
            return GetTrackingState("tower");
        }

        private bool GetTrackingState(string objId)
        {
            return trackableObjects.Where((x) => x.GetTrackedID() == objId).First().IsTracked;
        }

    }
}
