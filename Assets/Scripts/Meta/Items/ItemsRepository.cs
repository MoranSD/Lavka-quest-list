using Meta.Items.Core;
using Meta.Items.Core.Abstract;
using UnityEngine;

namespace Meta.Items
{
    [CreateAssetMenu(menuName = "Meta/Items/Repository")]
    public class ItemsRepository : ScriptableObject, IItemsRepository
    {
        [SerializeField] private Item[] _items;
        public Item[] GetAll() => _items;
    }
}