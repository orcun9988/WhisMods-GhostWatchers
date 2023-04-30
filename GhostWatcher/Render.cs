using System;
using UnityEngine;

namespace Whis
{
	public static class Render
	{
		public static GUIStyle StringStyle { get; set; } = new GUIStyle(GUI.skin.label);
		public static Color Color
		{
			get
			{
				return GUI.color;
			}
			set
			{
				GUI.color = value;
			}
		}
		public static void DrawLine(Vector2 from, Vector2 to, Color color)
		{
			Render.Color = color;
			Render.DrawLine(from, to);
		}
		public static void DrawLine(Vector2 from, Vector2 to)
		{
			float num = Vector2.Angle(from, to);
			GUIUtility.RotateAroundPivot(num, from);
			Render.DrawBox(from, Vector2.right * (from - to).magnitude, false);
			GUIUtility.RotateAroundPivot(-num, from);
		}
		public static void DrawBox(Vector2 position, Vector2 size, Color color, bool centered = true)
		{
			Render.Color = color;
			Render.DrawBox(position, size, centered);
		}
		public static void DrawBox(Vector2 position, Vector2 size, bool centered = true)
		{
			if (centered)
			{
				_ = position - size / 2f;
			}
			GUI.DrawTexture(new Rect(position, size), Texture2D.whiteTexture, 0);
		}
		public static void DrawString(Vector2 position, string label, Color color, bool centered = true)
		{
			Render.Color = color;
			Render.DrawString(position, label, centered);
			GUI.color = Color;
		}
		public static void DrawString(Vector2 position, string label, bool centered = true)
		{
			GUIContent guicontent = new GUIContent(label);
			Vector2 vector = Render.StringStyle.CalcSize(guicontent);
			GUI.Label(new Rect(centered ? (position - vector / 2f) : position, vector), guicontent);
		}

		public static Texture2D lineTex;
		public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
		{
			Matrix4x4 matrix = GUI.matrix;
			if (!lineTex)
				lineTex = new Texture2D(1, 1);

			Color color2 = GUI.color;
			GUI.color = color;
			float num = Vector3.Angle(pointB - pointA, Vector2.right);

			if (pointA.y > pointB.y)
				num = -num;

			GUIUtility.ScaleAroundPivot(new Vector2((pointB - pointA).magnitude, width), new Vector2(pointA.x, pointA.y + 0.5f));
			GUIUtility.RotateAroundPivot(num, pointA);
			GUI.DrawTexture(new Rect(pointA.x, pointA.y, 1f, 1f), lineTex);
			GUI.matrix = matrix;
			GUI.color = color2;
		}



		public static void DrawBox2(float x, float y, float w, float h, Color color, float thickness)
		{
			DrawLine(new Vector2(x, y), new Vector2(x + w, y), color, thickness);
			DrawLine(new Vector2(x, y), new Vector2(x, y + h), color, thickness);
			DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), color, thickness);
			DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), color, thickness);
		}

		public static void DrawBoneLine(Vector3 w2s_objectStart, Vector3 w2s_objectFinish, Color color)
		{
			if (w2s_objectStart != null && w2s_objectFinish != null)
			{
				Render.DrawLine(new Vector2(w2s_objectStart.x, (float)Screen.height - w2s_objectStart.y), new Vector2(w2s_objectFinish.x, (float)Screen.height - w2s_objectFinish.y), color, 1f);
			}
		}
	}
}
