using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    [System.Serializable]
    public class InventoryManager
    {
        [SerializeField] private List<InventorySlot> slots = new();
        [SerializeField] private float maxWeight;

        public List<InventorySlot> Slots => slots;
        public float MaxWeight => maxWeight;
        public float TotalWeight => slots.Sum(slot => slot.TotalWeight);

        private void Add(Item.Item item, int amount)
        {
        }

        private void Remove(Item.Item item, int amount)
        {
        }
    }
}