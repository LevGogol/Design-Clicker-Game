using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Azur.PlayableTemplate.Sound
{
    public static class TracksSection
    {
        public static event Action<AudioTrack> DeletePressed;
        
        public static void Draw(List<AudioTrack> tracks)
        {
            foreach (var track in tracks)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(track.Name.ToString());
                DeleteButton(track);
                EditorGUILayout.EndHorizontal();

                for (var i = 0; i < track.ClipsSettings.Length; i++)
                {
                    var clipWithSettings = track.ClipsSettings[i];
                    EditorGUILayout.BeginVertical(GUI.skin.window);

                    clipWithSettings.Clip = (AudioClip) EditorGUILayout.ObjectField("clip", clipWithSettings.Clip,
                        typeof(AudioClip),
                        false);
                    clipWithSettings.Pitch = EditorGUILayout.Slider("pitch", clipWithSettings.Pitch, 0f, 3f);
                    clipWithSettings.Volume = EditorGUILayout.Slider("volume", clipWithSettings.Volume, 0f, 1f);
                    DeleteClipButton(track, i);

                    EditorGUILayout.EndVertical();
                }

                AddClipButton(track);
                EditorGUILayout.EndVertical();
            }
        }

        private static void AddClipButton(AudioTrack track)
        {
            if (GUILayout.Button("+", GUILayout.Width(30), GUILayout.Height(30)))
            {
                var clips = new AudioClipSettings[track.ClipsSettings.Length + 1];
                for (int i = 0; i < clips.Length - 1; i++)
                {
                    clips[i] = track.ClipsSettings[i];
                }

                clips[clips.Length - 1] = new AudioClipSettings();
                
                track.ClipsSettings = clips;
            }
        }

        private static void DeleteClipButton(AudioTrack track, int index)
        {
            if (GUILayout.Button("-", GUILayout.Width(15), GUILayout.Height(15)))
            {
                var clips = new AudioClipSettings[track.ClipsSettings.Length - 1];
                for (int i = 0; i < index; i++)
                {
                    clips[i] = track.ClipsSettings[i];
                }

                for (int i = index + 1; i < clips.Length + 1; i++)
                {
                    clips[i - 1] = track.ClipsSettings[i];
                }

                track.ClipsSettings = clips;
            }
        }

        private static void DeleteButton(AudioTrack track)
        {
            if (GUILayout.Button("X", GUILayout.Width(40), GUILayout.Height(40)))
            {
                DeletePressed?.Invoke(track);
            }
        }
    }
}