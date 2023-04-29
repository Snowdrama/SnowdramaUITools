using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrama.UI
{
    /// <summary>
    /// This class fits all the children into the current RectTransform
    /// Use SnowUIContentFitter if you need the RectTransform to fit
    /// to the size of the children
    /// </summary>
    [ExecuteInEditMode]
    public class UIVerticalGroup : MonoBehaviour
    {
        [Header("Gap")]
        public float gapX = 0.1f;
        public float gapY = 0.1f;
        [Header("Active Items")]
        public bool useActive;
        [Header("Force Update")]
        public bool forceUpdate = false;
        public List<RectTransform> children = new List<RectTransform>();

        public int columnCount = 1;
        public float rowHeight = 40;

        [Header("Debug")]
        public int rowCount = 0;
        public float percentWidth;
        public float percentHeight;

        void LateUpdate()
        {
            if (transform.childCount != children.Count || forceUpdate)
            {
                forceUpdate = false;
                CollectChildren();
                CalculateValues();
                if (children.Count > 0)
                {
                    for (int y = 0; y < rowCount; y++)
                    {
                        for (int x = 0; x < columnCount; x++)
                        {
                            int index = x + (y * columnCount);
                            if (index < children.Count)
                            {
                                var child = children[index];
                                var minX = x * percentWidth;
                                var minY = 1.0f - ((y + 1) * percentHeight);


                                var maxX = x * percentWidth + percentWidth;
                                var maxY = 1.0f - (y * percentHeight);
                                child.anchorMin = new Vector2(minX, minY);
                                child.anchorMax = new Vector2(maxX, maxY);

                                child.offsetMin = new Vector2(0, 0);
                                child.offsetMax = new Vector2(0, 0);
                            }
                        }
                    }
                }
            }
        }


        private void CalculateValues()
        {
            rowCount = Mathf.CeilToInt((float)children.Count / (float)columnCount);

            percentWidth = 1.0f / (float)columnCount;
            percentHeight = 1.0f / (float)rowCount;
        }

        private void CollectChildren()
        {
            children.Clear();
            foreach (Transform child in transform)
            {
                if (useActive)
                {
                    if (child.gameObject.activeSelf)
                    {
                        children.Add(child.GetComponent<RectTransform>());
                    }
                }
                else
                {
                    children.Add(child.GetComponent<RectTransform>());
                }
            }
        }
    }
}
