using System;
using System.Reflection;

namespace InventoryAndStore
{
    public class ComparingArbitraryObjects
    {
        // Code originally found here:
        // https://stackoverflow.com/questions/375996/compare-the-content-of-two-objects-for-equality
        //
        // See modification notes below.
 
        public static bool Compare(object obj1, object obj2)
        {
            if (obj1 == null || obj2 == null)
            {
                return false;
            }
            if (!obj1.GetType().Equals(obj2.GetType()))
            {
                return false;
            }
 
            Type type = obj1.GetType();
            if (type.IsPrimitive || typeof(string).Equals(type))
            {
                return obj1.Equals(obj2);
            }
            if (type.IsArray)
            {
                Array first = obj1 as Array;
                Array second = obj2 as Array;
 
// hat tip to @aka3eka below... adding the suggested fix line here:
                if (first.Length != second.Length) return false; // THIS IS THE FIX..
 
                var en = first.GetEnumerator();
                int i = 0;
                while (en.MoveNext())
                {
                    if (!Compare(en.Current, second.GetValue(i)))
                        return false;
                    i++;
                }
            }
            else
            {
                // following mods to try and make it
                // useful for Unity ScriptableObject
                // equality comparisons:
                //
                // - ignore non-public fields
                // - disregard differences in name
                // - there may be other instance-ish fields but this at least
                //        gets two Instance-copied ScriptableObjects to compare 'true'
                //
                // If you get unexplained inequalities, put a breakpoint on the
                // 'return false' and check what the pi.Name field is and add
                // your own disregarding code below that matches the "name" one.
                //
                foreach (PropertyInfo pi in type.GetProperties(
                    /* BindingFlags.NonPublic | */ BindingFlags.Instance | BindingFlags.Public))
                {
                    if (pi.Name == "name")
                        continue;
                    var val = pi.GetValue(obj1,null);
                    var tval = pi.GetValue(obj2,null);
                    if (!Compare(val, tval))
                        return false;
                }
                foreach (FieldInfo fi in type.GetFields(
                    /* BindingFlags.NonPublic | */ BindingFlags.Instance | BindingFlags.Public))
                {
                    var val = fi.GetValue(obj1);
                    var tval = fi.GetValue(obj2);
                    if (!Compare(val, tval))
                        return false;
                }
            }
            return true;
        }
    }
}