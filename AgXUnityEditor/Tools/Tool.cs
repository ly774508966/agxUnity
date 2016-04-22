﻿using UnityEngine;
using UnityEditor;

namespace AgXUnityEditor.Tools
{
  public class Tool
  {
    /// <summary>
    /// Color of the x axis.
    /// </summary>
    /// <param name="alpha">Alpha value.</param>
    /// <returns>Color of the x axis.</returns>
    public static UnityEngine.Color GetXAxisColor( float alpha = 1.0f )
    {
      return new UnityEngine.Color( 1.0f, 0, 0, alpha );
    }

    /// <summary>
    /// Color of the y axis.
    /// </summary>
    /// <param name="alpha">Alpha value.</param>
    /// <returns>Color of the y axis.</returns>
    public static UnityEngine.Color GetYAxisColor( float alpha = 1.0f )
    {
      return new UnityEngine.Color( 0, 1.0f, 0, alpha );
    }

    /// <summary>
    /// Color of the z axis.
    /// </summary>
    /// <param name="alpha">Alpha value.</param>
    /// <returns>Color of the z axis.</returns>
    public static UnityEngine.Color GetZAxisColor( float alpha = 1.0f )
    {
      return new UnityEngine.Color( 0, 0, 1.0f, alpha );
    }

    /// <summary>
    /// Color of the center.
    /// </summary>
    /// <param name="alpha">Alpha value.</param>
    /// <returns>Color of the center.</returns>
    public static UnityEngine.Color GetCenterColor( float alpha = 1.0f )
    {
      return new UnityEngine.Color( 0.7f, 0.7f, 0.7f, alpha );
    }

    /// <summary>
    /// Creates a position tool given position and rotation.
    /// </summary>
    /// <param name="position">Current position.</param>
    /// <param name="rotation">Current rotation.</param>
    /// <param name="scale">Scale - default 1.0f.</param>
    /// <param name="alpha">Alpha - default 1.0f.</param>
    /// <returns>New position of the tool.</returns>
    public static Vector3 PositionTool( Vector3 position, Quaternion rotation, float scale = 1.0f, float alpha = 1.0f )
    {
      Vector3 snapSetting = new Vector3( 0.5f, 0.5f, 0.5f );

      Color orgColor = Handles.color;

      float handleSize = HandleUtility.GetHandleSize( position );
      Color color = Handles.color;
      Handles.color = GetXAxisColor( alpha );
      position = Handles.Slider( position, rotation * Vector3.right, scale * handleSize, new Handles.DrawCapFunction( Handles.ArrowCap ), snapSetting.x );
      Handles.color = GetYAxisColor( alpha );
      position = Handles.Slider( position, rotation * Vector3.up, scale * handleSize, new Handles.DrawCapFunction( Handles.ArrowCap ), snapSetting.y );
      Handles.color = GetZAxisColor( alpha );
      position = Handles.Slider( position, rotation * Vector3.forward, scale * handleSize, new Handles.DrawCapFunction( Handles.ArrowCap ), snapSetting.z );
      Handles.color = GetCenterColor( 0.6f * alpha );
      position = Handles.FreeMoveHandle( position, rotation, scale * handleSize * 0.15f, snapSetting, new Handles.DrawCapFunction( Handles.RectangleCap ) );

      Handles.color = orgColor;

      return position;
    }

    /// <summary>
    /// Creates a rotation tool given position and rotation.
    /// </summary>
    /// <param name="position">Current position.</param>
    /// <param name="rotation">Current rotation.</param>
    /// <param name="scale">Scale - default 1.0f.</param>
    /// <param name="alpha">Alpha - default 1.0f.</param>
    /// <returns>New rotation of the tool.</returns>
    public static Quaternion RotationTool( Vector3 position, Quaternion rotation, float scale = 1.0f, float alpha = 1.0f )
    {
      float snapSetting = 0.5f;

      Color orgColor = Handles.color;

      float handleSize = HandleUtility.GetHandleSize( position );
      Color color = Handles.color;
      Handles.color = GetXAxisColor( alpha );
      rotation = Handles.Disc( rotation, position, rotation * Vector3.right, scale * handleSize, true, snapSetting );
      Handles.color = GetYAxisColor( alpha );
      rotation = Handles.Disc( rotation, position, rotation * Vector3.up, scale * handleSize, true, snapSetting );
      Handles.color = GetZAxisColor( alpha );
      rotation = Handles.Disc( rotation, position, rotation * Vector3.forward, scale * handleSize, true, snapSetting );
      Handles.color = GetCenterColor( 0.6f * alpha );
      rotation = Handles.Disc( rotation, position, Camera.current.transform.forward, scale * handleSize * 1.1f, false, 0f );
      rotation = Handles.FreeRotateHandle( rotation, position, scale * handleSize );

      Handles.color = orgColor;

      return rotation;
    }

    /// <summary>
    /// Creates single direction slider tool.
    /// </summary>
    /// <param name="position">Position of the slider.</param>
    /// <param name="direction">Direction of the slider.</param>
    /// <param name="color">Color of the slider.</param>
    /// <param name="scale">Scale - default 1.0.</param>
    /// <returns>New position of the slider.</returns>
    public static Vector3 SliderTool( Vector3 position, Vector3 direction, Color color, float scale = 1.0f )
    {
      float snapSetting = 0.001f;

      Color prevColor = Handles.color;
      Handles.color = color;
      float handleSize = HandleUtility.GetHandleSize( position );
      position = Handles.Slider( position, direction, scale * handleSize, new Handles.DrawCapFunction( Handles.ArrowCap ), snapSetting );
      Handles.color = prevColor;

      return position;
    }

    /// <summary>
    /// Creates a slider tool but instead of the new position this function returns the movement.
    /// </summary>
    /// <param name="position">Position of the slider.</param>
    /// <param name="direction">Direction of the slider.</param>
    /// <param name="color">Color of the slider.</param>
    /// <param name="scale">Scale - default 1.0.</param>
    /// <returns>How much the slider has been moved.</returns>
    public static Vector3 DeltaSliderTool( Vector3 position, Vector3 direction, Color color, float scale = 1.0f )
    {
      Vector3 newPosition = SliderTool( position, direction, color, scale );
      return newPosition - position;
    }

    public Tool()
    {
    }

    public virtual void OnSceneViewGUI( SceneView sceneView ) { }
  }
}
