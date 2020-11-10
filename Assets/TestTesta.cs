using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TestTesta : MonoBehaviour
{
	public RectTransform targetImage;
	public Canvas overlayReferenceCanvas;
	public CanvasGroup portraitImg_canvasGrp;

	void Start()
	{

	}

	[ContextMenu("Move")]
	void Move()
	{
		overlayReferenceCanvas.renderMode = RenderMode.WorldSpace;
		RectTransform me = GetComponent<RectTransform>();
		

		me.SetParent(targetImage);
		me.anchorMin = Vector2.one / 2f;
		me.anchorMax = Vector2.one / 2f;
		Vector2 myScale = me.localScale;
		me.localScale = Vector3.one;
		me.sizeDelta = new Vector2((me.sizeDelta.x * myScale.x), (me.sizeDelta.y * myScale.y));

		Sequence mySequence = DOTween.Sequence();


		me.DOSizeDelta(targetImage.sizeDelta, 2f).SetEase(Ease.InOutCubic);
		mySequence.Append(me.DOAnchorPos3D(targetImage.anchoredPosition3D, 2f).SetEase(Ease.InOutCubic));
		mySequence.Append(portraitImg_canvasGrp.DOFade(1f, 0.5f));

		this.transform.DOLocalRotateQuaternion(Quaternion.identity, 2f).SetEase(Ease.InOutCubic);

		Image myImage = this.GetComponent<Image>();
		float targetPixelsPerUnit = targetImage.GetComponent<Image>().pixelsPerUnit;
		DOTween.To(()=> myImage.pixelsPerUnitMultiplier, x=> myImage.pixelsPerUnitMultiplier = x , targetPixelsPerUnit , 2f);


		// Debug.Break();
	}
}
