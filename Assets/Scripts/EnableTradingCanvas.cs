using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ChoicedDialogue {
    public class EnableTradingCanvas : MonoBehaviour
    {
        public CanvasGroup tradeCanvas;
        public CanvasGroup dialoguedCanvas;

        public void TradeEnable()
        {
            tradeCanvas.alpha = 1f;
            tradeCanvas.blocksRaycasts = true;
            dialoguedCanvas.GetComponent<CanvasGroup>().alpha = 0f;
            dialoguedCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;

        }
        public void TradeDisable()
        {
            dialoguedCanvas.GetComponent<CanvasGroup>().alpha = 1f;
            dialoguedCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
