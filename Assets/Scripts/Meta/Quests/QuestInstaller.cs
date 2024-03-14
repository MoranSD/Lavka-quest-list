using Meta.Items;
using Meta.Items.Core.Abstract;
using Meta.Locations;
using Meta.Locations.Core.Abstract;
using Meta.Quests.Core;
using Meta.Quests.Core.Generation;
using Meta.Quests.Core.Generation.Abstract;
using Meta.Quests.Core.Quests;
using Meta.UI.Quests;
using UnityEngine;
using Zenject;

namespace Meta.Quests
{
    public class QuestInstaller : MonoInstaller
    {
        [SerializeField] private QuestNamesContainer _questNamesContainer;
        [SerializeField] private QuestDescriptionsContainer _questDescriptionsContainer;
        [SerializeField] private LocationRepository _locationRepository;
        [SerializeField] private ItemsRepository _itemsRepository;
        [SerializeField] private QuestList _questList;

        public override void InstallBindings()
        {
            InstallGenerators();
            InstallCore();
            InstallUI();
        }
        private void InstallGenerators()
        {
            Container.Bind<IQuestGenerator<AdventureQuest>>().To<AdventureQuestGenerator>().AsSingle().WithArguments(_locationRepository, _questNamesContainer, _questDescriptionsContainer);
            Container.Bind<IQuestGenerator<CollectingQuest>>().To<CollectingQuestGenerator>().AsSingle().WithArguments(_itemsRepository, _questNamesContainer, _questDescriptionsContainer);
            Container.Bind<IQuestGeneratorsContainer>().To<QuestGeneratorsContainer>().AsSingle().WithArguments(Container);
        }
        private void InstallCore()
        {
            Container.Bind<QuestGeneratorProvider>().AsSingle();
            Container.Bind<QuestContainer>().AsSingle();
            Container.BindInterfacesAndSelfTo<QuestManager>().AsSingle();
        }
        private void InstallUI()
        {
            Container.Bind<QuestList>().FromInstance(_questList).AsSingle();
        }
    }
}
