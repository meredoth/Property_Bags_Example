using Unity.Properties;
using UnityEngine;

[assembly: GeneratePropertyBagsForAssembly]

namespace Property_Bags
{

[GeneratePropertyBag]
public partial class Square : MonoBehaviour
{
    public int number = 10;
    
    public AudioClip PublicAudio;
    public Sprite MySprite;
    public AudioClip[] ArrayAudio;
    
    [SerializeField] private AudioClip serializedAudio;
    [SerializeField] private string foo;
    
    [SerializeField] private AudioSource audioSource;
    
    private void Awake() => NullUtilities.ReplaceNull(this);

    private void Start() => audioSource.PlayOneShot(serializedAudio);
}
}
