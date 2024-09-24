using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGameObjectButton : EnvironmentButton
{
    [SerializeField] private MeshRenderer renderer;

    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material highlightedMaterial;

    public override void ToggleHover(bool value)
    {
        isBeingHovered = value;

        if (value)
        {
            List<Material> rendererMaterials = new List<Material>();
            foreach (Material material in renderer.materials)
            {
                rendererMaterials.Add(material);
            }

            rendererMaterials[1] = highlightedMaterial;
            rendererMaterials[2] = highlightedMaterial;
            rendererMaterials[8] = highlightedMaterial;

            renderer.materials = rendererMaterials.ToArray();
        }
        else
        {
            List<Material> rendererMaterials = new List<Material>();
            foreach (Material material in renderer.materials)
            {
                rendererMaterials.Add(material);
            }

            rendererMaterials[1] = normalMaterial;
            rendererMaterials[2] = normalMaterial;
            rendererMaterials[8] = normalMaterial;

            renderer.materials = rendererMaterials.ToArray();
        }
    }
}