using  UnityEngine;

namespace CyberCountry
{
    public interface ITrackerService
    {
        Transform GetCastlePos();
        Transform GetTowerPos();
        Transform GetPortalPos();

        bool IsCastleTracking();
        bool IsPortalTracking();
        bool IsTowerTracking();

    }
}