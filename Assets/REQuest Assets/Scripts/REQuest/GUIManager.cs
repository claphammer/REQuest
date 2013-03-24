using UnityEngine;

public class GUIManager : MonoBehaviour 
{
	public RenderTexture MiniMapTexture;
	public Material MiniMapMaterial;
	public bool MMisDrawn = true; //allow minimap to be taggled from an external button
	
	private float offset;
	
	void Awake()
	{
		offset = 10;
	}
	
	void OnGUI()
	{
		if(MMisDrawn == true)
			if(Event.current.type == EventType.Repaint)
				Graphics.DrawTexture(new Rect(Screen.width - 256 - offset, offset, 256, 256), MiniMapTexture, MiniMapMaterial);
	}

}
