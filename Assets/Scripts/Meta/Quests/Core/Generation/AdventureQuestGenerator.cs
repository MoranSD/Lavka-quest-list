using Meta.Locations.Core.Abstract;
using Meta.Quests.Core.Generation.Abstract;
using Meta.Quests.Core.Quests;

namespace Meta.Quests.Core.Generation
{
    public class AdventureQuestGenerator : BaseQuestGenerator<AdventureQuest>
    {
        protected override QuestType questType => QuestType.adventure;
        private ILocationRepository _locationRepository;
        public AdventureQuestGenerator(ILocationRepository locationRepository, IQuestNamesContainer namesContainer, IQuestDescriptionContainer descriptionContainer) : base(namesContainer, descriptionContainer)
        {
            _locationRepository = locationRepository;
        }
        public override AdventureQuest Generate(int questId)
        {
            var quest = new AdventureQuest();

            quest.id = questId;
            quest.type = QuestType.adventure;
            InsertName(quest);

            var locations = _locationRepository.GetAll();
            int locationId = new System.Random().Next(0, locations.Length);
            quest.targetLocation = locations[locationId];

            InsertDescription(quest, locations[locationId].name);

            return quest;
        }
    }
}
