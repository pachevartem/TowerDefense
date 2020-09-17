using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CyberCountry
{
    public class Simulator : MonoBehaviour, ITrackerService
    {
        public GameObject CastleTarget;
        public GameObject TowerTarget;
        public GameObject PortalTarget;

        private bool IsPortal = true;
        private bool IsCastle = true;
        private bool IsTower = true;
        
        public Transform GetCastlePos()
        {
            return CastleTarget.transform;
        }

        public Transform GetTowerPos()
        {
            return TowerTarget.transform;
        }

        public Transform GetPortalPos()
        {
            return PortalTarget.transform;
        }

        public bool IsCastleTracking()
        {            
            return IsCastle;
        }

        public bool IsPortalTracking()
        {
            return IsPortal;
        }

        public bool IsTowerTracking()
        {
            return IsTower;
        }
    }
}