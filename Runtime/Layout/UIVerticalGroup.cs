using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrama.UI
{
    /// <summary>
    /// This class fits all the children into horizontal columns based on some size.
    /// </summary>
    [ExecuteInEditMode]
    public class UIVerticalGroup : SnowUI
    {
        [Header("Vertical Settings")]
        public int numberOfRows = 0;
        void LateUpdate()
        {
            if (transform.childCount != children.Count || forceUpdate)
            {
                forceUpdate = false;
                CollectChildren();
                CalculateColumns(children.Count, numberOfRows);
                ProcessChildren();
            }
        }
    }
}
