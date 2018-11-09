using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GRP23
{
    namespace AxolotlWrath
    {
        [System.Serializable]
        public class GenericDifficultyValues<T>
        {
            public T easyValue;
            public T normalValue;
            public T hardValue;
        }

        [System.Serializable]
        public class IntDifficultyValues : GenericDifficultyValues<int>
        { }

        [System.Serializable]
        public class FloatDifficultyValues : GenericDifficultyValues<float>
        { }

        [System.Serializable]
        public class BoolDifficultyValues : GenericDifficultyValues<bool>
        { }

        [System.Serializable]
        public class Vector3DifficultyValues : GenericDifficultyValues<Vector3>
        { }

        [System.Serializable]
        public class TestDifficultyValues : GenericDifficultyValues<TestStruct>
        { }
    }
}