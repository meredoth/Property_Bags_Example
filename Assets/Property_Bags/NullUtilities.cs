using Unity.Properties;
using UnityEngine;

namespace Property_Bags
{
public static class NullUtilities
{
   public static void ReplaceNull<T>(T value) where T : MonoBehaviour
   {
      var visitor = new Visitor(value.gameObject);
      
      // main entry point to our visitor.
      PropertyContainer.Accept(visitor, ref value);
   }
}
}