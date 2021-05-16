﻿//using System;
//using System.Collections.Generic;
//using UnityEngine;

//namespace CZToolKit.Core.Blackboards
//{
//    [Serializable]
//    public class CZBlackboardWithGUID : CZBlackboard
//    {
//        /// <summary> Name与GUID映射表 </summary>
//        [SerializeField]
//        protected Dictionary<string,string> guidMap = new Dictionary<string, string>();

//        #region GetData
//        public override bool TryGetData<T>(string _name, out T _data, T _fallback = default)
//        {
//            if (guidMap.TryGetValue(_name, out string guid))
//            {
//                if (TryGetDataFromGUID(guid, out _data, _fallback))
//                    return true;
//            }

//            _data = _fallback;
//            return false;
//        }

//        public bool TryGetDataFromGUID<T>(string _guid, out T _data, T _fallback = default)
//        {
//            if (blackboard.TryGetValue(_guid, out ICZType property))
//            {
//                if (property is BlackboardPropertyGUID<T> tProperty)
//                {
//                    _data = tProperty.Value;
//                    return true;
//                }
//            }

//            _data = _fallback;
//            return false;
//        }
//        #endregion

//        #region Contains
//        public override bool ContainsName(string _name)
//        {
//            if (guidMap.TryGetValue(_name, out string guid))
//                return ContainsGUID(guid);
//            return false;
//        }

//        public bool ContainsGUID(string _guid)
//        {
//            return blackboard.ContainsKey(_guid);
//        }

//        public override bool ContainsName<T>(string _name)
//        {
//            if (guidMap.TryGetValue(_name, out string guid))
//                return ContainsGUID<T>(guid);
//            return false;
//        }

//        public bool ContainsGUID<T>(string _guid)
//        {
//            if (blackboard.TryGetValue(_guid, out ICZType property))
//            {
//                if (property is BlackboardPropertyGUID<T> tProperty)
//                    return true;
//            }
//            return false;
//        }
//        #endregion

//        #region Save
//        public override void SaveData<T>(string _name, T _data)
//        {
//            if (guidMap.TryGetValue(_name, out string guid))
//                SaveDataFromGUID(_name, guid, _data);
//            else
//            {
//                BlackboardPropertyGUID<T> tProperty = new BlackboardPropertyGUID<T>();
//                tProperty.Value = _data;
//                SaveData(_name, tProperty.GUID, tProperty);
//            }
//        }

//        private void SaveDataFromGUID<T>(string _name, string _guid, T _data)
//        {
//            if (blackboard.TryGetValue(_guid, out ICZType property))
//            {
//                if (property is BlackboardPropertyGUID<T> tProperty)
//                    tProperty.Value = _data;
//            }
//            else
//            {
//                BlackboardPropertyGUID<T> tProperty = new BlackboardPropertyGUID<T>();
//                tProperty.Value = _data;
//                SaveData(_name, _guid, tProperty);
//            }
//        }

//        private void SaveData(string _name, string _guid, ICZType _tProperty)
//        {
//            guidMap[_name] = _guid;
//            blackboard[_guid] = _tProperty;
//        }
//        #endregion

//        #region Remove
//        public override void RemoveData(string _name)
//        {
//            if (string.IsNullOrEmpty(_name)) return;

//            if (guidMap.TryGetValue(_name, out string guid))
//            {
//                guidMap.Remove(_name);
//                RemoveDataFromGUID(guid);
//            }
//        }

//        private void RemoveDataFromGUID(string _guid)
//        {
//            if (string.IsNullOrEmpty(_guid)) return;

//            if (blackboard.ContainsKey(_guid))
//                blackboard.Remove(_guid);
//        }
//        #endregion

//        public override bool Rename(string _oldName, string _newName)
//        {
//            if (!guidMap.TryGetValue(_oldName, out string guid)) { Debug.LogWarning($"{_oldName}不被包含在黑板数据内"); return false; }
//            if (string.IsNullOrEmpty(_newName)) return false;
//            if (guidMap.ContainsKey(_newName)) { Debug.LogWarning($"黑板内已存在同名数据{_newName}"); return false; }

//            guidMap[_newName] = guidMap[_oldName];
//            guidMap.Remove(_oldName);
//            return true;
//        }

//        public override void Clear()
//        {
//            base.Clear();
//            guidMap.Clear();
//        }
//    }

//    public interface IBlackboardPropertyGUID : ICZType
//    {
//        string GUID { get; }
//    }

//    [Serializable]
//    public class BlackboardPropertyGUID<T> : CZType<T>, IBlackboardPropertyGUID
//    {
//        [SerializeField]
//        readonly string guid;
//        public string GUID { get { return guid; } }

//        public BlackboardPropertyGUID()
//        {
//            guid = Guid.NewGuid().ToString();
//        }
//    }
//}