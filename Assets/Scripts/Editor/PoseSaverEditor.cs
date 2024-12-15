using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PoseSaver))]
public class PoseSaverEditor : Editor {
    public override void OnInspectorGUI()
    {
        // Llama al inspector predeterminado
        DrawDefaultInspector();

        // Obtén el objeto de la clase PoseSaver
        PoseSaver poseSaver = (PoseSaver)target;

        // Añade botones personalizados
        if (GUILayout.Button("Guardar Gesto Mano Izquierda"))
        {
            poseSaver.GuardarGestoManoIz();
        }

        if (GUILayout.Button("Guardar Gesto Mano Derecha"))
        {
            poseSaver.GuardarGestoManoDr();
        }
    }
}
