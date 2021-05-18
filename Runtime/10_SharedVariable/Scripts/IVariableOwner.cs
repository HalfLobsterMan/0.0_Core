﻿#region 注 释
/***
 *
 *  Title:
 *  
 *  Description:
 *  
 *  Date:
 *  Version:
 *  Writer: 
 *
 */
#endregion
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace CZToolKit.Core.SharedVariable
{
    public interface IVariableOwner
    {
        string GetOwnerName();

        int GetInstanceID();

        Object GetObject();

        SharedVariable GetVariable(string _guid);

        void SetVariable(SharedVariable _variable);

        void SetVariableValue(string _guid, object _value);

        IReadOnlyList<SharedVariable> GetVariables();

        void SetVariables(List<SharedVariable> _variables);
    }
}
