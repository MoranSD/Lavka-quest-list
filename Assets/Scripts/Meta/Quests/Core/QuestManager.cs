using Meta.Quests.Core.Generation;
using Meta.Quests.Core.Quests;
using System;

namespace Meta.Quests.Core
{
    public class QuestManager
    {
        public event Action<int> onAddQuest;
        public event Action<int> onRemoveQuest;

        private QuestContainer _container;
        private QuestGeneratorProvider _generatorProvider;
        public QuestManager(QuestContainer container, QuestGeneratorProvider generatorProvider)
        {
            _container = container;
            _generatorProvider = generatorProvider;
        }
        public void Add(QuestType questType)
        {
            int questId = GetFreeQuestIndex();
            var quest = _generatorProvider.GenerateQuest(questType, questId);

            if (_container.TryAdd(quest, questId))
                onAddQuest?.Invoke(questId);
        }
        public void Remove(int questId)
        {
            if(_container.TryRemove(questId))
                onRemoveQuest?.Invoke(questId);
        }
        public Quest GetQuest(int questId) => _container.questsMap[questId];
        private int GetFreeQuestIndex()
        {
            int index = 0;
            while (true)
            {
                if (_container.Contains(index))
                {
                    index++;
                    continue;
                }

                return index;
            }
        }
    }
}