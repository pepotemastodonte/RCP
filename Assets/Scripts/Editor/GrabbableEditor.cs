using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Grabbable))]
public class GrabbableEditor : Editor {
    public override void OnInspectorGUI()
    {
        Grabbable grabbable = (Grabbable)target;

        // Mostrar propiedades estándar
        grabbable.siendoAgarrado = EditorGUILayout.Toggle("Siendo Agarrado", grabbable.siendoAgarrado);       
        grabbable.KinematicAlAgarrar = EditorGUILayout.Toggle("Kinematic Al Agarrar", grabbable.KinematicAlAgarrar);



        grabbable.snapAlAgarrar = EditorGUILayout.Toggle("Snap Al Agarrar", grabbable.snapAlAgarrar);


        if (grabbable.snapAlAgarrar)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("EjeX", GUILayout.Width(30));
            grabbable.MoverEjeX = EditorGUILayout.Toggle(grabbable.MoverEjeX, GUILayout.Width(20));
            GUILayout.Label("EjeY", GUILayout.Width(30));
            grabbable.MoverEjeY = EditorGUILayout.Toggle(grabbable.MoverEjeY, GUILayout.Width(20));
            GUILayout.Label("EjeZ", GUILayout.Width(30));
            grabbable.MoverEjeZ = EditorGUILayout.Toggle(grabbable.MoverEjeZ, GUILayout.Width(20));
            EditorGUILayout.EndHorizontal();
        }

        // Mostrar la propiedad "Rotar Con La Mano"
        grabbable.RotarConLaMano = EditorGUILayout.Toggle("Rotar Con La Mano", grabbable.RotarConLaMano);

        // Mostrar los ejes solo si "Rotar Con La Mano" está activado
        if (grabbable.RotarConLaMano)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("EjeX", GUILayout.Width(30));
            grabbable.RotacionEjeX = EditorGUILayout.Toggle(grabbable.RotacionEjeX, GUILayout.Width(20));
            GUILayout.Label("EjeY", GUILayout.Width(30));
            grabbable.RotacionEjeY = EditorGUILayout.Toggle(grabbable.RotacionEjeY, GUILayout.Width(20));
            GUILayout.Label("EjeZ", GUILayout.Width(30));
            grabbable.RotacionEjeZ = EditorGUILayout.Toggle(grabbable.RotacionEjeZ, GUILayout.Width(20));
            EditorGUILayout.EndHorizontal();
        }

        // Asegurarse de guardar los cambios
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
