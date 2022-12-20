using UnityEngine;
using UnityEngine.UI;
using WIFramework.UI;

namespace WSH
{
    public class UI_Panel_Run : PanelBase
    {
        public override void Initialize()
        {
            base.Initialize();
            Deactive();
        }
    }
}