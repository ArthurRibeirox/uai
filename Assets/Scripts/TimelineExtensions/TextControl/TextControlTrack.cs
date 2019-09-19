using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using TMPro;

namespace TimelineExtensions
{
    [TrackColor(0.4f, 0.7f, 0.6f)]
    [TrackClipType(typeof(TextControlClip))]
    [TrackBindingType(typeof(TextMeshProUGUI))]
    public class TextControlTrack : TrackAsset
    {
    }
}
