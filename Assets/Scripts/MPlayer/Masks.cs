using System.Collections.Generic;
using UnityEngine;

namespace MPlayer
{
    [CreateAssetMenu(fileName = "Masks", menuName = "Scriptable Objects/Masks")]
    public class Masks : ScriptableObject
    {
        public List<Sprite> masks;
    }
}
