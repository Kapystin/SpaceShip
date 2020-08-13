using System;
using UnityEngine;

namespace SpaceShip
{
    public class UIController : MasterSingleton<UIController>
    {
        public event Action EventShowMenuView;
        public event Action EventHideMenuView;
        public event Action EventShowSelectLevelView;
        public event Action EventHideSelectLevelView;
        public event Action EventShowHealthView;
        public event Action EventHideHealthView;
        public event Action EventShowScoreView;
        public event Action EventHideScoreView;
        public event Action<string> EventShowGameOverView;
        public event Action EventHideGameOverView;

        public void CallEventShowMenuView()
        {
            EventShowMenuView?.Invoke();
        }

        public void CallEventHideMenuView()
        {
            EventHideMenuView?.Invoke();
        }

        public void CallEventShowSelectLevelView()
        {
            EventShowSelectLevelView?.Invoke();
        }

        public void CallEventHideSelectLevelView()
        {
            EventHideSelectLevelView?.Invoke();
        }

        public void CallEventShowHealthView()
        {
            EventShowHealthView?.Invoke();
        }

        public void CallEventHideHealthView()
        {
            EventHideHealthView?.Invoke();
        }

        public void CallEventShowScoreView()
        {
            EventShowScoreView?.Invoke();
        }

        public void CallEventHideScoreView()
        {
            EventHideScoreView?.Invoke();
        }

        public void CallEventShowGameOverView(string labelText = "")
        {
            EventShowGameOverView?.Invoke(labelText);
        }

        public void CallEventHideGameOverView()
        {
            EventHideGameOverView?.Invoke();
        }

    }
}
