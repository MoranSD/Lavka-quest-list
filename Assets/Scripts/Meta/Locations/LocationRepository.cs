using Meta.Locations.Core;
using Meta.Locations.Core.Abstract;
using UnityEngine;

namespace Meta.Locations
{
    [CreateAssetMenu(menuName = "Meta/Locations/Repository")]
    public class LocationRepository : ScriptableObject, ILocationRepository
    {
        [SerializeField] private Location[] _locations;
        public Location[] GetAll() => _locations;
    }
}