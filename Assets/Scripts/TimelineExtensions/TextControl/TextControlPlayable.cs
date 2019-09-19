using UnityEngine;
using UnityEngine.Playables;

using TMPro;

using DG.Tweening;
using CharTween;

// #if UNITY_EDITOR
// using DG.DOTweenEditor;
// #endif

namespace TimelineExtensions
{
    [System.Serializable]
    public class TextControlPlayable : PlayableBehaviour
    {
        public TextMeshProUGUI textMesh;

        private Sequence sequence;
        private CharTweener charTweener;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (textMesh == null)
            {
                textMesh = playerData as TextMeshProUGUI;
                charTweener = textMesh.GetCharTweener();

                if (!textMesh.gameObject.activeInHierarchy)
                    return;
            }

            if (!Application.isPlaying)
            {
                // PrepareTween(playable);
                // DOTweenEditorPreview.PrepareTweenForPreview(sequence, true, true, false);
                // sequence.SetUpdate(UpdateType.Manual);
                // DOTweenEditorPreview.Start();
                // sequence.Goto((float)playable.GetTime());
                // DOTween.ManualUpdate(0, 0);

                return;
            }

            Play(playable);
        }

        private void Play(Playable playable)
        {
            if (sequence == null || !sequence.IsPlaying())
            {
                KillSequence();
                PrepareTween(playable);
                sequence.Goto((float)playable.GetTime(), true);
            }
        }

        private float overlap = 0.5f;

        private void PrepareTween(Playable playable)
        {
            var duration = (float)playable.GetDuration();
            var charAnimationDuration = duration * (1 + overlap) / charTweener.CharacterCount;
            var charStartOffset = charAnimationDuration * (1 - overlap);

            sequence = DOTween.Sequence();
            sequence.AppendCallback(() => sequence = null);

            for (var i = 0; i < charTweener.CharacterCount; ++i)
            {
                var timeOffset = charStartOffset * i;

                var stepDuration = charAnimationDuration / 4;

                var charSequence = DOTween.Sequence();
                charSequence.Append(charTweener.DOLocalMoveY(i, 0.5f, stepDuration).SetEase(Ease.InOutCubic))
                    .Join(charTweener.DOFade(i, 0, stepDuration).From())
                    .Join(charTweener.DOScale(i, 0, stepDuration).From().SetEase(Ease.OutBack, 5))
                    .Append(charTweener.DOLocalMoveY(i, 0, stepDuration).SetEase(Ease.OutBounce));
                sequence.Insert(timeOffset, charSequence);
            }
        }

        private void KillSequence()
        {
            if (sequence != null)
            {
                sequence.Kill();
                sequence = null;
            }
        }
    }
}
