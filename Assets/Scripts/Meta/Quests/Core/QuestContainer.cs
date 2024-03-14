using System;
using System.Collections.Generic;
using Meta.Quests.Core.Quests;
using UnityEngine;

namespace Meta.Quests.Core
{
    public class QuestContainer
    {
        public Dictionary<int, Quest> questsMap { get; private set; } = new();
        public bool Contains(int questId) => questsMap.ContainsKey(questId);
        public bool TryAdd(Quest quest, int questId)
        {
            if (Contains(questId)) return false;

            questsMap[questId] = quest;
            return true;
        }
        public bool TryRemove(int questId)
        {
            if (Contains(questId) == false) return false;

            questsMap.Remove(questId);
            return true;
        }
    }
}