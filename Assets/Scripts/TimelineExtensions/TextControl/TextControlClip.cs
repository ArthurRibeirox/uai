using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

using TMPro;

namespace TimelineExtensions
{
    [System.Serializable]
    public class TextControlClip : PlayableAsset, ITimelineClipAsset
    {
        public ClipCaps clipCaps { get { return ClipCaps.All; } }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            return ScriptPlayable<TextControlPlayable>.Create(graph); ;
        }
    }
}
