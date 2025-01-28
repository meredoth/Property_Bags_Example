using Unity.Properties;
using UnityEngine;

namespace Property_Bags
{
public class Visitor : PropertyVisitor, IVisitPropertyAdapter<AudioClip>, IVisitPropertyAdapter<AudioSource>
{
   private readonly GameObject _go;
   private readonly AudioClip _defaultAudioClip;
   
   public Visitor(GameObject go)
   {
      _go = go;
      _defaultAudioClip = Resources.Load<AudioClip>("Default_Audio");
      
      AddAdapter(this);
   }

   protected override void VisitProperty<TContainer, TValue>
      (Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
   {
      // check if value is UnityEngine Object and if it is check the 'Unity' overloaded null
      if(value is Object unityObj && unityObj == null)
         Debug.LogWarning($"{property.Name} is NULL!!!!");
      
      // visit child properties if any
      if (value != null)
         PropertyContainer.Accept(this, ref value);
   }

   void IVisitPropertyAdapter<AudioClip>.Visit<TContainer>
      (in VisitContext<TContainer, AudioClip> context, ref TContainer container, ref AudioClip value)
   {
      if (value != null) return;
      
      Debug.LogWarning($"{context.Property.Name} is NULL! substituting with default audio clip");
      value = _defaultAudioClip;
   }

   void IVisitPropertyAdapter<AudioSource>.Visit<TContainer>
      (in VisitContext<TContainer, AudioSource> context, ref TContainer container, ref AudioSource value)
   {
      if (value != null) return;
      
      if (_go.TryGetComponent<AudioSource>(out var audioSource))
      {
         Debug.LogWarning($"{context.Property.Name} is NULL! Adding existing Audio source component!");
         value = audioSource;
      }
      else
      {
         Debug.LogWarning($"{context.Property.Name} is NULL! Adding new Audio source component!");
         var newAudioSource = _go.AddComponent<AudioSource>();
         value = newAudioSource;
      }
   }
}
}
