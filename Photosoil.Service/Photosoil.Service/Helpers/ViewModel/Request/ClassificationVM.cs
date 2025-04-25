using Photosoil.Core.Enum;

namespace Photosoil.Service.Helpers.ViewModel.Request
{
    public class ClassificationVM
    {
        public string? NameRu { get; set; }
        public string? NameEng { get; set; }
        public bool? IsAlphabeticallOrder { get; set; } = true;
        public bool IsMulti { get; set; } = true;
        public List<TermVM> Terms { get; set; } = new();

        public TranslationMode TranslationMode { get; set; } = TranslationMode.Neutral;
    }

    public class TermVM
    {
        public int? Id { get; set; }
        public int Order { get; set; } = 1;
        public string? NameRu { get; set; }
        public string? NameEng { get; set; }
    }
}
