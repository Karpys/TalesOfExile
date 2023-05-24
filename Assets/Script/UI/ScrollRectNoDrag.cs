using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KarpysDev.Script.UI
{
    public class ScrollRectNoDrag : ScrollRect
    {
        public override void OnBeginDrag(PointerEventData eventData) { }
        public override void OnDrag(PointerEventData eventData) { }
        public override void OnEndDrag(PointerEventData eventData) { }
    }
}
