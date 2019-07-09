using EstudoCriptografia.Extensions;

namespace EstudoCriptografia.Models
{
    public class InnerInnerViewModel
    {
        [EncryptedBuddy]
        public string Id { get; set; }
    }
}