using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseSaver : MonoBehaviour {
    [SerializeField] private Mano ManoIz;
    [SerializeField] private Mano ManoDr;

    [SerializeField] private List<Transform> Comps_ManIz = new List<Transform>();
    [SerializeField] private List<Transform> Comps_ManDr = new List<Transform>();

    // Intervalo en segundos para la comparación de gestos
    [SerializeField] private float intervaloComparacion = 0.5f; // Tiempo entre comparaciones (en segundos)

    // Objetos cuyo color se cambiará dependiendo de la comparación de gestos
    [SerializeField] private GameObject objetoManoIzquierda;
    [SerializeField] private GameObject objetoManoDerecha;

    private float tiempoRestante; // Temporizador para controlar la frecuencia de comparación

    [SerializeField] float margenError = 0.1f;


    private void Start()
    {
        // Inicializa el tiempo restante con el intervalo deseado
        tiempoRestante = intervaloComparacion;

        // Obtiene todos los hijos y los almacena en la lista
        Comps_ManIz = GetAllChildren(ManoIz.PadreMano);
        Comps_ManDr = GetAllChildren(ManoDr.PadreMano);
    }

    private void Update()
    {
        // Resta el tiempo transcurrido en cada frame
        tiempoRestante -= Time.deltaTime;

        // Cuando el temporizador llega a cero o menos, realizamos la comparación
        if (tiempoRestante <= 0f)
        {
            // Restablecemos el temporizador
            tiempoRestante = intervaloComparacion;

            // Llamamos a las comparaciones de gestos
            CompararGestos();
        }
    }

    private void CompararGestos()
    {
        // Puedes ajustar el margen de error según lo necesites
        

        // Comparar gestos de la mano izquierda
        bool gestoIzquierdaCoincide = CompararGesto(ManoIz, Comps_ManIz, margenError);
        CambiarColor(objetoManoIzquierda, gestoIzquierdaCoincide);

        // Comparar gestos de la mano derecha
        bool gestoDerechaCoincide = CompararGesto(ManoDr, Comps_ManDr, margenError);
        CambiarColor(objetoManoDerecha, gestoDerechaCoincide);
    }

    private void CambiarColor(GameObject objeto, bool gestoCoincidente)
    {
        // Cambiar el color del objeto dependiendo de si el gesto coincide
        if (objeto != null)
        {
            Renderer renderer = objeto.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Si el gesto coincide, pon el color verde, si no, pon el color rojo
                renderer.material.color = gestoCoincidente ? Color.green : Color.red;
            }
        }
    }

    public static List<Transform> GetAllChildren(Transform parent)
    {
        List<Transform> allChildren = new List<Transform>();
        Queue<Transform> toProcess = new Queue<Transform>();
        HashSet<Transform> processed = new HashSet<Transform>();

        // Agrega el objeto inicial a la cola
        toProcess.Enqueue(parent);

        while (toProcess.Count > 0)
        {
            Transform current = toProcess.Dequeue();

            // Verifica si ya se procesó este objeto
            if (processed.Contains(current))
                continue;

            // Marca el objeto como procesado
            processed.Add(current);

            // Itera sobre los hijos del objeto actual
            foreach (Transform child in current)
            {
                allChildren.Add(child);
                toProcess.Enqueue(child); // Agrega los hijos a la cola para procesar sus hijos
            }
        }

        return allChildren;
    }

    public void GuardarGestoManoIz()
    {
        GuardarGesto(ManoIz, Comps_ManIz);
    }

    public void GuardarGestoManoDr()
    {
        GuardarGesto(ManoDr, Comps_ManDr);
    }

    private void GuardarGesto(Mano mano, List<Transform> componentes)
    {
        Gestos nuevoGesto = new Gestos { gesto = new List<GestoData>() };

        foreach (Transform comp in componentes)
        {
            // Guardamos la posición y la rotación de cada componente
            GestoData data = new GestoData
            {
                posicion = comp.localPosition,
                rotacion = comp.localRotation
            };

            nuevoGesto.gesto.Add(data);
        }

        mano.listaGestos.Add(nuevoGesto);

        Debug.Log($"Gesto guardado para {mano.PadreMano.name}. Total de gestos: {mano.listaGestos.Count}");
    }

    public bool CompararGesto(Mano mano, List<Transform> componentes, float margenError)
    {
        foreach (var gesto in mano.listaGestos)
        {
            if (EsGestoCoincidente(gesto.gesto, componentes, margenError))
            {
                Debug.Log($"El gesto coincide con un gesto guardado en {mano.PadreMano.name}.");
                return true;
            }
        }

        Debug.Log($"No se encontró coincidencia en los gestos guardados de {mano.PadreMano.name}.");
        return false;
    }

    private bool EsGestoCoincidente(List<GestoData> gestoGuardado, List<Transform> gestoActual, float margenError)
    {
        // Verifica si ambos gestos tienen la misma cantidad de componentes
        if (gestoGuardado.Count != gestoActual.Count)
        {
            return false; // Si tienen diferente cantidad de componentes, no coinciden
        }

        // Recorre cada componente de los gestos guardados y actuales
        for (int i = 0; i < gestoGuardado.Count; i++)
        {
            GestoData guardado = gestoGuardado[i];
            Transform actual = gestoActual[i];

            // Compara las posiciones de ambos componentes
            if (Vector3.Distance(guardado.posicion, actual.localPosition) > margenError)
            {
                return false; // Si la distancia en posición excede el margen de error, no coinciden
            }

            // Compara las rotaciones de ambos componentes
            if (Quaternion.Angle(guardado.rotacion, actual.localRotation) > margenError)
            {
                return false; // Si la diferencia en rotación excede el margen de error, no coinciden
            }
        }

        return true; // Si no se encontró ninguna diferencia significativa, los gestos coinciden
    }
}

[System.Serializable]
public class Mano {
    public Transform PadreMano;
    public Transform GrabPosition;
    public List<Gestos> listaGestos = new List<Gestos>();
}

[System.Serializable]
public class Gestos {
    public List<GestoData> gesto = new List<GestoData>();
}

[System.Serializable]
public class GestoData {
    public Vector3 posicion;
    public Quaternion rotacion;
}
