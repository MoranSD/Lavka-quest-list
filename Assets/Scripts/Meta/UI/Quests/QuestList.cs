using Meta.Quests.Core;
using Meta.Quests.Core.Quests;
using Meta.UI.Quests.Widgets;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using Zenject;

namespace Meta.UI.Quests
{
    public class QuestList : MonoBehaviour
    {
        public bool isAnimating { get; private set; }

        [SerializeField] private RectTransform _containerTF;
        [SerializeField] private float _widgetSpace = 10;
        [SerializeField] private WidgetByType[] _widgets;

        [Inject] private QuestManager _questManager;

        private List<QuestWidget> _activeWidgets = new();

        private void Awake()
        {
            _questManager.onAddQuest += OnAddWidget;
            _questManager.onRemoveQuest += OnRemoveWidget;
        }
        private void OnDestroy()
        {
            _questManager.onAddQuest -= OnAddWidget;
            _questManager.onRemoveQuest -= OnRemoveWidget;

            foreach (var widget in _activeWidgets)
                widget.onPressComplete -= OnAddWidget;
        }
        private void OnAddWidget(int questId)
        {
            var newQuest = _questManager.GetQuest(questId);
            var widgetPrefab = _widgets.First(x => x.questType == newQuest.type).widget;

            var widget = Instantiate(widgetPrefab, _containerTF);
            _activeWidgets.Add(widget);
            UpdateContainerSize();

            widget.SetStats(newQuest);
            widget.onPressComplete += OnPressComplete;
            widget.transform.localPosition = new Vector2(0, CalculateWidgetHeight(widget));

            isAnimating = true;
            widget.PlayAnimation(QuestWidget.QuestWidgetAnimationType.show, () => { isAnimating = false; });
        }
        private void OnRemoveWidget(int questId)
        {
            var removedWidget = _activeWidgets.First(x => x.id == questId);
            _activeWidgets.Remove(removedWidget);
            removedWidget.onPressComplete -= OnPressComplete;

            isAnimating = true;
            removedWidget.PlayAnimation(QuestWidget.QuestWidgetAnimationType.hide, () => 
            { 
                isAnimating = false;

                DestroyImmediate(removedWidget.gameObject);
                
                UpdateContainerSize();
                UpdateWidgetsPosition();
            });
        }
        private void UpdateWidgetsPosition()
        {
            if (_activeWidgets.Count == 0) return;

            isAnimating = true;

            for (int i = 0; i < _activeWidgets.Count; i++)
            {
                _activeWidgets[i].MoveTo(
                    new Vector2(0, CalculateWidgetHeight(_activeWidgets[i])),
                    (i + 1) == _activeWidgets.Count ? () => isAnimating = false : null);
            }
        }
        private float CalculateWidgetHeight(QuestWidget widget)
        {
            float spaceHeight = 0;
            int widgetSiblingPosition = widget.transform.GetSiblingIndex();

            spaceHeight -= widgetSiblingPosition * _widgetSpace;

            float totalPreviousWidgetsHeight = 0;
            foreach (var previousWidget in _activeWidgets)
            {
                if (previousWidget == widget) continue;
                if (previousWidget.transform.GetSiblingIndex() >= widgetSiblingPosition) continue;

                totalPreviousWidgetsHeight -= previousWidget.panelHeight;
            }

            return spaceHeight + totalPreviousWidgetsHeight;
        }
        private void UpdateContainerSize()
        {
            float totalSize = 0;
            foreach (var widget in _activeWidgets)
            {
                totalSize += widget.panelHeight;
                totalSize += _widgetSpace;
            }

            var containerSize = _containerTF.sizeDelta;
            containerSize.y = totalSize;
            _containerTF.sizeDelta = containerSize;
        }
        private void OnPressComplete(int widgetId) => _questManager.Remove(widgetId);

        [System.Serializable]
        private struct WidgetByType
        {
            public QuestType questType;
            public QuestWidget widget;
        }
    }
}
