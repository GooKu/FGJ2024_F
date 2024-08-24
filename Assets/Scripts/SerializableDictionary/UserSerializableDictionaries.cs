using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class InteractiveDataStorage : SerializableDictionary.Storage<InteractiveData[]> { }
[Serializable]
public class ReferenceInteractiveDictionary : SerializableDictionary<string, InteractiveData[], InteractiveDataStorage> { }

