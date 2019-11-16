using Assets.Scripts.Core.Curves;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CurveVisualizer))]
public class CurveEditor : Editor
{
    // данная функция выполняет отрисовку инспектора компонента
    public override void OnInspectorGUI()
    {
        // выполняем отрисовку инспектора по умолчанию
        DrawDefaultInspector();
		CurveVisualizer curveVisualizer = ((CurveVisualizer)target);
		if (GUILayout.Button("Reverse"))
		{
		
			curveVisualizer.ReversePoints();
		}

	    if (GUILayout.Button("Shift up by 10"))
		{
			curveVisualizer.ShiftBy(Vector3.up * 10);
		}
		if (GUILayout.Button("Shift down by 10"))
		{
			curveVisualizer.ShiftBy(Vector3.down * 10);
		}
		if (curveVisualizer.globalSpace && GUILayout.Button("To Local"))
		{
			curveVisualizer.ToLocal();
		}
		if (!curveVisualizer.globalSpace && GUILayout.Button("To Global"))
		{
			curveVisualizer.ToGlobal();
		}
	}
 
    // отрисовка в сцене, здесь в отличии от компонента, где мы использовали
    // для отрисовки класс Gizmos используется клас Handles (манипуляторы)
    public void OnSceneGUI()
    {

		CurveVisualizer bezier = target as CurveVisualizer;
		if (bezier && bezier.ControlPoints != null && bezier.ControlPoints.Length > 0)
        {
			
            Undo.RecordObject(bezier, "dots moving");
            // Для каждой контрольной точки создаем манипулятор в виде сферы
            Quaternion rot = Quaternion.identity;
			float size = HandleUtility.GetHandleSize(bezier.globalSpace ? bezier.ControlPoints[0] : bezier.transform.TransformPoint(bezier.ControlPoints[0])) * 0.2f;
	        for (int i = 0; i < bezier.ControlPoints.Length; i++)
	        {
		        if (bezier.globalSpace)
		        {
			        bezier.ControlPoints[i] = Handles.FreeMoveHandle(bezier.ControlPoints[i],
				        rot, size, Vector3.zero, Handles.SphereHandleCap);
		        }
		        else
		        {
			        bezier.ControlPoints[i] = bezier.transform.InverseTransformPoint(
				        Handles.FreeMoveHandle(bezier.transform.TransformPoint(bezier.ControlPoints[i]),
					        rot, size, Vector3.zero, Handles.SphereHandleCap));
		        }
	        }
        }
    }
 
} 