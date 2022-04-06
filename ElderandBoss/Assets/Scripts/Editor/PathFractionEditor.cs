//#if UNITY_EDITOR
//using UnityEditor;
//using KingFeast.ScriptableObjects.Wave;
//using UnityEngine;

//namespace KingFeast.UnityEditor {

//    [CustomEditor(typeof(PathFraction))]
//    public class PathFractionEditor : Editor {
//        int selectedCurve;
//        private void OnEnable() {
//            SceneView.duringSceneGui += AddOnSceneGUI;
//        }
//        private void OnDisable() {
//            SceneView.duringSceneGui -= AddOnSceneGUI;
//        }

//        public override void OnInspectorGUI() {
//            PathFraction selectedPath = target as PathFraction;

//            if (selectedCurve > selectedPath.points.Length - 1)
//                selectedCurve = selectedPath.points.Length - 1;

//            if (selectedPath == null)
//                return;

//            if (GUILayout.Button("New Line")) {
//                AddCurve(ref selectedCurve);
//            }

//            GUILayout.Label($"Curve: {selectedCurve + 1}");
//            GUILayout.Label($"Index Value: {selectedCurve}");

//            if (GUILayout.Button("Previous Curve")) {
//                if (selectedCurve > 0)
//                    selectedCurve--;
//            }

//            if (GUILayout.Button("Next Curve")) {
//                if (selectedCurve < selectedPath.points.Length - 1)
//                    selectedCurve++;
//            }

//            DrawCurveSections(ref selectedCurve);
//        }

//        private void AddOnSceneGUI(SceneView sceneView) {
//            PathFraction selectedPath = target as PathFraction;

//            DrawHandles(selectedCurve);
//            DrawFullPath();
//        }

//        private void DrawCurveSections(ref int index) {
//            PathFraction selectedPath = target as PathFraction;

//            if (selectedPath.points.Length == 0)
//                return;

//            int delete = -1;

//            for (int i = 0; i < selectedPath.points.Length; i++) {
//                EditorGUI.BeginChangeCheck();


//                int size = 64;
//                EditorGUILayout.BeginHorizontal(GUILayout.Width(size));

//                EditorGUILayout.LabelField("Curve " + (i + 1), GUILayout.Width(64));

//                if (GUILayout.Button("Delete", GUILayout.Width(64))) {
//                    delete = i;
//                    if (index >= selectedPath.points.Length - 1 && index != 1)
//                        index = selectedPath.points.Length - 2;
//                }

//                GUI.enabled = index != i;
//                if (GUILayout.Button("Select", GUILayout.Width(64))) {
//                    Undo.RecordObject(target, "select node");
//                    index = i;
//                }
//                GUI.enabled = true;


//                EditorGUILayout.EndHorizontal();
//                EditorGUILayout.BeginHorizontal();

//                EditorGUILayout.BeginVertical();

//                GUI.enabled = index == i;
//                Vector3 firstPoint = EditorGUILayout.Vector3Field("First Point", selectedPath.points[i].bezierFirstPoint);

//                Vector3 endPosition;
//                endPosition = EditorGUILayout.Vector3Field("End Point", selectedPath.points[i].bezierLastPoint);

//                GUI.enabled = true;

//                EditorGUILayout.EndVertical();
//                EditorGUILayout.EndHorizontal();

//                if (EditorGUI.EndChangeCheck()) {
//                    Undo.RecordObject(target, "changed time or position");

//                    selectedPath.points[i].bezierFirstPoint = firstPoint;
//                    selectedPath.points[i].bezierLastPoint = endPosition;

//                    SceneView.RepaintAll();
//                }
//            }

//            if (delete != -1) {
//                Undo.RecordObject(target, "Removed point moving platform");
//                ArrayUtility.RemoveAt(ref selectedPath.points, delete);
//            }
//        }

//        private void AddCurve(ref int index) {
//            PathFraction selectedPath = target as PathFraction;

//            Undo.RecordObject(target, "added node");

//            Vector3 newPoint;
//            if (selectedPath.points.Length == 0) {
//                newPoint = Vector3.zero;
//                index = 1;
//            }
//            else
//                newPoint = selectedPath.points[selectedPath.points.Length - 1].bezierLastPoint;

//            BezierPoints points = new BezierPoints(newPoint); // Cria próximo ao ultimo ponto
//            ArrayUtility.Add(ref selectedPath.points, points);
//            SceneView.RepaintAll();
//        }

//        private void DrawHandles(int selectedCurve) {
//            PathFraction selectedPath = target as PathFraction;

//            Vector3 startPosition;
//            Vector3 endPosition;

//            Vector3 startTangent;
//            Vector3 endTangent;

//            Undo.RecordObject(target, "Handles");

//            if (selectedPath.points.Length <= 0)
//                return;
//            else {
//                //Draws each handle
//                if (selectedCurve != 0 && selectedPath.points.Length == 1)
//                    selectedCurve = 0;
//                startPosition = selectedPath.points[selectedCurve].bezierFirstPoint;
//                selectedPath.points[selectedCurve].bezierFirstPoint = Handles.PositionHandle(startPosition, Quaternion.identity);

//                endPosition = selectedPath.points[selectedCurve].bezierLastPoint;
//                selectedPath.points[selectedCurve].bezierLastPoint = Handles.PositionHandle(endPosition, Quaternion.identity);

//                startTangent = selectedPath.points[selectedCurve].firstPointTangent;
//                selectedPath.points[selectedCurve].firstPointTangent = Handles.PositionHandle(startTangent, Quaternion.identity);

//                endTangent = selectedPath.points[selectedCurve].lastPointTangent;
//                selectedPath.points[selectedCurve].lastPointTangent = Handles.PositionHandle(endTangent, Quaternion.identity);
//            }

//            if (selectedCurve > selectedPath.points.Length - 1)
//                selectedCurve = selectedPath.points.Length - 1;

//            Undo.RecordObject(target, "Moved object.");



//            if (selectedCurve > 0)
//                selectedPath.points[selectedCurve - 1].bezierLastPoint = startPosition;

//            if (selectedCurve < selectedPath.points.Length - 1)
//                selectedPath.points[selectedCurve + 1].bezierFirstPoint = endPosition;


//            Handles.color = Color.red;

//            Handles.DrawLine(startPosition, startTangent);
//            Handles.DrawLine(endPosition, endTangent);

//        }

//        private void DrawFullPath() {
//            PathFraction selectedPath = target as PathFraction;

//            Vector3 startPosition;
//            Vector3 endPosition;
//            Vector3 startTangent;
//            Vector3 endTangent;

//            for (int i = 0; i < selectedPath.points.Length; i++) {
//                startPosition = selectedPath.points[i].bezierFirstPoint;
//                endPosition = selectedPath.points[i].bezierLastPoint;
//                startTangent = selectedPath.points[i].firstPointTangent;
//                endTangent = selectedPath.points[i].lastPointTangent;
//                Handles.color = Color.blue;
//                Handles.DrawBezier(startPosition, endPosition, startTangent, endTangent, Color.blue, Texture2D.normalTexture, 5);
//            }
//        }
//    }
//}
//#endif
