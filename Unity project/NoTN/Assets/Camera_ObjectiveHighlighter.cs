using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_ObjectiveHighlighter : MonoBehaviour {

	private static Material highlightMatGood;
	private static Material highlightMatBad;

	private MeshRenderer[] highlightRenderers;
	private MeshRenderer[] existingRenderers;
	private GameObject highlightHolder;
	private SkinnedMeshRenderer[] highlightSkinnedRenderers;
	private SkinnedMeshRenderer[] existingSkinnedRenderers;
	public GameObject[] hideHighlight;
	private bool attachedToHand = true;

	private bool enableHighlight = true;
	// Use this for initialization
	void Start () {
		highlightMatGood = (Material) Resources.Load ("VR_GoodItemHighlighter", typeof (Material));
		highlightMatBad = (Material) Resources.Load ("VR_GoodItemHighlighter", typeof (Material));
		CreateHighlightRenderers ();
		if (highlightMatGood == null)
			Debug.LogError ("Hover Highlight Material is missing. Please create a material named 'VR_GoodItemHighlighter' and place it in a Resources folder");
		if (highlightMatBad == null)
			Debug.LogError ("Hover Highlight Material is missing. Please create a material named 'VR_GoodItemHighlighter' and place it in a Resources folder");
	}

	// Update is called once per frame
	void Update () {

		if (attachedToHand == true) {
			if (enableHighlight == true) {
				UpdateHighlightRenderers ();
			}
		}
	}

	private void CreateHighlightRenderers () {
		existingSkinnedRenderers = this.GetComponentsInChildren<SkinnedMeshRenderer> (true);
		highlightHolder = new GameObject ("Highlighter");
		highlightSkinnedRenderers = new SkinnedMeshRenderer[existingSkinnedRenderers.Length];

		for (int skinnedIndex = 0; skinnedIndex < existingSkinnedRenderers.Length; skinnedIndex++) {
			SkinnedMeshRenderer existingSkinned = existingSkinnedRenderers[skinnedIndex];

			if (ShouldIgnoreHighlight (existingSkinned))
				continue;

			GameObject newSkinnedHolder = new GameObject ("SkinnedHolder");
			newSkinnedHolder.transform.parent = highlightHolder.transform;
			SkinnedMeshRenderer newSkinned = newSkinnedHolder.AddComponent<SkinnedMeshRenderer> ();
			Material[] materials = new Material[existingSkinned.sharedMaterials.Length];
			for (int materialIndex = 0; materialIndex < materials.Length; materialIndex++) {
				materials[materialIndex] = highlightMatGood;
			}

			newSkinned.sharedMaterials = materials;
			newSkinned.sharedMesh = existingSkinned.sharedMesh;
			newSkinned.rootBone = existingSkinned.rootBone;
			newSkinned.updateWhenOffscreen = existingSkinned.updateWhenOffscreen;
			newSkinned.bones = existingSkinned.bones;

			highlightSkinnedRenderers[skinnedIndex] = newSkinned;
		}

		MeshFilter[] existingFilters = this.GetComponentsInChildren<MeshFilter> (true);
		existingRenderers = new MeshRenderer[existingFilters.Length];
		highlightRenderers = new MeshRenderer[existingFilters.Length];

		for (int filterIndex = 0; filterIndex < existingFilters.Length; filterIndex++) {
			MeshFilter existingFilter = existingFilters[filterIndex];
			MeshRenderer existingRenderer = existingFilter.GetComponent<MeshRenderer> ();

			if (existingFilter == null || existingRenderer == null || ShouldIgnoreHighlight (existingFilter))
				continue;

			GameObject newFilterHolder = new GameObject ("FilterHolder");
			newFilterHolder.transform.parent = highlightHolder.transform;
			MeshFilter newFilter = newFilterHolder.AddComponent<MeshFilter> ();
			newFilter.sharedMesh = existingFilter.sharedMesh;
			MeshRenderer newRenderer = newFilterHolder.AddComponent<MeshRenderer> ();

			Material[] materials = new Material[existingRenderer.sharedMaterials.Length];
			for (int materialIndex = 0; materialIndex < materials.Length; materialIndex++) {
				materials[materialIndex] = highlightMatGood;
			}
			newRenderer.sharedMaterials = materials;

			highlightRenderers[filterIndex] = newRenderer;
			existingRenderers[filterIndex] = existingRenderer;
		}
	}

	private void UpdateHighlightRenderers () {
		if (highlightHolder == null)
			return;

		for (int skinnedIndex = 0; skinnedIndex < existingSkinnedRenderers.Length; skinnedIndex++) {
			SkinnedMeshRenderer existingSkinned = existingSkinnedRenderers[skinnedIndex];
			SkinnedMeshRenderer highlightSkinned = highlightSkinnedRenderers[skinnedIndex];

			if (existingSkinned != null && highlightSkinned != null && attachedToHand == false) {
				highlightSkinned.transform.position = existingSkinned.transform.position;
				highlightSkinned.transform.rotation = existingSkinned.transform.rotation;
				highlightSkinned.transform.localScale = existingSkinned.transform.lossyScale;
				highlightSkinned.localBounds = existingSkinned.localBounds;
				highlightSkinned.enabled = enableHighlight && existingSkinned.enabled && existingSkinned.gameObject.activeInHierarchy;

				int blendShapeCount = existingSkinned.sharedMesh.blendShapeCount;
				for (int blendShapeIndex = 0; blendShapeIndex < blendShapeCount; blendShapeIndex++) {
					highlightSkinned.SetBlendShapeWeight (blendShapeIndex, existingSkinned.GetBlendShapeWeight (blendShapeIndex));
				}
			} else if (highlightSkinned != null)
				highlightSkinned.enabled = false;

		}

		for (int rendererIndex = 0; rendererIndex < highlightRenderers.Length; rendererIndex++) {
			MeshRenderer existingRenderer = existingRenderers[rendererIndex];
			MeshRenderer highlightRenderer = highlightRenderers[rendererIndex];

			if (existingRenderer != null && highlightRenderer != null && attachedToHand == false) {
				highlightRenderer.transform.position = existingRenderer.transform.position;
				highlightRenderer.transform.rotation = existingRenderer.transform.rotation;
				highlightRenderer.transform.localScale = existingRenderer.transform.lossyScale;
				highlightRenderer.enabled = enableHighlight && existingRenderer.enabled && existingRenderer.gameObject.activeInHierarchy;
			} else if (highlightRenderer != null)
				highlightRenderer.enabled = false;
		}
	}

	private bool ShouldIgnoreHighlight (Component component) {
		return ShouldIgnore (component.gameObject);
	}
	private bool ShouldIgnore (GameObject check) {
		for (int ignoreIndex = 0; ignoreIndex < hideHighlight.Length; ignoreIndex++) {
			if (check == hideHighlight[ignoreIndex])
				return true;
		}

		return false;
	}
}