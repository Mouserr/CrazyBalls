using UnityEngine.UI;

namespace Assets.Scripts.Screens
{
    public class NonDrawingGraphic : UnityEngine.UI.Graphic
    {
        public override void SetMaterialDirty() { return; }
        public override void SetVerticesDirty() { return; }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}