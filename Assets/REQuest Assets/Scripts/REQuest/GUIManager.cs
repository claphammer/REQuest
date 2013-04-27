using UnityEngine;

public class GUIManager : MonoBehaviour 
{
	public RenderTexture MiniMapTexture;
	public Material MiniMapMaterial;
	public bool MMisDrawn = true; //allow minimap to be toggled from an external button
	public int size;
	private float offset;
	
	void Awake()
	{
		offset = 10;
	}
	
	void OnGUI()
	{
		if(MMisDrawn == true)
			if(Event.current.type == EventType.Repaint)
				Graphics.DrawTexture(new Rect(Screen.width - size - offset, offset, size, size), MiniMapTexture, MiniMapMaterial);
	}

}
