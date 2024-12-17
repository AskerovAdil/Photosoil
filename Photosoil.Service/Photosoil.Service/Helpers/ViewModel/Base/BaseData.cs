namespace Photosoil.Service.Helpers.ViewModel.Base
{
    public class BaseData
    {
        public BaseData(int id) 
        {
            Id = id;
        }

        public int Id { get; set; }
        public string NameRu { get; set; }
        public string NameEng { get; set; }

        public string? CodeRu { get; set; }
        public string? CodeEng { get; set; }
    }
}