using Meta.Items.Core.Abstract;
using Meta.Quests.Core.Generation.Abstract;
using Meta.Quests.Core.Quests;

namespace Meta.Quests.Core.Generation
{
    public class CollectingQuestGenerator : BaseQuestGenerator<CollectingQuest>
    {
        protected override QuestType questType => QuestType.collecting;
        private IItemsRepository _itemsRepository;
        public CollectingQuestGenerator(IItemsRepository itemsRepository, IQuestNamesContainer namesContainer, IQuestDescriptionContainer descriptionContainer) : base(namesContainer, descriptionContainer)
        {
            _itemsRepository = itemsRepository;
        }
        public override CollectingQuest Generate(int questId)
        {
            var quest = new CollectingQuest();

            quest.id = questId;
            quest.type = QuestType.collecting;
            InsertName(quest);

            var items = _itemsRepository.GetAll();
            int itemId = new System.Random().Next(0, items.Length);
            quest.targetItem = items[itemId];

            int targetItemsCount = new System.Random().Next(1, 10);//я бы мог прокинуть конфиг, но да пофиг =)
            quest.targetCount = targetItemsCount;

            InsertDescription(quest, new string[] { items[itemId].name, targetItemsCount.ToString() });

            return quest;
        }
    }
}