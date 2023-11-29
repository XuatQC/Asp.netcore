namespace FNDSystem.Core.Dto
{
    public class SharedFolderDto
    {
        public required int Id { get; set; }
        public string? PlantName { get; set; }
        public int MenuPposi { get; set; }
        public string? DispName { get; set; }
        public string? DispNameEn { get; set; }
        public string? Path { get; set; }
        public bool Valid { get; set; }
        public bool ValidEn { get; set; }
    }
}
