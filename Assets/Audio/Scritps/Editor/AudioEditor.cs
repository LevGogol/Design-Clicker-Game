using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Azur.PlayableTemplate.Sound
{
    [CustomEditor(typeof(Audio))]
    public class AudioEditor : Editor
    {
        private string countClips = "0";
        private const string _enumFile = "TrackName";
        
        private Audio _audio;
        private string _pathToEnumFile;

        private List<Clip> _clips = new List<Clip>();

        [Serializable]
        private class Clip
        {
            public string _trackName;
            public AudioClip _clip;
        }

        private void OnEnable()
        {
            TracksSection.DeletePressed += RemoveTrack;
            _pathToEnumFile = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(_enumFile)[0]);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _audio = (Audio) target;

            var tracks = _audio.Tracks;
            tracks = RefreshClips(tracks);
            for (var i = 0; i < tracks.Count; i++)
            {
                var track = tracks[i];
                track.ID = ((TrackName) i).ToString();
                track.Name = (TrackName) i;
            }

            TracksSection.Draw(tracks);
            
            NewClipSection();

            if (GUILayout.Button("Add"))
            {
                AddTracks();
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(_audio.gameObject);
                EditorSceneManager.MarkSceneDirty(_audio.gameObject.scene);
            }

            _audio.Tracks = tracks;
        }

        private List<AudioTrack> RefreshClips(List<AudioTrack> oldClips)
        {
            int countTrack = Enum.GetNames(typeof(TrackName)).Length;
            List<AudioTrack> clips = new List<AudioTrack>(countTrack);

            for (int i = 0; i < countTrack; i++)
            {
                var enumName = (TrackName) i;
                AudioTrack track = TryRestoreTrack(oldClips, enumName.ToString());
                if (track == null)
                {
                    track = CreateNewTrack(enumName);
                }

                clips.Add(track);
            }

            return clips;
        }

        private void NewClipSection()
        {
            countClips = EditorGUILayout.TextField("Count clips", countClips);

            if (string.IsNullOrEmpty(countClips))
            {
                _clips = new List<Clip>();
                return;
            }
            
            var lenght = int.Parse(countClips);
            
            if(_clips.Count > lenght)   
                _clips = new List<Clip>();

            for (int i = 0; i < lenght; i++)
            {
                if(_clips.Count < lenght)   
                   _clips.Add(new Clip());
            }

            for (int i = 0; i < _clips.Count; i++)
            {
                var clip = _clips[i];
                clip._trackName = EditorGUILayout.TextField("Name", clip._trackName);
                clip._clip = (AudioClip) EditorGUILayout.ObjectField("Clip", clip._clip, typeof(AudioClip), false);
            }
        }

        private static AudioTrack TryRestoreTrack(List<AudioTrack> oldClips, string ID)
        {
            return oldClips.FirstOrDefault(o => o.ID == ID);
        }

        private void AddTracks()
        {
            if (_clips.Count == 0)
                return;

            for (int i = 0; i < _clips.Count; i++)
            {
                if (!Regex.IsMatch(_clips[i]._trackName, @"^[a-zA-Z][a-zA-Z0-9_]*$"))
                    return;
            
                EnumEditor.WriteToFile(_clips[i]._trackName, _pathToEnumFile);
            }
            Refresh();
        }

        private void RemoveTrack(AudioTrack track)
        {
            if (!EnumEditor.TryRemoveFromFile(track.Name.ToString(), _pathToEnumFile))
                return;

            Refresh();
        }

        private void Refresh()
        {
            Debug.Log("WAIT");
            var relativePath = _pathToEnumFile.Substring(_pathToEnumFile.IndexOf("Assets"));
            AssetDatabase.ImportAsset(relativePath);
        }

        private AudioTrack CreateNewTrack(TrackName enumName)
        {
            var clipsEmpty = _clips.Count == 0;
            var track = new AudioTrack
            {
                Name = enumName,
                ClipsSettings = new AudioClipSettings[1]
                {
                    new AudioClipSettings(clipsEmpty ? null : _clips.First()._clip)
                }
            };

            if(!clipsEmpty)
                _clips.Remove(_clips.First());
            
            countClips = "0";
            
            return track;
        }

        private void OnDisable()
        {
            TracksSection.DeletePressed -= RemoveTrack;
        }
    }
}