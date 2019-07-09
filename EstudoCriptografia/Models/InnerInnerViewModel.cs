using Cryptographer;

namespace EstudoCriptografia.Models
{
    public class InnerInnerViewModel
    {
        [EncryptedOnView]
        public string Id { get; set; }
    }
}