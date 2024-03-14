using Meta.Quests.Core.Quests;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Meta.UI.Quests.Widgets
{
    public partial class QuestWidget : MonoBehaviour
    {
        public event Action<int> onPressComplete;

        [field: SerializeField] public float panelHeight { get; private set; } = 100;
        public int id { get; private set; } = -1;

        [SerializeField] private Button _completeButton;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Ease _ease;
        [SerializeField] private float _duration;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _completeButton.onClick.AddListener(OnPressComplete);
            _rectTransform = GetComponent<RectTransform>();
        }
        private void OnDestroy()
        {
            _completeButton.onClick.RemoveListener(OnPressComplete);

            if (DOTween.IsTweening(this))
                DOTween.Kill(this);
        }
        public virtual void SetStats(Quest quest)
        {
            id = quest.id;
            _name.text = quest.name;
            _description.text = quest.description;
        }
        public void MoveTo(Vector2 position, Action callBack = null)
        {
            _rectTransform.DOAnchorPos(position, _duration).SetEase(_ease).OnComplete(() =>
            {
                if (transform == null) return;

                callBack?.Invoke();
            });
        }
        public void PlayAnimation(QuestWidgetAnimationType animationType, Action callBack = null)
        {

            switch (animationType)
            {
                case QuestWidgetAnimationType.show:
                    transform.localScale = Vector3.zero;
                    transform.DOScale(Vector3.one, _duration).SetEase(_ease).OnComplete(() =>
                    {
                        if (transform == null) return;

                        callBack?.Invoke();
                    });
                    break;
                case QuestWidgetAnimationType.hide:
                    transform.localScale = Vector3.one;
                    transform.DOScale(Vector3.zero, _duration).SetEase(_ease).OnComplete(() =>
                    {
                        if (transform == null) return;

                        callBack?.Invoke();
                    });
                    break;
            }
        }
        private void OnPressComplete() => onPressComplete?.Invoke(id);
    }
}
