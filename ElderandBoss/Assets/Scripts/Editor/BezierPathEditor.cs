#if UNITY_EDITOR
using System.Linq;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Elderand.ScriptableObjects;

namespace Elderand.Editor
{
    [CustomEditor(typeof(BezierPath))]
    public class BezierPathEditor : OdinEditor
    {
        private BezierPath bezierPath;
        private int editFractionIndex;

        protected override void OnEnable()
        {
            base.OnEnable();
            bezierPath = target as BezierPath;
            SceneView.duringSceneGui += AddOnSceneGUI;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            SceneView.duringSceneGui -= AddOnSceneGUI;
        }

        public override void OnInspectorGUI() {
            SirenixEditorGUI.Title("Bezier Path Editor", null, TextAlignment.Center, true);

            EditorGUILayout.Space(10);
            if (GUILayout.Button("Add New Path Fraction")) {
                AddNewPathFraction();
            }
            EditorGUILayout.Space(10);

            SirenixEditorGUI.BeginBox();
            Draw();
            SirenixEditorGUI.EndBox();
        }

        private void Draw() {

            Undo.RecordObject(bezierPath, "Inspector");
            BezierCurve lastCurve = bezierPath.pathFractions[0].curves[0];
            bool changeLastEnd = false;

            // Draw Path Fraction
            for (int i = 0; i < bezierPath.pathFractions.Count; i++) {
                PathFraction pathFraction = bezierPath.pathFractions[i];

                GUILayout.Space(10);
                SirenixEditorGUI.Title($"Path Fraction {i + 1}", null, TextAlignment.Left, true);
                GUILayout.Space(10);


                // Buttons
                 EditorGUILayout.BeginHorizontal();

                GUI.enabled = editFractionIndex != i;
                if (GUILayout.Button("Edit Curve", GUILayout.MaxWidth(200))) {
                    editFractionIndex = i;
                }
                GUI.enabled = true;

                if (GUILayout.Button("Add New Curve", GUILayout.MaxWidth(200))) {

                    BezierCurve newCurve = new BezierCurve(pathFraction.curves.Last().endPosition);
                    pathFraction.curves.Add(newCurve);
                    editFractionIndex = i;
                }

                if (GUILayout.Button("Delete Path Fraction", GUILayout.MaxWidth(200))) {

                    bezierPath.pathFractions.Remove(pathFraction);

                    if (i == editFractionIndex && i == bezierPath.pathFractions.Count) {
                        editFractionIndex--;
                    }
                    else {
                        editFractionIndex = i;
                    }
                    return;
                }

                EditorGUILayout.EndHorizontal();
                // Buttons

                GUILayout.Space(10);

                // Draw Curves
                SirenixEditorGUI.BeginBox(); // Could be Fouldout

                for (int j = 0; j < pathFraction.curves.Count; j++) {
                    BezierCurve curve = pathFraction.curves[j];

                    // Delete Curve
                    EditorGUILayout.Space(10);
                    EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

                    GUILayout.FlexibleSpace();
                    GUI.enabled = pathFraction.curves.Count > 1;
                    if (GUILayout.Button("Delete Curve", GUILayout.MaxWidth(120))) {
                        editFractionIndex = i;
                        pathFraction.curves.Remove(curve);
                        return;
                    }
                    GUI.enabled = true;
                    GUILayout.FlexibleSpace();

                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space(10);
                    // Delete Curve

                    if (changeLastEnd) {
                        curve.startPosition = lastCurve.endPosition;
                    }

                    curve.startPosition = EditorGUILayout.Vector3Field("Start Position", curve.startPosition);
                    
                    // If change the start, have to change the last end
                    if (lastCurve != curve && curve.startPosition != lastCurve.endPosition) {
                        lastCurve.endPosition = curve.startPosition;
                    }
                    lastCurve = curve;

                    curve.startTangent = EditorGUILayout.Vector3Field("Start Tangent", curve.startTangent);
                    curve.endTangent = EditorGUILayout.Vector3Field("End Tangent", curve.endTangent);

                    Vector3 endPosition = curve.endPosition;
                    curve.endPosition = EditorGUILayout.Vector3Field("End Position", endPosition);

                    changeLastEnd = curve.endPosition != endPosition;

                    if(j < pathFraction.curves.Count - 1) {
                        EditorGUILayout.Space(10);
                        SirenixEditorGUI.HorizontalLineSeparator();
                    }
                }

                SirenixEditorGUI.EndBox();
            }

            SceneView.RepaintAll();
        }


        private void DrawDeleteCurve(PathFraction pathFraction, int index, BezierCurve curve) {
            EditorGUILayout.Space(10);
            EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

            GUILayout.FlexibleSpace();
            GUI.enabled = pathFraction.curves.Count > 1;
            if (GUILayout.Button("Delete Curve", GUILayout.MaxWidth(120))) {
                editFractionIndex = index;
                pathFraction.curves.Remove(curve);
                return;
            }
            GUI.enabled = true;
            GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);
        }
       

        private void AddNewPathFraction() {
            PathFraction newPathFraction = new PathFraction();
            BezierCurve newCurve = new BezierCurve(bezierPath.GetLastPosition());
            newPathFraction.curves.Add(newCurve);

            bezierPath.pathFractions.Add(newPathFraction);
            editFractionIndex = bezierPath.pathFractions.Count - 1;
        }

        private void AddOnSceneGUI(SceneView sceneView)
        {
            DrawHandles();
        }

        private void DrawHandles() {
            Undo.RecordObject(bezierPath, "Handles");

            BezierCurve lastCurve = bezierPath.pathFractions[0].curves[0];
            bool changeLastEnd = false;
            for (int i = 0; i < bezierPath.pathFractions.Count; i++) {
                PathFraction pathFraction = bezierPath.pathFractions[i];

                for (int j = 0; j < pathFraction.curves.Count; j++) {
                    BezierCurve curve = pathFraction.curves[j];

                    if (changeLastEnd) {
                        curve.startPosition = lastCurve.endPosition;
                    }

                    if (i == editFractionIndex) {

                        curve.startPosition = Handles.PositionHandle(curve.startPosition, Quaternion.identity);
                        // Is not the first curve && change the startPosition
                        if (lastCurve != curve && curve.startPosition != lastCurve.endPosition) {
                            lastCurve.endPosition = curve.startPosition;
                        }

                        curve.startTangent = Handles.PositionHandle(curve.startTangent, Quaternion.identity);
                        curve.endTangent = Handles.PositionHandle(curve.endTangent, Quaternion.identity);

                        Vector3 endPosition = curve.endPosition;
                        curve.endPosition = Handles.PositionHandle(curve.endPosition, Quaternion.identity);

                        // Change the last
                        changeLastEnd = curve.endPosition != endPosition;

                        Handles.color = Color.red;
                        Handles.DrawLine(curve.startPosition, curve.startTangent);
                        Handles.DrawLine(curve.endPosition, curve.endTangent);
                    }

                    lastCurve = curve;
                    Color color = PathPackColor(bezierPath.pathFractions.IndexOf(pathFraction));
                    Handles.DrawBezier(curve.startPosition, curve.endPosition, curve.startTangent, curve.endTangent, color, Texture2D.normalTexture, 5);
                }
            }
        }

        private Color PathPackColor(int index)
        {
            index++;
            if (index > 5)
                index %= 5;
            switch (index)
            {
                default:
                case 1:
                    return Color.blue;
                case 2:
                    return Color.green;
                case 3:
                    return Color.magenta;
                case 4:
                    return Color.yellow;
                case 5:
                    return Color.cyan;
            }
        }

    }
}
#endif